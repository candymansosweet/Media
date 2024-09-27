using Application.PathService.Interfaces;
using Application.PathService.Requests;
using AutoMapper;
using Infrastructure;
using Microsoft.Extensions.Options;
using Unitilities.DateTimeUnitilities;

namespace Application.PathService.Implements
{
    public class PathService : IPathService
    {
        private readonly string _uploadPath;
        public PathService(IOptions<FileSettings> options)
        {
            _uploadPath = options.Value.UploadPath;
        }
        public async Task<PathDto> CreatePath(PathDto createFolderDto)
        {
            string[] dateTimeUpload = FormatToString.FormatToPathDeep("dd/MM/yyyy");
            return new PathDto()
            {
                ModuleName = createFolderDto.ModuleName,
                OwnerId = createFolderDto.OwnerId,
                Url = Path.Combine(_uploadPath, createFolderDto.OwnerId.ToString(), createFolderDto.ModuleName, dateTimeUpload[2], dateTimeUpload[1], dateTimeUpload[0])
            };
        }
    }
}
