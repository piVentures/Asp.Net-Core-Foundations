using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Data;
using System.Linq;

namespace EmployeeAdminPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Route: api/employees
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext; // EF Core DB context

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext; // Inject DB context
        }

        [HttpGet] // GET api/employees
        public IActionResult GetAllEmployees()
        {
            var allEmployees = dbContext.Employees.ToList(); // Fetch all employees
            return Ok(allEmployees); // Return 200 OK with data
        }
    }
}
