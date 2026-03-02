using Microsoft.EntityFrameworkCore;
using AuthDemo.Api.Models;

namespace AuthDemo.Api.Extensions
{
    public static class EFCoreExtensions
    {
        public static IServiceCollection InjectDbContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        config.GetConnectionString("DefaultConnection")));
        return services;
        }
    }
}