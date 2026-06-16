namespace MeuValorLiquido.WebApp.Tests;

public class PerformancePageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public PerformancePageTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Home_Should_Not_Load_Jquery_By_Default()
    {
        var html = await client.GetStringAsync("/");

        html.Should().NotContain("jquery.min");
        html.Should().Contain("/js/site.");
        html.Should().Contain("defer");
        html.Should().Contain("rel=\"preload\"");
    }

    [Fact]
    public async Task Contact_Page_Should_Load_Jquery_For_Validation()
    {
        var html = await client.GetStringAsync("/contato");

        html.Should().Contain("jquery.min");
        html.Should().Contain("jquery.validate");
    }

    [Fact]
    public async Task Sitemap_Should_Expose_Output_Cache_Control()
    {
        using var response = await client.GetAsync("/sitemap.xml");

        response.IsSuccessStatusCode.Should().BeTrue();
        response.Headers.CacheControl?.MaxAge.Should().BeGreaterThan(TimeSpan.Zero);
    }

    [Fact]
    public async Task Site_Css_Should_Have_Long_Lived_Cache_Control()
    {
        var html = await client.GetStringAsync("/");
        var match = Regex.Match(html, "href=\"(/css/site\\.[^\"]+\\.css)\"");
        match.Success.Should().BeTrue();

        using var response = await client.GetAsync(match.Groups[1].Value);

        response.IsSuccessStatusCode.Should().BeTrue();
        response.Headers.CacheControl?.ToString().Should().Contain("immutable");
    }
}
