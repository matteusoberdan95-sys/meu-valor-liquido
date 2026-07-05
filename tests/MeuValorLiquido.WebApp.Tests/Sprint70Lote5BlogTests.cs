namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint70Lote5BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private const int MinReadingWords = 850;

    private readonly HttpClient client;

    public Sprint70Lote5BlogTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> Sprint70Lote5EditorialSlugs =>
        BlogEditorialCatalog.Sprint70Lote5EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint70Lote5EditorialSlugs))]
    public async Task Sprint70Lote5_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
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
    [MemberData(nameof(Sprint70Lote5EditorialSlugs))]
    public void Sprint70Lote5_Article_Should_Have_Minimum_Reading_Length(string slug)
    {
        var article = BlogArticleSeedData.GetAll().Single(a => a.Slug == slug);
        var plain = Regex.Replace(article.Content, "<[^>]+>", " ");
        var words = plain.Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries);

        words.Length.Should().BeGreaterThanOrEqualTo(MinReadingWords, $"artigo {slug} deve ter ~5 min de leitura (>={MinReadingWords} palavras)");
    }

    [Fact]
    public async Task Sprint70Lote5_Should_Add_Two_Editorial_Articles()
    {
        BlogEditorialCatalog.Sprint70Lote5EditorialSlugs.Should().HaveCount(2);

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote5EditorialSlugs)
        {
            BlogArticleSeedData.GetAll().Any(a => a.Slug == slug).Should().BeTrue();
            var response = await client.GetAsync($"/blog/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint70Lote5_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        foreach (var slug in BlogEditorialCatalog.Sprint70Lote5EditorialSlugs)
        {
            xml.Should().Contain($"/blog/{slug}");
        }
    }

    [Fact]
    public async Task Negociar_Salario_Hub_Should_Link_Sprint70Lote5_Articles()
    {
        var html = await client.GetStringAsync("/negociar-salario");

        html.Should().Contain("/blog/vale-transporte-home-office-hibrido");
        html.Should().Contain("/blog/plano-saude-holerite-coparticipacao");
    }

    [Fact]
    public void Recent_Editorial_Articles_Should_Use_Five_Day_Publication_Cadence()
    {
        var posts = BlogArticleSeedData.GetAll().ToDictionary(post => post.Slug, StringComparer.OrdinalIgnoreCase);

        posts["ferias-coletivas-clt-guia-completo"].PublishedAt.Should().Be(new DateOnly(2026, 6, 5));
        posts["pedir-demissao-ou-aguardar-dispensa"].PublishedAt.Should().Be(new DateOnly(2026, 6, 10));
        posts["dissidio-salarial-2026-como-avaliar"].PublishedAt.Should().Be(new DateOnly(2026, 6, 15));
        posts["vale-refeicao-desconto-holerite"].PublishedAt.Should().Be(new DateOnly(2026, 6, 20));
        posts["experiencia-clt-direitos-e-rescisao"].PublishedAt.Should().Be(new DateOnly(2026, 6, 25));
        posts["home-office-clt-descontos"].PublishedAt.Should().Be(new DateOnly(2026, 6, 30));
        posts["vale-transporte-home-office-hibrido"].PublishedAt.Should().Be(new DateOnly(2026, 7, 5));
        posts["plano-saude-holerite-coparticipacao"].PublishedAt.Should().Be(new DateOnly(2026, 7, 5));
    }
}
