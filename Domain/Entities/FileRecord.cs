using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class FileRecord: BaseModel
    {
        public string FileName { get; set; }
        public string IdOnService { get; set; } // xác định vị trí trên môi trường lưu trữ
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int ServiceType { get; set; }
        public Guid OwnerId { get; set; }

    }
}
