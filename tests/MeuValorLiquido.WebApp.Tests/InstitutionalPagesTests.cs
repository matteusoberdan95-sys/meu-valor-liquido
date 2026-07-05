namespace MeuValorLiquido.WebApp.Tests;

public class InstitutionalPagesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public InstitutionalPagesTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/sobre", "Nossa missão", "Como calculamos")]
    [InlineData("/como-calculamos", "Tabelas de 2026", "BrTaxTables2026")]
    [InlineData("/politica-de-privacidade", "Google AdSense", "localStorage")]
    [InlineData("/politica-de-cookies", "Política de Cookies", "mvl-cookie-consent")]
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
        xml.Should().Contain("/politica-de-cookies");
    }

    [Fact]
    public async Task Home_Should_Include_Cookie_Consent_Banner()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("data-cookie-consent");
        html.Should().Contain("/politica-de-cookies");
        html.Should().Contain("/politica-de-privacidade");
        html.Should().Contain("Aceitar todos");
        html.Should().Contain("Rejeitar todos");
        html.Should().Contain("Personalizar");
        html.Should().Contain("data-cookie-consent-preferences");
        html.Should().Contain("cookie-consent.");
    }

    [Fact]
    public async Task Cookie_Consent_Script_Should_Handle_Reject_And_Customize()
    {
        var script = await client.GetStringAsync("/js/cookie-consent.js");

        script.Should().Contain("data-cookie-consent-close");
        script.Should().Contain("data-cookie-consent-customize");
        script.Should().Contain("data-cookie-consent-save");
        script.Should().Contain("data-cookie-consent-advertising");
        script.Should().Contain("saveConsent(false");
    }

    [Fact]
    public async Task Como_Calculamos_Should_Show_Benchmark_Sources()
    {
        var html = await client.GetStringAsync("/como-calculamos");

        html.Should().Contain("Paridade automatizada");
        html.Should().Contain("CalculatorBenchmarkCatalog");
        html.Should().Contain(CalculatorBenchmarkCatalog.All.Count.ToString());
        html.Should().Contain("Portaria Interministerial MPS/MF");
        html.Should().Contain("CLT e criterios internos documentados");
    }

    [Fact]
    public async Task Ad_Slots_Should_Show_Placeholder_When_Ads_Disabled()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");

        html.Should().Contain("Espaço publicitário");
        html.Should().NotContain("adsbygoogle");
    }
    [Fact]
    public async Task Home_Should_Not_Render_Adsense_Verification_Script_By_Default()
    {
        var html = await client.GetStringAsync("/");

        html.Should().NotContain("pagead2.googlesyndication.com/pagead/js/adsbygoogle.js");
    }

    [Fact]
    public async Task Home_Should_Render_Adsense_Verification_Script_When_Configured()
    {
        var configuredClient = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Ads:VerificationEnabled"] = "true",
                    ["Ads:PublisherId"] = "ca-pub-test"
                });
            });
        }).CreateClient();

        var response = await configuredClient.GetAsync("/");
        var html = await response.Content.ReadAsStringAsync();

        html.Should().Contain("https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-test");
        html.Should().Contain("crossorigin=\"anonymous\"");
        html.Should().NotContain("data-ad-client=\"ca-pub-test\"");
        response.Headers.GetValues("Content-Security-Policy").First().Should().Contain("pagead2.googlesyndication.com");
    }
}
