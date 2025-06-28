using Microsoft.AspNetCore.Mvc.Testing;

namespace Fp.Api.Test.Services;

public class TodoServiceIntegrationTest(WebApplicationFactory<Program> factory) :
    IClassFixture<WebApplicationFactory<Program>>
{
    private WebApplicationFactory<Program> Factory { get; } = factory;

    [Theory]
    [InlineData("/todo")]
    public async Task GetTodos_ReturnsOk(string endpoint)
    {
        var client = Factory.CreateClient();

        var response = await client.GetAsync(endpoint);

        response.EnsureSuccessStatusCode();

        Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
    }

    [Theory]
    [InlineData("/todo/1")]
    [InlineData("/todo/2")]
    [InlineData("/todo/3")]
    public async Task DeleteTodos_ReturnsOk(string endpoint)
    {
        var client = Factory.CreateClient();

        var response = await client.DeleteAsync(endpoint);

        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();

        Assert.Equal(0L, stream.Length);
    }
}