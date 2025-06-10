using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Unified.Domain.Entities;

namespace Unified.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<SalesTransaction> SalesTransactions { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<BookRequest> BookRequests { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<TicketSubcategory> TicketSubcategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookRequest>()
            .HasOne(br => br.ProcessedByAdmin)
            .WithMany(e => e.RequestsProcessed)
            .HasForeignKey(br => br.ProcessedByAdminId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookRequest>()
                .HasOne(br => br.RequestedByUser)
                .WithMany()
                .HasForeignKey(br => br.RequestedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SalesTransaction>()
                .HasOne(st => st.Book)
                .WithMany()
                .HasForeignKey(st => st.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SalesTransaction>()
                .HasOne(st => st.SoldByEmployee)
                .WithMany()
                .HasForeignKey(st => st.SoldByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Subcategory)
                .WithMany(sc => sc.Tickets)
                .HasForeignKey(t => t.SubcategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketSubcategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
                .HasOne(e => e.Designation)
                .WithMany()
                .HasForeignKey(e => e.DesignationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
