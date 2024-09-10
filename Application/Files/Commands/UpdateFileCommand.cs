using MediatR;

namespace Application.Files.Commands
{
    public class UpdateFileCommand : IRequest
    {
        public int Id { get; set; }
        public Guid OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
    }
}
