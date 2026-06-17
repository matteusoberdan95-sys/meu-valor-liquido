namespace MeuValorLiquido.WebApp.Tests;
public class RescisaoCalculatorPageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public RescisaoCalculatorPageTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Rescisao_Page_Should_Show_Essential_Fields()
    {
        var html = await client.GetStringAsync("/calculadoras/rescisao-clt");

        html.Should().Contain("Motivo do desligamento");
        html.Should().Contain("Input_AdmissionDate");
        html.Should().Contain("Input_TerminationDate");
        html.Should().Contain("Input_NoticePeriod");
        html.Should().Contain("type=\"date\"");
        html.Should().Contain("Pediu demiss");
        html.Should().Contain("Ajustar detalhes");
        html.Should().Contain("data-mask=\"currency\"");
        html.Should().Contain("calculator-input-masks");
        html.Should().Contain("valora-stitch-rescisao");
        html.Should().Contain("Dados do Contrato");
        (html.Contains("RESUMO DA RESCIS") || html.Contains("RESUMO DA RESCIS&#xC3;O")).Should().BeTrue();
    }
}
