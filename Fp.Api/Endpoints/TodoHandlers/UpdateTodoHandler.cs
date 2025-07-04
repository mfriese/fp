using Fp.Api.Models.DTO;
using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class UpdateTodoHandler
{
    public static IResult Handle(
        int id,
        ITodoService service,
        UpdateTodoRequest request,
        ILogger<UpdateTodoHandler> logger)
    {
        logger.LogDebug("Updating todo with Id '{Id}'", id);

        var success = service.Update(id, request);

        if (!success)
        {
            logger.LogWarning("Failed to update todo with Id '{Id}'", id);

            return Results.NotFound($"Item with Id '{id}' could not be updated.");
        }

        logger.LogInformation("Todo with Id '{Id}' updated successfully", id);

        return Results.Ok();
    }
}