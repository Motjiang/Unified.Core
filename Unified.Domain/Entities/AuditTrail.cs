using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
    public class AuditTrail
    {
        [Key]
        public int AuditTrailId { get; set; }
        public string Action { get; set; }
        public string IpAddress { get; set; }
        public string Description { get; set; }
        public string TableAffected { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
