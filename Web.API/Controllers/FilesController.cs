using Application.Common.Repositories;
using Application.Common.Repositories.FileRecordRepository;
using Application.Factories;
using Application.FileStorageService.Requests;
using Application.Models.FileDtos;
using Application.Models.PathDtos;
using Application.PathService.Interfaces;
using Common.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IFileStorageFactory _storageFactory;
    private readonly IFileRecordRepository _fileRecordRepository;
    private readonly IPathService _pathService;

    public FileController(IFileStorageFactory storageFactory, IFileRecordRepository fileRecordRepository, IPathService pathService)
    {
        _storageFactory = storageFactory;
        _fileRecordRepository = fileRecordRepository;
        _pathService = pathService;
    }

    [HttpGet("query")]
    public async Task<IActionResult> QueryFiles([FromQuery] FileRecordQuery fileRecordQuery)
    {
        return Ok(await _fileRecordRepository.FilterAsync(fileRecordQuery));
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileUpload)
    {

        var storageService = _storageFactory.GetStorageService(fileUpload.storageType);
        using (var stream = fileUpload.file.OpenReadStream())
        {
            var fileUrl = await storageService.UploadFileAsync(fileUpload);
            return Ok(fileUrl);
        }
    }

    // Download file
    [HttpGet("download")]
    public async Task<IActionResult> DownloadFile(
        [FromQuery] string idOnServer,
        [FromQuery] EnumValue.FileServiceType storageType
        )
    {
        var storageService = _storageFactory.GetStorageService(storageType);
        return File(await storageService.DownloadFileAsync(idOnServer), "application/octet-stream");
    }
}
