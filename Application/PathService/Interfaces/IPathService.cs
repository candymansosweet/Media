using Application.PathService.Requests;

namespace Application.PathService.Interfaces
{
    public interface IPathService
    {
        public Task<PathDto> CreatePath(PathDto createFolderDto);
    }
}
