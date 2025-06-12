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
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [Required(ErrorMessage = "Designation name is required")]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        public int DepartmentId { get; set; }
    }
}
