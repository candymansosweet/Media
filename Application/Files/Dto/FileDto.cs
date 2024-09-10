using Application.Common.Mapping;

namespace Application.Files.Dto
{
    public class FileDto : IMapFrom<Domain.Entities.File>
    {
        public Guid OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
    }
}
