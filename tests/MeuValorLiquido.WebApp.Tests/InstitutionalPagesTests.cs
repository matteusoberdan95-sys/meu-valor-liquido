namespace MeuValorLiquido.WebApp.Tests;

public class InstitutionalPagesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public InstitutionalPagesTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/sobre", "Nossa missão", "Como calculamos")]
    [InlineData("/como-calculamos", "Tabelas de 2026", "BrTaxTables")]
    [InlineData("/politica-de-privacidade", "Google AdSense", "localStorage")]
    [InlineData("/termos-de-uso", "Natureza do serviço", "educativo")]
    [InlineData("/aviso-legal", "Estimativas, não laudos", "consultoria")]
    public async Task Institutional_Page_Should_Contain_Key_Content(string url, string phraseA, string phraseB)
    {
        var html = await client.GetStringAsync(url);

        html.Should().Contain(phraseA);
        html.Should().Contain(phraseB);
        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Como_Calculamos()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/como-calculamos");
    }

    [Fact]
    public async Task Ad_Slots_Should_Show_Placeholder_When_Ads_Disabled()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        html.Should().Contain("Espaço publicitário");
        html.Should().NotContain("adsbygoogle");
    }
}
