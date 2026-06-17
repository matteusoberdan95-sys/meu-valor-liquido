namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint54JourneyTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint54JourneyTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Proposta_Salarial_Should_Render_Journey_Next_Steps_With_Shareable_Links()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(4000m, SecondaryAmount: 4800m, TransportDiscount: 200m));
        var html = await client.GetStringAsync(
            $"/calculadoras/proposta-salarial?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("Pr&#xF3;ximo passo");
        html.Should().Contain("Proposta recebida");
        html.Should().Contain("/calculadoras/salario-liquido?r=");
        html.Should().Contain("/calculadoras/pj-vs-clt?r=");
        html.Should().Contain("jornada=proposta-recebida");
        html.Should().Contain("Pr&#xF3;ximo passo:");
    }

    [Fact]
    public async Task Rescisao_Should_Render_Fgts_And_Faq_Journey_Links()
    {
        var input = new CalculatorInput(
            Amount: 4500m,
            Months: 24,
            FgtsBalance: 5000m,
            TerminationReason: TerminationReason.DismissalWithoutCause);
        var token = CalculatorInputShareCodec.Encode(input);
        var html = await client.GetStringAsync(
            $"/calculadoras/rescisao-clt?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("Sa&#xED;da da empresa");
        html.Should().Contain("/calculadoras/fgts?r=");
        html.Should().Contain("/duvidas/multa-fgts-40-porcento");
        html.Should().Contain("jornada=saida-empresa");
    }

    [Fact]
    public async Task Bruto_Necessario_Should_Render_Liquido_And_Salary_Band_Journey_Links()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(
            Amount: 4000m,
            Dependents: 0,
            TransportDiscount: 150m));
        var html = await client.GetStringAsync(
            $"/calculadoras/salario-bruto-necessario?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("L&#xED;quido desejado");
        html.Should().Contain("/calculadoras/salario-liquido?r=");
        html.Should().Contain("/salario-liquido/");
        html.Should().Contain("jornada=liquido-desejado");
    }

    [Fact]
    public async Task Journey_Should_Continue_On_Intermediate_Calculator()
    {
        var proposedInput = new CalculatorInput(4000m, SecondaryAmount: 4800m);
        var liquidoInput = CalculatorJourneyInputMapper.MapForCalculatorStep(
            CalculatorJourneyCatalog.PropostaRecebida,
            "salario-liquido",
            proposedInput,
            null)!;
        var token = CalculatorInputShareCodec.Encode(liquidoInput);
        var html = await client.GetStringAsync(
            $"/calculadoras/salario-liquido?r={Uri.EscapeDataString(token)}&jornada=proposta-recebida");

        html.Should().Contain("Pr&#xF3;ximo passo");
        html.Should().Contain("/calculadoras/pj-vs-clt?r=");
        html.Should().Contain("jornada=proposta-recebida");

        var journeyStart = html.IndexOf("valora-journey-next", StringComparison.Ordinal);
        var journeyEnd = html.IndexOf("Continue explorando", StringComparison.Ordinal);
        journeyStart.Should().BeGreaterThan(0);
        journeyEnd.Should().BeGreaterThan(journeyStart);
        var journeySection = html[journeyStart..journeyEnd];
        journeySection.Should().NotContain("/calculadoras/salario-liquido?r=");
    }
}
