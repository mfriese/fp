using Fp.Api.Models;

namespace Fp.Api.Test.Samples;
public class TodoModelSamples
{
    public static readonly TodoModel[] Todos =
    [
        new() {
            Header = "Todo 1",
            Description = "Description 1",
            IsCompleted = false
        },
        new() {
            Header = "Todo 2",
            Description = "Description 2",
            IsCompleted = true
        },
        new() {
            Header = "Todo 3",
            Description = "Description 3",
            IsCompleted = false
        }
    ];
}
