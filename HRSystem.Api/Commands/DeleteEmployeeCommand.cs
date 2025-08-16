using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Commands
{
    public class DeleteEmployeeCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }

        public DeleteEmployeeCommand(int id)
        {
            Id = id;
        }
    }
}
