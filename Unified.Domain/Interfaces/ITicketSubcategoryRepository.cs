using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Domain.Interfaces
{
    public interface ITicketSubcategoryRepository
    {
        Task<IEnumerable<TicketSubcategory>> GetAllTicketSubcategoriesAsync();
        Task<TicketSubcategory> GetTicketSubcategoryByIdAsync(int id);
        Task AddTicketSubcategoryAsync(TicketSubcategory ticketSubcategory);
        Task UpdateTicketSubcategoryAsync(TicketSubcategory ticketSubcategory);
        Task DeleteTicketSubcategoryAsync(TicketSubcategory ticketSubcategory);
    }
}
