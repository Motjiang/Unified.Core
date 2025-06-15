using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Department;
using Unified.Application.DTOs.Designation;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Infrastructure.Data;

namespace Unified.Core.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IDesignationService _designationService;
        private readonly ApplicationDbContext _context;

        public DesignationController(UserManager<Employee> userManager, IDesignationService designationService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _designationService = designationService;
            _context = context;
        }

        [HttpGet("get-all-designations")]
        public async Task<IActionResult> GetAllDesignations([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchString = "")
        {
            try
            {
                var allDesignations = (await _designationService.GetAllAsync()).ToList(); 

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    allDesignations = allDesignations
                        .Where(d => d.title.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                var totalCount = allDesignations.Count;

                var pagedDesignations = allDesignations
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                if (!pagedDesignations.Any())
                {
                    return NotFound(new
                    {
                        title = "No Designations Found",
                        message = "There are no matching designations."
                    });
                }

                return Ok(new
                {
                    data = pagedDesignations,
                    totalCount = totalCount,
                    pageIndex = pageIndex,
                    pageSize = pageSize
                });
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



        [HttpGet("get-designation-by-id/{id}")]
        public async Task<IActionResult> GetDesignationById(int id)
        {
            try
            {
                var designation = await _designationService.GetByIdAsync(id);
                if (designation == null)
                {
                    return NotFound(new { title = "Designation Not Found", message = "No active designation found." });
                }
                return Ok(designation);
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

        [HttpPost("add-designation")]
        public async Task<IActionResult> AddDesignation([FromBody] CreateDesignationDto designationDto)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (designationDto == null)
            {
                return BadRequest(new { title = "Invalid Input", message = "Designation data is required." });
            } 

            if (await CheckDesignatioonExistsAsync(designationDto.title))
            {
                return Conflict(new { title = "Designation Exists", message = "A designation with this title already exists." });
            }

            try
            {
                await _designationService.AddAsync(designationDto);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Created Designation",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Designation '{designationDto.title}' created successfully.",
                    TableAffected = "Designation",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The designation has been updated successfully." });
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

        [HttpPut("update-designation")]
        public async Task<IActionResult> UpdateDesignation([FromBody] DesignationDto designationDto)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (designationDto == null || designationDto.designationId <= 0)
            {
                return BadRequest(new { title = "Invalid Input", message = "Designation data is required." });
            }

            if (await CheckDesignatioonExistsAsync(designationDto.title, designationDto.designationId))
            {
                return Conflict(new { title = "Designation Exists", message = "A designation with this title already exists." });
            }

            try
            {
                await _designationService.UpdateAsync(designationDto);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Updated Designation",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Designation '{designationDto.title}' updated successfully.",
                    TableAffected = "Designation",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The designation has been updated successfully." });
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

        [HttpPatch("delete-designation")]
        public async Task<IActionResult> DeleteDesignation([FromBody] DesignationDto designationDto)
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (designationDto == null || designationDto.designationId <= 0)
            {
                return BadRequest(new { title = "Invalid Input", message = "Designation data is required." });
            }

            try
            {
                await _designationService.DeleteAsync(designationDto);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Deleted Designation",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Designation '{designationDto.title}' deleted successfully.",
                    TableAffected = "Designation",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The designation has been deleted successfully." });
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

        #region Helpers
        private async Task<bool> CheckDesignatioonExistsAsync(string title, int? designationIdToExclude = null)
        {
            return await _context.Designations
                .AnyAsync(x => x.Title.ToLower() == title.ToLower() &&
                              (!designationIdToExclude.HasValue || x.DepartmentId != designationIdToExclude.Value));
        }
        #endregion
    }
}
