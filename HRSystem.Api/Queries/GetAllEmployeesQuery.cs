using HRSystem.Api.Models;
using MediatR;

namespace HRSystem.Api.Queries
{
    public class GetAllEmployeesQuery:IRequest<List<Employee>>
    {
        // مافيش حاجة جوه الكلاس عشان إحنا عاوزين كل الموظفين
    }
}
