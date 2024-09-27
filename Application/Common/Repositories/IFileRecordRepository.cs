using Application.FileStorageService.Requests;
using Common.Models;
using Domain.Entities;

namespace Application.Common.Repositories
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
