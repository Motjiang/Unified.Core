using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unified.Application.DTOs.Book;
using Unified.Application.DTOs.Leave;

namespace Unified.Application.Interfaces
{
    public interface ILeaveRequestService
    {
        Task<IEnumerable<LeaveRequestDto>> GetAllBookCategoriesAsync();
        Task<LeaveRequestDto> GetBookCategoryByIdAsync(int id);
        Task AddBookCategoryAsync(CreateLeaveRequestDto LeaveRequest);
        Task UpdateBookCategoryAsync(LeaveRequestDto LeaveRequest);
        Task DeleteBookCategoryAsync(LeaveRequestDto LeaveRequest);
    }
}
