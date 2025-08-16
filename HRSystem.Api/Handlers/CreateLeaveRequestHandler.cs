using HRSystem.Api.Commands;
using HRSystem.Api.Data;
using HRSystem.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Handlers
{
    public class CreateLeaveRequestHandler : IRequestHandler<CreateLeaveRequestCommand, int>
    {
        private readonly HRDbContext _context;

        public CreateLeaveRequestHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // التحقق من صحة التواريخ
            if (request.EndDate < request.StartDate)
                throw new ArgumentException("End date cannot be before start date");

            if (request.StartDate < DateTime.Today)
                throw new ArgumentException("Cannot request leave for past dates");

            // حساب عدد الأيام (استثناء عطل نهاية الأسبوع)
            int daysRequested = CalculateBusinessDays(request.StartDate, request.EndDate);

            // التحقق من الرصيد المتاح
            var currentYear = DateTime.Now.Year;
            var leaveBalance = await _context.LeaveBalances
                .FirstOrDefaultAsync(lb => lb.EmployeeId == request.EmployeeId
                                        && lb.LeaveTypeId == request.LeaveTypeId
                                        && lb.Year == currentYear, cancellationToken);

            if (leaveBalance == null)
            {
                // إنشاء رصيد جديد للموظف
                var leaveType = await _context.LeaveTypes.FindAsync(request.LeaveTypeId);
                leaveBalance = new LeaveBalance
                {
                    EmployeeId = request.EmployeeId,
                    LeaveTypeId = request.LeaveTypeId,
                    Year = currentYear,
                    AllocatedDays = leaveType?.MaxDaysPerYear ?? 0,
                    UsedDays = 0
                };
                _context.LeaveBalances.Add(leaveBalance);
            }

            if (leaveBalance.RemainingDays < daysRequested)
                throw new InvalidOperationException($"Insufficient leave balance. Available: {leaveBalance.RemainingDays}, Requested: {daysRequested}");

            // إنشاء طلب الإجازة
            var leaveRequest = new LeaveRequest
            {
                EmployeeId = request.EmployeeId,
                LeaveTypeId = request.LeaveTypeId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                DaysRequested = daysRequested,
                Reason = request.Reason,
                Status = LeaveStatus.Pending,
                RequestDate = DateTime.Now
            };

            _context.LeaveRequests.Add(leaveRequest);
            await _context.SaveChangesAsync(cancellationToken);

            return leaveRequest.Id;
        }

        private static int CalculateBusinessDays(DateTime startDate, DateTime endDate)
        {
            int businessDays = 0;
            DateTime current = startDate;

            while (current <= endDate)
            {
                // استثناء السبت والأحد (في مصر الإجازة الجمعة والسبت)
                if (current.DayOfWeek != DayOfWeek.Friday && current.DayOfWeek != DayOfWeek.Saturday)
                {
                    businessDays++;
                }
                current = current.AddDays(1);
            }

            return businessDays;
        }
    }
}
