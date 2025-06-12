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
        Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
        Task<TicketDto> GetTicketByIdAsync(int id);
        Task AddTicketAsync(CreateTicketDto book);
        Task UpdateTicketAsync(TicketDto book);
        Task DeleteTicketAsync(TicketDto book);
    }
}
