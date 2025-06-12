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
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task AddBookAsync(CreateBookDto book)
        {
            await _bookRepository.AddBookAsync(_mapper.Map<Book>(book));
        }

        public async Task DeleteBookAsync(BookDto book)
        {
            await _bookRepository.DeleteBookAsync(_mapper.Map<Book>(book));
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return _mapper.Map<IEnumerable<BookDto>>(await _bookRepository.GetAllBooksAsync());
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            return _mapper.Map<BookDto>(await _bookRepository.GetBookByIdAsync(id));
        }

        public async Task UpdateBookAsync(BookDto book)
        {
            await _bookRepository.UpdateBookAsync(_mapper.Map<Book>(book));
        }
    }
}
