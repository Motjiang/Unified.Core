using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Application.DTOs.Admin
{
    public class ViewEmployeeDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsLocked { get; set; }
        public DateTime DateCreated { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
