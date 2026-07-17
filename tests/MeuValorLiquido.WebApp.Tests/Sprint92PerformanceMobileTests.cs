namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint92PerformanceMobileTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint92PerformanceMobileTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Home_Should_Prioritize_Hero_Image_And_Skip_Adsense_Init_When_Ads_Off()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("home-hero");
        html.Should().Contain("fetchpriority=\"high\"");
        html.Should().Contain("loading=\"eager\"");
        html.Should().NotContain("/js/adsense-init");
        html.Should().Contain("font-loader");
        html.Should().Contain("defer");
    }

    [Fact]
    public async Task Blog_Article_Should_Prioritize_Hero_And_Keep_Body_Without_Js_Dependency()
    {
        var html = await client.GetStringAsync("/blog/o-que-e-salario-liquido");

        html.Should().Contain("fetchpriority=\"high\"");
        html.Should().Contain("loading=\"eager\"");
        html.Should().Contain("valora-stitch-blog-article-body");
        html.Should().Contain("id=\"article-summary\"");
        html.Should().NotContain("/js/adsense-init");
        html.Should().Contain("blog-article-progress");
    }

    [Fact]
    public async Task Site_Css_Should_Define_Touch_Targets_And_Overflow_Guards()
    {
        var html = await client.GetStringAsync("/como-calculamos");
        var match = Regex.Match(html, "href=\"(/css/site\\.[^\"]+\\.css)\"");
        match.Success.Should().BeTrue();

        var css = await client.GetStringAsync(match.Groups[1].Value);

        css.Should().Contain("overflow-x: clip");
        css.Should().Contain("min-width: 2.75rem");
        css.Should().Contain("min-height: 2.75rem");
        css.Should().Contain("aspect-ratio: 1200 / 675");
    }

    [Theory]
    [InlineData("/como-calculamos")]
    [InlineData("/sobre")]
    [InlineData("/politica-editorial")]
    public async Task Editorial_Page_Should_Remain_Readable_In_Html(string path)
    {
        var html = await client.GetStringAsync(path);

        html.Should().Contain("<main");
        html.Should().Contain("<h1");
        html.Length.Should().BeGreaterThan(1500);
    }

    [Fact]
    public async Task Static_Js_Should_Keep_Immutable_Cache_Headers()
    {
        using var response = await client.GetAsync("/js/font-loader.js");

        response.IsSuccessStatusCode.Should().BeTrue();
        response.Headers.CacheControl?.ToString().Should().Contain("immutable");
    }
}
