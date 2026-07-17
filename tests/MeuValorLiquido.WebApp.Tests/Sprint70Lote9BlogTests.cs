namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint70Lote9BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const int MinReadingWords = 850;

    private readonly HttpClient client;

    public Sprint70Lote9BlogTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> Sprint70Lote9EditorialSlugs =>
        BlogEditorialCatalog.Sprint70Lote9EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint70Lote9EditorialSlugs))]
    public async Task Sprint70Lote9_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
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
    [MemberData(nameof(Sprint70Lote9EditorialSlugs))]
    public void Sprint70Lote9_Article_Should_Have_Minimum_Reading_Length(string slug)
    {
        var article = BlogArticleSeedData.GetAll().Single(a => a.Slug == slug);
        var plain = Regex.Replace(article.Content, "<[^>]+>", " ");
        var words = plain.Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries);

        words.Length.Should().BeGreaterThanOrEqualTo(MinReadingWords, $"artigo {slug} deve ter ~5 min de leitura (>={MinReadingWords} palavras)");
    }

    [Fact]
    public async Task Sprint70Lote9_Should_Add_Two_Editorial_Articles()
    {
        BlogEditorialCatalog.Sprint70Lote9EditorialSlugs.Should().HaveCount(2);

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote9EditorialSlugs)
        {
            BlogArticleSeedData.GetAll().Any(a => a.Slug == slug).Should().BeTrue();
            var response = await client.GetAsync($"/blog/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

    [Fact]
    public void Sprint70Lote9_Slugs_Should_Be_Sprint70_Editorial()
    {
        foreach (var slug in BlogEditorialCatalog.Sprint70Lote9EditorialSlugs)
        {
            BlogEditorialCatalog.IsSprint70Editorial(slug).Should().BeTrue();
            BlogEditorialCatalog.RequiresEditorialValidation(slug).Should().BeTrue();
        }
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint70Lote9_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote9EditorialSlugs)
        {
            xml.Should().Contain($"/blog/{slug}");
        }
    }

    [Fact]
    public async Task Negociar_Salario_Hub_Should_Link_Sprint70Lote9_Article()
    {
        var html = await client.GetStringAsync("/negociar-salario");

        html.Should().Contain("/blog/comissao-variavel-no-holerite");
    }

    [Fact]
    public async Task Virar_Pj_Hub_Should_Link_Sprint70Lote9_Article()
    {
        var html = await client.GetStringAsync("/virar-pj");

        html.Should().Contain("/blog/reserva-impostos-e-provisoes-ao-virar-pj");
    }

    [Fact]
    public void Sprint70Lote9_Articles_Should_Use_Publication_Date()
    {
        var posts = BlogArticleSeedData.GetAll().ToDictionary(post => post.Slug, StringComparer.OrdinalIgnoreCase);

        posts["comissao-variavel-no-holerite"].PublishedAt.Should().Be(new DateOnly(2026, 7, 17));
        posts["reserva-impostos-e-provisoes-ao-virar-pj"].PublishedAt.Should().Be(new DateOnly(2026, 7, 17));
    }
}
