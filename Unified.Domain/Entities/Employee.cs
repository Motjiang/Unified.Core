using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Unified.Domain.Entities
{
    public class Employee : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
