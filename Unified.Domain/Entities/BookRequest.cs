using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unified.Domain.Entities
{
   public  class BookRequest
    {
        [Key]
        public int BookRequestId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? PublishedDate { get; set; }
        public int? CategoryId { get; set; }  

        public string RequestedByUserId { get; set; }
        public Employee? RequestedByUser { get; set; }

        public DateTime RequestDate { get; set; } 

        public string Status { get; set; } 

        public string? ProcessedByAdminId { get; set; }
        public Employee? ProcessedByAdmin { get; set; }
        public DateTime? ProcessedDate { get; set; }

        public string? Notes { get; set; }
    }
}
