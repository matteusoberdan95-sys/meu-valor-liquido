namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint76PanelCompareTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint76PanelCompareTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task MeuPainel_Should_Render_Compare_Section()
    {
        var html = await client.GetStringAsync("/meu-painel");

        html.Should().Contain("data-local-panel-compare");
        html.Should().Contain("data-local-panel-compare-content");
        html.Should().Contain("Comparar cen");
        html.Should().Contain("data-local-panel-page");

        var siteScript = await client.GetStringAsync("/js/site.js");

        siteScript.Should().Contain("/js/local-panel.js");
        siteScript.Should().Contain("[data-local-panel-save], [data-local-panel-page], [data-local-panel-count]");
    }

    [Fact]
    public async Task LocalPanel_Script_Should_Support_Compare_Selection()
    {
        var script = await client.GetStringAsync("/js/local-panel.js");

        script.Should().Contain("mvl-local-panel-v1");
        script.Should().Contain("renderPanelCompare");
        script.Should().Contain("data-compare-select");
        script.Should().Contain("valora-stitch-panel-compare-grid");
        script.Should().Contain("netAmountValue");
        script.Should().Contain("parseMoney");
        script.Should().Contain("toggleCompareSelection");
    }

    [Fact]
    public async Task LocalPanel_Script_Should_Expose_Compare_Api()
    {
        var script = await client.GetStringAsync("/js/local-panel.js");

        script.Should().Contain("renderPanelCompare");
        script.Should().Contain("clearCompareSelection");
        script.Should().Contain("compareSelection");
    }
}
