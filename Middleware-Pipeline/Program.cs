using WebApp.MiddlewareComponents;

var builder = WebApplication.CreateBuilder(args);

// // Registers CustomMiddleware for dependency injection
builder.Services.AddTransient<CustomMiddleware>();

var app = builder.Build(); 

#region Middleware - 1
// First middleware in the pipeline
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1: Before calling next \r\n");

    await next(context);

    await context.Response.WriteAsync("Middleware #1: After calling next \r\n");  
});
#endregion

#region Conditional Middleware with UseWhen
// Branch the pipeline when request path starts with /employees and query contains "id"
// This only runs for requests that satisfy the condition
app.UseWhen(context =>
    context.Request.Path.StartsWithSegments("/employees") && context.Request.Query.ContainsKey("id"),
    (appBuilder) =>
{
    // Middleware #5 inside the conditional branch
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #5: Before calling next \r\n");
        await next(context);
        await context.Response.WriteAsync("Middleware #5: After calling next \r\n");
    });

    // Middleware #6 inside the conditional branch
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #6: Before calling next \r\n");
        await next(context);
        await context.Response.WriteAsync("Middleware #6: After calling next \r\n");
    });
});
#endregion

// Runs custom middleware in the pipeline
app.UseMiddleware<CustomMiddleware>();

#region Middleware - 2
// Second middleware in the main pipeline
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #2: Before calling next \r\n");
    await next(context);
    await context.Response.WriteAsync("Middleware #2: After calling next \r\n");
});
#endregion

#region Middleware - 3
// Third middleware in the main pipeline
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3: Before calling next \r\n");
    await next(context);
    await context.Response.WriteAsync("Middleware #3: After calling next \r\n");
});
#endregion

#region Terminal Middleware
// Terminal middleware that ends the pipeline
app.Run();
#endregion
