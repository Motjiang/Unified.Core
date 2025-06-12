using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Department;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;

namespace Unified.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateDepartmentDto department)
        {
            await _departmentRepository.AddDepartmentAsync(_mapper.Map<Department>(department));
        }

        public async Task DeleteAsync(DepartmentDto department)
        {
            await _departmentRepository.DeleteDepartmentAsync(_mapper.Map<Department>(department));
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<DepartmentDto>>(await _departmentRepository.GetAllDepartmentsAsync());
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            return _mapper.Map<DepartmentDto>(await _departmentRepository.GetDepartmentByIdAsync(id));
        }

        public async Task UpdateAsync(DepartmentDto department)
        {
            await _departmentRepository.UpdateDepartmentAsync(_mapper.Map<Department>(department));
        }
    }
}
