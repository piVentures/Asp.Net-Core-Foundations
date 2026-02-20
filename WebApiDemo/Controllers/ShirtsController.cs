using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models.Repositories;
using WebApiDemo.Models;
using WebApiDemo.Filters;
using WebApiDemo.Filters.ExceptionFilters;
namespace WebApiDemo.Controllers
{
    // Routing → Filters → Binding → Validation → Action 
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase {
        

        [HttpGet]
        public IActionResult GetShirt()
        {
            var shirts = ShirtRepository.GetShirts();
            return Ok(shirts);
        }  

    [HttpGet("{id}")]
    [Shirt_validateShirtIdFilter]
    public IActionResult GetShirtById(int id)
        {
          return Ok(ShirtRepository.GetShirtById(id));
        }

    [HttpPost]
    [Shirt_ValidateCreateShirtFilter]
     public IActionResult CreateShirt([FromBody]Shirt shirt)
        {
            if (shirt == null)
            {
                return BadRequest("Shirt data is null.");
            }
            var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color,shirt.Size);
            if (existingShirt != null)
            {
                return Conflict("A shirt with the same properties already exists.");
            }
            ShirtRepository.Addshirt(shirt);

            // Return a 201 Created response with the location of the newly created shirt 
            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
           
        }

    //   This method is responsible for updating an existing shirt. It takes the shirt ID from the URL and the updated shirt data from the request body. It first checks if the ID in the URL matches the ID of the shirt being updated. If they don't match, it returns a Bad Request response. If they match, it attempts to update the shirt in the repository. If the shirt with the specified ID doesn't exist, it returns a Not Found response. If the update is successful, it returns a No Content response indicating that the update was successful but there's no content to return.

        [HttpPut("{id}")]
        [Shirt_validateShirtIdFilter]
        [Shirt_ValidateUpdateFilter]
        [Shirt_HandleUpdateExeptionsFilterAttibute]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
        ShirtRepository.UpdateShirt(shirt);
           
        return NoContent(); 

        }

        [HttpDelete("{id}")]
        [Shirt_validateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
           var shirt = ShirtRepository.GetShirtById(id);
           ShirtRepository.DeleteShirt(id);
           return Ok(shirt);
        }
    }

}

