using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models.Repositories;
using WebApiDemo.Models;
using WebApiDemo.Filters;
namespace WebApiDemo.Controllers
{
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

        [HttpPut("{id}")]
        public IActionResult UpdateShirt(int id)
        {
             return Ok($"updating a shirt");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteShirt(int id)
        {
            return Ok($"Deleting a shirt: {id}");
        }
    }

}