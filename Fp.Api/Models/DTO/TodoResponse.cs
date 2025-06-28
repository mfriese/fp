namespace Fp.Api.Models.DTO;

public class TodoResponse
{
    public int Id { get; set; }
    public string Header { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
