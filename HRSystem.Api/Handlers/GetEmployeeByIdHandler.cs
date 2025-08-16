using HRSystem.Api.Data;
using HRSystem.Api.Models;
using HRSystem.Api.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HRSystem.Api.Handlers
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Employee?>
    {
        private readonly HRDbContext _context;

        public GetEmployeeByIdHandler(HRDbContext context)
        {
            _context = context;
        }

        public async Task<Employee?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .Where(e => e.Id == request.Id && e.IsActive)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
