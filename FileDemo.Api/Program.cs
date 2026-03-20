using Azure.Storage.Blobs;
using FileDemo.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// get connection string
var connectionString = builder.Configuration.GetConnectionString("BlobStorage");

// Register BlobServiceClient 
builder.Services.AddSingleton(new BlobServiceClient(connectionString));

// register service
builder.Services.AddScoped<IBlobService, BlobService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
