using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Domain.Interfaces
{
    public interface IBookCategoryRepository
    {
        Task<IEnumerable<BookCategory>> GetAllBookCategoryAsync();
        Task<BookCategory> GetBookCategoryByIdAsync(int id);
        Task AddBookCategoryAsync(BookCategory bookCategory);
        Task UpdateBookCategoryAsync(BookCategory bookCategory);
        Task DeleteBookCategoryAsync(BookCategory bookCategory);
    }
}
