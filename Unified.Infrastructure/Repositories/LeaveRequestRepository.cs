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
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        ApplicationDbContext _context;

        public LeaveRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LeaveRequest leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllAsync()
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(lr => lr.RequestedByEmployee)
                .Include(lr => lr.ProcessedByEmployee)
                .ToListAsync();

            return leaveRequests;
        }

        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(lr => lr.RequestedByEmployee)
                .Include(lr => lr.ProcessedByEmployee)
                .FirstOrDefaultAsync(lr => lr.LeaveRequestId == id);

            return leaveRequest;
        }

        public async Task UpdateAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();
        }
    }
}
