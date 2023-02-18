using ECommerce_be.Application.Abstractions;
using ECommerce_be.Application.Repositories;
using ECommerce_be.Persistence.Concretes;
using ECommerce_be.Persistence.Contexts;
using ECommerce_be.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce_be.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ECommerce_beDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // AddScoped --> AddDbContext'in Service Lifetime'ı ile aynı olduğu için seçilmiştir.
            // AddScoped --> Her request için ayrı bir nesne oluşturulup gönderilir. İş bittikten sonra imha (dispose) edilir.
            // AddSingleton --> Her request için aynı (tekil yalnızca 1 kere oluşturulur) nesne gönderilir.
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IProductService, ProductService>();
        }
    }
}
