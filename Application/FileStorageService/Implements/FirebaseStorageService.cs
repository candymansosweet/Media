using Application.FileStorageService.Interfaces;
using Application.FileStorageService.Requests;

namespace Application.FileStorageService.FileStorageServices
{
    public class FirebaseStorageService : IFileStorageService
    {
        public Task DeleteFileAsync(string fileUrl)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadFileAsync(string fileUrl)
        {
            throw new NotImplementedException();
        }

        public Task<string> UploadFileAsync(FileUploadDto fileUploadDto)
        {
            throw new NotImplementedException();
        }
    }
}
