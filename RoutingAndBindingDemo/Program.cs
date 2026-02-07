var builder = WebApplication.CreateBuilder(args);

/*
 * Register routing services and custom route constraints
 * NOTE: Must be done BEFORE building the app
 */
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("pos", typeof(PositionConstraint));
});

/*
 * Build the application
 * After this point, services are READ-ONLY
 */
var app = builder.Build();

/*
 * Enable routing middleware
 * Matches incoming requests to defined endpoints
 */
app.UseRouting();

/*
 * Define endpoints explicitly
 * (Older style, great for learning routing concepts)
 */
app.UseEndpoints(endpoints =>
{
    // Home page
    endpoints.MapGet("/", async context =>

    {   context.Response.ContentType="text/html";
        await context.Response.WriteAsync("welcome to home page");
    });

    // GET /employees
    endpoints.MapGet("/employees", async context =>
    {
        await context.Response.WriteAsync("get an employee");
    });

    // POST /employees
    endpoints.MapPost("/employees", async context =>
    {
        await context.Response.WriteAsync("create an employee");
    });

    // PUT /employees
    endpoints.MapPut("/employees", async context =>
    {
        await context.Response.WriteAsync("update an employee");
    });

    // DELETE /employees
    endpoints.MapDelete("/employees", async context =>
    {
        await context.Response.WriteAsync("delete an employee");
    });

    /*
     * GET /employees/positions/{position}
     * Uses custom :pos route constraint
     * Allowed values: manager, developer
     */
    endpoints.MapGet("/employees/positions/{position:pos}", async context =>
    {
        var position = context.Request.RouteValues["position"];
        await context.Response.WriteAsync($"Get employees under position: {position}");
    });

    /*
     * Route with default and optional parameters
     * {category=shirts} → default value
     * {size?}           → optional
     * {id?}             → optional
     */
    endpoints.MapGet("/{category=shirts}/{size?}/{id?}", async context =>
    {
        await context.Response.WriteAsync(
            $"The category: {context.Request.RouteValues["category"]}, " +
            $"size: {context.Request.RouteValues["size"]}, " +
            $"id: {context.Request.RouteValues["id"]}"
        );
    });
});

app.Run();

/*
 * Custom route constraint for validating employee positions
 */
class PositionConstraint : IRouteConstraint
{
    public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        // Validate route parameter existence
        if (!values.ContainsKey(routeKey) || values[routeKey] is null)
            return false;

        var position = values[routeKey]!.ToString();

        // Allow only predefined values
        return position.Equals("manager", StringComparison.OrdinalIgnoreCase) ||
               position.Equals("developer", StringComparison.OrdinalIgnoreCase);
    }
}
