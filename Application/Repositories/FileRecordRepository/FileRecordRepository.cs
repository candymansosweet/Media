using Application.Models.FileDtos;
using Common.Models;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Application.Repositories.FileRecordRepository
{
    public class FileRecordRepository : IFileRecordRepository
    {
        private readonly AppDbContext _context;

        public FileRecordRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(FileRecord fileRecord)
        {
            _context.FileRecords.Add(fileRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<FileRecord> GetByIdAsync(Guid id)
        {
            return await _context.FileRecords.FindAsync(id);
        }

        public async Task UpdateAsync(FileRecord fileRecord)
        {
            var existingRecord = await GetByIdAsync(fileRecord.Id);
            if (existingRecord != null)
            {
                existingRecord.FileName = fileRecord.FileName;
                existingRecord.IdOnService = fileRecord.IdOnService;
                existingRecord.FileType = fileRecord.FileType;
                existingRecord.FileSize = fileRecord.FileSize;
                existingRecord.UpdatedBy = fileRecord.UpdatedBy;
                existingRecord.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var fileRecord = await GetByIdAsync(id);
            if (fileRecord != null)
            {
                _context.FileRecords.Remove(fileRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedList<FileRecord>> FilterAsync(FileRecordQuery fileRecordQuery)
        {
            var query = _context.FileRecords.AsQueryable();

            if (fileRecordQuery.OwnerId.HasValue)
            {
                query = query.Where(f => f.OwnerId == fileRecordQuery.OwnerId.Value);
            }

            if (!string.IsNullOrEmpty(fileRecordQuery.FileName))
            {
                query = query.Where(f => f.FileName.Contains(fileRecordQuery.FileName));
            }

            if (!string.IsNullOrEmpty(fileRecordQuery.FileType))
            {
                query = query.Where(f => f.FileType.Contains(fileRecordQuery.FileType));
            }

            if (!string.IsNullOrEmpty(fileRecordQuery.FilePath))
            {
                query = query.Where(f => f.IdOnService.Contains(fileRecordQuery.FilePath));
            }
            return await PaginatedList<FileRecord>.CreateAsync(
                query,
                fileRecordQuery.PageNumber,
                fileRecordQuery.PageSize
            );
        }
    }

}
