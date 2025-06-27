using Fp.Api.Models;
using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class UpdateTodoHandler
{
    public static async Task<IResult> HandleAsync(
        ITodoService service,
        Request request)
    {
        if (request is null)
        {
            return Results.BadRequest("item cannot be null.");
        }

        // TODO Model with Id might not exist? -> Problem

        await service.UpdateAsync(new TodoModel()
        {
            Id = request.Id,
            IsCompleted = request.IsCompleted,
            Header = request.Header,
            Description = request.Description
        });

        return Results.Ok();
    }

    public record Request(
        int Id,
        bool IsCompleted,
        string Header,
        string Description);
}