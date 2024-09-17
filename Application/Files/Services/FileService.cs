using Application.Files.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Exceptions;
using Common.Models;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Unitilities.DateTimeUnitilities;

namespace Application.Files.Services
{
    public class FileService : IFileService
    {
        private readonly IMapper _mapper;
        private readonly string _uploadPath;
        private readonly AppDbContext _context;
        public FileService(IOptions<FileSettings> options, AppDbContext context, IMapper mapper)
        {
            _uploadPath = options.Value.UploadPath;
            _context = context;
            _mapper = mapper;
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
            // Lưu thông tin file vào SQL Server
            var mediaFile = new MediaFile
            {
                FileName = fileDto.file.FileName,
                FilePath = relativePath,
                FileType = fileDto.file.ContentType,
                FileSize = fileDto.file.Length,
                OwnerId = fileDto.OwnerId
            };
            _context.MediaFiles.Add(mediaFile);
            await _context.SaveChangesAsync();
            return relativePath;
        }
        public async Task<string> DeleteFile(int id)
        {
            var aaa = _context.MediaFiles;
            var mediaFile = await _context.MediaFiles.FirstOrDefaultAsync(m => m.Id == id); ;
            if (mediaFile == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Không tìm thấy media");
            }
            var path = mediaFile.FilePath.Replace("/", "\\");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (!System.IO.File.Exists(filePath))
            {
                throw new AppException(ExceptionCode.Notfound, "Đường dẫn không đúng");
            }
            System.IO.File.Delete(filePath);
            _context.MediaFiles.Remove(mediaFile);
            await _context.SaveChangesAsync();
            return filePath;
        }

        public async Task<PaginatedList<MediaFileDto>> QueryMediaFile(MediaFileQuery query)
        {
            // Bắt đầu từ truy vấn cơ bản
            var mediaFilesQuery = _context.MediaFiles.AsQueryable();

            // Lọc theo tên file nếu có
            if (!string.IsNullOrEmpty(query.FileName))
            {
                mediaFilesQuery = mediaFilesQuery.Where(f => f.FileName.Contains(query.FileName));
            }

            // Lọc theo loại file nếu có
            if (!string.IsNullOrEmpty(query.FileType))
            {
                mediaFilesQuery = mediaFilesQuery.Where(f => f.FileType == query.FileType);
            }

            // Lọc theo kích thước file nếu có
            if (query.FileSize > 0)
            {
                mediaFilesQuery = mediaFilesQuery.Where(f => f.FileSize == query.FileSize);
            }

            // Lọc theo OwnerId nếu có
            if (query.OwnerId.HasValue)
            {
                mediaFilesQuery = mediaFilesQuery.Where(f => f.OwnerId == query.OwnerId);
            }

            // Thực hiện phân trang bằng PaginatedList
            var paginatedCategories = await PaginatedList<MediaFileDto>.CreateAsync(
                mediaFilesQuery.ProjectTo<MediaFileDto>(_mapper.ConfigurationProvider),
                query.PageNumber,
                query.PageSize
            );
            return paginatedCategories;
        }
    }
}
