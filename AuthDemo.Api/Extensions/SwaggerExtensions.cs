using AuthDemo.Api.Models;

namespace AuthDemo.Api.Extensions
{
    // Swagger configuration extensions
    public static class SwaggerExtensions
    {
        // Register Swagger services
        public static IServiceCollection AddSwaggerExplorer(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer(); // enables endpoint discovery
            services.AddSwaggerGen();           // generates OpenAPI/Swagger docs
            return services;
        }

        // Enable Swagger middleware
        public static WebApplication ConfigureSwaggerExplorer(this WebApplication app)
        {
            // Run Swagger only in development environment
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();    // creates swagger.json
                app.UseSwaggerUI();  // enables Swagger UI page
            }

            return app;
        }
    }
}
