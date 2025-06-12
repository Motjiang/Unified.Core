using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Designation;

namespace Unified.Application.Interfaces
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationDto>> GetAllAsync();
        Task<DesignationDto> GetByIdAsync(int id);
        Task AddAsync(CreateDesignationDto designation);
        Task UpdateAsync(DesignationDto designation);
        Task DeleteAsync(DesignationDto designation);
    }
}
