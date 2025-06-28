using Fp.Api.Models.Db;
using Fp.Api.Persistence;

namespace Fp.Api.Seed;

public static class SeedData
{
    public static void Initialize(TodoDbContext context)
    {
        if (context.Set<TodoModel>().Any())
        {
            return; // DB has been seeded
        }

        context.Set<TodoModel>().AddRange(
            new TodoModel
            {
                Header = "Todo 1",
                Description = "Description 1",
                IsCompleted = false
            },
            new TodoModel
            {
                Header = "Todo 2",
                Description = "Description 2",
                IsCompleted = true
            },
            new TodoModel
            {
                Header = "Todo 3",
                Description = "Description 3",
                IsCompleted = false
            }
        );

        context.SaveChanges();
    }
}
