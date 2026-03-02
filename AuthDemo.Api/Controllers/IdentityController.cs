using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthDemo.Api.Models.Dtos;
using AuthDemo.Api.Models.Entities;

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
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(
            [FromBody] UserRegistrationModel model)
        {
            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return Ok(new { message = "User created successfully" });

            return BadRequest(result.Errors);
        }

        // ========================
        // SIGNIN
        // ========================
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

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserID", user.Id.ToString())
                }),
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
