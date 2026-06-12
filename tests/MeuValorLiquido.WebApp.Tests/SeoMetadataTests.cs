using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MeuValorLiquido.WebApp.Tests;

public class SeoMetadataTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public SeoMetadataTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Calculator_Page_Should_Include_Faq_JsonLd()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        html.Should().Contain("FAQPage");
    }

    [Fact]
    public async Task Blog_Post_Should_Include_Article_JsonLd()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("Article");
        html.Should().Contain("headline");
    }

    [Fact]
    public async Task Home_Should_Include_OpenGraph_Tags()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("property=\"og:title\"");
        html.Should().Contain("property=\"og:description\"");
        html.Should().Contain("rel=\"canonical\"");
    }
}
