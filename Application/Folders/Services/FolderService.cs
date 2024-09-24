using Application.Folders.RequestDtos;
using Application.Folders.ResponseDtos;
using AutoMapper;
using Infrastructure;
using Microsoft.Extensions.Options;
using Unitilities.DateTimeUnitilities;

namespace Application.Folders.Services
{
    public class FolderService : IFolderService
    {
        private readonly string _uploadPath;
        public FolderService(IOptions<FileSettings> options)
        {
            _uploadPath = options.Value.UploadPath;
        }
        public async Task<FolderRespone> CreateFolder(CreateFolderDto createFolderDto)
        {
            var directoryPath = CreateFolderPath(createFolderDto);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // Chỉ tạo thư mục
            }
            return new FolderRespone
            {
                OwnerId = createFolderDto.OwnerId,
                ModuleName = createFolderDto.ModuleName,
                Path = directoryPath
            };
        }
        public string CreateFolderPath(CreateFolderDto createFolderDto)
        {
            string[] dateTimeUpload = FormatToString.FormatToPathDeep("dd/MM/yyyy");
            if (createFolderDto.RootPath == null)
            {
                return Path.Combine(Directory.GetCurrentDirectory(), _uploadPath, createFolderDto.OwnerId.ToString(), createFolderDto.ModuleName, dateTimeUpload[2], dateTimeUpload[1], dateTimeUpload[0]);
            }
            else
            {
                return Path.Combine(Directory.GetCurrentDirectory(), createFolderDto.RootPath, createFolderDto.OwnerId.ToString(), createFolderDto.ModuleName, dateTimeUpload[2], dateTimeUpload[1], dateTimeUpload[0]);
            }
        }

        public Task<FolderRespone> CreateFolderGoogleDrive(CreateFolderDto createFolderDto)
        {
            throw new NotImplementedException();
        }
    }
}
