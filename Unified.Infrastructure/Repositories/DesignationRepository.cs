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
    public class DesignationRepository : IDesignationRepository
    {
        private readonly ApplicationDbContext _context;

        public DesignationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDesignationAsync(Designation designation)
        {
            await _context.Designations.AddAsync(designation);
            await _context.SaveChangesAsync();
        }

        public async Task<Designation> GetDesignationByIdAsync(int id)
        {
            var designation = await _context.Designations
                .FirstOrDefaultAsync(d => d.DesignationId == id && d.Status == "Active");

            return designation;
        }

        public async Task<IEnumerable<Designation>> GetDesignationsAsync()
        {
            var designations = await _context.Designations
                .Where(d => d.Status == "Active")
                .ToListAsync();

            return designations;
        }

        public async Task UpdateDesignationAsync(Designation designation)
        {
            _context.Designations.Update(designation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDesignationAsync(Designation designation)
        {
            _context.Designations.Update(designation);
            await _context.SaveChangesAsync();
        }
    }
}
