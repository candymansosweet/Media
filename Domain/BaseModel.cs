namespace Domain
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public void Delete()
        {
            IsDeleted = true;
            UpdatedDate = DateTime.Now;
        }
    }
}
