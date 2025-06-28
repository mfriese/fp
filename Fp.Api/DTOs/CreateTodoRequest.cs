namespace Fp.Api.DTOs;

public class CreateTodoRequest
{
    public required string Header { get; set; }
    public required string Description { get; set; }
}
