namespace MeuValorLiquido.WebApp.Tests;

public class Sprint83AdSenseTrustTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint83AdSenseTrustTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Blog_Post_Should_Show_Author_Profile_And_LinkedIn()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("Responsável editorial");
        html.Should().Contain("Matteus Oberdan");
        html.Should().Contain("https://www.linkedin.com/in/matteus-oberdan-203205289/");
        html.Should().Contain("/images/authors/matteus-oberdan.png");
        html.Should().Contain("Política Editorial");
    }

    [Fact]
    public async Task Article_JsonLd_Should_Expose_Author_Person_SameAs_And_Image()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("\"@type\":\"Person\"");
        html.Should().Contain("\"name\":\"Matteus Oberdan\"");
        html.Should().Contain("\"sameAs\":[\"https://www.linkedin.com/in/matteus-oberdan-203205289/\"]");
        html.Should().Contain("/images/authors/matteus-oberdan.png");
    }

    [Fact]
    public async Task Editorial_Policy_Should_Be_Indexable_And_Explain_Review_Process()
    {
        var html = await client.GetStringAsync("/politica-editorial");

        html.Should().Contain("Política Editorial");
        html.Should().Contain("fontes oficiais");
        html.Should().Contain("Correções");
        html.Should().Contain("Matteus Oberdan");
        html.Should().Contain("BreadcrumbList");
        html.Should().NotContain("noindex");
    }

    [Fact]
    public async Task Institutional_Pages_Should_Link_Editorial_Trust_Signals()
    {
        var about = await client.GetStringAsync("/sobre");
        var methodology = await client.GetStringAsync("/como-calculamos");
        var home = await client.GetStringAsync("/");

        about.Should().Contain("Política Editorial");
        about.Should().Contain("Matteus Oberdan");
        methodology.Should().Contain("Critério editorial e revisão");
        methodology.Should().Contain("Fontes oficiais primeiro");
        home.Should().Contain("/politica-editorial");
    }

    [Fact]
    public async Task Sitemap_And_Site_Map_Should_Include_Editorial_Policy()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");
        var html = await client.GetStringAsync("/mapa-do-site");

        xml.Should().Contain("/politica-editorial");
        html.Should().Contain("Política Editorial");
    }
}
