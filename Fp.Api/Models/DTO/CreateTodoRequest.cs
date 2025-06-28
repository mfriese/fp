namespace Fp.Api.Models.DTO;

public class CreateTodoRequest
{
    public required string Header { get; set; }
    public required string Description { get; set; }
}
