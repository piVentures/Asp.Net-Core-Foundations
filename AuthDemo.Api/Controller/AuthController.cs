using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AuthDemo.Api.Entities;
using AuthDemo.Api.Models;
using Microsoft.AspNetCore.Identity; 

namespace AuthDemo.Api.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // Note: Static users will reset whenever the app restarts.
        public static User user = new();

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
         
            var passwordHasher = new PasswordHasher<User>();
            var hashedPassword = passwordHasher.HashPassword(user, request.Password);

            user.UserName = request.UserName;
            user.PasswordHash = hashedPassword;

            return Ok(user);
        }
    }
}