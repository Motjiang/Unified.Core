using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Department;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Infrastructure.Data;

[Authorize(Roles = "Admin,HR")]
[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly UserManager<Employee> _userManager;
    private readonly IDepartmentService _departmentService;
    private readonly ApplicationDbContext _context;

    public DepartmentController(UserManager<Employee> userManager, IDepartmentService departmentService, ApplicationDbContext context)
    {
        _userManager = userManager;
        _departmentService = departmentService;
        _context = context;
    }

    [HttpGet("get-all-departments")]
    public async Task<IActionResult> GetAllDepartments([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10, [FromQuery] string searchString = "")
    {
        try
        {
            var allDepartments = (await _departmentService.GetAllAsync()).ToList();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                allDepartments = allDepartments
                    .Where(d => d.name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var totalCount = allDepartments.Count;

            var pagedDepartments = allDepartments
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (!pagedDepartments.Any())
            {
                return NotFound(new
                {
                    title = "No Departments Found",
                    message = "There are no matching departments."
                });
            }

            return Ok(new
            {
                data = pagedDepartments,
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


    [HttpGet("get-department-by-id/{id}")]
    public async Task<IActionResult> GetDepartmentById(int id)
    {
        try
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound(new { title = "Department Not Found", message = "No active department found." });
            }
            return Ok(department);
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

    [HttpPost("add-department")]
    public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentDto departmentDto)
    {
        try
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (departmentDto == null)
            {
                return BadRequest(new { title = "Invalid Data", message = "Department data is required." });
            }

            if (await CheckDepartmentExistsAsync(departmentDto.name))
            {
                return Conflict(new { title = "Already Exists", message = "A department with this name already exists." });
            }

            await _departmentService.AddAsync(departmentDto);

            var systemAuditLog = new AuditTrail
            {
                Action = "Created Department",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Description = $"Department '{departmentDto.name}' created successfully.",
                TableAffected = "Department",
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

    [HttpPut("update-department")]
    public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentDto departmentDto)
    {
        try
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (departmentDto == null || departmentDto.id <= 0)
            {
                return BadRequest(new { title = "Invalid Data", message = "Valid department data is required." });
            }

            if (await CheckDepartmentExistsAsync(departmentDto.name, departmentDto.id))
            {
                return Conflict(new { title = "Already Exists", message = "A department with this name already exists." });
            }

            await _departmentService.UpdateAsync(departmentDto);

            var systemAuditLog = new AuditTrail
            {
                Action = "Updated Department",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Description = $"Department '{departmentDto.name}' updated successfully.",
                TableAffected = "Department",
                Date = DateTime.UtcNow,
                EmployeeId = loggedInUser.Id,
            };

            _context.AuditTrails.Add(systemAuditLog);
            await _context.SaveChangesAsync();

            return Ok(new { title = "Success", message = "The department has been updated successfully." });
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

    [HttpPatch("delete-department")]
    public async Task<IActionResult> DeleteDepartment([FromBody] DepartmentDto departmentDto)
    {
        try
        {
            var loggedInUser = await _userManager.GetUserAsync(User);

            if (departmentDto == null || departmentDto.id <= 0)
            {
                return BadRequest(new { title = "Invalid Data", message = "Valid department data is required." });
            }

            await _departmentService.DeleteAsync(departmentDto);

            var systemAuditLog = new AuditTrail
            {
                Action = "Deleted Department",
                IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                Description = $"Department '{departmentDto.name}' deleted successfully.",
                TableAffected = "Department",
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

    #region Helpers
    private async Task<bool> CheckDepartmentExistsAsync(string name, int? departmentIdToExclude = null)
    {
        return await _context.Departments
            .AnyAsync(x => x.Name.ToLower() == name.ToLower() &&
                          (!departmentIdToExclude.HasValue || x.DepartmentId != departmentIdToExclude.Value));
    }
    #endregion
}
