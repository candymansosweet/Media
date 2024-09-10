using MediatR;

namespace Application.Files.Commands
{
    public class CreateFileCommand : IRequest<Guid>
    {
        public Guid OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
    }
}
