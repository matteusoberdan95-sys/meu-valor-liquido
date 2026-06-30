namespace MeuValorLiquido.WebApp.Tests;
public class BlogContentTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public BlogContentTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Blog_Index_Should_List_At_Least_15_Articles()
    {
        var html = await client.GetStringAsync("/blog");
        var match = Regex.Match(html, @"(\d+) artigos educativos");

        match.Success.Should().BeTrue();
        int.Parse(match.Groups[1].Value).Should().BeGreaterThanOrEqualTo(15);
        html.Should().Contain("valora-stitch-blog-hub");
        html.Should().Contain("Conteúdo Educativo");
        html.Should().Contain("valora-stitch-blog-featured");
        html.Should().Contain("ferias-coletivas-clt-guia-completo");
    }

    [Fact]
    public async Task Blog_Post_Should_Link_To_Related_Calculator()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("/calculadoras/salario-liquido");
        html.Should().Contain("data-testid=\"blog-conversion-panel\"");
        html.Should().Contain("data-blog-conversion-action=\"calculator\"");
        html.Should().Contain("valora-stitch-blog-article");
        html.Should().Contain("Matteus Oberdan");
        html.Should().Contain("valora-stitch-blog-hero--has-image");
        html.Should().Contain("id=\"dica-pratica\"");
        html.Should().Contain("id=\"como-validamos\"");
    }

    [Fact]
    public async Task Blog_Post_With_Invalid_Slug_Should_Return_404()
    {
        var response = await client.GetAsync("/blog/slug-inexistente-xyz");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Sitemap_Should_Include_Blog_Post_Urls()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/blog/o-que-e-salario-liquido");
        xml.Should().Contain("/blog/como-conferir-holerite");
        xml.Should().Contain("/blog/planejamento-financeiro-com-salario");
    }
}
