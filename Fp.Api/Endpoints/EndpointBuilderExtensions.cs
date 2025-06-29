using Fp.Api.Endpoints.TodoHandlers;
using Fp.Api.Models.DTO;

namespace Fp.Api.Endpoints;

public static class EndpointBuilderExtensions
{
    public static IEndpointRouteBuilder MapTodoEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.
            MapGet("/todo", GetAllTodoHandler.Handle).
            Produces<IEnumerable<TodoResponse>>(StatusCodes.Status200OK);

        builder.
            MapGet("/todo/{id}", GetTodoHandler.Handle).
            Produces<IEnumerable<TodoResponse>>(StatusCodes.Status200OK).
            Produces<string>(StatusCodes.Status404NotFound);

        builder.
            MapPost("/todo", CreateTodoHandler.HandleAsync).
            Produces<TodoResponse>(StatusCodes.Status201Created);

        builder.
            MapDelete("/todo/{id}", DeleteTodoHandler.HandleAsync).
            Produces(StatusCodes.Status200OK).
            Produces<string>(StatusCodes.Status404NotFound);

        builder.
            MapPatch("/todo/{id}", UpdateTodoHandler.HandleAsync).
            Produces(StatusCodes.Status200OK).
            Produces<string>(StatusCodes.Status404NotFound);

        return builder;
    }
}
