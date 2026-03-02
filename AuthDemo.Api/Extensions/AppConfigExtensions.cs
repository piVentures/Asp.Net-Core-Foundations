namespace AuthDemo.Api.Extensions
{
    public static class AppConfigExtensions
    {
        public static WebApplication ConfigureCors(this WebApplication app, IConfiguration config)
        {
            app.UseCors();
            return app;
        }
    }
}