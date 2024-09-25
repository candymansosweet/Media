using Application.Factories;
using Application.Files.Services;
using Application.Interfaces;
using Application.Models.FileDtos;
using Application.Repositories.FileRecordRepository;
using Application.Services.FolderServices;
using Common.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;
public class 
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
    public async Task<IActionResult> UploadFile(
        [FromForm] IFormFile file, 
        [FromQuery] EnumValue.FileServiceType storageType, 
        [FromQuery] Guid ownerId, 
        [FromQuery] string moduleName
    )
    {
        Application.Models.PathDtos.PathDto path = await _pathService.CreatePath(new Application.Models.PathDtos.PathDto()
        {
            OwnerId = ownerId,
            ModuleName = moduleName
        });
        var storageService = _storageFactory.GetStorageService(storageType);
        using (var stream = file.OpenReadStream())
        {

            var fileUrl = await storageService.UploadFileAsync(new Application.Models.FileDtos.FileUploadDto()
            {
                fileName = file.FileName,
                fileStream = stream,
                path = path.Url
            });

            var fileRecord = new FileRecord()
            {
                FileName = file.FileName,
                IdOnService = fileUrl,
                FileType = file.ContentType,
                FileSize = file.Length,
                ServiceType = (int) storageType,
                OwnerId = ownerId,
            };
            await _fileRecordRepository.AddAsync(fileRecord);
            return Ok(fileRecord);
        }
    }

    // Download file
    [HttpGet("download-by-infor")]
    public async Task<IActionResult> DownloadFile(
        [FromQuery] string idOnServer,
        [FromQuery] EnumValue.FileServiceType storageType
        )
    {
        var storageService = _storageFactory.GetStorageService(storageType);
        return File(await storageService.DownloadFileAsync(idOnServer), "application/octet-stream");
    }
    //[HttpGet("download-by-path")]
    //public IActionResult DownloadFile(string fileDown)
    //{
    //    byte[] fileBytes = _fileService.DownloadFile(fileDown);
    //    return File(fileBytes, "application/octet-stream", fileDown);
    //}

    //// Delete file
    //[HttpDelete("delete")]
    //public async Task<IActionResult> DeleteFile(int id)
    //{
    //    return Ok(await _fileService.DeleteFile(id));
    //}
}
