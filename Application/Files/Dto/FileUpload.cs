using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Files.Dto
{
    public class FileUpload
    {
        [FromQuery]
        public string ModuleName { get; set; }
        [FromQuery]
        public Guid OwnerId { get; set; }
        [FromForm]
        public IFormFile file { get; set; }
    }
}
