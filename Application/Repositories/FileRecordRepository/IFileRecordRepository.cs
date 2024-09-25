using Application.Models.FileDto;
using Common.Models;
using Domain.Entities;

namespace Application.Repositories.FileRecordRepository
{
    public interface IFileRecordRepository
    {
        Task AddAsync(FileRecord fileRecord);
        Task<FileRecord> GetByIdAsync(Guid id);
        Task UpdateAsync(FileRecord fileRecord);
        Task DeleteAsync(Guid id);
        Task<PaginatedList<FileRecord>> FilterAsync(FileRecordQuery fileRecordQuery);
    }
}
