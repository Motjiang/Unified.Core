using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Book;

namespace Unified.Application.Interfaces
{
    public interface IBookRequestService
    {
        Task<IEnumerable<BookRequestDto>> GetAllBookRequestsAsync();
        Task<BookRequestDto> GetBookRequestByIdAsync(int id);
        Task AddBookRequestAsync(CreateBookRequestDto bookRequest);
        Task UpdateBookRequestAsync(BookRequestDto bookRequest);
        Task DeleteBookRequestAsync(BookRequestDto bookRequest);
    }
}
