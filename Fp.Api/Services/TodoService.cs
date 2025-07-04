using Fp.Api.Models;
using Fp.Api.Models.Db;
using Fp.Api.Models.DTO;
using Fp.Api.Persistence;

namespace Fp.Api.Services;

public class TodoService(
    IRepository<TodoModel> repository,
    ILogger<TodoService> logger,
    IUnitOfWork unitOfWork) : ITodoService
{
    private IRepository<TodoModel> Repository { get; } = repository;
    private IUnitOfWork UnitOfWork { get; } = unitOfWork;
    private ILogger<TodoService> Logger { get; } = logger;

    public TodoResponse Create(CreateTodoRequest request)
    {
        Logger.LogDebug("Creating a todo item with header: {Header}", request.Header);

        var model = request.ToModel();

        Repository.Add(model);

        UnitOfWork.SaveAll();

        Logger.LogInformation("New todo item created with ID: {Id}", model.Id);

        return model.ToResponse();
    }

    public bool Delete(int id)
    {
        Logger.LogDebug("Deleting a todo item with id: {@Id}", id);

        var success = Repository.Remove(id);
        UnitOfWork.SaveAll();

        Logger.LogInformation("Todo item with ID: {Id} deleted: {success}", id, success);

        return success;
    }

    public bool Update(int id, UpdateTodoRequest request)
    {
        Logger.LogDebug("Updating a todo item with id: {@Id}", id);

        var dbModel = Repository.GetById(id);

        if (dbModel is null)
            return false;

        Logger.LogDebug("Found todo item with id: {@Id}, updating...", id);

        dbModel.Patch(request);

        UnitOfWork.SaveAll();

        Logger.LogInformation("Todo item with ID: {Id} updated successfully", id);

        return true;
    }

    public IEnumerable<TodoResponse> GetAll()
    {
        Logger.LogInformation("Retrieving all todo items");

        return Repository.Get().Select(mm => mm.ToResponse()).AsEnumerable();
    }

    public TodoResponse? GetById(int id)
    {
        Logger.LogInformation("Retrieving todo item with id: {@Id}", id);

        return Repository.GetById(id)?.ToResponse();
    }
}
