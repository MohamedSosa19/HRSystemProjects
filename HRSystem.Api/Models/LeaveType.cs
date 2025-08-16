using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Models
{
    public class LeaveType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        public int MaxDaysPerYear { get; set; }

        public bool RequiresApproval { get; set; } = true;

        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();
    }
}
