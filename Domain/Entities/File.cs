namespace Domain.Entities
{
    public class File: BaseModel
    {
        public Guid OwnerId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileUrl { get; set; }
    }
}
