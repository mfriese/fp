namespace Fp.App.Models;

public class TodoModel
{
    public int Id { get; set; }
    public string Header { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}
