using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Book;

namespace Unified.Application.Interfaces
{
    public interface IBookCategoryService
    {
        Task<IEnumerable<BookCategoryDto>> GetAllBookCategoriesAsync();
        Task<BookCategoryDto> GetBookCategoryByIdAsync(int id);
        Task AddBookCategoryAsync(CreateBookCategoryDto bookCategory);
        Task UpdateBookCategoryAsync(BookCategoryDto bookCategory);
        Task DeleteBookCategoryAsync(BookCategoryDto bookCategory);
    }
}
