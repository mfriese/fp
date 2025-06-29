using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class GetTodoHandler
{
    public static IResult Handle(int id, ITodoService service)
    {
        var item = service.GetById(id);

        if (item is null)
        {
            return Results.NotFound($"Item with Id '{id}' could not be found.");
        }

        return Results.Ok(item);
    }
}
