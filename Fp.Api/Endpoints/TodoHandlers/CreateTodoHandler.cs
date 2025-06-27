using Fp.Api.Models;
using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class CreateTodoHandler
{
    public static async Task<IResult> HandleAsync(
        ITodoService service,
        Request request)
    {
        if (request is null)
        {
            return Results.BadRequest("item cannot be null.");
        }

        var newModel = new TodoModel()
        {
            Header = request.Header,
            Description = request.Description
        };

        await service.CreateAsync(newModel);

        return Results.Created($"/todos/{newModel.Id}", newModel);
    }

    public record Request(
        string Header,
        string Description);

    public record Response(
        int Id);
}
