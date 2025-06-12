using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Book;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;

namespace Unified.Application.Services
{
    public class BookRequestService : IBookRequestService
    {
        private readonly IBookRequestRepository _bookRequestRepository;
        private readonly IMapper _mapper;

        public BookRequestService(IBookRequestRepository bookRequestRepository, IMapper mapper)
        {
            _bookRequestRepository = bookRequestRepository;
            _mapper = mapper;
        }

        public async Task AddBookRequestAsync(CreateBookRequestDto bookRequest)
        {
            await _bookRequestRepository.AddBookRequestAsync(_mapper.Map<BookRequest>(bookRequest));
        }

        public async Task DeleteBookRequestAsync(BookRequestDto bookRequest)
        {
            await _bookRequestRepository.DeleteBookRequestAsync(_mapper.Map<BookRequest>(bookRequest));
        }

        public async Task<IEnumerable<BookRequestDto>> GetAllBookRequestsAsync()
        {
            return _mapper.Map<IEnumerable<BookRequestDto>>(await _bookRequestRepository.GetAllBookRequestsAsync());
        }

        public async Task<BookRequestDto> GetBookRequestByIdAsync(int id)
        {
            return _mapper.Map<BookRequestDto>(await _bookRequestRepository.GetBookRequestByIdAsync(id));
        }

        public async Task UpdateBookRequestAsync(BookRequestDto bookRequest)
        {
            await _bookRequestRepository.UpdateBookRequestAsync(_mapper.Map<BookRequest>(bookRequest));
        }
    }
}
