using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Employee;

namespace Unified.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string id);
        Task<EmployeeDto> GetEmployeeByIdAsync(string id);
        Task AddEmployeeAsync(CreateEmployeeDto employee, string role, string id);
        Task UpdateEmployeeAsync(EmployeeDto employee, string role);
        Task DeleteEmployeeAsync(EmployeeDto employee);
    }
}
