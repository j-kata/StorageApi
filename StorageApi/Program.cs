using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StorageApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StorageApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StorageApiContext")
    ?? throw new InvalidOperationException("Connection string 'StorageApiContext' not found.")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Storage API V1");
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
