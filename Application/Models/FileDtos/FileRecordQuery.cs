using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.FileDtos
{
    public class FileRecordQuery : BasePaginatedQuery
    {
        public string? FileName { get; set; }
        public string? FilePath { get; set; }
        public string? FileType { get; set; }
        public long? FileSize { get; set; }
        public int ServiceType { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
