namespace MeuValorLiquido.WebApp.Tests;

public class BrandAssetsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public BrandAssetsTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Home_Should_Use_Brand_Logo_And_Favicon()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("valora-brand-mark");
        html.Should().Contain("valora-brand-mark--stitch");
        html.Should().Contain("valora-brand-wordmark");
        html.Should().Contain("Meu Valor Líquido");
        html.Should().Contain("favicon");
        html.Should().Contain("apple-touch-icon");
    }

    [Fact]
    public async Task Home_Should_Use_Webp_Og_Image()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("og-default.webp");
    }

    [Theory]
    [InlineData("/favicon.ico")]
    [InlineData("/apple-touch-icon.png")]
    [InlineData("/images/og-default.webp")]
    [InlineData("/images/brand/logo-horizontal.webp")]
    [InlineData("/images/hero/home-hero.webp")]
    [InlineData("/images/blog/o-que-e-salario-liquido.webp")]
    public async Task Brand_Asset_Should_Be_Served(string path)
    {
        var response = await client.GetAsync(path);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
