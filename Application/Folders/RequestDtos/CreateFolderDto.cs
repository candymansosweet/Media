﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Folders.RequestDtos
{
    public class PathDto
    {
        // tạo folder cho ai
        public Guid OwnerId { get; set; }
        // Module nào
        public string ModuleName { get; set; }
    }
}
