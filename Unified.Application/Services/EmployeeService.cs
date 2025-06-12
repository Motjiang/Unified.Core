using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Employee;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;

namespace Unified.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task AddEmployeeAsync(CreateEmployeeDto employee, string role, string id)
        {
            var entity = _mapper.Map<Employee>(employee);
            await _employeeRepository.AddEmployeeAsync(entity, role, id);
        }

        public async Task DeleteEmployeeAsync(EmployeeDto employee)
        {
            await _employeeRepository.DeleteEmployeeAsync(_mapper.Map<Employee>(employee));
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string id)
        {
            return _mapper.Map<IEnumerable<EmployeeDto>>(await _employeeRepository.GetAllEmployeesAsync(id));
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(string id)
        {
            return _mapper.Map<EmployeeDto>(await _employeeRepository.GetEmployeeByIdAsync(id));
        }

        public async Task UpdateEmployeeAsync(EmployeeDto employee, string role)
        {
            var entity = _mapper.Map<Employee>(employee);
            await _employeeRepository.UpdateEmployeeAsync(entity, role);
        }
    }
}
