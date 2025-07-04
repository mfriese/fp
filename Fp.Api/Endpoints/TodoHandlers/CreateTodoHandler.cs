using Fp.Api.Models.DTO;
using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class CreateTodoHandler
{
    public static IResult Handle(
        ITodoService service,
        CreateTodoRequest request,
        ILogger<CreateTodoHandler> logger)
    {
        logger.LogDebug("Creating a new todo item with: {@Request}", request);

        if (request is null)
        {
            logger.LogWarning("Request is null.");

            return Results.BadRequest("item cannot be null.");
        }

        logger.LogDebug("Request is valid, proceeding to create the todo item.");

        var result = service.Create(request);

        logger.LogInformation("Created todo item with Id: {Id} and Title: {Header}",
            result.Id, result.Header);

        return Results.Created($"/todos/{result.Id}", result);
    }
}
