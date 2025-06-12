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
        Task<IEnumerable<TicketCategoryDto>> GetAllBooksAsync();
        Task<TicketCategoryDto> GetBookByIdAsync(int id);
        Task AddBookAsync(CreateTicketCategoryDto book);
        Task UpdateBookAsync(TicketCategoryDto book);
        Task DeleteBookAsync(TicketCategoryDto book);
    }
}
