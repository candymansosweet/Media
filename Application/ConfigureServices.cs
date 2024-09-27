using Application.Common.Repositories;
using Application.Factories;
using Application.FileStorageService.FileStorageServices;
using Application.PathService.Interfaces;
using Application.Services.FolderServices;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            // Đăng ký MediatR với Assembly hiện tại (Application)
            services.AddAutoMapper(assembly);
            services.AddScoped<IPathService, PathService>();
            services.AddScoped<IFileStorageFactory, FileStorageFactory>();
            services.AddScoped<IFileRecordRepository, FileRecordRepository>();
            services.AddScoped<LocalStorageService>();
            services.AddScoped<GoogleDriveStorageService>();
            services.AddScoped<FirebaseStorageService>();
            return services;
        }
    }
}
