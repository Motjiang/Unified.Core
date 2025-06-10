using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unified.Domain.Entities
{
    public class TicketSubcategory
    {
        [Key]
        public int SubcategoryId { get; set; }
        
        public string Name { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }

        public TicketCategory? Category { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
