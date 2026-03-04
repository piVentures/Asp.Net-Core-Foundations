using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
    
// this is for users who verifies account with authentication
        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok(new { message = "This endpoint requires authentication!" });
        }
     
// this is for for authenticated users with admin roles
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok(new { message = "Only Admins can see this!" });
        }

// this is for roles with either admin or teacher
        [Authorize(Roles = "Admin,Teacher")]
        [HttpGet("AdminOrTeacher")]
        public IActionResult AdminOrTeacher()
        {
            return Ok(new { message = "Only Admins or Teachers can access this endpoint!" });
        }

// POLICY based authorization using claims and roles in combination

        [Authorize(Policy = "HasLibraryId")]
        [HttpGet("library-members")]
        public IActionResult LibraryMembers()
        {
            return Ok(new { message = "Only users with a LibraryId claim can access this endpoint!" });
        }

// this is for teacher role with female only policies    
        [Authorize(Roles ="Teacher", Policy ="FemaleOnly")]
        [HttpGet("ApplyForMaternityLeave")]
        public IActionResult ApplyForMaternityLeave()
        {
            return Ok(new { message = "Applied for Maternity leave." });
        }
        

// this must satisify both of the policies
        [Authorize(Policy = "Under10")]
        [Authorize(Policy = "FemaleOnly")]
        [HttpGet("Under10sAndFemale")]
        public IActionResult Under10sAndFemale()
        {
            return Ok(new { message = "Under 10 and Female" });
        }


    }
}
