using HRSystem.Api.Data;
using HRSystem.Api.Models;
using HRSystem.Api.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Handlers
{
    public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, List<Employee>>
    {
        private readonly HRDbContext _context;

        public GetAllEmployeesHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            // جيب كل الموظفين النشطين مرتبين حسب اسم العائلة
            return await _context.Employees
                .Where(e => e.IsActive)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName)
                .ToListAsync(cancellationToken);
        }
    }
}
