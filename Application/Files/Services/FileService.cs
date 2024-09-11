using Application.Files.Dto;
using Common.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadPath;
        public FileService(IOptions<FileSettings> options) 
        {
            _uploadPath = options.Value.UploadPath;
        }
        public byte[]? DownloadFile(FileDownload fileDto)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _uploadPath, fileDto.ModuleName, fileDto.FileName);
            if (!System.IO.File.Exists(filePath))
            {
                throw new AppException(ExceptionCode.Notfound, "Đường dẫn không đúng");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }

        public async Task<string> UploadImage(FileUpload fileDto)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _uploadPath, fileDto.ModuleName ,fileDto.file.FileName);
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(_uploadPath))
            {
                Directory.CreateDirectory(_uploadPath);
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileDto.file.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
