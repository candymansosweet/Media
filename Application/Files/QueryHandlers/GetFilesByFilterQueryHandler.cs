using Application.Files.Dto;
using Application.Files.Queries;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Files.QueryHandlers
{
    public class GetFilesByFilterQueryHandler : IRequestHandler<GetFilesByFilterQuery, PaginatedList<FileDto>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GetFilesByFilterQueryHandler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<FileDto>> Handle(GetFilesByFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Files.AsQueryable();

            if (request.OwnerId.HasValue)
            {
                query = query.Where(f => f.OwnerId == request.OwnerId.Value);
            }

            if (!string.IsNullOrEmpty(request.FileName))
            {
                query = query.Where(f => f.FileName.Contains(request.FileName));
            }

            if (!string.IsNullOrEmpty(request.FileType))
            {
                query = query.Where(f => f.FileType.Contains(request.FileType));
            }

            if (!string.IsNullOrEmpty(request.FileUrl))
            {
                query = query.Where(f => f.FileUrl.Contains(request.FileUrl));
            }

            var paginatedFiles = await PaginatedList<FileDto>.CreateAsync(
                query.ProjectTo<FileDto>(_mapper.ConfigurationProvider),
                request.PageIndex,
                request.PageSize
            );
            return paginatedFiles;
        }
    }
}
