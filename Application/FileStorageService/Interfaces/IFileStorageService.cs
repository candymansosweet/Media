using Application.FileStorageService.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileStorageService.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(FileUploadDto fileUploadDto);
        Task DeleteFileAsync(string fileUrlOrId);
        Task<Stream> DownloadFileAsync(string fileUrlOrId);
    }
}
