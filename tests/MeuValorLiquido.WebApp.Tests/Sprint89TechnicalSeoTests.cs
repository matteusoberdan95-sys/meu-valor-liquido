namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint89TechnicalSeoTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public Sprint89TechnicalSeoTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing"));
        client = this.factory.CreateClient();
    }

    public static TheoryData<string, string> PermanentAliases => new()
    {
        { "/calculadora-salario-bruto", "/calculadoras/salario-bruto-necessario" },
        { "/quanto-preciso-ganhar-para-receber-liquido", "/calculadoras/salario-bruto-necessario" },
        { "/proposta-salarial", "/calculadoras/proposta-salarial" },
        { "/comparar-proposta-salarial", "/calculadoras/proposta-salarial" },
        { "/clt-vs-pj", "/clt-pj" },
        { "/painel", "/meu-painel" },
        { "/incorporar", "/widget" },
        { "/duvidas/o-que-e-irrf", "/duvidas/irrf-quem-paga-e-como-calcular" },
        { "/widget/salario-liquido", "/calculadoras/salario-liquido?embed=1" }
    };

    [Theory]
    [InlineData("/assistente")]
    [InlineData("/correcoes")]
    [InlineData("/meu-painel")]
    [InlineData("/metricas-internas")]
    [InlineData("/newsletter")]
    [InlineData("/widget")]
    public async Task Low_Value_Or_Personal_Page_Should_Be_NoIndex_And_Absent_From_Sitemap(string path)
    {
        using var response = await client.GetAsync(path);
        var html = await response.Content.ReadAsStringAsync();
        var sitemap = await client.GetStringAsync("/sitemap.xml");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Headers.GetValues("X-Robots-Tag").Should().ContainSingle().Which.Should().Be("noindex");
        html.Should().Contain("name=\"robots\" content=\"noindex");
        sitemap.Should().NotContain($"<loc>https://meuvalorliquido.com{path}</loc>");
    }

    [Fact]
    public async Task Filtered_Blog_Should_Be_NoIndex_With_Canonical_Pointing_To_Hub()
    {
        var html = await client.GetStringAsync("/blog?cat=trabalho");

        html.Should().Contain("name=\"robots\" content=\"noindex,follow\"");
        html.Should().Contain("<link rel=\"canonical\" href=\"https://meuvalorliquido.com/blog\"");
    }

    [Fact]
    public async Task Calculator_Query_State_Should_Be_NoIndex_With_Canonical_Pointing_To_Tool()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido?valor=3500");

        html.Should().Contain("name=\"robots\" content=\"noindex,follow\"");
        html.Should().Contain(
            "<link rel=\"canonical\" href=\"https://meuvalorliquido.com/calculadoras/salario-liquido\"");
    }

    [Theory]
    [InlineData("/calculadoras?q=nao-existe", "/calculadoras")]
    [InlineData("/duvidas?q=nao-existe", "/duvidas")]
    public async Task Filtered_Hub_Should_Be_NoIndex_With_Canonical_Pointing_To_Hub(
        string requested,
        string canonicalPath)
    {
        var html = await client.GetStringAsync(requested);

        html.Should().Contain("name=\"robots\" content=\"noindex,follow\"");
        html.Should().Contain($"<link rel=\"canonical\" href=\"https://meuvalorliquido.com{canonicalPath}\"");
    }

    [Fact]
    public async Task Sitemap_Should_Contain_Only_Unique_Canonical_Indexable_Urls()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");
        var document = XDocument.Parse(xml);
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var entries = document.Descendants(ns + "url").ToList();
        var locations = entries.Select(entry => entry.Element(ns + "loc")!.Value).ToList();

        locations.Should().OnlyHaveUniqueItems();
        foreach (var location in locations)
        {
            var uri = new Uri(location);
            uri.Scheme.Should().Be(Uri.UriSchemeHttps);
            uri.Host.Should().Be("meuvalorliquido.com");
            uri.Query.Should().BeEmpty();
            (uri.AbsolutePath == "/" || !uri.AbsolutePath.EndsWith('/')).Should().BeTrue();
            uri.AbsolutePath.Should().Be(uri.AbsolutePath.ToLowerInvariant());
        }

        foreach (var entry in entries)
        {
            DateOnly.TryParse(entry.Element(ns + "lastmod")?.Value, out _).Should().BeTrue();
        }
        entries.Single(entry =>
                entry.Element(ns + "loc")?.Value == "https://meuvalorliquido.com/calculadoras/salario-liquido")
            .Element(ns + "lastmod")?.Value
            .Should().Be("2026-07-17");

        foreach (var noIndexPath in SeoRoutePolicyCatalog.NoIndexPagePaths)
        {
            locations.Should().NotContain($"https://meuvalorliquido.com{noIndexPath}");
        }
    }

    [Theory]
    [MemberData(nameof(PermanentAliases))]
    public async Task Legacy_Alias_Should_Permanently_Redirect_To_Canonical_Url(string alias, string destination)
    {
        using var noRedirectClient = CreateNoRedirectClient();
        using var response = await noRedirectClient.GetAsync(alias);

        response.StatusCode.Should().Be(HttpStatusCode.MovedPermanently);
        response.Headers.Location?.ToString().Should().Be(destination);
    }

    [Theory]
    [InlineData("/Calculadoras", "/calculadoras")]
    [InlineData("/blog/", "/blog")]
    [InlineData("/SALARIO-LIQUIDO/3000/", "/salario-liquido/3000")]
    [InlineData("/SITEMAP.XML/", "/sitemap.xml")]
    public async Task Case_And_Trailing_Slash_Variations_Should_Permanently_Redirect(string requested, string canonical)
    {
        using var noRedirectClient = CreateNoRedirectClient();
        using var response = await noRedirectClient.GetAsync(requested);

        response.StatusCode.Should().Be(HttpStatusCode.MovedPermanently);
        response.Headers.Location?.ToString().Should().Be(canonical);
    }

    [Fact]
    public async Task Missing_And_Error_Pages_Should_Return_Real_Status_Without_Canonical()
    {
        using var noRedirectClient = CreateNoRedirectClient();
        using var missing = await noRedirectClient.GetAsync("/calculadoras/nao-existe");
        using var error = await noRedirectClient.GetAsync("/Error");
        var missingHtml = await missing.Content.ReadAsStringAsync();
        var errorHtml = await error.Content.ReadAsStringAsync();

        missing.StatusCode.Should().Be(HttpStatusCode.NotFound);
        error.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        missingHtml.Should().Contain("noindex,nofollow");
        errorHtml.Should().Contain("noindex,nofollow");
        missingHtml.Should().NotContain("rel=\"canonical\"");
        errorHtml.Should().NotContain("rel=\"canonical\"");
    }

    [Fact]
    public async Task Operational_Endpoints_Should_Expose_Crawler_Protection()
    {
        using var health = await client.GetAsync("/health");
        var robots = await client.GetStringAsync("/robots.txt");

        health.Headers.GetValues("X-Robots-Tag").Should().ContainSingle().Which.Should().Be("noindex, nofollow");
        robots.Should().Contain("Disallow: /api/");
        robots.Should().Contain("Disallow: /health");
        robots.Should().Contain("Disallow: /*/resultado.pdf$");
    }

    private HttpClient CreateNoRedirectClient() =>
        factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                BaseAddress = new Uri("https://localhost")
            });
}
