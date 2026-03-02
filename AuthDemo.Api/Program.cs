using Microsoft.AspNetCore.Identity;
using AuthDemo.Api.Models;
using AuthDemo.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

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

