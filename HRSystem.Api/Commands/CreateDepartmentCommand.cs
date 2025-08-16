using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Commands
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int? ManagerId { get; set; }
    }
}
