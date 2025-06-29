using Fp.App.Models;
using Refit;

namespace Fp.App.Api;

public interface ITodoApi
{
    [Get("/todo")]
    Task<ApiResponse<IEnumerable<TodoModel>>> GetTodosAsync();

    [Get("/todo/{id}")]
    Task<ApiResponse<TodoModel>> GetTodoAsync(
        int id);

    [Post("/todo")]
    Task<ApiResponse<TodoModel>> CreateTodoAsync(
        [Body] CreateTodoModel request);

    [Delete("/todo/{id}")]
    Task<ApiResponse<HttpContent>> DeleteTodoAsync(
        int id);

    [Patch("/todo/{id}")]
    Task<ApiResponse<HttpContent>> UpdateTodoAsync(
        int id,
        [Body] UpdateTodoModel request);
}
