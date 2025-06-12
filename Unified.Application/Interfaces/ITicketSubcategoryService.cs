using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Ticket;

namespace Unified.Application.Interfaces
{
    public interface ITicketSubcategoryService
    {
        Task<IEnumerable<TicketSubcategoryDto>> GetAllTicketSubcategoriesAsync();
        Task<TicketSubcategoryDto> GetTicketSubcategoryByIdAsync(int id);
        Task AddTicketSubcategoryAsync(CreateTicketSubcategoryDto ticketSubcategory);
        Task UpdateTicketSubcategoryAsync(TicketSubcategoryDto ticketSubcategory);
        Task DeleteTicketSubcategoryAsync(TicketSubcategoryDto ticketSubcategory);
    }
}
