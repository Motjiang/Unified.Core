using AutoMapper;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Admin;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public EmployeeController(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet("get-members")]
        public async Task<ActionResult<IEnumerable<ViewEmployeeDto>>> GetMembers()
        {
            var users = await _userManager.Users
                .Where(x => x.UserName != DataSeed.AdminUserName)
                .ToListAsync();

            var members = _mapper.Map<List<ViewEmployeeDto>>(users);

            // Populate roles and lock status manually
            for (int i = 0; i < users.Count; i++)
            {
                members[i].IsLocked = await _userManager.IsLockedOutAsync(users[i]);
                members[i].Roles = (await _userManager.GetRolesAsync(users[i])).ToList();
            }

            return Ok(members);
        }

    }
}
