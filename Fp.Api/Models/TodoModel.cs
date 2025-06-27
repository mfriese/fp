using System.ComponentModel.DataAnnotations;

namespace Fp.Api.Models;

public class TodoModel
{
    [Key]
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
    public string? Header { get; set; }
    public string? Description { get; set; }
}
