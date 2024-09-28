using Application.FileStorageService.FileStorageServices;
using Application.FileStorageService.Interfaces;
using Common.Constants;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Factories
{
    public interface IFileStorageFactory
    {
        IFileStorageService GetStorageService(EnumValue.FileServiceType storageType);
    }

    public class FileStorageFactory : IFileStorageFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public FileStorageFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileStorageService GetStorageService(EnumValue.FileServiceType storageType)
        {
            return storageType switch
            {
                EnumValue.FileServiceType.Local => _serviceProvider.GetRequiredService<LocalStorageService>(),
                EnumValue.FileServiceType.GoogleDrive => _serviceProvider.GetRequiredService<GoogleDriveStorageService>(),
                EnumValue.FileServiceType.Firebase => _serviceProvider.GetRequiredService<FirebaseStorageService>(),
                _ => throw new NotSupportedException($"Storage type {storageType} is not supported.")
            };
        }
    }
}
