using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Unified.Domain.Entities;

namespace Unified.Application.Services
{
    public class JWTService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Employee> _userManager;
        private readonly SymmetricSecurityKey _jwtKey;

        public JWTService(IConfiguration config, UserManager<Employee> userManager)
        {
            _config = config;
            _userManager = userManager;
            _jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
        }

        public async Task<string> CreateJWT(Employee employee)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id),
                new Claim(ClaimTypes.Email, employee.UserName),
                new Claim(ClaimTypes.GivenName, employee.FirstName),
                new Claim(ClaimTypes.Surname, employee.LastName)
            };

            var roles = await _userManager.GetRolesAsync(employee);
            userClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var creadentials = new SigningCredentials(_jwtKey, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_config["JWT:ExpiresInDays"])),
                SigningCredentials = creadentials,
                Issuer = _config["JWT:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(jwt);
        }
    }
}

