namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint72PropostaComparisonTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint72PropostaComparisonTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Proposta_Salarial_Should_Render_Visual_Comparison()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(4000m, SecondaryAmount: 4800m, TransportDiscount: 200m));
        var html = await client.GetStringAsync(
            $"/calculadoras/proposta-salarial?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("valora-stitch-proposta");
        html.Should().Contain("valora-stitch-proposta-compare-grid");
        html.Should().Contain("Salário atual");
        html.Should().Contain("Líquido proposto estimado");
        html.Should().Contain("Ganho real");
        html.Should().Contain("Ganho no bolso");
        html.Should().Contain("valora-stitch-proposta-bar-fill");
    }

    [Fact]
    public async Task Proposta_Salarial_Should_Keep_Share_Journey_And_Pdf()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(4000m, SecondaryAmount: 4800m));
        var html = await client.GetStringAsync(
            $"/calculadoras/proposta-salarial?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("Compartilhar estimativa");
        html.Should().Contain("Baixar PDF");
        html.Should().Contain("Pr&#xF3;ximo passo");
        html.Should().Contain("jornada=proposta-recebida");
    }

    [Fact]
    public async Task Proposta_Salarial_Reduction_Should_Show_Loss_Verdict()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(5000m, SecondaryAmount: 4500m));
        var html = await client.GetStringAsync(
            $"/calculadoras/proposta-salarial?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("valora-stitch-proposta-verdict--loss");
        html.Should().Contain("Redu&#xE7;&#xE3;o");
    }
}
