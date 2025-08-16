using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Commands
{
    public class CreateLeaveRequestCommand : IRequest<int>
    {
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int LeaveTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Reason { get; set; } = string.Empty;
    }
}
