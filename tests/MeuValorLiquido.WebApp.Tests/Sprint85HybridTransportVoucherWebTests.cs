namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint85HybridTransportVoucherWebTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint85HybridTransportVoucherWebTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Calculator_Page_Should_Render_Targeted_Form_And_Faq()
    {
        var html = WebUtility.HtmlDecode(await client.GetStringAsync("/calculadoras/vale-transporte-hibrido"));

        html.Should().Contain("Vale-transporte híbrido");
        html.Should().Contain("Salário base");
        html.Should().Contain("Custo ida e volta por dia");
        html.Should().Contain("Dias presenciais no mês");
        html.Should().Contain("Desconto atual no holerite");
        html.Should().Contain("Como calcular vale-transporte proporcional no trabalho híbrido?");
    }

    [Fact]
    public async Task Calculator_Should_Be_Discoverable_From_Sitemap_And_Negotiation_Hub()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");
        var hub = WebUtility.HtmlDecode(await client.GetStringAsync("/negociar-salario"));

        xml.Should().Contain("https://meuvalorliquido.com/calculadoras/vale-transporte-hibrido");
        hub.Should().Contain("/calculadoras/vale-transporte-hibrido");
        hub.Should().Contain("VT proporcional");
    }

    [Fact]
    public async Task Hybrid_Transport_Article_Should_Link_To_Calculator_And_Faq_Block()
    {
        var html = WebUtility.HtmlDecode(await client.GetStringAsync("/blog/vale-transporte-home-office-hibrido"));

        html.Should().Contain("/calculadoras/vale-transporte-hibrido");
        html.Should().Contain("Perguntas frequentes sobre VT híbrido");
        html.Should().Contain("Calcular VT híbrido");
        html.Should().Contain("/conferir-holerite");
    }

    [Fact]
    public void Blog_Seed_Should_Use_Hybrid_Transport_Calculator_As_Primary_Conversion()
    {
        var article = BlogArticleSeedData.GetAll().Single(post =>
            post.Slug == "vale-transporte-home-office-hibrido");

        article.RelatedCalculatorSlug.Should().Be("vale-transporte-hibrido");
    }
}
