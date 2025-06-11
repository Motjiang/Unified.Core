using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        public DateTime DateRequested { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalDays => (EndDate - StartDate).Days + 1;

        public string Status { get; set; }
       
        public string RequestedByEmployeeId { get; set; }
        public Employee? RequestedByEmployee { get; set; }

        public string? ProcessedByEmployeeId { get; set; }
        public Employee? ProcessedByEmployee { get; set; }

        public DateTime? DateApproved { get; set; } 
    }
}
