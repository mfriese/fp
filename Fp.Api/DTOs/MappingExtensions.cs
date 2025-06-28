using Fp.Api.Models;

namespace Fp.Api.DTOs;

public static class MappingExtensions
{
    public static TodoResponse ToResponse(this TodoModel model) => new()
    {
        Id = model.Id,
        Header = model.Header,
        Description = model.Description,
        IsCompleted = model.IsCompleted
    };

    public static TodoModel ToModel(this CreateTodoRequest dto) => new()
    {
        Header = dto.Header,
        Description = dto.Description,
        IsCompleted = false // Default value for IsCompleted
    };

    public static void Patch(this TodoModel model, UpdateTodoRequest dto)
    {
        if (dto.IsCompleted is not null)
            model.IsCompleted = dto.IsCompleted.Value;

        if (dto.Title is not null)
            model.Header = dto.Title;

        if (dto.Description is not null)
            model.Description = dto.Description;
    }
}
