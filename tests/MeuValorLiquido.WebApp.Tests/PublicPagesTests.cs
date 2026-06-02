using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MeuValorLiquido.WebApp.Tests;

public class PublicPagesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public PublicPagesTests(WebApplicationFactory<Program> factory)
    {
        client = factory.CreateClient();
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/calculadoras")]
    [InlineData("/calculadoras/salario-liquido")]
    [InlineData("/sobre")]
    [InlineData("/contato")]
    [InlineData("/politica-de-privacidade")]
    [InlineData("/termos-de-uso")]
    [InlineData("/aviso-legal")]
    [InlineData("/blog")]
    public async Task Public_Page_Should_Load(string url)
    {
        var response = await client.GetAsync(url);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
