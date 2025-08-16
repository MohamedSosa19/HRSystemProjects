using HRSystem.Api.Commands;
using HRSystem.Api.Data;
using HRSystem.Api.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Handlers
{
    public class ApproveLeaveRequestHandler : IRequestHandler<ApproveLeaveRequestCommand, bool>
    {
        private readonly HRDbContext _context;

        public ApproveLeaveRequestHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ApproveLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(lr => lr.Employee)
                .FirstOrDefaultAsync(lr => lr.Id == request.LeaveRequestId, cancellationToken);

            if (leaveRequest == null || leaveRequest.Status != LeaveStatus.Pending)
                return false;

            // تحديث حالة الطلب
            leaveRequest.Status = request.IsApproved ? LeaveStatus.Approved : LeaveStatus.Rejected;
            leaveRequest.ApprovedById = request.ApprovedById;
            leaveRequest.ApprovedDate = DateTime.Now;
            leaveRequest.ApprovalComments = request.Comments;

            // إذا تمت الموافقة، خصم من الرصيد
            if (request.IsApproved)
            {
                var leaveBalance = await _context.LeaveBalances
                    .FirstOrDefaultAsync(lb => lb.EmployeeId == leaveRequest.EmployeeId
                                            && lb.LeaveTypeId == leaveRequest.LeaveTypeId
                                            && lb.Year == DateTime.Now.Year, cancellationToken);

                if (leaveBalance != null)
                {
                    leaveBalance.UsedDays += leaveRequest.DaysRequested;
                    leaveBalance.LastUpdated = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
