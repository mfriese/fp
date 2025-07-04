using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class DeleteTodoHandler
{
    public static IResult Handle(
        int id,
        ITodoService service,
        ILogger<DeleteTodoHandler> logger)
    {
        logger.LogDebug("Deleting todo item with id: {@Id}", id);

        if (service.Delete(id))
        {
            logger.LogInformation("Todo item with id: {@Id} deleted successfully", id);

            return Results.Ok();
        }

        logger.LogWarning("Todo item with id: {@Id} not found", id);

        return Results.NotFound($"No item with Id '{id}' was found.");
    }
}
