using Fp.Api.Endpoints;
using Fp.Api.Models.Db;
using Fp.Api.Persistence;
using Fp.Api.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<TodoDbContext>(options
    => options.UseInMemoryDatabase("todoDb"));

builder.Services.AddTransient<IUnitOfWork>(provider
    => provider.GetRequiredService<TodoDbContext>());

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IRepository<TodoModel>, Repository<TodoModel>>();

builder.Host.UseSerilog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // Seed some data for testing ...
    // SeedData.Initialize(scope.ServiceProvider.GetRequiredService<TodoDbContext>());
}

// Provide api decumentation when in development.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapTodoEndpoints();

app.Run();

// Need class handle from testing project
public partial class Program { }
