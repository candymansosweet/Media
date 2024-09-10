using Microsoft.AspNetCore.Http;

namespace Common.Services.MediaService
{
    public interface IMediaService
    {
        Task<string> UploadImage(IFormFile image);
        Task<List<string>> UploadImages(List<IFormFile> images);
    }
}
