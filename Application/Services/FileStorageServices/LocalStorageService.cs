using Application.Interfaces;
using Application.Models.FileDtos;

namespace Application.Services.FileStorageServices
{
    public class LocalStorageService : IFileStorageService
    {
        public async Task<string> UploadFileAsync(FileUploadDto fileUploadDto)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileUploadDto.path.Replace($"/", "\\"), fileUploadDto.fileName);
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileUploadDto.fileStream.CopyToAsync(file);
            }
            return filePath; // Trả về đường dẫn local
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
