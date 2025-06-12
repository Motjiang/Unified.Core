using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Book;
using Unified.Application.DTOs.Ticket;

namespace Unified.Application.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetAllBooksAsync();
        Task<TicketDto> GetBookByIdAsync(int id);
        Task AddBookAsync(CreateTicketDto book);
        Task UpdateBookAsync(TicketDto book);
        Task DeleteBookAsync(TicketDto book);
    }
}
