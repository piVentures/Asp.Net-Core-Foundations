using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AuthDemo.Api.Models.Entities;

namespace AuthDemo.Api.Models
{
    // Database context integrating EF Core with ASP.NET Identity
    public class AppDbContext : IdentityDbContext
    {
        // Constructor receives DbContext options from DI
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Users table (custom AppUser entity)
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
