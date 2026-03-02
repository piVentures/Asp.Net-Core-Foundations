using AuthDemo.Api.Models;

namespace AuthDemo.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerExplorer(this IServiceCollection services)
        {
           services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

             public static WebApplication ConfigureSwaggerExplorer(this WebApplication app)
        {
      // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
}
            return app;
        }
    }
}