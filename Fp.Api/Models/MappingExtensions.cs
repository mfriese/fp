using Fp.Api.Models.Db;
using Fp.Api.Models.DTO;

namespace Fp.Api.Models;

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

        if (dto.Header is not null)
            model.Header = dto.Header;

        if (dto.Description is not null)
            model.Description = dto.Description;
    }
}
