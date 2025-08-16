using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Commands
{
    public class ApproveLeaveRequestCommand : IRequest<bool>
    {
        [Required]
        public int LeaveRequestId { get; set; }

        [Required]
        public int ApprovedById { get; set; }

        [Required]
        public bool IsApproved { get; set; }

        public string Comments { get; set; } = string.Empty;
    }
}
