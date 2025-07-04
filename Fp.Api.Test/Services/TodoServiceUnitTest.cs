using Fp.Api.Models.Db;
using Fp.Api.Models.DTO;
using Fp.Api.Persistence;
using Fp.Api.Services;
using Fp.Api.Test.Samples;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace Fp.Api.Test.Services;

public class TodoServiceUnitTest
{
    [Fact]
    public void Update_WithNonExistingModel_Fails()
    {
        var sample = TodoModelSamples.Todos[0];

        var repoMock = new Mock<IRepository<TodoModel>>();
        repoMock.Setup(r => r.
            GetById(It.IsAny<int>())).
            Returns((TodoModel?)null);

        var uowMock = new Mock<IUnitOfWork>();
        var logMock = NullLogger<TodoService>.Instance;

        var service = new TodoService(repoMock.Object, logMock, uowMock.Object);

        var result = service.Update(42, new()
        {
            IsCompleted = true,
            Title = "my new title",
            Description = "my new description"
        });

        Assert.False(result);
        uowMock.Verify(u => u.SaveAll(), Times.Never);
    }

    [Fact]
    public void Update_WithExistingModel_UpdatesPropertiesAndSaves()
    {
        var sample = TodoModelSamples.Todos[0];

        var update = new UpdateTodoRequest()
        {
            IsCompleted = true,
            Title = "my new title",
            Description = "my new description"
        };

        var repoMock = new Mock<IRepository<TodoModel>>();
        repoMock.Setup(r => r.
            GetById(It.Is<int>(ii => ii == sample.Id))).
            Returns(sample);

        var uowMock = new Mock<IUnitOfWork>();
        var logMock = NullLogger<TodoService>.Instance;

        var service = new TodoService(repoMock.Object, logMock, uowMock.Object);

        var result = service.Update(sample.Id, update);

        Assert.True(result);
        Assert.Equal(update.Title, sample.Header);
        Assert.Equal(update.Description, sample.Description);
        Assert.Equal(update.IsCompleted, sample.IsCompleted);
        uowMock.Verify(u => u.SaveAll(), Times.Once);
    }

    // TODO more tests
}
