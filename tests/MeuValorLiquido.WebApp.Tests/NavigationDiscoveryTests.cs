namespace MeuValorLiquido.WebApp.Tests;

/// <summary>Links de descoberta para hubs temáticos e conferir holerite (regressão de navegação).</summary>
public sealed class NavigationDiscoveryTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public NavigationDiscoveryTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Home_Should_Link_Thematic_Hubs_And_Conferir_Holerite()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("data-testid=\"thematic-hub-desligamento\"");
        html.Should().Contain("href=\"/desligamento\"");
        html.Should().Contain("data-testid=\"thematic-hub-negociar-salario\"");
        html.Should().Contain("href=\"/negociar-salario\"");
        html.Should().Contain("data-testid=\"thematic-hub-virar-pj\"");
        html.Should().Contain("href=\"/virar-pj\"");
        html.Should().Contain("data-testid=\"conferir-holerite-card\"");
        html.Should().Contain("href=\"/conferir-holerite\"");
    }

    [Fact]
    public async Task Layout_Should_Expose_Nav_Links_For_Guias_And_Holerite()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("data-testid=\"nav-guias\"");
        html.Should().Contain("data-testid=\"nav-conferir-holerite\"");
        html.Should().Contain("data-testid=\"nav-mobile-guias\"");
        html.Should().Contain("data-testid=\"nav-mobile-conferir-holerite\"");
        html.Should().Contain("data-testid=\"footer-conferir-holerite\"");
        html.Should().Contain("data-testid=\"footer-hub-desligamento\"");
    }

    [Fact]
    public async Task Calculadoras_Hub_Should_Link_Jornadas_And_Holerite()
    {
        var html = await client.GetStringAsync("/calculadoras");

        html.Should().Contain("Jornadas por momento");
        html.Should().Contain("href=\"/desligamento\"");
        html.Should().Contain("data-testid=\"conferir-holerite-card\"");
    }

    [Theory]
    [InlineData("rescisao-clt", "thematic-hub-promo-desligamento", "/desligamento")]
    [InlineData("fgts", "thematic-hub-promo-desligamento", "/desligamento")]
    [InlineData("proposta-salarial", "thematic-hub-promo-negociar-salario", "/negociar-salario")]
    [InlineData("pj-vs-clt", "thematic-hub-promo-virar-pj", "/virar-pj")]
    public async Task Calculator_Detail_Should_Promote_Related_Thematic_Hub(
        string slug,
        string promoTestId,
        string hubPath)
    {
        var html = await client.GetStringAsync($"/calculadoras/{slug}");

        html.Should().Contain($"data-testid=\"{promoTestId}\"");
        html.Should().Contain($"href=\"{hubPath}\"");
    }

    [Theory]
    [InlineData("salario-liquido")]
    [InlineData("proposta-salarial")]
    [InlineData("inss")]
    [InlineData("irrf")]
    public async Task Calculator_Detail_Should_Promote_Conferir_Holerite(string slug)
    {
        var html = await client.GetStringAsync($"/calculadoras/{slug}");

        html.Should().Contain("data-testid=\"conferir-holerite-promo\"");
        html.Should().Contain("href=\"/conferir-holerite\"");
    }

    [Theory]
    [InlineData("rescisao-clt")]
    [InlineData("financiamento")]
    public async Task Calculator_Detail_Without_Holerite_Should_Not_Show_Holerite_Promo(string slug)
    {
        var html = await client.GetStringAsync($"/calculadoras/{slug}");

        html.Should().NotContain("data-testid=\"conferir-holerite-promo\"");
    }
}
