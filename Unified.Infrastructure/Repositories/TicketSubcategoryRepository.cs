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
    public class TicketSubcategoryRepository : ITicketSubcategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public TicketSubcategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTicketSubcategoryAsync(TicketSubcategory ticketSubcategory)
        {
            ticketSubcategory.Status = "Active"; // Set default status to Active
            await _context.TicketSubcategories.AddAsync(ticketSubcategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketSubcategory>> GetAllTicketSubcategoriesAsync()
        {
            var ticketSubcategories = await _context.TicketSubcategories
                .Where(tsc => tsc.Status == "Active")
                .ToListAsync();

            return ticketSubcategories;
        }

        public async Task<TicketSubcategory> GetTicketSubcategoryByIdAsync(int id)
        {
            var ticketSubcategory = await _context.TicketSubcategories
                .FirstOrDefaultAsync(tsc => tsc.SubcategoryId == id && tsc.Status == "Active");

            return ticketSubcategory;
        }

        public async Task UpdateTicketSubcategoryAsync(TicketSubcategory ticketSubcategory)
        {
            _context.TicketSubcategories.Update(ticketSubcategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTicketSubcategoryAsync(TicketSubcategory ticketSubcategory)
        {
            ticketSubcategory.Status = "Inactive"; // Soft delete by setting status to Inactive
            _context.TicketSubcategories.Update(ticketSubcategory);
            await _context.SaveChangesAsync();
        }
    }
}
