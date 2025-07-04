using Fp.Api.Persistence;
using Fp.Api.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fp.Api.Test.Factories;
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private string DbName { get; }

    public TestWebApplicationFactory()
    {
        DbName = "db_" + Guid.NewGuid().ToString("N");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveService(services, typeof(DbContextOptions<TodoDbContext>));
            RemoveService(services, typeof(IUnitOfWork));

            // Create a different database every time a context is added
            services.AddDbContext<TodoDbContext>(options =>
            {
                options.UseInMemoryDatabase(DbName);
            });

            services.AddScoped<IUnitOfWork>(provider =>
                provider.GetRequiredService<TodoDbContext>());

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<TodoDbContext>();
            db.Database.EnsureCreated();

            SeedData.Initialize(db);
        });
    }

    private static void RemoveService(IServiceCollection services, Type serviceType)
    {
        // Trash the previously registered DbContextOptions for TodoDbContext
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType == serviceType);
        if (descriptor != null)
            services.Remove(descriptor);
    }
}
