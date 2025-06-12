using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Ticket;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;

namespace Unified.Application.Services
{
    public class TicketCategoryService : ITicketCategoryService
    {
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;

        public TicketCategoryService(ITicketCategoryRepository ticketCategoryRepository, IMapper mapper)
        {
            _ticketCategoryRepository = ticketCategoryRepository;
            _mapper = mapper;
        }

        public async Task AddTicketCategoryAsync(CreateTicketCategoryDto ticket)
        {
            await _ticketCategoryRepository.AddTicketCategoryAsync(_mapper.Map<TicketCategory>(ticket));
        }

        public async Task DeleteTicketCategoryAsync(TicketCategoryDto ticket)
        {
            await _ticketCategoryRepository.DeleteTicketCategoryAsync(_mapper.Map<TicketCategory>(ticket));
        }

        public async Task<IEnumerable<TicketCategoryDto>> GetAllTicketCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<TicketCategoryDto>>(await _ticketCategoryRepository.GetAllTicketCategoriesAsync());
        }

        public async Task<TicketCategoryDto> GetTicketCategoryByIdAsync(int id)
        {
            return _mapper.Map<TicketCategoryDto>(await _ticketCategoryRepository.GetTicketCategoryByIdAsync(id));
        }

        public async Task UpdateTicketCategoryAsync(TicketCategoryDto ticket)
        {
            await _ticketCategoryRepository.UpdateTicketCategoryAsync(_mapper.Map<TicketCategory>(ticket));
        }
    }
}
