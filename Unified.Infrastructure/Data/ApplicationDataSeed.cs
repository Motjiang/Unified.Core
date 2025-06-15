using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Infrastructure.Data
{
    public class ApplicationDataSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDataSeed(ApplicationDbContext context, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeContextAsync()
        {
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Count() > 0)
            {
                // applies any pending migration into our database
                await _context.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = DataSeed.AdminRole });
                await _roleManager.CreateAsync(new IdentityRole { Name = DataSeed.HrRole });
            }

            if (!_userManager.Users.AnyAsync().GetAwaiter().GetResult())
            {
                var admin = new Employee
                {
                    FirstName = "admin",
                    LastName = "user001",
                    UserName = DataSeed.AdminUserName,
                    Email = DataSeed.AdminUserName,
                    Status = "Active",
                    CreatedBy = "0000",
                    DateCreated = DateTime.Now,
                    EmailConfirmed = true,
                    DepartmentId = 1,
                    DesignationId = 1
                };
                await _userManager.CreateAsync(admin, "123456");
                await _userManager.AddToRoleAsync(admin, DataSeed.AdminRole);
                await _userManager.AddClaimsAsync(admin, new Claim[]
                {
                    new Claim(ClaimTypes.Email, admin.Email),
                    new Claim(ClaimTypes.Surname, admin.LastName)
                });

                var hr = new Employee
                {
                    FirstName = "Hr",
                    LastName = "user002",
                    UserName = DataSeed.HrUserName,
                    Email = DataSeed.HrUserName,
                    Status = "Active",
                    CreatedBy = "000",
                    DateCreated = DateTime.Now,
                    EmailConfirmed = true,
                    DepartmentId = 2,
                    DesignationId = 2
                };
                await _userManager.CreateAsync(hr, "123456");
                await _userManager.AddToRoleAsync(hr, DataSeed.HrRole);
                await _userManager.AddClaimsAsync(hr, new Claim[]
                {
                    new Claim(ClaimTypes.Email, hr.Email),
                    new Claim(ClaimTypes.Surname, hr.LastName)
                });
            }
        }
    }
}
