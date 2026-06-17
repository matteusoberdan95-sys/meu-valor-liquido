namespace MeuValorLiquido.WebApp.Tests;

/// <summary>Rotas e assets críticos para go-live (Sprint 19).</summary>
public class GoLiveSmokeTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public GoLiveSmokeTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/duvidas")]
    [InlineData("/duvidas/quanto-desconta-inss-2026")]
    [InlineData("/meu-painel")]
    [InlineData("/widget")]
    [InlineData("/mapa-do-site")]
    [InlineData("/robots.txt")]
    [InlineData("/favicon.ico")]
    public async Task Go_Live_Route_Or_Asset_Should_Be_Available(string path)
    {
        var response = await client.GetAsync(path);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Robots_Should_Reference_Sitemap()
    {
        var body = await client.GetStringAsync("/robots.txt");

        body.Should().Contain("Sitemap:");
        body.Should().Contain("sitemap.xml");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Core_Pages_And_Calculators()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("<loc>https://meuvalorliquido.com.br/</loc>");
        xml.Should().Contain("/calculadoras/salario-liquido");
        xml.Should().Contain("/como-calculamos");
        xml.Should().Contain("/politica-de-privacidade");
    }

    [Fact]
    public async Task Health_Should_Report_Healthy()
    {
        var response = await client.GetAsync("/health");

        response.IsSuccessStatusCode.Should().BeTrue();
        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("Healthy");
    }

    [Fact]
    public async Task Calculadoras_Hub_Should_Match_Stitch_Bento_Layout()
    {
        var html = await client.GetStringAsync("/calculadoras");

        html.Should().Contain("valora-stitch-calc-hub");
        html.Should().Contain("valora-stitch-calc-featured");
        html.Should().Contain("Mais utilizada");
        html.Should().Contain("Sugerir ferramenta");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task Home_Should_Match_Stitch_Mobile_Shell()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("valora-bottom-nav");
        html.Should().Contain("valora-stitch-hero");
        html.Should().Contain("valora-stitch-bento");
        html.Should().Contain("O que você quer calcular?");
        html.Should().Contain("Relatórios completos");
    }

    [Fact]
    public async Task Calculator_Detail_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        html.Should().Contain("valora-stitch-calc-detail");
        html.Should().Contain("valora-stitch-calc-detail-hero");
        html.Should().Contain("valora-stitch-calc-detail-badge");
        (html.Contains("Simulação CLT") || html.Contains("Simula&#xE7;&#xE3;o CLT")).Should().BeTrue();
        html.Should().Contain("CALCULAR AGORA");
        html.Should().Contain("ESTIMATIVA ATUAL");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task PjVsClt_Should_Match_Stitch_Comparator_Layout()
    {
        var html = await client.GetStringAsync("/calculadoras/pj-vs-clt");

        html.Should().Contain("valora-stitch-cltpj");
        html.Should().Contain("Entradas CLT");
        html.Should().Contain("Entradas PJ");
        html.Should().Contain("CALCULAR AGORA");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task Faq_Hub_Should_Match_Stitch_Layout()
    {
        var html = await client.GetStringAsync("/duvidas");

        html.Should().Contain("valora-stitch-faq-hub");
        html.Should().Contain("Como podemos ajudar hoje?");
        html.Should().Contain("Perguntas Populares");
        html.Should().Contain("Regime CLT");
        html.Should().Contain("Entre em contato conosco");
        html.Should().Contain("valora-bottom-nav");
    }

    [Fact]
    public async Task Home_Should_Send_Security_Headers()
    {
        var response = await client.GetAsync("/");

        response.Headers.TryGetValues("X-Content-Type-Options", out var nosniff).Should().BeTrue();
        nosniff!.First().Should().Be("nosniff");
        response.Headers.TryGetValues("Content-Security-Policy", out _).Should().BeTrue();
    }
}
