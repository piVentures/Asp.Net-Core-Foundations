using AuthDemo.Api.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerExplorer()
    .InjectDbContext(builder.Configuration)
    .AddIdntityHandlerAndStore()
    .ConfigureIdentityOptions()
    .AddIdentityAuth(builder.Configuration);

//

var app = builder.Build();

app.ConfigureSwaggerExplorer()
    .ConfigureCors(builder.Configuration)
    .AddIdentityAuthMiddlewares();


app.MapControllers();

app.Run();

