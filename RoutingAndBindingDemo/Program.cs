var builder = WebApplication.CreateBuilder(args);

// Register custom route constraint
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("pos", typeof(PositionConstraint));
});

var app = builder.Build();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    // Home page with test links
    endpoints.MapGet("/", async context =>
    {   
        context.Response.ContentType = "text/html";
        await context.Response.WriteAsync("Welcome to home page<br/>Test custom constraint:<br/>");
        await context.Response.WriteAsync("/employees/positions/manager (should work)<br/>");
        await context.Response.WriteAsync("/employees/positions/developer (should work)<br/>");
        await context.Response.WriteAsync("/employees/positions/designer (should fail - 404)");
    });

    // CRUD endpoints for employees
    endpoints.MapGet("/employees", async context => await context.Response.WriteAsync("get all employees"));
    endpoints.MapPost("/employees", async context => await context.Response.WriteAsync("create an employee"));
    endpoints.MapPut("/employees", async context => await context.Response.WriteAsync("update an employee"));
    endpoints.MapDelete("/employees", async context => await context.Response.WriteAsync("delete an employee"));

    // Constrained route - Only allows manager/developer
    endpoints.MapGet("/employees/positions/{position:pos}", async context =>
    {
        var position = context.Request.RouteValues["position"];
        await context.Response.WriteAsync($"Get employees under position: {position}");
    });

    // Catch invalid positions (404) - Must come AFTER constrained route
    endpoints.MapGet("/employees/positions/{position}", async context =>
    {
        context.Response.StatusCode = 404;
        var position = context.Request.RouteValues["position"];
        await context.Response.WriteAsync($"Invalid position: '{position}'. Allowed: 'manager' or 'developer'");
    });

    // Route with default/optional parameters
    endpoints.MapGet("/{category=shirts}/{size?}/{id?}", async context =>
    {
        await context.Response.WriteAsync(
            $"Category: {context.Request.RouteValues["category"]}, " +
            $"Size: {context.Request.RouteValues["size"]}, " +
            $"ID: {context.Request.RouteValues["id"]}"
        );
    });

    // Global 404 catch-all
    endpoints.MapGet("/{*path}", async context =>
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync($"Route not found");
    });
});

app.Run();

// Custom constraint class - validates position parameter
class PositionConstraint : IRouteConstraint
{
    private static readonly HashSet<string> _validPositions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "manager",
        "developer"
    };

    public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        // Check if parameter exists
        if (!values.ContainsKey(routeKey) || values[routeKey] is null)
            return false;

        var position = values[routeKey]!.ToString();
        
        // Validate against allowed values
        return _validPositions.Contains(position);
    }
}