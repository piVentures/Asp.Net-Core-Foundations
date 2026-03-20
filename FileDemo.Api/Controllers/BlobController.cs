using Microsoft.AspNetCore.Mvc;
using FileDemo.Api.Services;

namespace FileDemo.Api.Controllers;

[ApiController]
[Route("api/files")]
public class BlobController : ControllerBase
{
    private readonly IBlobService _blobService;

    public BlobController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    //  Upload file
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File is empty");

        using var stream = file.OpenReadStream();

        var fileId = await _blobService.UploadAsync(
            stream,
            file.ContentType,
            cancellationToken);

        return Ok(new { fileId });
    }

    //  Download file
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Download(
        Guid id,
        CancellationToken cancellationToken)
    {
        var file = await _blobService.DownloadAsync(id, cancellationToken);

        return File(
            file.Stream,
            file.ContentType,
            fileDownloadName: $"{id}"
        );
    }

    //  Delete file
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _blobService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
