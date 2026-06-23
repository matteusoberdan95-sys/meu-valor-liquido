namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint73RescisaoChecklistTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint73RescisaoChecklistTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Desligamento_Hub_Should_Render_Interactive_Checklist()
    {
        var html = await client.GetStringAsync("/desligamento");

        html.Should().Contain("data-rescisao-checklist");
        html.Should().Contain("Checklist do desligamento");
        html.Should().Contain("Simular verbas rescis");
        html.Should().Contain("/calculadoras/rescisao-clt");
        html.Should().Contain("/calculadoras/fgts");
        html.Should().Contain("/calculadoras/seguro-desemprego");
        html.Should().Contain("rescisao-checklist");
        html.Should().Contain("data-rescisao-checklist-toggle=\"simular-rescisao\"");
    }

    [Fact]
    public async Task Desligamento_Hub_Should_Include_Faq_Rich_Snippets()
    {
        var html = await client.GetStringAsync("/desligamento");

        html.Should().Contain("\"@type\":\"FAQPage\"");
        html.Should().Contain("multa-fgts-40-porcento");
    }

    [Fact]
    public async Task Other_Thematic_Hubs_Should_Not_Render_Rescisao_Checklist()
    {
        var html = await client.GetStringAsync("/negociar-salario");

        html.Should().NotContain("data-rescisao-checklist");
        html.Should().NotContain("Checklist do desligamento");
    }

    [Fact]
    public void Rescisao_Checklist_Catalog_Should_Define_Eight_Steps()
    {
        RescisaoChecklistCatalog.GetAll().Should().HaveCount(8);
        RescisaoChecklistCatalog.GetAll().Should().OnlyHaveUniqueItems(item => item.Id);
    }
}
