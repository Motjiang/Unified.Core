using System.ComponentModel.DataAnnotations;

namespace Unified.Domain.Entities
{
    public class TicketCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<TicketSubcategory> Subcategories { get; set; } = new List<TicketSubcategory>();
    }
}
