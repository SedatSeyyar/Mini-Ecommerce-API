using ECommerce_be.Application.Abstractions.Storage;
using ECommerce_be.Infrastructure.Services;
using ECommerce_be.Infrastructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce_be.Infrastructure
{

    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
