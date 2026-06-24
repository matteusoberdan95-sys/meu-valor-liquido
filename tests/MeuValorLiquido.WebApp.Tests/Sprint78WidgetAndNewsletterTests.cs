namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint78WidgetAndNewsletterTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint78WidgetAndNewsletterTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Widget_Hub_Should_Render_Stitch_Builder()
    {
        var html = await client.GetStringAsync("/widget");

        html.Should().Contain("valora-stitch-widget-hub");
        html.Should().Contain("data-widget-hub");
        html.Should().Contain("mvl-widget-catalog");
        html.Should().Contain("data-widget-chip");
        html.Should().Contain("data-copy-widget");
        html.Should().Contain("sem anúncios");
        html.Should().Contain("Montar snippet");
        html.Should().Contain("salario-liquido");
        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Widget_Script_Should_Support_Catalog_Picker()
    {
        var script = await client.GetStringAsync("/js/widget-hub.js");

        script.Should().Contain("mvl-widget-catalog");
        script.Should().Contain("data-widget-chip");
        script.Should().Contain("iframeCode");
    }

    [Fact]
    public async Task Newsletter_Page_Should_Show_Weekly_Preview()
    {
        var html = await client.GetStringAsync("/newsletter");

        html.Should().Contain("valora-stitch-newsletter-preview");
        html.Should().Contain("curadoria semanal");
        html.Should().Contain("terça-feira");
        html.Should().Contain("Calculadora em foco");
        html.Should().Contain("/calculadoras/salario-liquido");
    }

    [Fact]
    public void WeeklyNewsletterTemplate_Should_Define_Sample_Blocks()
    {
        var blocks = WeeklyNewsletterTemplateCatalog.GetSampleIssue();

        blocks.Should().HaveCountGreaterThanOrEqualTo(5);
        blocks.Should().Contain(b => b.Label == "Calculadora em foco");
        WeeklyNewsletterTemplateCatalog.CadenceLabel.Should().Contain("terça-feira");
    }

    [Fact]
    public async Task Embed_Calculator_Should_Still_Be_Without_Ads()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido?embed=1");

        html.Should().Contain("valora-embed-body");
        html.Should().NotContain("ad-slot");
    }
}
