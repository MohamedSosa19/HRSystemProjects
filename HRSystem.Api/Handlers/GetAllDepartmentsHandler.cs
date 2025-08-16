using HRSystem.Api.Data;
using HRSystem.Api.Models;
using HRSystem.Api.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Handlers
{
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, List<Department>>
    {
        private readonly HRDbContext _context;

        public GetAllDepartmentsHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Departments
                .Include(d => d.Manager)
                .Include(d => d.Employees.Where(e => e.IsActive))
                .Where(d => d.IsActive)
                .OrderBy(d => d.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
