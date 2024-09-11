using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Files.Dto
{
    public class FileUpload
    {
        public string ModuleName { get; set; }
        public IFormFile file { get; set; }
    }
}
