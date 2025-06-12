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

        public EmployeeController(UserManager<Employee> userManager, IEmployeeService employeeService)
        {
            _userManager = userManager;
            _employeeService = employeeService;
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
        }


        #region
        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
        #endregion
    }
}
