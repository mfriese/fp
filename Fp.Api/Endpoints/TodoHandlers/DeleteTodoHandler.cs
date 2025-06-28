using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class DeleteTodoHandler
{
    public static async Task<IResult> HandleAsync(ITodoService service, int id)
    {
        if (await service.DeleteAsync(id))
        {
            return Results.Ok();
        }

        return Results.NotFound($"No item with Id '{id}' was found.");
    }
}
