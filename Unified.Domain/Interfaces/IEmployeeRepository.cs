using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync( string id);
        Task<Employee> GetEmployeeByIdAsync(string id);
        Task AddEmployeeAsync(Employee employee, string role, string id);
        Task UpdateEmployeeAsync(Employee employee, string role);
        Task DeleteEmployeeAsync(Employee employee);
    }
}
