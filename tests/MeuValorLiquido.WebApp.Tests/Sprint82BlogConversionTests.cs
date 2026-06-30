namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint82BlogConversionTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint82BlogConversionTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    public static IEnumerable<object[]> ArticlesWithCalculator =>
        BlogArticleSeedData.GetAll()
            .Where(article => !string.IsNullOrWhiteSpace(article.RelatedCalculatorSlug))
            .Select(article => new object[] { article.Slug, article.RelatedCalculatorSlug! });

    [Theory]
    [MemberData(nameof(ArticlesWithCalculator))]
    public async Task Blog_Article_With_Related_Calculator_Should_Show_Conversion_Panel(
        string slug,
        string calculatorSlug)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain("data-testid=\"blog-conversion-panel\"");
        html.Should().Contain("data-blog-conversion-action=\"calculator\"");
        html.Should().Contain("data-blog-conversion-action=\"assistant\"");
        html.Should().Contain($"/calculadoras/{calculatorSlug}");
        html.Should().Contain("/assistente");
    }

    [Theory]
    [InlineData("experiencia-clt-direitos-e-rescisao", "/desligamento", "/duvidas/rescisao-pedido-demissao-o-que-recebo")]
    [InlineData("home-office-clt-descontos", "/negociar-salario", "/duvidas/como-calcular-salario-liquido")]
    [InlineData("pj-ou-clt-qual-melhor", "/virar-pj", "/duvidas/pj-ou-clt-qual-compensa")]
    [InlineData("tabela-irrf-2026-guia", "/como-calculamos", "/duvidas/irrf-quem-paga-e-como-calcular")]
    public async Task Blog_Conversion_Panel_Should_Link_Contextual_Hub_And_Faq(
        string slug,
        string hubPath,
        string faqPath)
    {
        var html = await client.GetStringAsync($"/blog/{slug}");

        html.Should().Contain(hubPath);
        html.Should().Contain(faqPath);
    }
}
