using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Book;
using Unified.Application.DTOs.Ticket;

namespace Unified.Application.Interfaces
{
    public interface ITicketCategoryService
    {
        Task<IEnumerable<TicketCategoryDto>> GetAllTicketCategoriesAsync();
        Task<TicketCategoryDto> GetTicketCategoryByIdAsync(int id);
        Task AddTicketCategoryAsync(CreateTicketCategoryDto book);
        Task UpdateTicketCategoryAsync(TicketCategoryDto book);
        Task DeleteTicketCategoryAsync(TicketCategoryDto book);
    }
}
