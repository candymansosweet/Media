using Application.Files.Dto;
using MediatR;

namespace Application.Files.Queries
{
    public class GetFilesByFilterQuery : IRequest<List<FileDto>>
    {
        public Guid? OwnerId { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? FileUrl { get; set; }
    }
}
