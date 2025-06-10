using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public ICollection<Designation> Designations { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
