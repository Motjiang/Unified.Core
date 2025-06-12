using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Application.DTOs.Book
{
    public class CreateBookRequestDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Author { get; set; }

        [DataType(DataType.Date)]
        public DateTime? PublishedDate { get; set; }
        
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Employee number is required")]
        public string RequestedByUserId { get; set; }

        [Required(ErrorMessage = "Request date is required")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        public string? ProcessedByAdminId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ProcessedDate { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot exceed 500 characters")]
        public string? Notes { get; set; }
    }
}
