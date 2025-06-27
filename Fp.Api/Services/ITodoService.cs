using Fp.Api.Models;

namespace Fp.Api.Services;

public interface ITodoService
{
    public Task CreateAsync(TodoModel model);
    public Task UpdateAsync(TodoModel model);
    public IEnumerable<TodoModel> GetAll();
    public TodoModel? GetById(int id);
    public Task<bool> DeleteAsync(int id);

}
