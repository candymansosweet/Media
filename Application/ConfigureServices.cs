using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            // Đăng ký MediatR với Assembly hiện tại (Application)
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);
            return services;
        }
    }
}
