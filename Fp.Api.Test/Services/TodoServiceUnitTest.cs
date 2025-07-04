using Fp.Api.Models.Db;
using Fp.Api.Persistence;
using Fp.Api.Services;
using Fp.Api.Test.Samples;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Fp.Api.Test.Services;

public class TodoServiceUnitTest
{
    [Fact]
    public void Update_ModelDoesNotExist()
    {
        var sample = TodoModelSamples.Todos[0];

        var repo = new Mock<IRepository<TodoModel>>();
        repo.Setup(r => r.
            GetById(It.IsAny<int>())).
            Returns((TodoModel?)null);

        var uow = new Mock<IUnitOfWork>();
        var log = NullLogger<TodoService>.Instance;

        var service = new TodoService(repo.Object, log, uow.Object);

        var result = service.Update(42, new()
        {
            IsCompleted = true,
            Title = "my new title",
            Description = "my new description"
        });

        Assert.False(result);
        uow.Verify(u => u.SaveAll(), Times.Never);
    }

    [Fact]
    public void Update_ModelDoesExist()
    {
        var sample = TodoModelSamples.Todos[0];

        var repo = new Mock<IRepository<TodoModel>>();
        repo.Setup(r => r.
            GetById(It.Is<int>(ii => ii == sample.Id))).
            Returns(sample);

        var uow = new Mock<IUnitOfWork>();
        var log = NullLogger<TodoService>.Instance;

        var service = new TodoService(repo.Object, log, uow.Object);

        var result = service.Update(sample.Id, new()
        {
            IsCompleted = true,
            Title = "my new title",
            Description = "my new description"
        });

        Assert.True(result);
        uow.Verify(u => u.SaveAll(), Times.Once);
    }
}
