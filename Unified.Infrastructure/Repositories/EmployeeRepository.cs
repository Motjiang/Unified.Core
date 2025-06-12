using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Employee> _userManager;

        public EmployeeRepository(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task AddEmployeeAsync(Employee employee, string role, string id)
        {
            employee.CreatedBy = id;
            employee.DateCreated = DateTime.UtcNow;
            employee.Status = "Active";
            employee.EmailConfirmed = true; 
            employee.UserName = employee.Email;

            await _userManager.CreateAsync(employee, DataSeed.Password);
            await _userManager.AddToRoleAsync(employee, role);
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

        public async Task UpdateEmployeeAsync(Employee employee, string newRole)
        {
            var existingUser = await _userManager.FindByIdAsync(employee.Id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            // Update profile info
            existingUser.FirstName = employee.FirstName;
            existingUser.LastName = employee.LastName;
            existingUser.Email = employee.Email;
            existingUser.UserName = employee.Email;
            existingUser.DepartmentId = employee.DepartmentId;
            existingUser.DesignationId = employee.DesignationId;

            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to update employee profile.");
            }

            // Update role
            var currentRoles = await _userManager.GetRolesAsync(existingUser);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
            }

            await _userManager.AddToRoleAsync(existingUser, newRole);
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            employee.Status = "Inactive";
            _context.Users.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
