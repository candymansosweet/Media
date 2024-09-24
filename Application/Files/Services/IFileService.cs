using Application.Files.Dto;
using Common.Models;
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
        //Task<PaginatedList<MediaFileDto>> QueryMediaFile(MediaFileQuery fileDto);
        Task<string> UploadFile(FileUpload fileDto);
        //byte[] DownloadFile(FileDownload fileDto);
        //byte[] DownloadFile(string fileDto);
        //Task<string> DeleteFile(int id);
    }
}
