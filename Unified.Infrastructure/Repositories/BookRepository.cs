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
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBookAsync(Book book)
        {
            book.Status = "Active"; // Set default status to Active
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }               

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            var books = await _context.Books.Include(b => b.Category)
                .Where(b => b.Status == "Active")
                .ToListAsync();

            return books;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _context.Books.Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.BookId == id && b.Status == "Active");

            return book;
        }

        public async Task UpdateBookAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookAsync(Book book)
        {
            book.Status = "Inactive"; // Soft delete by setting status to Inactive
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
    }
}
