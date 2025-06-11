using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Domain.Interfaces
{
    public interface ITicketCategoryRepository
    {
        Task<IEnumerable<TicketCategory>> GetAllTicketCategoriesAsync();
        Task<TicketCategory> GetTicketCategoryByIdAsync(int id);
        Task AddTicketCategoryAsync(TicketCategory ticketCategory);
        Task UpdateTicketCategoryAsync(TicketCategory ticketCategory);
        Task DeleteTicketCategoryAsync(TicketCategory ticketCategory);
    }
}
