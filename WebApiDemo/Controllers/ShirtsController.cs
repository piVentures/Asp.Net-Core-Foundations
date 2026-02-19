using Microsoft.AspNetCore.Mvc;

namespace WebApiDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShirtsController: ControllerBase {

        [HttpGet]
        public string GetShirt()
        {
            return "reading all the shirts";
        }

    [HttpGet("{id}")]
    public string GetShirtById(int id)
        {
            return $"Reading shirt: {id}";
        }

    [HttpPost]
    public string CreateShirt()
        {
            return $"Creating a shirt";
        }

        [HttpPut("{id}")]
        public string UpdateShirt(int id)
        {
            return $"Updating a shirt: {id}";
        }

        [HttpDelete("{id}")]
        public string DeleteShirt(int id)
        {
            return $"Deleting a shirt: {id}";
        }
    }

}