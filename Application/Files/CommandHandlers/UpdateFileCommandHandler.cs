using Application.Files.Commands;
using Application.Files.Dto;
using AutoMapper;
using Common.Exceptions;
using Infrastructure;
using MediatR;

namespace Application.Files.CommandHandlers
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, FileDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UpdateFileCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FileDto> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var file = await _context.Files.FindAsync(request.Id);
            if (file == null)
            {
                throw new AppException(ExceptionCode.Notfound, "Không tìm thấy file cần sửa");
            }
            file = _mapper.Map<Domain.Entities.File>(request);
            _context.Files.Update(file);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<FileDto>(file);
        }
    }
}
