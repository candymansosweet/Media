using MediatR;

namespace Application.Files.Commands
{
    public class DeleteFileCommand : IRequest
    {
        public int Id { get; set; }
    }
}
