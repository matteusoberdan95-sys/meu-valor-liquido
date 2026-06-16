namespace MeuValorLiquido.WebApp.Tests;

public class BrandAssetsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public BrandAssetsTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Home_Should_Include_Brand_Logo_And_Favicon()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("logo-horizontal");
        html.Should().Contain("favicon");
        html.Should().Contain("apple-touch-icon");
    }

    [Fact]
    public async Task Home_Should_Use_Png_Og_Image()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("og-default.png");
    }

    [Theory]
    [InlineData("/favicon.ico")]
    [InlineData("/images/brand/logo-horizontal.png")]
    [InlineData("/images/og-default.png")]
    [InlineData("/apple-touch-icon.png")]
    public async Task Brand_Asset_Should_Be_Served(string path)
    {
        var response = await client.GetAsync(path);

        response.IsSuccessStatusCode.Should().BeTrue();
    }
}
