using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.WebApp.Infrastructure;

namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint63CalculatorFidelityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint63CalculatorFidelityTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Rescisao_Page_Should_Render_Stitch_Shell()
    {
        var html = await client.GetStringAsync("/calculadoras/rescisao-clt");

        html.Should().Contain("valora-stitch-rescisao");
        html.Should().Contain("saldo de salário, aviso prévio, férias, 13º");
        html.Should().Contain("Dados do Contrato");
    }

    [Fact]
    public async Task Rescisao_With_Result_Should_Render_Multi_Card_Breakdown()
    {
        var input = new CalculatorInput(
            3000m,
            SecondaryAmount: 15m,
            Months: 12,
            TerminationReason: TerminationReason.DismissalWithoutCause);
        var token = CalculatorInputShareCodec.Encode(input);
        var html = await client.GetStringAsync($"/calculadoras/rescisao-clt?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("valora-stitch-rescisao-breakdown");
        html.Should().Contain("Verbas rescisórias");
        html.Should().Contain("valora-stitch-rescisao-line-card");
        html.Should().Contain("valora-stitch-rescisao-summary");
        html.Should().Contain("Total líquido estimado");
        html.Should().Contain("Dica importante");
        html.Should().NotContain("data-result-view-root");
    }

    [Fact]
    public async Task PjVsClt_Page_Should_Render_Wizard_Visuals()
    {
        var html = await client.GetStringAsync("/calculadoras/pj-vs-clt");

        html.Should().Contain("Wizard de Comparação");
        html.Should().Contain("valora-stitch-cltpj-factor-r");
        html.Should().Contain("Factor-R");
        html.Should().Contain("data-pj-step=\"4\"");
        html.Should().Contain("3. Benefícios");
    }

    [Fact]
    public async Task PjVsClt_With_Result_Should_Render_Annual_Verdict()
    {
        var input = new CalculatorInput(
            5000m,
            SecondaryAmount: 6200m,
            Rate: 6m,
            ProLaborePercent: 28m,
            SimplesAnnex: SimplesAnnex.AnnexThree);
        var token = CalculatorInputShareCodec.Encode(input);
        var html = await client.GetStringAsync($"/calculadoras/pj-vs-clt?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("valora-stitch-cltpj-annual-compare");
        html.Should().Contain("Líquido anual PJ");
        html.Should().Contain("Detalhamento CLT");
        html.Should().Contain("Detalhamento PJ");
        html.Should().Contain("valora-stitch-cltpj-verdict-hero-badge");
        html.Should().Contain("Veredito:");
    }
}

public sealed class RescisaoStitchResultBuilderTests
{
    [Fact]
    public void TryBuild_Should_Group_Fgts_Cards()
    {
        var result = new CalculationResult(
            "rescisao-clt",
            "Rescisão CLT",
            new Money(10000m),
            [
                new CalculationLineItem("Saldo de salário", new Money(1000m), CalculationLineType.Income),
                new CalculationLineItem("INSS", new Money(200m), CalculationLineType.Discount),
                new CalculationLineItem("Saldo FGTS estimado (8% × meses)", new Money(3000m), CalculationLineType.Income),
                new CalculationLineItem("Multa FGTS (40%)", new Money(1200m), CalculationLineType.Income)
            ],
            new Money(8500m),
            "Explicação",
            "Disclaimer");

        var summary = TerminationResultGrouper.Group(result);
        var model = RescisaoStitchResultBuilder.TryBuild(result, summary, null, []);

        model.Should().NotBeNull();
        model!.FgtsBalanceCard!.Amount.Should().Be(3000m);
        model.FgtsPenaltyCard!.Amount.Should().Be(1200m);
        model.FgtsPackageTotal.Should().Be(4200m);
    }
}

public sealed class PjVsCltStitchDisplayBuilderTests
{
    [Fact]
    public void Build_Should_Compute_Advantage_Percent()
    {
        var breakdown = new CltPjComparisonBreakdown(
            new CltSideBreakdown(5000m, 500m, 400m, 0m, 3500m, new CltHiddenBenefitsBreakdown(400m, 416.67m, 555.56m, 1372.23m, 16466.76m)),
            new PjSideBreakdown(6000m, 360m, 5640m, 1680m, 184.8m, 100m, 200m, 1195.2m, 3960m),
            1195.2m - 3500m,
            6000m,
            6m,
            28m,
            SimplesAnnex.AnnexThree);

        var display = PjVsCltStitchDisplayBuilder.Build(breakdown);

        display.CltWins.Should().BeTrue();
        display.PjBarPercent.Should().BeLessThan(display.CltBarPercent);
        display.AdvantagePercent.Should().BeGreaterThan(0m);
    }
}
