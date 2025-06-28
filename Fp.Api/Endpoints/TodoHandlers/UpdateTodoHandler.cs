using Fp.Api.DTOs;
using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class UpdateTodoHandler
{
    public static async Task<IResult> HandleAsync(
        ITodoService service,
        UpdateTodoRequest request,
        int id)
    {
        if (request is null)
        {
            return Results.BadRequest("item cannot be null.");
        }

        var success = await service.UpdateAsync(id, request);

        if (!success)
        {
            return Results.NotFound($"Item with Id '{id}' could not be updated.");
        }

        return Results.Ok();
    }
}