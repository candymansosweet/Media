using Application.Factories;
using Application.FileStorageService.Interfaces;
using Application.FileStorageService.Requests;
using Application.PathService.Interfaces;
using Application.PathService.Requests;
using Application.Repositories.FileRecordRepository;
using Domain.Entities;

namespace Application.FileStorageService.FileStorageServices
{
    public class LocalStorageService : IFileStorageService
    {
        private readonly IFileStorageFactory _storageFactory;
        private readonly IPathService _pathService;

        public LocalStorageService(IFileStorageFactory storageFactory, IPathService pathService)
        {
            _storageFactory = storageFactory;
            _pathService = pathService;
        }

        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto)
        {
            PathDto path = await _pathService.CreatePath(new PathDto()
            {
                OwnerId = fileUpload.ownerId,
                ModuleName = fileUpload.moduleName
            });
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDto.path.Replace($"/", "\\"));
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // Chỉ tạo thư mục
            }
            var filePath = Path.Combine(folderPath, fileUploadDto.fileName);
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileUploadDto.fileStream.CopyToAsync(file);
            }

            var fileRecord = new FileRecord()
            {
                FileName = fileUploadDto.fileName,
                IdOnService = fileUploadDto.path,
                FileType = fileUpload.file.ContentType,
                FileSize = fileUpload.file.Length,
                ServiceType = (int)fileUpload.storageType,
                OwnerId = fileUpload.ownerId,
            };
            await _fileRecordRepository.AddAsync(fileRecord);

            return Path.Combine(fileUploadDto.path.Replace($"/", "\\")); // Trả về đường dẫn local
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
