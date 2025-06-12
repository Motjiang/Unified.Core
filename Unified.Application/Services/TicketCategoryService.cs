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
    public class TicketCategoryService : ITicketCategoryService
    {
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;

        public TicketCategoryService(ITicketCategoryRepository ticketCategoryRepository, IMapper mapper)
        {
            _ticketCategoryRepository = ticketCategoryRepository;
            _mapper = mapper;
        }

        public async Task AddBookAsync(CreateTicketCategoryDto book)
        {
            await _ticketCategoryRepository.AddTicketCategoryAsync(_mapper.Map<TicketCategory>(book));
        }

        public async Task DeleteBookAsync(TicketCategoryDto book)
        {
            await _ticketCategoryRepository.DeleteTicketCategoryAsync(_mapper.Map<TicketCategory>(book));
        }

        public async Task<IEnumerable<TicketCategoryDto>> GetAllBooksAsync()
        {
            return _mapper.Map<IEnumerable<TicketCategoryDto>>(await _ticketCategoryRepository.GetAllTicketCategoriesAsync());
        }

        public async Task<TicketCategoryDto> GetBookByIdAsync(int id)
        {
            return _mapper.Map<TicketCategoryDto>(await _ticketCategoryRepository.GetTicketCategoryByIdAsync(id));
        }

        public async Task UpdateBookAsync(TicketCategoryDto book)
        {
            await _ticketCategoryRepository.UpdateTicketCategoryAsync(_mapper.Map<TicketCategory>(book));
        }
    }
}
