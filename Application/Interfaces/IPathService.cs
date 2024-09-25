using Application.Models.PathDtos;

namespace Application.Interfaces
{
    public interface IPathService
    {
        public Task<PathDto> CreatePath(PathDto createFolderDto);
    }
}
