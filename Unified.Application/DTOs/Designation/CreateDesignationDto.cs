using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Application.DTOs.Designation
{
    public class CreateDesignationDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be at most 100 characters")]
        public string title { get; init; }

        [Required(ErrorMessage = "Status is required")]
        public string status { get; init; }

        [Required(ErrorMessage = "Department name is required")]
        public int departmentId { get; init; }
    }
}
