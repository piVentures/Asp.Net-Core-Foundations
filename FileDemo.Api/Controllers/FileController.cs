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




        [HttpPost("upload-multiple"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadMultipleFiles([FromForm] MultipleUploadModel model)
        {
            var response = new Dictionary<string, string>();
            if(model.Files == null || model.Files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            foreach(var file in model.Files)
            {
                 var folderName = Path.Combine("Resources", "AllFiles");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);  
                }
                var fileName = file.FileName;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);   

                if (!System.IO.File.Exists(fullPath))
                {
                  using var memoryStream = new MemoryStream();  
                    await file.CopyToAsync(memoryStream);
                    await System.IO.File.WriteAllBytesAsync(fullPath, memoryStream.ToArray());
                    response.Add(fileName, dbPath);
                }
                else
                {
                    response.Add(fileName, "File already exists.")  ;
                }  
            }
            return Ok(new{ response } );
        }



        [HttpGet("download/{fileName}")]
     public async Task<IActionResult> DownloadByName(string fileName)
{
    var folderName = Path.Combine("Resources", "AllFiles");
    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
    var fullPath = Path.Combine(pathToSave, fileName);

    if (!System.IO.File.Exists(fullPath))
    {
        return NotFound("File not found.");
    }

    var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);

    return File(fileBytes, "application/octet-stream", fileName);
}
    }
}
