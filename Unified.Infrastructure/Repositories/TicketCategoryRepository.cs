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
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddTicketCategoryAsync(TicketCategory ticketCategory)
        {
            ticketCategory.Status = "Active"; // Set default status to Active
            await _context.TicketCategories.AddAsync(ticketCategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketCategory>> GetAllTicketCategoriesAsync()
        {
            var ticketCategories = await _context.TicketCategories
                 .Where(tc => tc.Status == "Active")
                 .ToListAsync();

            return ticketCategories;
        }

        public async Task<TicketCategory> GetTicketCategoryByIdAsync(int id)
        {
            var ticketCategory = await _context.TicketCategories
                .FirstOrDefaultAsync(tc => tc.CategoryId == id && tc.Status == "Active");

            return ticketCategory;
        }

        public async Task UpdateTicketCategoryAsync(TicketCategory ticketCategory)
        {
            _context.TicketCategories.Update(ticketCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketCategoryAsync(TicketCategory ticketCategory)
        {
            ticketCategory.Status = "Inactive"; // Soft delete by setting status to Inactive
            _context.TicketCategories.Update(ticketCategory);
            await _context.SaveChangesAsync();
        }
    }
}
