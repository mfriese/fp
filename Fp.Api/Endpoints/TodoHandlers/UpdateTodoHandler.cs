using Fp.Api.Models;
using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class UpdateTodoHandler
{
    public static async Task<IResult> HandleAsync(
        ITodoService service,
        Request request,
        int id)
    {
        if (request is null)
        {
            return Results.BadRequest("item cannot be null.");
        }

        await service.UpdateAsync(id, new TodoModel()
        {
            IsCompleted = request.IsCompleted,
            Header = request.Header,
            Description = request.Description
        });

        return Results.Ok();
    }

    public record Request(
        bool IsCompleted,
        string Header,
        string Description);
}