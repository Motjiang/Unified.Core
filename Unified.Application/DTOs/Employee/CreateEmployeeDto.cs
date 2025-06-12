using System;
using System.ComponentModel.DataAnnotations;

namespace Unified.Application.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name cannot exceed 100 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name cannot exceed 100 characters")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^\\w+@[a-zA-Z_]+?\\.[a-zA-Z]{2,3}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Designation name is required")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}
