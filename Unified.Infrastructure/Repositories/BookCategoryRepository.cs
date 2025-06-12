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
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public BookCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBookCategoryAsync(BookCategory bookCategory)
        {
             bookCategory.Status = "Active"; // Set default status to Active
            await _context.BookCategories.AddAsync(bookCategory);
            await _context.SaveChangesAsync();
        }        

        public async Task<IEnumerable<BookCategory>> GetAllBookCategoryAsync()
        {
            var bookCategories = await _context.BookCategories
                .Where(bc => bc.Status == "Active").ToListAsync();

            return bookCategories;
        }

        public async Task<BookCategory> GetBookCategoryByIdAsync(int id)
        {
            var bookCategory = await _context.BookCategories
                .FirstOrDefaultAsync(bc => bc.BookCategoryId == id && bc.Status == "Active");

            return bookCategory;
        }

        public async Task UpdateBookCategoryAsync(BookCategory bookCategory)
        {
            _context.BookCategories.Update(bookCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookCategoryAsync(BookCategory bookCategory)
        {
            bookCategory.Status = "Inactive"; // Soft delete by setting status to Inactive
            _context.BookCategories.Update(bookCategory);
            await _context.SaveChangesAsync();
        }
    }
}
