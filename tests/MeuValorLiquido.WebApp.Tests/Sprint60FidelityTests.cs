using FluentAssertions;
using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.WebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MeuValorLiquido.WebApp.Tests;

public sealed class SalarioLiquidoStitchResultBuilderTests
{
    [Fact]
    public void TryBuild_Should_Compute_Percentages_And_Segments()
    {
        var result = new CalculationResult(
            "salario-liquido",
            "Salário líquido",
            Money.From(5000m),
            new[]
            {
                new CalculationLineItem("INSS", Money.From(500m), CalculationLineType.Discount),
                new CalculationLineItem("IRRF", Money.From(300m), CalculationLineType.Discount)
            },
            Money.From(4200m),
            "Explicação",
            "Aviso");

        var viewModel = SalarioLiquidoStitchResultBuilder.TryBuild(result);

        viewModel.Should().NotBeNull();
        viewModel!.NetPercentOfGross.Should().Be(84m);
        viewModel.EffectiveTaxPercent.Should().Be(16m);
        viewModel.DonutSegments.Should().Contain(s => s.Label == "Líquido");
        viewModel.DonutSegments.Should().Contain(s => s.Label == "INSS");
    }

    [Fact]
    public void TryBuild_Should_Return_Null_For_Other_Slugs()
    {
        var result = new CalculationResult(
            "ferias",
            "Férias",
            Money.From(1000m),
            Array.Empty<CalculationLineItem>(),
            Money.From(900m),
            "Explicação",
            "Aviso");

        SalarioLiquidoStitchResultBuilder.TryBuild(result).Should().BeNull();
    }
}

public sealed class Sprint60FidelityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint60FidelityTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Home_Should_Expose_Desktop_Methodology_Section()
    {
        var html = await client.GetStringAsync("/");
        html.Should().Contain("valora-stitch-home-methodology");
        html.Should().Contain("Nossa Metodologia");
        html.Should().Contain("valora-stitch-home-desktop-bento");
    }

    [Fact]
    public async Task SalarioLiquido_Should_Expose_Stitch_Result_Markup()
    {
        var html = await client.GetStringAsync("/calculadoras/salario-liquido");
        html.Should().Contain("valora-stitch-salario");
        html.Should().Contain("Calcular Valor");
    }
}
