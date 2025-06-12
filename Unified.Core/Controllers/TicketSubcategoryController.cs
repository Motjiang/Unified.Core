using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Book;
using Unified.Application.DTOs.Ticket;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Infrastructure.Data;

namespace Unified.Core.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketSubcategoryController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly ITicketSubcategoryService _ticketSubcategoryService;
        private readonly ApplicationDbContext _context;

        public TicketSubcategoryController(UserManager<Employee> userManager, ITicketSubcategoryService ticketSubcategoryService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _ticketSubcategoryService = ticketSubcategoryService;
            _context = context;
        }

        [HttpGet("get-all-ticket-subcategories")]
        public async Task<IActionResult> GetAllTicketSubcategories()
        {
            try
            {
                var subcategories = await _ticketSubcategoryService.GetAllTicketSubcategoriesAsync();
                if (subcategories == null || !subcategories.Any())
                {
                    return NotFound(new { title = "No Subcategories Found", message = "There are no ticket subcategories in the system." });
                }
                return Ok(subcategories);
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

        [HttpGet("get-ticket-subcategory-by-id/{id}")]
        public async Task<IActionResult> GetTicketSubcategoryById(int id)
        {
            try
            {
                var subcategory = await _ticketSubcategoryService.GetTicketSubcategoryByIdAsync(id);
                if (subcategory == null)
                {
                    return NotFound(new { title = "Subcategory Not Found", message = "No active subcategory found." });
                }
                return Ok(subcategory);
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

        [HttpPost("add-ticket-subcategory")]
        public async Task<IActionResult> AddTicketSubcategory([FromBody] CreateTicketSubcategoryDto subcategory)
        {            
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);

                if (subcategory == null || string.IsNullOrWhiteSpace(subcategory.Name))
                {
                    return BadRequest(new { title = "Invalid Input", message = "Subcategory name is required." });
                }

                if (await CheckTicketSubcategoryExistsAsync(subcategory.Name))
                {
                    return Conflict(new { title = "Subcategory Exists", message = "A subcategory with this name already exists." });
                }

                await _ticketSubcategoryService.AddTicketSubcategoryAsync(subcategory);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Created Ticket Subcategory",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Ticket Subcategory '{subcategory.Name}' created successfully.",
                    TableAffected = "TicketSubcategories",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The Ticket Subcategory has been added successfully." });
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

        [HttpPut("update-ticket-subcategory")]
        public async Task<IActionResult> UpdateTicketSubcategory([FromBody] TicketSubcategoryDto subcategory)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (subcategory == null || subcategory.SubcategoryId <= 0 || string.IsNullOrWhiteSpace(subcategory.Name))
                {
                    return BadRequest(new { title = "Invalid Input", message = "Subcategory ID and name are required." });
                }
                if (await CheckTicketSubcategoryExistsAsync(subcategory.Name, subcategory.SubcategoryId))
                {
                    return Conflict(new { title = "Subcategory Exists", message = "A subcategory with this name already exists." });
                }
                await _ticketSubcategoryService.UpdateTicketSubcategoryAsync(subcategory);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Updated Ticket Subcategory",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Ticket Subcategory '{subcategory.Name}' updated successfully.",
                    TableAffected = "TicketSubcategories",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };
                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();
                return Ok(new { title = "Success", message = "The Ticket Subcategory has been updated successfully." });
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

        [HttpPatch("delete-ticket-subcategory")]
        public async Task<IActionResult> DeleteTicketSubcategory([FromBody] TicketSubcategoryDto subcategory)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (subcategory == null || subcategory.SubcategoryId <= 0)
                {
                    return BadRequest(new { title = "Invalid Input", message = "Subcategory ID is required." });
                }
                var existingSubcategory = await _ticketSubcategoryService.GetTicketSubcategoryByIdAsync(subcategory.SubcategoryId);
                if (existingSubcategory == null)
                {
                    return NotFound(new { title = "Subcategory Not Found", message = "No active subcategory found." });
                }
                await _ticketSubcategoryService.DeleteTicketSubcategoryAsync(subcategory);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Deleted Ticket Subcategory",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Ticket Subcategory '{existingSubcategory.Name}' deleted successfully.",
                    TableAffected = "TicketSubcategories",
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
        private async Task<bool> CheckTicketSubcategoryExistsAsync(string name, int? SubcategoryIdToExclude = null)
        {
            return await _context.TicketSubcategories
                .AnyAsync(x => x.Name.ToLower() == name.ToLower() &&
                              (!SubcategoryIdToExclude.HasValue || x.SubcategoryId != SubcategoryIdToExclude.Value));
        }
        #endregion
    }
}
