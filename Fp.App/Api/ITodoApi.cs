using Fp.App.Models;
using Refit;

namespace Fp.App.Api;

public interface ITodoApi
{
    [Get("/todo")]
    Task<ApiResponse<IEnumerable<TodoModel>>> GetTodosAsync();

    [Post("/todo")]
    Task<ApiResponse<TodoModel>> CreateTodoAsync(
        [Body] CreateTodoModel request);

    [Delete("/todo/{id}")]
    Task<ApiResponse<object>> DeleteTodoAsync(
        int id);

    [Patch("/todo/{id}")]
    Task<ApiResponse<object>> UpdateTodoAsync(
        int id,
        [Body] UpdateTodoModel request);
}
