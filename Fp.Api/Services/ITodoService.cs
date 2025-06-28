using Fp.Api.Models.DTO;

namespace Fp.Api.Services;

public interface ITodoService
{
    public Task<TodoResponse> CreateAsync(CreateTodoRequest request);
    public Task<bool> UpdateAsync(int id, UpdateTodoRequest request);
    public IEnumerable<TodoResponse> GetAll();
    public TodoResponse? GetById(int id);
    public Task<bool> DeleteAsync(int id);

}
