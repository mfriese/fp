using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class GetAllTodoHandler
{
    public static IResult Handle(
        ITodoService service,
        ILogger<GetAllTodoHandler> logger)
    {
        logger.LogDebug("Getting all items");

        var all = service.GetAll();

        logger.LogInformation("Found {Count} items", all.Count());

        return Results.Ok(all);
    }
}
