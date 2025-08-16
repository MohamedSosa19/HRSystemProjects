using HRSystem.Api.Commands;
using HRSystem.Api.Data;
using HRSystem.Api.Models;
using MediatR;

namespace HRSystem.Api.Handlers
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, int>
    {
        private readonly HRDbContext _context;

        public CreateDepartmentHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = new Department
            {
                Name = request.Name,
                Description = request.Description,
                ManagerId = request.ManagerId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync(cancellationToken);

            return department.Id;
        }
    }
}
