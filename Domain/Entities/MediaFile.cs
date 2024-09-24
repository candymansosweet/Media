using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class MediaFile: BaseModel
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public Guid OwnerId { get; set; }

    }
}
