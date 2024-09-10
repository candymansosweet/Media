using Common.Services.MediaService;

namespace Prodcut.API
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            services.AddScoped<IMediaService, MediaService>();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddRouting(options => options.LowercaseUrls = true);
            return services;
        }
    }
}
