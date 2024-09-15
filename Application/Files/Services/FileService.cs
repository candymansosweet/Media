using Application.Files.Dto;
using Common.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unitilities.DateTimeUnitilities;

namespace Application.Files.Services
{
    public class FileService : IFileService
    {
        private readonly string _uploadPath;
        public FileService(IOptions<FileSettings> options) 
        {
            _uploadPath = options.Value.UploadPath;
        }
        public byte[] DownloadFile(FileDownload fileDto)
        {
            string dateTimeUpload = FormatToString.FormatToPath("dd/MM/yyyy");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _uploadPath, fileDto.ModuleName, fileDto.ObjectName, dateTimeUpload, fileDto.FileName);
            if (!System.IO.File.Exists(filePath))
            {
                throw new AppException(ExceptionCode.Notfound, "Đường dẫn không đúng");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }
        public byte[] DownloadFile(string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (!System.IO.File.Exists(filePath))
            {
                throw new AppException(ExceptionCode.Notfound, "Đường dẫn không đúng");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return fileBytes;
        }

        public async Task<string> UploadFile(FileUpload fileDto)
        {
            string dateTimeUpload = FormatToString.FormatToPath("dd/MM/yyyy");
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), _uploadPath, fileDto.ModuleName, fileDto.ObjectName, dateTimeUpload);
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // Chỉ tạo thư mục
            }
            // Tạo đường dẫn đến file (bao gồm cả tên file)
            var filePath = Path.Combine(directoryPath, fileDto.file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileDto.file.CopyToAsync(stream);
            }
            var relativePath = Path.Combine("Uploads", fileDto.ModuleName, fileDto.ObjectName, dateTimeUpload, fileDto.file.FileName).Replace("\\", "/");
            return relativePath;
        }
        public async Task<string> DeleteFile(string path)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _uploadPath, path);
            if (!System.IO.File.Exists(filePath))
            {
                throw new AppException(ExceptionCode.Notfound, "Đường dẫn không đúng");
            }
            System.IO.File.Delete(filePath);
            return filePath;
        }
    }
}
