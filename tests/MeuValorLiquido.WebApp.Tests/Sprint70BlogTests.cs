namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint70BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint70BlogTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> Sprint70EditorialSlugs =>
        BlogEditorialCatalog.Sprint70EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint70EditorialSlugs))]
    public async Task Sprint70_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain("/como-calculamos");
        html.Should().Contain("id=\"como-validamos\"");
        html.Should().Contain("/calculadoras/");
        html.Should().Contain("/duvidas/");
        html.Should().Contain("Article");
    }

    [Fact]
    public async Task Sprint70_Should_Add_Two_Editorial_Articles()
    {
        BlogEditorialCatalog.Sprint70EditorialSlugs.Should().HaveCount(2);

        foreach (var slug in BlogEditorialCatalog.Sprint70EditorialSlugs)
        {
            BlogArticleSeedData.GetAll().Any(a => a.Slug == slug).Should().BeTrue();
            var response = await client.GetAsync($"/blog/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint70_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        foreach (var slug in BlogEditorialCatalog.Sprint70EditorialSlugs)
        {
            xml.Should().Contain($"/blog/{slug}");
        }
    }

    [Fact]
    public async Task Desligamento_Hub_Should_Link_Sprint70_Article()
    {
        var html = await client.GetStringAsync("/desligamento");
        html.Should().Contain("/blog/acordo-484a-verbas-e-multa-fgts");
    }

    [Fact]
    public async Task VirarPj_Hub_Should_Link_Sprint70_Article()
    {
        var html = await client.GetStringAsync("/virar-pj");
        html.Should().Contain("/blog/custo-total-clt-para-empregador");
    }
}
