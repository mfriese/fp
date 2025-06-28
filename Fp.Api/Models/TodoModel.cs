using System.ComponentModel.DataAnnotations;

namespace Fp.Api.Models;

public class TodoModel
{
    [Key]
    public int Id { get; set; }
    public bool IsCompleted { get; set; }
    public required string Header { get; set; }
    public required string Description { get; set; }
}
