using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Leave;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Domain.Interfaces;

namespace Unified.Application.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task AddBookCategoryAsync(CreateLeaveRequestDto LeaveRequest)
        {
            await _leaveRequestRepository.AddAsync(_mapper.Map<LeaveRequest>(LeaveRequest));
        }

        public async Task DeleteBookCategoryAsync(LeaveRequestDto LeaveRequest)
        {
            await _leaveRequestRepository.DeleteAsync(_mapper.Map<LeaveRequest>(LeaveRequest));
        }

        public async Task<IEnumerable<LeaveRequestDto>> GetAllBookCategoriesAsync()
        {
            return _mapper.Map<IEnumerable<LeaveRequestDto>>(await _leaveRequestRepository.GetAllAsync());
        }

        public async Task<LeaveRequestDto> GetBookCategoryByIdAsync(int id)
        {
            return _mapper.Map<LeaveRequestDto>(await _leaveRequestRepository.GetByIdAsync(id));
        }

        public async Task UpdateBookCategoryAsync(LeaveRequestDto LeaveRequest)
        {
            await _leaveRequestRepository.UpdateAsync(_mapper.Map<LeaveRequest>(LeaveRequest));
        }
    }
}
