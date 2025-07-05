using Microsoft.Extensions.DependencyInjection;
using MiniETicaret.Application.Interfaces.Repositories;
using MiniETicaret.Infrastructure.Persistence.Repositories;
using MiniETicaret.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniETicaret.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MiniETicaret.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("memoryDb")); // In-Memory DB'yi burada tanımlıyoruz.

            // Bir IProductRepository istendiğinde, ona ProductRepository'nin bir örneğini ver.
            services.AddScoped<IProductRepository, ProductRepository>();

            // services.AddScoped<IProductRepository, ProductRepository>(); satırının altına ekleyin.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisURL") ?? configuration["CacheSettings:RedisURL"];
            });
        }
    }
}
