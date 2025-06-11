using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Domain.Interfaces
{
    public interface IDesignationRepository
    {
        Task<IEnumerable<Designation>> GetDesignationsAsync();
        Task<Designation> GetDesignationByIdAsync(int id);
        Task AddDesignationAsync(Designation designation);
        Task UpdateDesignationAsync(Designation designation);
        Task DeleteDesignationAsync(Designation designation);
    }
}
