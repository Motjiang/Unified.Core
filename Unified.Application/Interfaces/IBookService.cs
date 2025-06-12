using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Book;

namespace Unified.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(int id);
        Task AddBookAsync(CreateBookDto book);
        Task UpdateBookAsync(BookDto book);
        Task DeleteBookAsync(BookDto book);
    }
}
