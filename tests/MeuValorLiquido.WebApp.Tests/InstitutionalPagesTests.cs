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
    [InlineData("/politica-editorial", "Política Editorial", "fontes oficiais")]
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
        xml.Should().Contain("/politica-editorial");
    }

    [Fact]
    public async Task AdsTxt_Should_Expose_Google_Publisher_Record()
    {
        var adsTxt = await client.GetStringAsync("/ads.txt");

        adsTxt.Trim().Should().Be("google.com, pub-4150358596824425, DIRECT, f08c47fec0942fa0");
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
        html.Should().Contain("data-cookie-consent-analytics");
        html.Should().Contain("data-cookie-consent-personalization");
        html.Should().Contain("data-cookie-consent-advertising");
        html.Should().Contain("data-policy-version=\"2026-07-17\"");
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
        script.Should().Contain("saveCategories(false, false, false)");
        script.Should().Contain("policyVersion = \"2026-07-17\"");
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

    [Theory]
    [InlineData("/")]
    [InlineData("/calculadoras/salario-liquido")]
    [InlineData("/duvidas")]
    [InlineData("/assistente")]
    public async Task Public_Pages_Should_Not_Show_Ad_Placeholders_When_Ads_Disabled(string url)
    {
        var html = await client.GetStringAsync(url);

        html.Should().NotContain("Espaço publicitário");
        html.Should().NotContain("Espaço Publicitário");
        html.Should().NotContain("Espaço reservado para anúncio");
        html.Should().NotContain("ad-slot-wrap");
        html.Should().NotContain("adsbygoogle");
    }
    [Fact]
    public async Task Home_Should_Not_Render_Adsense_Verification_Script_By_Default()
    {
        var html = await client.GetStringAsync("/");

        html.Should().NotContain("pagead2.googlesyndication.com/pagead/js/adsbygoogle.js");
    }

    [Fact]
    public async Task Home_Should_Defer_Fonts_And_LocalPanel_From_Initial_Html()
    {
        var html = await client.GetStringAsync("/");
        var fontLoader = await client.GetStringAsync("/js/font-loader.js");
        var cssLoader = await client.GetStringAsync("/js/css-loader.js");

        html.Should().Contain("/js/font-loader");
        html.Should().Contain("/js/css-loader");
        html.Should().Contain("data-deferred-stylesheet");
        html.Should().Contain("valora-stitch-home");
        html.Should().NotContain("fonts.googleapis.com/css2");
        html.Should().NotContain("/js/local-panel");
        html.Should().Contain("rel=\"stylesheet\" href=\"/css/site");
        html.Should().Contain("media=\"(min-width: 992px)\"");
        html.Should().Contain("rel=\"preload\" href=\"/css/site");
        html.Should().Contain("media=\"(max-width: 991.98px)\"");
        html.Should().Contain("<noscript");
        fontLoader.Should().Contain("fonts.googleapis.com/css2");
        fontLoader.Should().Contain("Material+Symbols+Outlined");
        cssLoader.Should().Contain("link[data-deferred-stylesheet]");
        cssLoader.Should().Contain("window.matchMedia(preload.media).matches");
        cssLoader.Should().Contain("stylesheet.rel = \"stylesheet\"");
    }

    [Fact]
    public async Task Home_Should_Preload_Decorative_Hero_Image_Only_On_Desktop()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("rel=\"preload\" as=\"image\"");
        html.Should().Contain("media=\"(min-width: 992px)\"");
        html.Should().Contain("images/hero/home-hero");
        html.Should().Contain("loading=\"eager\"");
        html.Should().Contain("fetchpriority=\"high\"");
    }

    [Fact]
    public async Task Home_Should_Use_Scriptless_Adsense_Verification_Tag_When_Configured()
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

        html.Should().Contain("<meta name=\"google-adsense-account\" content=\"ca-pub-test\"");
        html.Should().Contain("data-ads-script-enabled=\"false\"");
        html.Should().Contain("data-ads-publisher=\"ca-pub-test\"");
        html.Should().NotContain("https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-test");
        html.Should().NotContain("data-ad-client=\"ca-pub-test\"");
        response.Headers.GetValues("Content-Security-Policy").First().Should().NotContain("pagead2.googlesyndication.com");
    }
}
