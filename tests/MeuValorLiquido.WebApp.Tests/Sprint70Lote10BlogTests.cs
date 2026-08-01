namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint70Lote10BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const int MinReadingWords = 850;

    private readonly HttpClient client;

    public Sprint70Lote10BlogTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> Sprint70Lote10EditorialSlugs =>
        BlogEditorialCatalog.Sprint70Lote10EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint70Lote10EditorialSlugs))]
    public async Task Sprint70Lote10_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
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
    [MemberData(nameof(Sprint70Lote10EditorialSlugs))]
    public void Sprint70Lote10_Article_Should_Have_Minimum_Reading_Length(string slug)
    {
        var article = BlogArticleSeedData.GetAll().Single(a => a.Slug == slug);
        var plain = Regex.Replace(article.Content, "<[^>]+>", " ");
        var words = plain.Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries);

        words.Length.Should().BeGreaterThanOrEqualTo(MinReadingWords, $"artigo {slug} deve ter ~5 min de leitura (>={MinReadingWords} palavras)");
    }

    [Fact]
    public async Task Sprint70Lote10_Should_Add_Two_Editorial_Articles()
    {
        BlogEditorialCatalog.Sprint70Lote10EditorialSlugs.Should().HaveCount(2);

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote10EditorialSlugs)
        {
            BlogArticleSeedData.GetAll().Any(a => a.Slug == slug).Should().BeTrue();
            var response = await client.GetAsync($"/blog/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

    [Fact]
    public void Sprint70Lote10_Slugs_Should_Be_Sprint70_Editorial()
    {
        foreach (var slug in BlogEditorialCatalog.Sprint70Lote10EditorialSlugs)
        {
            BlogEditorialCatalog.IsSprint70Editorial(slug).Should().BeTrue();
            BlogEditorialCatalog.RequiresEditorialValidation(slug).Should().BeTrue();
        }
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint70Lote10_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote10EditorialSlugs)
        {
            xml.Should().Contain($"/blog/{slug}");
        }
    }

    [Fact]
    public async Task Negociar_Salario_Hub_Should_Link_Sprint70Lote10_Articles()
    {
        var html = await client.GetStringAsync("/negociar-salario");

        html.Should().Contain("/blog/vale-transporte-vr-orcamento-mensal");
        html.Should().Contain("/blog/salario-minimo-impacto-holerite");
    }

    [Fact]
    public void Sprint70Lote10_Articles_Should_Use_Publication_Date()
    {
        var posts = BlogArticleSeedData.GetAll().ToDictionary(post => post.Slug, StringComparer.OrdinalIgnoreCase);

        posts["vale-transporte-vr-orcamento-mensal"].PublishedAt.Should().Be(new DateOnly(2026, 8, 1));
        posts["salario-minimo-impacto-holerite"].PublishedAt.Should().Be(new DateOnly(2026, 8, 1));
    }
}
