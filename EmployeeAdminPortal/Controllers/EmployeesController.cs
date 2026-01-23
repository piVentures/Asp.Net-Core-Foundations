using Microsoft.AspNetCore.Mvc;
using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Models.Dtos;
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

        
        
        [HttpPost] // POST api/employees
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            // Map the incoming DTO to the Employee entity
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,      // Employee name from request
                Email = addEmployeeDto.Email,    // Employee email from request
                Phone = addEmployeeDto.Phone,    // Optional phone number
                Salary = addEmployeeDto.Salary   // Salary
            };

            dbContext.Employees.Add(employeeEntity); // Add the new employee to the DbContext
            dbContext.SaveChanges();                 // Persist changes to the database

            return Ok(employeeEntity); // Return 200 OK with the newly added employee
        }

    }
}
