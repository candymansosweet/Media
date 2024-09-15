using Application.Files.Dto;
using Application.Files.Services;
using Common.Constants;
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
    public async Task<IActionResult> UploadFile([FromForm] FileUpload file)
    {
        return Ok(await _fileService.UploadFile(file));
    }
    // Download file
    [HttpGet("download-by-infor")]
    public IActionResult DownloadFile([FromQuery] FileDownload fileDown)
    {
        byte[] fileBytes = _fileService.DownloadFile(fileDown);
        return File(fileBytes, "application/octet-stream", fileDown.FileName);
    }
    [HttpGet("download-by-path")]
    public IActionResult DownloadFile(string fileDown)
    {
        byte[] fileBytes = _fileService.DownloadFile(fileDown);
        return File(fileBytes, "application/octet-stream", fileDown);
    }

    // Delete file
    [HttpDelete("delete/{fileName}")]
    public IActionResult DeleteFile(string path)
    {
        return Ok(_fileService.DeleteFile(path));
    }
}
