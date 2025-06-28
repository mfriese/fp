using Fp.Api.Endpoints;
using Fp.Api.Models.Db;
using Fp.Api.Persistence;
using Fp.Api.Seed;
using Fp.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TodoDbContext>(options
    => options.UseInMemoryDatabase("todoDb"));

builder.Services.AddScoped<IUnitOfWork>(provider
    => provider.GetRequiredService<TodoDbContext>());

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IRepository<TodoModel>, Repository<TodoModel>>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    SeedData.Initialize(scope.ServiceProvider.GetRequiredService<TodoDbContext>());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapTodoEndpoints();

app.Run();

public partial class Program { }
