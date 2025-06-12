using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Department;

namespace Unified.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto> GetByIdAsync(int id);
        Task AddAsync(CreateDepartmentDto department);
        Task UpdateAsync(DepartmentDto department);
        Task DeleteAsync(DepartmentDto department);
    }
}
