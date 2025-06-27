using Fp.Api.Endpoints.TodoHandlers;

namespace Fp.Api.Endpoints;

public static class IEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.
            MapGet("/todos", GetTodoHandler.Handle).
            Produces<IEnumerable<GetTodoHandler.Response>>(StatusCodes.Status200OK);

        builder.
            MapPost("/todos", CreateTodoHandler.HandleAsync).
            Produces<CreateTodoHandler.Response>(StatusCodes.Status201Created);

        builder.
            MapDelete("/todos/{id}", DeleteTodoHandler.HandleAsync).
            Produces(StatusCodes.Status200OK).
            Produces<string>(StatusCodes.Status404NotFound);

        builder.
            MapPatch("/todos", UpdateTodoHandler.HandleAsync).
            Produces(StatusCodes.Status200OK);

        return builder;
    }
}
