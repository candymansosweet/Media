using Application.Files.Services;
using Application.Folders.Services;
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
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IFolderService, FolderService>();
            return services;
        }
    }
}
