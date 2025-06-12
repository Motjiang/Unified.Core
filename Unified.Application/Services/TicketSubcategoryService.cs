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
    public class TicketSubcategoryService : ITicketSubcategoryService
    {
        private readonly ITicketSubcategoryRepository _ticketSubcategoryRepository;
        private readonly IMapper _mapper;

        public TicketSubcategoryService(ITicketSubcategoryRepository ticketSubcategoryRepository, IMapper mapper)
        {
            _ticketSubcategoryRepository = ticketSubcategoryRepository;
            _mapper = mapper;
        }

        public async Task AddTicketSubcategoryAsync(CreateTicketSubcategoryDto ticketSubcategory)
        {
            await _ticketSubcategoryRepository.AddTicketSubcategoryAsync(_mapper.Map<TicketSubcategory>(ticketSubcategory));
        }

        public async Task DeleteTicketSubcategoryAsync(TicketSubcategoryDto ticketSubcategory)
        {
            await _ticketSubcategoryRepository.DeleteTicketSubcategoryAsync(_mapper.Map<TicketSubcategory>(ticketSubcategory));
        }

        public async Task<IEnumerable<TicketSubcategoryDto>> GetAllTicketSubcategoriesAsync()
        {
            return _mapper.Map<IEnumerable<TicketSubcategoryDto>>(await _ticketSubcategoryRepository.GetAllTicketSubcategoriesAsync());
        }

        public async Task<TicketSubcategoryDto> GetTicketSubcategoryByIdAsync(int id)
        {
            return _mapper.Map<TicketSubcategoryDto>(await _ticketSubcategoryRepository.GetTicketSubcategoryByIdAsync(id));
        }

        public async Task UpdateTicketSubcategoryAsync(TicketSubcategoryDto ticketSubcategory)
        {
            await _ticketSubcategoryRepository.UpdateTicketSubcategoryAsync(_mapper.Map<TicketSubcategory>(ticketSubcategory));
        }
    }
}
