using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Application.DTOs.Department
{
    public class DepartmentDto
    {
        public int id { get; init; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name must be at most {1} characters")]
        public string name { get; init; }

        [Required(ErrorMessage = "Status is required")]
        public string status { get; init; }
    }
}
