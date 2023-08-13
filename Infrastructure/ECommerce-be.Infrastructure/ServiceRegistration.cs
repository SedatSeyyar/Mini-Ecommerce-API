﻿using ECommerce_be.Application.Services;
using ECommerce_be.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce_be.Infrastructure
{

    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}