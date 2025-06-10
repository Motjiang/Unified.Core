using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public BookCategory? Category { get; set; }
        public decimal Price { get; set; }        
        public int StockQuantity { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost price must be greater than {0}.")]
        public decimal CostPrice { get; set; }
        public ICollection<SalesTransaction>? SalesTransactions { get; set; }
    }
}
