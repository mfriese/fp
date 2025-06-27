namespace Fp.Api.Models;

public class TodoModel
{
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
    public string? Header { get; set; }
    public string? Description { get; set; }
}
