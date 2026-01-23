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

        [HttpGet]
        [Route("{id:guid}")]
         public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();

            }
            return Ok(employee);
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



        [HttpPut]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto )
        {
            var employee = dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;

            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
         public IActionResult DeleteEmployeeById(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee is null)
            {
                return NotFound();

            }
            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok();
        }

        

    }
}



/*
EmployeesController - Conceptual Overview and Logic Analysis

This controller demonstrates key ASP.NET Core and Entity Framework Core concepts for building a RESTful API:

1. Dependency Injection & DbContext:
   - The controller relies on constructor injection to receive an ApplicationDbContext instance.
   - This allows seamless database access without manually instantiating the context, promoting testability and decoupling.
   - DbContext represents a session with the database and tracks entity states (Added, Modified, Deleted).

2. Entity Framework Core (EF Core) Integration:
   - The controller works directly with EF Core entities (Employee) to perform CRUD operations.
   - EF Core handles object-relational mapping (ORM) and translates LINQ queries into SQL for SQLite.
   - DbSet<TEntity> represents a collection of entities in the database and provides methods like Add(), Find(), Remove(), and LINQ queries like ToList().

3. DTO Usage:
   - AddEmployeeDto and UpdateEmployeeDto separate the API contract from the database schema.
   - DTOs prevent over-posting attacks by exposing only required fields and avoiding direct entity binding.
   - Mapping DTOs to entities ensures data integrity and aligns with domain-driven design principles.

4. IActionResult & HTTP Abstractions:
   - Endpoints return IActionResult, allowing flexible responses like Ok(), NotFound(), or BadRequest().
   - This abstraction decouples the logic from specific HTTP responses while keeping the API RESTful.

5. RESTful Routing & Attribute Routing:
   - Attribute routing (e.g., [Route("api/[controller]")]) defines clean and intuitive REST endpoints.
   - Route parameters (e.g., {id:guid}) enable model binding, which automatically converts HTTP request data into C# types.

6. Change Tracking & Persistence:
   - EF Core tracks entity states; adding or modifying entities updates their state.
   - Calling SaveChanges() commits all tracked changes to the database in a single transaction.
   - This abstracts away manual SQL, ensuring consistency and atomicity.

7. Overall Logic:
   - The controller acts as a mediator between HTTP requests and the database.
   - It leverages dependency injection, EF Core, and DTOs to enforce separation of concerns, maintainable code, and safe data handling.
   - It exemplifies the modern ASP.NET Core approach to building scalable, testable APIs with ORM support.

*/
