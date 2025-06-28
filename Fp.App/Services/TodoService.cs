using Fp.App.Api;
using Fp.App.Models;
using System.Reactive.Linq;

namespace Fp.App.Services;

public class TodoService(ITodoApi api) : ITodoService
{
    public ITodoApi Api { get; } = api;

    public IObservable<TodoModel> CreateTodo(CreateTodoModel request)
        => Observable.FromAsync(async () =>
        {
            var result = await Api.CreateTodoAsync(request);
            await result.EnsureSuccessfulAsync();

            // Checked above, should never be null
            return result.Content!;
        });

    public IObservable<bool> DeleteTodo(int id)
        => Observable.FromAsync(async () =>
        {
            var result = await Api.DeleteTodoAsync(id);
            await result.EnsureSuccessfulAsync();
            return true;
        });

    public IObservable<IEnumerable<TodoModel>> GetTodos()
        => Observable.FromAsync(async () =>
        {
            var result = await Api.GetTodosAsync();
            await result.EnsureSuccessfulAsync();

            // Checked above, should never be null
            return result.Content!;
        });

    public IObservable<bool> UpdateTodo(int id, UpdateTodoModel request)
        => Observable.FromAsync(async () =>
        {
            var result = await Api.UpdateTodoAsync(id, request);
            await result.EnsureSuccessfulAsync();
            return true;
        });
}
