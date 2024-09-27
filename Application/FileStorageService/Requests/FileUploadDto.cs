using Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.FileStorageService.Requests
{
    public class FileUploadDto
    {
        [FromForm]
        public IFormFile file { get; set; }

        [FromQuery]
        public EnumValue.FileServiceType storageType { get; set; }

        [FromQuery]
        public Guid ownerId { get; set; }

        [FromQuery]
        public string moduleName { get; set; }
    }
}
