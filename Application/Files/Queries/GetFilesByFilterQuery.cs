using Application.Files.Dto;
using Common.Models;
using MediatR;

namespace Application.Files.Queries
{
    public class GetFilesByFilterQuery : IRequest<PaginatedList<FileDto>>
    {
        public Guid? OwnerId { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public string? FileUrl { get; set; }
        public int PageIndex { get; set; } = 1; // Mặc định là trang 1
        public int PageSize { get; set; } = 10; // Mặc định là 10 danh mục mỗi trang
    }
}
