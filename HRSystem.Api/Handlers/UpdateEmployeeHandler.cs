using HRSystem.Api.Commands;
using HRSystem.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly HRDbContext _context;

        public UpdateEmployeeHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.IsActive, cancellationToken);

            if (employee == null)
                return false;

            // تحديث البيانات
            employee.FirstName = request.FirstName;
            employee.LastName = request.LastName;
            employee.Email = request.Email;
            employee.Position = request.Position;
            employee.Salary = request.Salary;
            employee.DepartmentId = request.DepartmentId > 0 ? request.DepartmentId : null;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
