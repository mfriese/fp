using Fp.Api.Models;
using Fp.Api.Models.Db;
using Fp.Api.Models.DTO;
using Fp.Api.Persistence;

namespace Fp.Api.Services;

public class TodoService(
    IRepository<TodoModel> repository,
    IUnitOfWork unitOfWork) : ITodoService
{
    private IRepository<TodoModel> Repository { get; } = repository;
    private IUnitOfWork UnitOfWork { get; } = unitOfWork;

    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request)
    {
        var model = request.ToModel();

        Repository.Add(model);

        await UnitOfWork.SaveChangesAsync();

        return model.ToResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var success = Repository.Remove(id);
        await UnitOfWork.SaveChangesAsync();
        return success;
    }

    public async Task<bool> UpdateAsync(int id, UpdateTodoRequest request)
    {
        var dbModel = Repository.GetById(id);

        if (dbModel is null)
            return false;

        dbModel.Patch(request);

        await UnitOfWork.SaveChangesAsync();

        return true;
    }

    public IEnumerable<TodoResponse> GetAll()
        => Repository.Get().Select(mm => mm.ToResponse()).AsEnumerable();

    public TodoResponse? GetById(int id)
        => Repository.GetById(id)?.ToResponse();
}
