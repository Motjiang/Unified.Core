using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Ticket;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;

namespace Unified.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task AddBookAsync(CreateTicketDto book)
        {
            await _ticketRepository.AddTicketAsync(_mapper.Map<Ticket>(book));
        }

        public async Task DeleteBookAsync(TicketDto book)
        {
            await _ticketRepository.DeleteTicketAsync(_mapper.Map<Ticket>(book));
        }

        public async Task<IEnumerable<TicketDto>> GetAllBooksAsync()
        {
            return _mapper.Map<IEnumerable<TicketDto>>(await _ticketRepository.GetAllTicketsAsync());   
        }

        public async Task<TicketDto> GetBookByIdAsync(int id)
        {
            return _mapper.Map<TicketDto>(await _ticketRepository.GetTicketByIdAsync(id));
        }

        public async Task UpdateBookAsync(TicketDto book)
        {
            await _ticketRepository.UpdateTicketAsync(_mapper.Map<Ticket>(book));
        }
    }
}
