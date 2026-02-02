using WebApp.MiddlewareComponents;

var builder = WebApplication.CreateBuilder(args);

// // Registers CustomMiddleware for dependency injection
builder.Services.AddTransient<CustomMiddleware>();
builder.Services.AddTransient<CustomExceptionHandler>();

var app = builder.Build(); 

app.UseMiddleware<CustomExceptionHandler>();

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
    
    // throw new ApplicationException("Exception just for testing the CustomException Handler");

    await context.Response.WriteAsync("Middleware #3: Before calling next \r\n");
    await next(context);
    await context.Response.WriteAsync("Middleware #3: After calling next \r\n");
});
#endregion


#region HttpContext
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    // --- Request Information ---
    var method = context.Request.Method;                   // GET, POST, etc.
    var scheme = context.Request.Scheme;                   // http / https
    var host = context.Request.Host;                       // localhost:5000
    var path = context.Request.Path;                       // /employees
    var queryString = context.Request.QueryString;         // ?id=42
    var queryId = context.Request.Query["id"];             // 42
    var protocol = context.Request.Protocol;               // HTTP/1.1
    var userAgent = context.Request.Headers["User-Agent"]; // client info
    var contentType = context.Request.ContentType;         // e.g., application/json

    await context.Response.WriteAsync(
        $"--- Request Info ---\r\n" +
        $"Method: {method}\r\n" +
        $"Scheme: {scheme}\r\n" +
        $"Host: {host}\r\n" +
        $"Path: {path}\r\n" +
        $"Query: {queryString}\r\n" +
        $"Protocol: {protocol}\r\n" +
        $"User-Agent: {userAgent}\r\n" +
        $"Content-Type: {contentType}\r\n\r\n"
    );

    // --- Call Next Middleware ---
    await next(context);

    // --- Response Information (after next) ---
    var statusCode = context.Response.StatusCode;          // e.g., 200
    var responseContentType = context.Response.ContentType; // e.g., text/plain
    var responseHeaders = context.Response.Headers;       // all headers

    await context.Response.WriteAsync(
        $"--- Response Info ---\r\n" +
        $"Status Code: {statusCode}\r\n" +
        $"Content-Type: {responseContentType}\r\n" +
        $"Headers: {string.Join(", ", responseHeaders.Select(h => $"{h.Key}={h.Value}"))}\r\n" +
        $"Processed by middleware after next\r\n"
    );
});
#endregion


#region Terminal middleware
app.Run(async context =>
{
    await context.Response.WriteAsync("This is terminal middleware (app.Run)\r\n");
});
#endregion

app.Run(); 

