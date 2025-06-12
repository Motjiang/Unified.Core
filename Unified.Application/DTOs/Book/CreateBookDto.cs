using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Application.DTOs.Book
{
    public class CreateBookDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(150, ErrorMessage = "Title cannot exceed 150 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Published date is required")]
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }
        [Required(ErrorMessage = "Edition is required")]
        [StringLength(50, ErrorMessage = "Edition cannot exceed 50 characters")]
        public string Edition { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Cost price is required")]
        [Range(0.01, 100000, ErrorMessage = "Cost price must be between 0.01 and 100000")]
        public decimal CostPrice { get; set; }
    }
}
