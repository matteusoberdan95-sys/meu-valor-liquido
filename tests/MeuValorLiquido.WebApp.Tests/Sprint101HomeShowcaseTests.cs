namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint101HomeShowcaseTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint101HomeShowcaseTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Home_Should_Render_Three_Real_Product_Showcase_Slides()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("data-home-showcase");
        html.Should().Contain("home-showcase-salary");
        html.Should().Contain("home-showcase-payslip");
        html.Should().Contain("home-showcase-assistant");
        html.Should().Contain("Salário líquido");
        html.Should().Contain("Conferir holerite");
        html.Should().Contain("Assistente educativo");
    }

    [Fact]
    public async Task Site_Script_Should_Automatically_Rotate_And_Respect_Reduced_Motion()
    {
        var script = await client.GetStringAsync("/js/site.js");

        script.Should().Contain("data-home-showcase");
        script.Should().Contain("prefers-reduced-motion: reduce");
        script.Should().Contain("window.setInterval");
        script.Should().Contain("6500");
    }
}
