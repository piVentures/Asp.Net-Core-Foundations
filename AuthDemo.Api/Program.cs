using AuthDemo.Api.Extensions;

// Create the WebApplication builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers(); // Register controllers for API endpoints

// Configure Swagger, Database, and Identity services
builder.Services.AddSwaggerExplorer()          // Add Swagger/OpenAPI support
    .InjectDbContext(builder.Configuration)    // Configure and inject the database context
    .AddIdntityHandlerAndStore()               // Add Identity handler and store for authentication
    .ConfigureIdentityOptions()                // Configure Identity options like password rules, lockout, etc.
    .AddIdentityAuth(builder.Configuration);  // Configure JWT or cookie-based authentication

// Build the WebApplication
var app = builder.Build();

// Configure middleware pipeline
app.ConfigureSwaggerExplorer()                 // Enable Swagger UI and endpoint documentation
    .ConfigureCors(builder.Configuration)      // Configure CORS policies
    .AddIdentityAuthMiddlewares();             // Add authentication & authorization middlewares

// Map controller routes
app.MapControllers(); // Map all controller endpoints

// Run the application
app.Run();
