using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Designation;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;

namespace Unified.Application.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;
        private readonly IMapper _mapper;

        public DesignationService(IDesignationRepository designationRepository, IMapper mapper)
        {
            _designationRepository = designationRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CreateDesignationDto designation)
        {
            await _designationRepository.AddDesignationAsync(_mapper.Map<Designation>(designation));
        }

        public async Task DeleteAsync(DesignationDto designation)
        {
            await _designationRepository.DeleteDesignationAsync(_mapper.Map<Designation>(designation));
        }

        public async Task<IEnumerable<DesignationDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<DesignationDto>>(await _designationRepository.GetDesignationsAsync());
        }

        public async Task<DesignationDto> GetByIdAsync(int id)
        {
            return _mapper.Map<DesignationDto>(await _designationRepository.GetDesignationByIdAsync(id));
        }

        public async Task UpdateAsync(DesignationDto designation)
        {
            await _designationRepository.UpdateDesignationAsync(_mapper.Map<Designation>(designation));
        }
    }
}
