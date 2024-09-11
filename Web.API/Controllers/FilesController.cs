using Application.Files.Dto;
using Application.Files.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    // Upload file
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(FileUpload file)
    {
        return Ok(await _fileService.UploadImage(file));
    }

    // Download file
    [HttpGet("download/{fileName}")]
    public IActionResult DownloadFile(FileDownload fileDown)
    {
        byte[]? fileBytes = _fileService.DownloadFile(fileDown);
        return File(fileBytes?? null, "application/octet-stream", fileDown.FileName);
    }

    // Delete file
    [HttpDelete("delete/{fileName}")]
    public IActionResult DeleteFile(string fileName)
    {
        var filePath = Path.Combine(_uploadPath, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        System.IO.File.Delete(filePath);
        return Ok();
    }
}
