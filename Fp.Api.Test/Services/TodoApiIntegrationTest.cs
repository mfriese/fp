using Fp.Api.Models.Db;
using Fp.Api.Models.DTO;
using Fp.Api.Test.Factories;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace Fp.Api.Test.Services;

public class TodoApiIntegrationTest(TestWebApplicationFactory factory) :
    IClassFixture<TestWebApplicationFactory>
{
    private WebApplicationFactory<Program> Factory { get; } = factory;

    [Theory]
    [InlineData("/todo")]
    public async Task GetTodos_WhenCalled_ReturnsExpectedTodos(string endpoint)
    {
        var client = Factory.CreateClient();

        var response = await client.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var todoItems = await GetResponse<TodoModel[]>(response);

        Assert.NotNull(todoItems);
        Assert.NotEmpty(todoItems);
        // Might not contain Id = 1, db is used testwide, 1 is deleted at some point!!
        Assert.Contains(todoItems, t => t.Id == 2);
        Assert.Contains(todoItems, t => t.Id == 3);

        Assert.All(todoItems, item =>
        {
            Assert.False(string.IsNullOrEmpty(item.Header));
            Assert.False(string.IsNullOrEmpty(item.Description));
            Assert.InRange(item.Id, 1, int.MaxValue);
        });
    }

    [Theory]
    [InlineData("/todo/2")]
    [InlineData("/todo/3")]
    public async Task GetTodo_WhenCalled_ReturnsSingleTodo(string endpoint)
    {
        var client = Factory.CreateClient();

        var response = await client.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        var todoItem = await GetResponse<TodoModel>(response);

        Assert.Contains(todoItem?.Id, new int?[] { 2, 3 });
    }

    [Theory]
    [InlineData("/todo/2")]
    public async Task UpdateTodo_WhenCalled_ChangesTodo(string endpoint)
    {
        var client = Factory.CreateClient();

        var updateDto = new UpdateTodoRequest
        {
            Title = "Updated title",
            Description = "Updated description",
            IsCompleted = true
        };

        var json = JsonSerializer.Serialize(updateDto, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PatchAsync(endpoint, content);

        response.EnsureSuccessStatusCode();
    }

    [Theory]
    [InlineData("/todo/1")]
    public async Task Delete_WhenCalled_ReturnsOk(string endpoint)
    {
        var client = Factory.CreateClient();

        var response = await client.DeleteAsync(endpoint);

        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();

        Assert.Equal(0L, stream.Length);
    }

    /// <summary>
    /// Convenience function
    /// </summary>
    /// <typeparam name="T">Type to read from content.</typeparam>
    /// <param name="response">http respnse object.</param>
    /// <returns>The object if any, else null.</returns>
    private static async Task<T?> GetResponse<T>(HttpResponseMessage response)
        where T : class
    {
        using var contentStream = await response.Content.ReadAsStreamAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await JsonSerializer.DeserializeAsync<T>(
            contentStream, options);
    }
}