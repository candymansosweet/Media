using Application.Files.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}
