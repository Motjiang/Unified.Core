using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
    public class SalesTransaction
    {
        [Key]
        public int SalesTransactionId { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        [NotMapped]
        public decimal TotalAmount => PricePerUnit * Quantity;
        public DateTime SaleDate { get; set; }
        public string SoldByEmployeeId { get; set; }
        public Employee? SoldByEmployee { get; set; }
    }
}
