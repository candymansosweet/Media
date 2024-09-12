using Application.Files.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Services
{
    public interface IFileService
    {
        Task<string> UploadImage(FileUpload fileDto);
        byte[] DownloadFile(FileDownload fileDto);

        Task<string> DeleteFile(string filePath);
    }
}
