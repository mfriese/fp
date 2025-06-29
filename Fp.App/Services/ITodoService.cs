using Fp.App.Models;

namespace Fp.App.Services;

public interface ITodoService
{
    IObservable<IEnumerable<TodoModel>> GetTodos();
    IObservable<TodoModel> GetTodo(int id);
    IObservable<TodoModel> CreateTodo(CreateTodoModel request);
    IObservable<bool> UpdateTodo(int id, UpdateTodoModel request);
    IObservable<bool> DeleteTodo(int id);
}
