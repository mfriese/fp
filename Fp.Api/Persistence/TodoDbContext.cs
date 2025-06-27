using Fp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fp.Api.Persistence;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options), IUnitOfWork
{
    public Task SaveChangesAsync()
        => Task.FromResult(SaveChanges());

    public DbSet<TodoModel> Todos { get; set; }
}
