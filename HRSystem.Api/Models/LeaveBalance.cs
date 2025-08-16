using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int LeaveTypeId { get; set; }

        [Required]
        public int Year { get; set; }

        public int AllocatedDays { get; set; }

        public int UsedDays { get; set; }

        public int RemainingDays => AllocatedDays - UsedDays;

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Navigation Properties
        public Employee Employee { get; set; } = null!;
        public LeaveType LeaveType { get; set; } = null!;
    }
}
