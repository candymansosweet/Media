using Application.Files.Dto;
using MediatR;

namespace Application.Files.Commands
{
    public class DeleteFileCommand : IRequest<FileDto>
    {
        public int Id { get; set; }
    }
}
