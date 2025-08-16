using HRSystem.Api.Models;
using MediatR;

namespace HRSystem.Api.Queries
{
    public class GetEmployeeByIdQuery : IRequest<Employee?>
    {
        public int Id { get; set; }

        public GetEmployeeByIdQuery(int id)
        {
            Id = id;
        }
    }

}
