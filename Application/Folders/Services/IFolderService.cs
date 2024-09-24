using Application.Folders.RequestDtos;
using Application.Folders.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Folders.Services
{
    public interface IFolderService
    {
        public Task<FolderRespone> CreateFolder(CreateFolderDto createFolderDto);
        public Task<FolderRespone> CreateFolderGoogleDrive(CreateFolderDto createFolderDto);
    }
}
