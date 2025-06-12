using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Admin;
using Unified.Application.DTOs.Employee;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Infrastructure.Data;

namespace Unified.Core.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDbContext _context;

        public EmployeeController(UserManager<Employee> userManager, IEmployeeService employeeService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _employeeService = employeeService;
            _context = context;
        }

        [HttpGet("get-all-employees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var loogedInUser = await _userManager.GetUserAsync(User);

            var employees = await _employeeService.GetAllEmployeesAsync(loogedInUser.Id);

            if (employees == null || !employees.Any())
            {
                return NotFound(new
                {
                    title = "No Employees Found",
                    message = "There are no employees in the system."
                });
            }

            return Ok(employees);
        }

        [HttpGet("get-employee-by-id/{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound(new
                {
                    title = "Employee Not Found",
                    message = "No active employee found"
                });
            }
            return Ok(employee);
        }

        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto employeeDto)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);

                if (loggedInUser == null)
                {
                    return Unauthorized(new
                    {
                        title = "Unauthorized",
                        message = "You must be logged in as HR/Admin to create an employee."
                    });
                }

                if (await CheckEmailExistsAsync(employeeDto.Email))
                {
                    return Conflict(new
                    {
                        title = "Email Already Exists",
                        message = "An employee with this email already exists."
                    });
                }

                await _employeeService.AddEmployeeAsync(employeeDto, loggedInUser.Id);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Created Employee",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Employee '{employeeDto.FirstName} {employeeDto.LastName}' created successfully.",
                    TableAffected = "Employee",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The employee has been added successfully." });
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

        [HttpPut("update-employee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);

                if (employeeDto == null || string.IsNullOrEmpty(employeeDto.Id))
                {
                    return BadRequest(new
                    {
                        title = "Invalid Data",
                        message = "Employee data is required."
                    });
                }

                var existingEmployee = await _employeeService.GetEmployeeByIdAsync(employeeDto.Id);

                if (existingEmployee == null)
                {
                    return NotFound(new
                    {
                        title = "Employee Not Found",
                        message = "No active employee found."
                    });
                }

                if (await CheckEmailExistsAsync(employeeDto.Email, employeeDto.Id))
                {
                    return Conflict(new
                    {
                        title = "Email Already Exists",
                        message = "An employee with this email already exists."
                    });
                }

                await _employeeService.UpdateEmployeeAsync(employeeDto);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Updated Employee",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Employee '{employeeDto.FirstName} {employeeDto.LastName}' updated successfully.",
                    TableAffected = "Employee",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The employee has been updated successfully." });
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

        [HttpPatch("delete-employee")]
        public async Task<IActionResult> DeleteEmployee([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (employeeDto == null || string.IsNullOrEmpty(employeeDto.Id))
                {
                    return BadRequest(new
                    {
                        title = "Invalid Data",
                        message = "Valid employee data is required."
                    });
                }
                var existingEmployee = await _employeeService.GetEmployeeByIdAsync(employeeDto.Id);
                if (existingEmployee == null)
                {
                    return NotFound(new
                    {
                        title = "Employee Not Found",
                        message = "No active employee found."
                    });
                }
                await _employeeService.DeleteEmployeeAsync(employeeDto);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Deleted Employee",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Employee '{employeeDto.FirstName} {employeeDto.LastName}' deleted successfully.",
                    TableAffected = "Employee",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id
                };
                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();
                return Ok(new { title = "Success", message = "The employee has been deleted successfully." });
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
        private async Task<bool> CheckEmailExistsAsync(string email, string? userIdToExclude = null)
        {
            return await _userManager.Users
                .AnyAsync(x => x.Email.ToLower() == email.ToLower()
                            && (userIdToExclude == null || x.Id != userIdToExclude));
        }
        #endregion
    }
}
