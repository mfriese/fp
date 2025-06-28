using Fp.Api.Models.DTO;
using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class CreateTodoHandler
{
    public static async Task<IResult> HandleAsync(
        ITodoService service,
        CreateTodoRequest request)
    {
        if (request is null)
        {
            return Results.BadRequest("item cannot be null.");
        }

        var result = await service.CreateAsync(request);

        return Results.Created($"/todos/{result.Id}", result);
    }
}
