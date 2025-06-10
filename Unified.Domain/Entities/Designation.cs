using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
    public class Designation
    {
        [Key]
        public int DesignationId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
