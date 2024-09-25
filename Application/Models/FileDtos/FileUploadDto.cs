using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.FileDtos
{
    public class FileUploadDto
    {
        public string fileName { get; set; }
        public string? path { get; set; }
        public Stream fileStream { get; set; }
    }
}
