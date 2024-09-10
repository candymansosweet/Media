using Application.Common.Mapping;
using Application.Files.Dto;
using MediatR;

namespace Application.Files.Commands
{
    public class UpdateFileCommand : IRequest<FileDto>, IMapTo<Domain.Entities.File>
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
    }
}
