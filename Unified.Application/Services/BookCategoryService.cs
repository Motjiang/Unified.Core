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
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IBookCategoryRepository _bookCategoryRepository;
        private readonly IMapper _mapper;

        public BookCategoryService(IBookCategoryRepository bookCategoryRepository, IMapper mapper)
        {
            _bookCategoryRepository = bookCategoryRepository;
            _mapper = mapper;
        }


        public async Task AddBookCategoryAsync(CreateBookCategoryDto bookCategory)
        {
            await _bookCategoryRepository.AddBookCategoryAsync(_mapper.Map<BookCategory>(bookCategory));
        }

        public async Task<IEnumerable<BookCategoryDto>> GetAllBookCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<BookCategoryDto>>(await _bookCategoryRepository.GetAllBookCategoryAsync());
        }

        public async Task<BookCategoryDto> GetBookCategoryByIdAsync(int id)
        {
            return _mapper.Map<BookCategoryDto>(await _bookCategoryRepository.GetBookCategoryByIdAsync(id));
        }

        public async Task UpdateBookCategoryAsync(BookCategoryDto bookCategory)
        {
            await _bookCategoryRepository.UpdateBookCategoryAsync(_mapper.Map<BookCategory>(bookCategory));
        }

        public async Task DeleteBookCategoryAsync(BookCategoryDto bookCategory)
        {
            await _bookCategoryRepository.DeleteBookCategoryAsync(_mapper.Map<BookCategory>(bookCategory));
        }
    }
}
