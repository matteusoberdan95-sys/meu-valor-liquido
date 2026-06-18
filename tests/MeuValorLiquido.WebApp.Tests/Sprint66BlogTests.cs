namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint66BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint66BlogTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> Sprint66EditorialSlugs =>
        BlogEditorialCatalog.Sprint66EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint66EditorialSlugs))]
    public async Task Sprint66_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain("/como-calculamos");
        html.Should().Contain("id=\"como-validamos\"");
        html.Should().Contain("/calculadoras/");
        html.Should().Contain("/duvidas/");
        html.Should().Contain("Article");
    }

    [Fact]
    public async Task Sprint66_Should_Add_Four_New_Editorial_Articles()
    {
        BlogEditorialCatalog.Sprint66EditorialSlugs.Should().HaveCount(4);

        foreach (var slug in BlogEditorialCatalog.Sprint66EditorialSlugs)
        {
            BlogArticleSeedData.GetAll().Any(a => a.Slug == slug).Should().BeTrue();
            var response = await client.GetAsync($"/blog/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint66_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/blog/irrf-2026-reducao-imposto");
        xml.Should().Contain("/blog/seguro-desemprego-quem-tem-direito");
        xml.Should().Contain("/blog/multa-fgts-40-ou-20");
        xml.Should().Contain("/blog/aumento-salario-quanto-sobra-liquido");
    }

    [Theory]
    [InlineData("/desligamento", "seguro-desemprego-quem-tem-direito", "multa-fgts-40-ou-20")]
    [InlineData("/negociar-salario", "aumento-salario-quanto-sobra-liquido", "irrf-2026-reducao-imposto")]
    public async Task Thematic_Hub_Should_List_Sprint66_Articles(
        string hubPath,
        string articleSlugA,
        string articleSlugB)
    {
        var html = await client.GetStringAsync(hubPath);

        html.Should().Contain($"/blog/{articleSlugA}");
        html.Should().Contain($"/blog/{articleSlugB}");
    }

    [Fact]
    public async Task Seguro_Desemprego_Article_Should_Link_Desligamento_Hub()
    {
        var html = await client.GetStringAsync("/blog/seguro-desemprego-quem-tem-direito");

        html.Should().Contain("/desligamento");
        html.Should().Contain("/calculadoras/rescisao-clt");
        html.Should().Contain("/calculadoras/seguro-desemprego");
    }

    [Fact]
    public async Task Aumento_Salario_Article_Should_Link_Negociar_Hub()
    {
        var html = await client.GetStringAsync("/blog/aumento-salario-quanto-sobra-liquido");

        html.Should().Contain("/negociar-salario");
        html.Should().Contain("/calculadoras/proposta-salarial");
    }
}
