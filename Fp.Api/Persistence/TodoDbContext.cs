using Fp.Api.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace Fp.Api.Persistence;

public class TodoDbContext(DbContextOptions<TodoDbContext> options) : DbContext(options), IUnitOfWork
{
    public void SaveAll()
        => SaveChanges();

    public DbSet<TodoModel> Todos { get; set; }
}
