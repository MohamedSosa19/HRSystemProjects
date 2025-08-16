using HRSystem.Api.Commands;
using HRSystem.Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Handlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
    {
        private readonly HRDbContext _context;

        public DeleteEmployeeHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.IsActive, cancellationToken);

            if (employee == null)
                return false;

            // Soft delete - نخلي IsActive = false بدل ما نمسح الموظف
            employee.IsActive = false;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
