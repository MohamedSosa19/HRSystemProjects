using HRSystem.Api.Models;
using MediatR;

namespace HRSystem.Api.Queries
{
    public class GetAllDepartmentsQuery : IRequest<List<Department>>
    {
    }
}
