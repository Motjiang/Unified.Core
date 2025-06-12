using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Book;
using Unified.Application.DTOs.Department;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Infrastructure.Data;

namespace Unified.Core.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IBookCategoryService _bookCategoryService;
        private readonly ApplicationDbContext _context;

        public BookCategoryController(UserManager<Employee> userManager, IBookCategoryService bookCategoryService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _bookCategoryService = bookCategoryService;
            _context = context;
        }

        [HttpGet("get-all-book-categories")]
        public async Task<IActionResult> GetAllBookCategories()
        {
            try
            {
                var categories = await _bookCategoryService.GetAllBookCategoriesAsync();
                if (categories == null || !categories.Any())
                {
                    return NotFound(new { title = "No Categories Found", message = "There are no book categories in the system." });
                }
                return Ok(categories);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [HttpGet("get-book-category-by-id/{id}")]
        public async Task<IActionResult> GetBookCategoryById(int id)
        {
            try
            {
                var category = await _bookCategoryService.GetBookCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound(new { title = "Category Not Found", message = "No active category found." });
                }
                return Ok(category);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [HttpPost("add-book-category")]
        public async Task<IActionResult> AddBookCategory([FromBody] CreateBookCategoryDto bookCategoryDto)
        {            
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);

                if (bookCategoryDto == null || string.IsNullOrWhiteSpace(bookCategoryDto.Name))
                {
                    return BadRequest(new { title = "Invalid Input", message = "Book category name is required." });
                }

                if (await CheckBookCategoryExistsAsync(bookCategoryDto.Name))
                {
                    return Conflict(new { title = "Category Exists", message = "A book category with this name already exists." });
                }

                await _bookCategoryService.AddBookCategoryAsync(bookCategoryDto);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Created Book Category",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Book Category '{bookCategoryDto.Name}' created successfully.",
                    TableAffected = "BookCategories",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The department has been added successfully." });

            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [HttpPut("update-book-category")]
        public async Task<IActionResult> UpdateBookCategory([FromBody] BookCategoryDto bookCategoryDto)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (bookCategoryDto == null || bookCategoryDto.BookCategoryId <= 0 || string.IsNullOrWhiteSpace(bookCategoryDto.Name))
                {
                    return BadRequest(new { title = "Invalid Input", message = "Book category data is required." });
                }
                if (await CheckBookCategoryExistsAsync(bookCategoryDto.Name, bookCategoryDto.BookCategoryId))
                {
                    return Conflict(new { title = "Category Exists", message = "A book category with this name already exists." });
                }
                await _bookCategoryService.UpdateBookCategoryAsync(bookCategoryDto);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Updated Book Category",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Book Category '{bookCategoryDto.Name}' updated successfully.",
                    TableAffected = "BookCategories",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };
                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();
                return Ok(new { title = "Success", message = "The book category has been updated successfully." });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [HttpPatch("delete-book-category")]
        public async Task<IActionResult> DeleteBookCategory([FromBody] BookCategoryDto bookCategoryDto)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (bookCategoryDto == null || bookCategoryDto.BookCategoryId <= 0)
                {
                    return BadRequest(new { title = "Invalid Input", message = "Valid book category data is required." });
                }
                await _bookCategoryService.DeleteBookCategoryAsync(bookCategoryDto);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Deleted Book Category",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Book Category '{bookCategoryDto.Name}' deleted successfully.",
                    TableAffected = "BookCategories",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };
                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        #region
        private async Task<bool> CheckBookCategoryExistsAsync(string name, int? bookCategoryIdToExclude = null)
        {
            return await _context.BookCategories
                .AnyAsync(x => x.Name.ToLower() == name.ToLower() &&
                              (!bookCategoryIdToExclude.HasValue || x.BookCategoryId != bookCategoryIdToExclude.Value));
        }
        #endregion
    }
}
