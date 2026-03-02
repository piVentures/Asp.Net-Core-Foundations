using Microsoft.EntityFrameworkCore;
using AuthDemo.Api.Models;

namespace AuthDemo.Api.Extensions
{
    // EF Core configuration extensions
    public static class EFCoreExtensions
    {
        // Register DbContext with dependency injection
        public static IServiceCollection InjectDbContext(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(
                    config.GetConnectionString("DefaultConnection"))); // use SQLite connection

            return services;
        }
    }
}
