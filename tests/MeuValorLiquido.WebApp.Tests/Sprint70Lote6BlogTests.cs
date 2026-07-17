namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint70Lote6BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const int MinReadingWords = 850;

    private readonly HttpClient client;

    public Sprint70Lote6BlogTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> Sprint70Lote6EditorialSlugs =>
        BlogEditorialCatalog.Sprint70Lote6EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint70Lote6EditorialSlugs))]
    public async Task Sprint70Lote6_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain("/como-calculamos");
        html.Should().Contain("id=\"como-validamos\"");
        html.Should().Contain("/calculadoras/");
        html.Should().Contain("/duvidas/");
        html.Should().Contain("id=\"dica-pratica\"");
        html.Should().Contain("Article");
    }

    [Theory]
    [MemberData(nameof(Sprint70Lote6EditorialSlugs))]
    public void Sprint70Lote6_Article_Should_Have_Minimum_Reading_Length(string slug)
    {
        var article = BlogArticleSeedData.GetAll().Single(a => a.Slug == slug);
        var plain = Regex.Replace(article.Content, "<[^>]+>", " ");
        var words = plain.Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries);

        words.Length.Should().BeGreaterThanOrEqualTo(MinReadingWords, $"artigo {slug} deve ter ~5 min de leitura (>={MinReadingWords} palavras)");
    }

    [Fact]
    public async Task Sprint70Lote6_Should_Add_Two_Editorial_Articles()
    {
        BlogEditorialCatalog.Sprint70Lote6EditorialSlugs.Should().HaveCount(2);

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote6EditorialSlugs)
        {
            BlogArticleSeedData.GetAll().Any(a => a.Slug == slug).Should().BeTrue();
            var response = await client.GetAsync($"/blog/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

    [Fact]
    public void Sprint70Lote6_Slugs_Should_Be_Sprint70_Editorial()
    {
        foreach (var slug in BlogEditorialCatalog.Sprint70Lote6EditorialSlugs)
        {
            BlogEditorialCatalog.IsSprint70Editorial(slug).Should().BeTrue();
            BlogEditorialCatalog.RequiresEditorialValidation(slug).Should().BeTrue();
        }
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint70Lote6_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote6EditorialSlugs)
        {
            xml.Should().Contain($"/blog/{slug}");
        }
    }

    [Fact]
    public async Task Desligamento_Hub_Should_Link_Sprint70Lote6_Article()
    {
        var html = await client.GetStringAsync("/desligamento");

        html.Should().Contain("/blog/aviso-previo-trabalhado-vs-indenizado");
    }

    [Fact]
    public async Task Negociar_Salario_Hub_Should_Link_Sprint70Lote6_Article()
    {
        var html = await client.GetStringAsync("/negociar-salario");

        html.Should().Contain("/blog/adicional-noturno-clt-como-calcular");
    }

    [Fact]
    public void Recent_Editorial_Articles_Should_Use_Five_Day_Publication_Cadence()
    {
        var posts = BlogArticleSeedData.GetAll().ToDictionary(post => post.Slug, StringComparer.OrdinalIgnoreCase);

        posts["vale-transporte-home-office-hibrido"].PublishedAt.Should().Be(new DateOnly(2026, 7, 5));
        posts["plano-saude-holerite-coparticipacao"].PublishedAt.Should().Be(new DateOnly(2026, 7, 5));
        posts["aviso-previo-trabalhado-vs-indenizado"].PublishedAt.Should().Be(new DateOnly(2026, 7, 10));
        posts["adicional-noturno-clt-como-calcular"].PublishedAt.Should().Be(new DateOnly(2026, 7, 15));
    }
}
