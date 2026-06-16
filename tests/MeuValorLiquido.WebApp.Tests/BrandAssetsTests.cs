namespace MeuValorLiquido.WebApp.Tests;

public class BrandAssetsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public BrandAssetsTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Home_Should_Use_Text_Brand_Mark_And_Favicon()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("valora-brand-mark");
        html.Should().Contain("Meu Valor Líquido");
        html.Should().Contain("favicon");
        html.Should().NotContain("logo-horizontal");
    }

    [Fact]
    public async Task Home_Should_Use_Svg_Og_Image()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("og-default.svg");
    }

    [Theory]
    [InlineData("/favicon.ico")]
    [InlineData("/images/og-default.svg")]
    public async Task Brand_Asset_Should_Be_Served(string path)
    {
        var response = await client.GetAsync(path);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
