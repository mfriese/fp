using Fp.Api.Models;
using Fp.Api.Persistence;
using Fp.Api.Services;
using Fp.Api.Test.Samples;
using Moq;

namespace Fp.Api.Test.Services;

public class TodoServiceUnitTest
{
    [Fact]
    public async Task Update_ModelDoesNotExist()
    {
        var sample = TodoModelSamples.Todos[0];

        var repo = new Mock<IRepository<TodoModel>>();
        repo.Setup(r => r.
            GetById(It.IsAny<int>())).
            Returns((TodoModel?)null);

        var uow = new Mock<IUnitOfWork>();

        var service = new TodoService(repo.Object, uow.Object);

        var result = await service.UpdateAsync(42, sample);

        Assert.False(result);
    }

    [Fact]
    public async Task Update_ModelDoesExist()
    {
        var sample = TodoModelSamples.Todos[0];

        var repo = new Mock<IRepository<TodoModel>>();
        repo.Setup(r => r.
            GetById(It.Is<int>(ii => ii == sample.Id))).
            Returns(sample);
        repo.Setup(r => r.
            Update(It.Is<TodoModel>(m => m.Id == sample.Id)));

        var uow = new Mock<IUnitOfWork>();

        var service = new TodoService(repo.Object, uow.Object);

        var result = await service.UpdateAsync(sample.Id, sample);

        Assert.True(result);
    }
}
