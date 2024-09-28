using Application.Common.Repositories;
using Application.Factories;
using Application.FileStorageService.Interfaces;
using Application.FileStorageService.Requests;
using Application.PathService.Interfaces;
using Application.PathService.Requests;
using Domain.Entities;

namespace Application.FileStorageService.FileStorageServices
{
    public class LocalStorageService : IFileStorageService
    {
        private readonly IFileRecordRepository _fileRecordRepository;
        private readonly IPathService _pathService;

        public LocalStorageService(IFileRecordRepository fileRecordRepository, IPathService pathService)
        {
            _fileRecordRepository = fileRecordRepository;
            _pathService = pathService;
        }

        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto)
        {
            // lấy đường dẫn
            PathDto path = await _pathService.CreatePath(new PathDto()
            {
                OwnerId = fileUploadDto.ownerId,
                ModuleName = fileUploadDto.moduleName
            });
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), path.Url.Replace($"/", "\\"));
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // Chỉ tạo thư mục
            }
            var filePath = Path.Combine(folderPath, fileUploadDto.file.Name);
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileUploadDto.file.CopyToAsync(file);
            }
            var fileRecord = new FileRecord()
            {
                FileName = fileUploadDto.file.FileName,
                IdOnService = filePath,
                FileType = fileUploadDto.file.ContentType,
                FileSize = fileUploadDto.file.Length,
                ServiceType = (int)fileUploadDto.storageType,
                OwnerId = fileUploadDto.ownerId,
            };
            await _fileRecordRepository.AddAsync(fileRecord);
            return Path.Combine(filePath.Replace($"/", "\\")); // Trả về đường dẫn local
        }

        public Task DeleteFileAsync(string fileUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl.Replace($"/", "\\"));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Task.CompletedTask;
        }

        public Task<Stream> DownloadFileAsync(string fileUrl)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUrl.Replace($"/", "\\"));
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return Task.FromResult((Stream)stream);
        }

    }

}
