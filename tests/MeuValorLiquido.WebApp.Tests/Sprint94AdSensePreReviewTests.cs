namespace MeuValorLiquido.WebApp.Tests;

/// <summary>
/// Pré-revisão final AdSense (Sprint 94 / plano Sprint 9).
/// Trava regressões críticas antes de solicitar ou reenviar ao Google.
/// </summary>
public sealed class Sprint94AdSensePreReviewTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string[] InstitutionalPaths =
    [
        "/sobre",
        "/contato",
        "/politica-de-privacidade",
        "/politica-de-cookies",
        "/termos-de-uso",
        "/aviso-legal",
        "/como-calculamos",
        "/politica-editorial",
        "/autores/matteus-oberdan",
        "/correcoes"
    ];

    private static readonly string[] ForbiddenClaims =
    [
        "+250k",
        "250 mil",
        "ML Prime",
        "IA 2080",
        "melhor do Brasil",
        "★★★★★",
        "depoimento fictício"
    ];

    private readonly HttpClient client;

    public Sprint94AdSensePreReviewTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [MemberData(nameof(InstitutionalRouteData))]
    public async Task Institutional_Pages_Should_Be_Reachable(string path)
    {
        using var response = await client.GetAsync(path);
        response.IsSuccessStatusCode.Should().BeTrue($"página institucional {path} deve responder 2xx");
    }

    public static IEnumerable<object[]> InstitutionalRouteData =>
        InstitutionalPaths.Select(path => new object[] { path });

    [Fact]
    public async Task Home_Should_Not_Expose_False_Trust_Or_Popularity_Claims()
    {
        var html = await client.GetStringAsync("/");

        foreach (var claim in ForbiddenClaims)
        {
            html.Should().NotContain(claim);
        }

        html.Should().NotContain("jquery.min");
        html.Should().Contain("defer");
    }

    [Fact]
    public async Task Ads_Off_Should_Leave_Home_And_Calculator_Without_Placeholders_Or_Ad_Script()
    {
        var home = await client.GetStringAsync("/");
        var calculator = await client.GetStringAsync("/calculadoras/salario-liquido");

        foreach (var html in new[] { home, calculator })
        {
            html.Should().NotContain("adsbygoogle.js");
            html.Should().NotContain("class=\"adsbygoogle\"");
            html.Should().NotContain("Espaço reservado para anúncio");
            html.Should().NotContain("/js/adsense-init");
        }
    }

    [Fact]
    public async Task Consent_Banner_Should_Remain_Versioned_With_Four_Categories()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("data-consent-version=\"2\"");
        html.Should().Contain("data-policy-version=\"2026-07-17\"");
        html.Should().Contain("Cookies essenciais");
        html.Should().Contain(">Analytics</strong>");
        html.Should().Contain(">Personalização</strong>");
        html.Should().Contain(">Publicidade</strong>");
    }

    [Fact]
    public async Task Seo_Surface_Should_Expose_Sitemap_Robots_And_Canonical_Home()
    {
        var robots = await client.GetStringAsync("/robots.txt");
        robots.Should().Contain("Sitemap:");
        robots.Should().Contain("sitemap.xml");
        robots.Should().Contain("Disallow: /api/");

        var sitemap = await client.GetStringAsync("/sitemap.xml");
        sitemap.Should().Contain("/calculadoras/salario-liquido");
        sitemap.Should().Contain("/politica-editorial");
        sitemap.Should().Contain("/autores/matteus-oberdan");
        sitemap.Should().NotContain("/assistente");
        sitemap.Should().NotContain("/meu-painel");
        sitemap.Should().NotContain("/newsletter");

        var home = await client.GetStringAsync("/");
        home.Should().Contain("rel=\"canonical\"");
        home.Should().Contain("index,follow");
    }

    [Fact]
    public async Task Missing_Calculator_Should_Return_Real_404()
    {
        using var response = await client.GetAsync("/calculadoras/slug-inexistente-pre-revisao-94");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Ads_Txt_Should_Keep_Concrete_Publisher_Id()
    {
        var body = await client.GetStringAsync("/ads.txt");
        body.Should().Contain("google.com, pub-4150358596824425, DIRECT, f08c47fec0942fa0");
        body.Should().NotContain("pub-0000000000000000");
        body.Should().NotContain("REPLACE");
    }

    [Fact]
    public void Priority_Calculators_Should_Have_Editorial_Catalog_Entries()
    {
        var contents = CalculatorEditorialCatalog.All.ToList();
        contents.Should().HaveCountGreaterThanOrEqualTo(12);
        contents.Should().OnlyContain(content =>
            !string.IsNullOrWhiteSpace(content.Summary)
            && !string.IsNullOrWhiteSpace(content.HowItWorks)
            && content.Sources.Count > 0
            && content.LastReviewedAt.Year >= 2026);
    }

    [Fact]
    public async Task Editorial_Authority_Signals_Should_Remain_Public()
    {
        var author = await client.GetStringAsync("/autores/matteus-oberdan");
        author.Should().Contain("linkedin.com");
        author.Should().Contain("Person");

        var policy = await client.GetStringAsync("/politica-editorial");
        policy.Should().Contain("Fontes");
        policy.Should().Contain("Correções");

        var corrections = await client.GetAsync("/correcoes");
        corrections.IsSuccessStatusCode.Should().BeTrue();
        var correctionsHtml = await corrections.Content.ReadAsStringAsync();
        correctionsHtml.Should().Contain("noindex");
    }
}
