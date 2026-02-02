using System.Net;
using System.Net.Cache;

namespace WebApp.MiddlewareComponents
{
    public class CustomExceptionHandler: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // context.Response.ContentType = "text/html";
                await next(context);
            }
            catch (Exception ex)
            {
                
                await context.Response.WriteAsync("<h2>Error</h2>");
                 await context.Response.WriteAsync($"<h2>{ex.Message}</h2>");
            }
        }
    }
}
