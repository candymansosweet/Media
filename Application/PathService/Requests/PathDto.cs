namespace Application.PathService.Requests
{
    public class PathDto
    {
        // tạo folder cho ai
        public Guid OwnerId { get; set; }
        // Module nào
        public string ModuleName { get; set; }
        public string? Url { get; set; }
    }
}
