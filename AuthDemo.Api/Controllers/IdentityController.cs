using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthDemo.Api.Models.Dtos;
using AuthDemo.Api.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace AuthDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        // ✅ Dependency Injection via constructor
        public UsersController(
            UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        // ========================
        // SIGNUP
        // ========================
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(
            [FromBody] UserRegistrationModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName,
                Gender = model.Gender,
                LibraryId = model.LibraryId,
                DOB = DateTime.Now.AddYears(-model.Age)
            };

            var result = await _userManager.CreateAsync(user, model.Password);
await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
                return Ok(new { message = "User created successfully" });

            return BadRequest(result.Errors);
        }

        // ========================
        // SIGNIN
        // ========================
        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> Signin(
            [FromBody] LoginModel loginModel)
        {
            var user = await _userManager
                .FindByEmailAsync(loginModel.Email);

            if (user == null ||
                !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                
                return BadRequest(new
                {
                    message = "User name or password is incorrect"
                });
            }
           
            // 🔐 Create JWT Token
            var signInKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["AppSettings:JWTSecret"]!)
            );

// Get all roles assigned to the user from ASP.NET Identity
var roles = await _userManager.GetRolesAsync(user);

// Create ClaimsIdentity that will be embedded inside the JWT token
// Claims = pieces of information about the user
ClaimsIdentity claims = new ClaimsIdentity(new Claim[]
{
    // Unique user identifier (Primary key from Identity)
    new Claim("UserID", user.Id.ToString()),

    // Custom claim: user's gender
    new Claim("Gender", user.Gender.ToString()),

    // Custom claim: dynamically calculated age
    new Claim("Age", (DateTime.Now.Year - user.DOB?.Year ?? 0).ToString()),

    // Role claim (used for [Authorize(Roles = "...")])
    new Claim(ClaimTypes.Role, roles.First())
});

// If the user has a LibraryId, add it as an additional claim
if (user.LibraryId != null)
{
    claims.AddClaim(
        new Claim("LibraryID", user.LibraryId.ToString()!)
    );
}

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    signInKey,
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return Ok(new { token });
        }
    }
}
