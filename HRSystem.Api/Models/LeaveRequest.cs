using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Models
{
    public enum LeaveStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Cancelled = 4
    }
    public class LeaveRequest
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int LeaveTypeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public int DaysRequested { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;

        public int? ApprovedById { get; set; }

        public DateTime? ApprovedDate { get; set; }

        [MaxLength(200)]
        public string ApprovalComments { get; set; } = string.Empty;

        public DateTime RequestDate { get; set; } = DateTime.Now;

        // Navigation Properties
        public Employee Employee { get; set; } = null!;
        public LeaveType LeaveType { get; set; } = null!;
        public Employee? ApprovedBy { get; set; }
    }
}
