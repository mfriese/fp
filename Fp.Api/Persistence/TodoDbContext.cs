using Fp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Fp.Api.Persistence;

public class TodoDbContext : DbContext, IUnitOfWork
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<TodoModel> Todos { get; set; }

    public Task SaveChangesAsync()
        => Task.FromResult(SaveChanges());
}
