using HRSystem.Api.Commands;
using HRSystem.Api.Data;
using HRSystem.Api.Models;
using MediatR;

namespace HRSystem.Api.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly HRDbContext _context;

        public CreateEmployeeHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Position = request.Position,
                Salary = request.Salary,
                HireDate = DateTime.Now,
                IsActive = true
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}
