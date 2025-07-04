using Fp.Api.Models.DTO;

namespace Fp.Api.Services;

public interface ITodoService
{
    public TodoResponse Create(CreateTodoRequest request);
    public bool Update(int id, UpdateTodoRequest request);
    public IEnumerable<TodoResponse> GetAll();
    public TodoResponse? GetById(int id);
    public bool Delete(int id);

}
