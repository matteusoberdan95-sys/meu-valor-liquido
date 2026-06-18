namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint65ThematicHubTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint65ThematicHubTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Theory]
    [InlineData("/desligamento", "Saiu da empresa", "rescisao-clt")]
    [InlineData("/negociar-salario", "Negociando sal", "proposta-salarial")]
    [InlineData("/virar-pj", "Pensando em virar PJ", "pj-vs-clt")]
    public async Task ThematicHub_Should_Render_Core_Content(
        string path,
        string heroTitleFragment,
        string primarySlug)
    {
        var html = await client.GetStringAsync(path);

        html.Should().Contain("valora-stitch-thematic-hub");
        html.Should().Contain(heroTitleFragment);
        html.Should().Contain("Calculadoras recomendadas");
        html.Should().Contain("Artigos relacionados");
        html.Should().Contain("Dúvidas frequentes");
        html.Should().Contain($"/calculadoras/{primarySlug}");
        html.Should().Contain("valora-journey-next");
        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Thematic_Hubs()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/desligamento");
        xml.Should().Contain("/negociar-salario");
        xml.Should().Contain("/virar-pj");
    }

    [Fact]
    public async Task MapaDoSite_Should_Link_Thematic_Hubs()
    {
        var html = await client.GetStringAsync("/mapa-do-site");

        html.Should().Contain("Jornadas temáticas");
        html.Should().Contain("/desligamento");
        html.Should().Contain("/negociar-salario");
        html.Should().Contain("/virar-pj");
    }
}
