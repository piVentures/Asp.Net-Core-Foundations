using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuthDemo.Api.Models.Entities
{
    // Custom application user extending ASP.NET IdentityUser
    public class AppUser : IdentityUser
    {
        // Additional user property stored in database
        [Column(TypeName = "nvarchar(100)")] // sets column type and max size
        public string? FullName { get; set; }
    }   
}
