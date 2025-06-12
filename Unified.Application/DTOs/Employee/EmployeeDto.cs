using System;
using System.ComponentModel.DataAnnotations;

namespace Unified.Application.DTOs.Employee
{
    public class EmployeeDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int? Age { get; set; }

        [StringLength(13, ErrorMessage = "Identity number cannot exceed 13 characters")]
        public string? IdentityNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateOnly? DateOfBirth { get; set; }

        public string? Province { get; set; }

        [Required(ErrorMessage = "City name is required")]
        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Street address is required")]
        [StringLength(100, ErrorMessage = "Street address cannot exceed 100 characters")]
        public string? StreetAddress { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [StringLength(10)]
        public string? PostalCode { get; set; }
        
        public string? CreatedBy { get; set; }

        public string? Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        [Required(ErrorMessage = "Designation name is required")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        public int DepartmentId { get; set; }
    }
}
