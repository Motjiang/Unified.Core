using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Application.DTOs.Ticket
{
    public class TicketDto
    {
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        public string Priority { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "CreatedBy is required")]
        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ResolvedDate { get; set; }

        public string? ResolvedBy { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Subcategory is required")]
        public int SubcategoryId { get; set; }
    }
}
