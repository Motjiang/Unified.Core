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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDepartmentAsync(Department department)
        {
            department.Status = "Active"; // Set default status to Active
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            var departments = await _context.Departments.Where(d => d.Status == "Active")
                .ToListAsync();

            return departments;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments
                .FirstOrDefaultAsync(d => d.DepartmentId == id && d.Status == "Active");

            return department;
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDepartmentAsync(Department department)
        {
            department.Status = "Inactive"; // Soft delete
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }
    }
}
