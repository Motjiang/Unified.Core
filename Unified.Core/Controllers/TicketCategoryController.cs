using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Ticket;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Infrastructure.Data;

namespace Unified.Core.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketCategoryController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ITicketCategoryService _ticketCategoryService;
        private readonly ApplicationDbContext _context;

        public TicketCategoryController(UserManager<Employee> userManager, ITicketCategoryService ticketCategoryService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _ticketCategoryService = ticketCategoryService;
            _context = context;
        }

        [HttpGet("get-all-ticket-categories")]
        public async Task<IActionResult> GetAllTicketCategories()
        {
            try
            {
                var categories = await _ticketCategoryService.GetAllTicketCategoriesAsync();
                if (categories == null || !categories.Any())
                {
                    return NotFound(new { title = "No Categories Found", message = "There are no ticket categories in the system." });
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

        [HttpGet("get-ticket-category-by-id/{id}")]
        public async Task<IActionResult> GetTicketCategoryById(int id)
        {
            try
            {
                var category = await _ticketCategoryService.GetTicketCategoryByIdAsync(id);
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

        [HttpPost("add-ticket-category")]
        public async Task<IActionResult> AddTicketCategory([FromBody] CreateTicketCategoryDto category)
        {
            
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (category == null || string.IsNullOrWhiteSpace(category.Name))
                {
                    return BadRequest(new { title = "Invalid Input", message = "Category name is required." });
                }

                if (await CheckTicketCategoryExistsAsync(category.Name))
                {
                    return Conflict(new { title = "Category Exists", message = "A category with this name already exists." });
                }

                await _ticketCategoryService.AddTicketCategoryAsync(category);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Created Ticket Category",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Ticket Category '{category.Name}' created successfully.",
                    TableAffected = "TicketCategories",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The Ticket Category has been added successfully." });
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

        [HttpPut("update-ticket-category")]
        public async Task<IActionResult> UpdateTicketCategory([FromBody] TicketCategoryDto category)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (category == null || string.IsNullOrWhiteSpace(category.Name) || category.CategoryId <= 0)
                {
                    return BadRequest(new { title = "Invalid Input", message = "Category name and ID are required." });
                }
                if (await CheckTicketCategoryExistsAsync(category.Name, category.CategoryId))
                {
                    return Conflict(new { title = "Category Exists", message = "A category with this name already exists." });
                }
                await _ticketCategoryService.UpdateTicketCategoryAsync(category);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Updated Ticket Category",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Ticket Category '{category.Name}' updated successfully.",
                    TableAffected = "TicketCategories",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };
                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();
                return Ok(new { title = "Success", message = "The Ticket Category has been updated successfully." });
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

        [HttpPatch("delete-ticket-category")]
        public async Task<IActionResult> DeleteTicketCategory([FromBody] TicketCategoryDto category)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (category == null || category.CategoryId <= 0)
                {
                    return BadRequest(new { title = "Invalid Input", message = "Category ID is required." });
                }
                var existingCategory = await _ticketCategoryService.GetTicketCategoryByIdAsync(category.CategoryId);
                if (existingCategory == null)
                {
                    return NotFound(new { title = "Category Not Found", message = "No active category found." });
                }
                await _ticketCategoryService.DeleteTicketCategoryAsync(category);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Deleted Ticket Category",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Ticket Category '{existingCategory.Name}' deleted successfully.",
                    TableAffected = "TicketCategories",
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
        private async Task<bool> CheckTicketCategoryExistsAsync(string name, int? ticketSubcategoryIdToExclude = null)
        {
            return await _context.TicketSubcategories
                .AnyAsync(x => x.Name.ToLower() == name.ToLower() &&
                              (!ticketSubcategoryIdToExclude.HasValue || x.SubcategoryId != ticketSubcategoryIdToExclude.Value));
        }
        #endregion
    }
}
