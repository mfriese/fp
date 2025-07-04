using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class GetTodoHandler
{
    public static IResult Handle(
        int id,
        ITodoService service,
        ILogger<GetTodoHandler> logger)
    {
        logger.LogDebug("Get todo item with id: {Id}", id);

        var item = service.GetById(id);

        if (item is null)
        {
            logger.LogWarning("Item with Id '{Id}' not found.", id);

            return Results.NotFound($"Item with Id '{id}' could not be found.");
        }

        logger.LogInformation("Item with Id '{Id}' found.", id);

        return Results.Ok(item);
    }
}
