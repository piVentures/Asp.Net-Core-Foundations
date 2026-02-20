using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models.Repositories;
using WebApiDemo.Models;
using WebApiDemo.Filters;
namespace WebApiDemo.Controllers
{
    // Routing → Filters → Binding → Validation → Action 
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase {
        

        [HttpGet]
        public IActionResult GetShirt()
        {
            return Ok("reading all the shirts");
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
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {

            try
            {
                 ShirtRepository.UpdateShirt(shirt);
            }
            catch
            {
                if (!ShirtRepository.ShirtExists(id))
            {
                return NotFound($"Shirt with ID {id} not found.");
                throw;
            }
            }
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            return Ok($"Deleting a shirt: {id}");
        }
    }

}

