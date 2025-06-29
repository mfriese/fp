using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class GetAllTodoHandler
{
    public static IResult Handle(ITodoService service)
    {
        var all = service.GetAll();

        return Results.Ok(all);
    }
}
