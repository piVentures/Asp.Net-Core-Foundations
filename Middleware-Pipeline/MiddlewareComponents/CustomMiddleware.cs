namespace WebApp.MiddlewareComponents
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync(
                "Kira's Custom Middleware: Before calling next\r\n"
            );

            await next(context);

            await context.Response.WriteAsync(
                "Kira's Custom Middleware: After calling next\r\n"
            );
        }
    }
}
