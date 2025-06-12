using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    }
}
