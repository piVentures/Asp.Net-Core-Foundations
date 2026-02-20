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
            return Ok($"Creating a shirt");
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