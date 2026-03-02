using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AuthDemo.Api.Models.Entities
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(100)")]
        public string? FullName { get; set; }
    }   
}