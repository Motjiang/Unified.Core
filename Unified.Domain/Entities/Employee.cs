using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unified.Domain.Entities
{
    public class Employee : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public string? IdentityNumber { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Province { get; set; }
        public string? City { get; set; }
        public string? StreetAddress { get; set; }
        public string? PostalCode { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateRemoved { get; set; }

        public int? DesignationId { get; set; }

        [ForeignKey(nameof(DesignationId))]
        public Designation Designation { get; set; }

        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; }
        public ICollection<BookRequest>? RequestsMade { get; set; } = new List<BookRequest>();      
        public ICollection<BookRequest>? RequestsProcessed { get; set; } = new List<BookRequest>();

        public ICollection<LeaveRequest>? LeaveRequestsSubmitted { get; set; } = new List<LeaveRequest>();
        public ICollection<LeaveRequest>? LeaveRequestsProcessed { get; set; } = new List<LeaveRequest>();
    }
}

