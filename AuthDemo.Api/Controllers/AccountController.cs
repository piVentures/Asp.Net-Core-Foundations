using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthDemo.Api.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;



namespace AuthDemo.Api.Controllers
{
    [Authorize]
     [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet("UserProfile")]
        public async Task<IActionResult> GetUserProfile(ClaimsPrincipal user, UserManager<AppUser> userManager)
        {
            string UserID = user.Claims.First(x => x.Type == "UserID").Value;
            var userDetails = await userManager.FindByIdAsync(UserID);
            return Ok( new {
                Email = userDetails?.Email,
                FullName = userDetails?.FullName

            });

        }
    }
} 