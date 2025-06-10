using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Unified.Domain.Entities
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public string? ResolvedBy { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
        public TicketCategory? Category { get; set; }
        public TicketSubcategory? Subcategory { get; set; }
    }
}
