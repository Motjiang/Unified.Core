using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Domain.Interfaces
{
    public interface IBookRequestRepository
    {
        Task<IEnumerable<BookRequest>> GetAllBookRequestsAsync();
        Task<BookRequest> GetBookRequestByIdAsync(int id);
        Task AddBookRequestAsync(BookRequest bookRequest);
        Task UpdateBookRequestAsync(BookRequest bookRequest);
        Task DeleteBookRequestAsync(BookRequest bookRequest);
    }
}
