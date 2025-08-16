using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Commands
{
    public class UpdateEmployeeCommand : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Position { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }
    }
}
