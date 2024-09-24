using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Folders.ResponseDtos
{
    public class FolderRespone
    {
        // tạo folder cho ai
        public Guid OwnerId { get; set; }
        // Module nào
        public string ModuleName { get; set; }
        public string Path { get; set; } // đường dẫn vào thư mục vừa được tạo
    }
}
