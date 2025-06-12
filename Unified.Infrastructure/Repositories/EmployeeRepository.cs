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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddEmployeeAsync(Employee employee, string id)
        {
            employee.CreatedBy = id;
            employee.DateCreated = DateTime.UtcNow;
            employee.Status = "Active";
            employee.EmailConfirmed = true; 
            employee.UserName = employee.Email;

            await _context.Users.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync( string id)
        {
            var employees = await _context.Users
                .Include(e => e.Designation)
                .Include(e => e.Department)
                .Where(e => e.Status == "Active" && e.Id != id)
                .ToListAsync();

            return employees;
        }

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            var employee = await _context.Users
                 .Include(e => e.Designation)
                 .Include(e => e.Department)
                 .FirstOrDefaultAsync(e => e.Id == id && e.Status == "Active");

            return employee;
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Users.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            _context.Users.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
