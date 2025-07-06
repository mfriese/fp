namespace Fp.Api.Models.DTO;

public class UpdateTodoRequest
{
    public string? Header { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
}
