using System.ComponentModel.DataAnnotations;

namespace HRSystem.Api.Models
{
    public class Employee
    {
        //public int Id { get; set; }

        //[Required]
        //[MaxLength(50)]
        //public string FirstName { get; set; } = string.Empty;

        //[Required]
        //[MaxLength(50)]
        //public string LastName { get; set; } = string.Empty;

        //[Required]
        //[EmailAddress]
        //[MaxLength(100)]
        //public string Email { get; set; } = string.Empty;

        //[Required]
        //[MaxLength(100)]
        //public string Position { get; set; } = string.Empty;

        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        //public decimal Salary { get; set; }

        //public DateTime HireDate { get; set; } = DateTime.Now;

        //public bool IsActive { get; set; } = true;



        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Position { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than 0")]
        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;

        public int? DepartmentId { get; set; }

        // Navigation Properties
        public Department? Department { get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveBalance> LeaveBalances { get; set; } = new List<LeaveBalance>();

        // Computed Properties
        public string FullName => $"{FirstName} {LastName}";
    }
}
