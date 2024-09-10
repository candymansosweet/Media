using Application.Common.Interfaces;
using Application.Files.Commands;
using AutoMapper;
using Common.Exceptions;
using Infrastructure;
using MediatR;

namespace Application.Files.CommandHandlers
{
    public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DeleteFileCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FindAsync(request.Id);

            if (file == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Không tìm thấy file cần xóa");
            }
            _context.Files.Remove(file);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
