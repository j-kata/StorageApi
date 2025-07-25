﻿using Microsoft.EntityFrameworkCore;
using StorageApi.Data;
using StorageApi.Models.Entities;
using StorageApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StorageApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StorageApiContext")
    ?? throw new InvalidOperationException("Connection string 'StorageApiContext' not found.")));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
