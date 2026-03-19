using Microsoft.AspNetCore.Mvc;
using FileDemo.Api.Models;

namespace FileDemo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class FileController : ControllerBase
    {
        [HttpPost("upload"), DisableRequestSizeLimit]
        public async Task <IActionResult> UploadFile([FromForm] FileUploadModel model)
        {
            if(model.File == null || model.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

        var folderName = Path.Combine("Resources", "AllFiles");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (!Directory.Exists(pathToSave))
        {
            Directory.CreateDirectory(pathToSave);
        }
        var fileName = model.File.FileName;
        var fullPath = Path.Combine(pathToSave, fileName);
        var dbPath = Path.Combine(folderName, fileName);

            if (System.IO.File.Exists(fullPath))
            {
                return BadRequest("File already exists.");
            } 

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }   

            return Ok(new {dbPath});
        }
    }
}