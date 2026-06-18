namespace MeuValorLiquido.WebApp.Tests;

using MeuValorLiquido.WebApp.Data;

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
        html.Should().Contain("/images/brand/logo-horizontal");
        html.Should().Contain("Meu Valor Líquido");
        html.Should().Contain("favicon");
        html.Should().Contain("apple-touch-icon");
        html.Should().Contain("/images/brand/logo-icon");
    }

    [Fact]
    public async Task Footer_Should_Use_Stacked_Logo()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("/images/brand/logo-stacked");
        html.Should().Contain("valora-brand-mark--footer");
        html.Should().Contain("valora-brand-logo--footer");
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
    [InlineData("/images/brand/logo-horizontal.png")]
    [InlineData("/images/brand/logo-stacked.png")]
    [InlineData("/images/brand/logo-icon.png")]
    [InlineData("/images/hero/home-hero.webp")]
    [InlineData("/images/blog/o-que-e-salario-liquido.webp")]
    public async Task Brand_Asset_Should_Be_Served(string path)
    {
        var response = await client.GetAsync(path);

        response.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task All_Blog_Posts_Should_Serve_Hero_Images()
    {
        foreach (var post in BlogArticleSeedData.GetAll())
        {
            var response = await client.GetAsync($"/images/blog/{post.Slug}.webp");
            response.IsSuccessStatusCode.Should().BeTrue($"hero image missing for {post.Slug}");
        }
    }
}
