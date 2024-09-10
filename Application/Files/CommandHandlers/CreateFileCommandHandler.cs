using Application.Files.Commands;
using Application.Files.Dto;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Application.Files.CommandHandlers
{
    public class CreateFileCommandHandler : IRequestHandler<CreateFileCommand, FileDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CreateFileCommandHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FileDto> Handle(CreateFileCommand request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<Domain.Entities.File>(request);
            _context.Files.Add(file);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<FileDto>(file);
        }
    }
}
