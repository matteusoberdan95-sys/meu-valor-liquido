namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint58BlogTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint58BlogTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    public static IEnumerable<object[]> Sprint58EditorialSlugs =>
        BlogEditorialCatalog.Sprint58EditorialSlugs.Select(slug => new object[] { slug });

    [Theory]
    [MemberData(nameof(Sprint58EditorialSlugs))]
    public async Task Sprint58_Article_Should_Link_Calculator_Methodology_And_Faq(string slug)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain("/como-calculamos");
        html.Should().Contain("id=\"como-validamos\"");
        html.Should().Contain("Como validamos esta estimativa");
        html.Should().Contain("Estimativa educativa");
        html.Should().Contain("/calculadoras/");
        html.Should().Contain("/duvidas/");
        html.Should().Contain("Article");
        html.Should().Contain("headline");
    }

    [Fact]
    public async Task Como_Conferir_Holerite_Should_Mention_Checklist()
    {
        var html = await client.GetStringAsync("/blog/como-conferir-holerite");

        html.Should().Contain("Checklist");
        html.Should().Contain("/calculadoras/salario-liquido");
        html.Should().Contain("Como validamos");
    }

    [Fact]
    public async Task Rescisao_Vs_Trct_Should_Explain_Document_Gap()
    {
        var html = await client.GetStringAsync("/blog/rescisao-clt-vs-trct");

        html.Should().Contain("TRCT");
        html.Should().Contain("/calculadoras/rescisao-clt");
        html.Should().Contain("/duvidas/multa-fgts-40-porcento");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Sprint58_Articles()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/blog/como-conferir-holerite");
        xml.Should().Contain("/blog/rescisao-clt-vs-trct");
    }

    [Fact]
    public async Task Blog_Post_Should_Link_Validation_In_Aside()
    {
        var html = await client.GetStringAsync("/blog/como-conferir-holerite");

        html.Should().Contain("href=\"#como-validamos\"");
        html.Should().Contain("Como validamos");
    }
}
