using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Application.DTOs.Leave
{
    public class LeaveRequestDto
    {
        public int LeaveRequestId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateRequested { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int TotalDays => (EndDate - StartDate).Days + 1;

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Requesting employee is required")]
        public string RequestedByEmployeeId { get; set; }

        public string? ProcessedByEmployeeId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateApproved { get; set; }
    }
}
