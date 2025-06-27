using Fp.Api.Services;

namespace Fp.Api.Endpoints.TodoHandlers;

public class GetTodoHandler
{
    public static IResult Handle(
        ITodoService service)
    {
        var all = service.GetAll();

        var response = all.Select(x => new Response(
            x.Id,
            x.Header,
            x.Description,
            x.IsCompleted));

        return Results.Ok(response);
    }

    public record Response(
        int Id,
        string? Header,
        string? Description,
        bool IsCompleted);
}
