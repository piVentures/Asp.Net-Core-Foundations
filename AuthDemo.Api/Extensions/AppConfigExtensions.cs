namespace AuthDemo.Api.Extensions
{
    // Application configuration extensions
    public static class AppConfigExtensions
    {
        // Enable CORS middleware in request pipeline
        public static WebApplication ConfigureCors(
            this WebApplication app,
            IConfiguration config)
        {
            app.UseCors(); // allows cross-origin requests

            return app;
        }
    }
}
