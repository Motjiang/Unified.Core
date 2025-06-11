using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;
using Unified.Infrastructure.Data;

namespace Unified.Infrastructure.Repositories
{
    public class BookRequestRepository : IBookRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBookRequestAsync(BookRequest bookRequest)
        {
            await _context.BookRequests.AddAsync(bookRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookRequest>> GetAllBookRequestsAsync()
        {
            var bookRequests = await _context.BookRequests
                .Include(br => br.RequestedByUser)
                .Include(br => br.ProcessedByAdmin)
                .Where(br => br.Status == "Active")
                .ToListAsync();

            return bookRequests;
        }

        public async Task<BookRequest> GetBookRequestByIdAsync(int id)
        {
            var bookRequest = await _context.BookRequests
                .Include(br => br.RequestedByUser)
                .Include(br => br.ProcessedByAdmin)
                .FirstOrDefaultAsync(br => br.BookRequestId == id && br.Status == "Active");

            return bookRequest;
        }

        public async Task UpdateBookRequestAsync(BookRequest bookRequest)
        {
           _context.BookRequests.Update(bookRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookRequestAsync(BookRequest bookRequest)
        {
            _context.BookRequests.Update(bookRequest);
            await _context.SaveChangesAsync();
        }
    }
}
