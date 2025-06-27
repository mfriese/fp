using Fp.Api.Models;
using Fp.Api.Persistence;

namespace Fp.Api.Services;

public class TodoService : ITodoService
{
    private IRepository<TodoModel> Repository { get; }
    private IUnitOfWork UnitOfWork { get; }
    public TodoService(
        IRepository<TodoModel> repository,
        IUnitOfWork unitOfWork)
    {
        Repository = repository;
        UnitOfWork = unitOfWork;
    }

    public async Task CreateAsync(TodoModel model)
    {
        Repository.Add(model);
        await UnitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var success = Repository.Remove(id);
        await UnitOfWork.SaveChangesAsync();
        return success;
    }

    public async Task<bool> UpdateAsync(int id, TodoModel model)
    {
        // TODO do not check if the model exists and just update it aka create a new one?

        if (Repository.GetById(id) is not null)
        {
            model.Id = id;
            Repository.Update(model);
            await UnitOfWork.SaveChangesAsync();

            return true;
        }

        return false;
    }

    public IEnumerable<TodoModel> GetAll()
        => Repository.Get().AsEnumerable();

    public TodoModel? GetById(int id)
        => Repository.GetById(id);
}
