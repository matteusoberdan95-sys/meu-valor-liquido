using FluentAssertions;
using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.Calculators.Tests;

public class TerminationCalculationTests
{
    private readonly CalculatorApplicationService service = new(
        new InMemoryCalculatorCatalogService(),
        new CalculatorInputValidator(),
        new CalculationEngine(new InssCalculator(), new IrrfCalculator()));

    private static CalculatorInput BaseInput(TerminationReason reason) =>
        new(5000m, SecondaryAmount: 15m, Months: 24, TerminationReason: reason);

    [Fact]
    public void Termination_Dismissal_Should_Include_Fgts_Fine()
    {
        var result = service.Calculate("rescisao-clt", BaseInput(TerminationReason.DismissalWithoutCause));

        result.IsSuccess.Should().BeTrue();
        result.Value.LineItems.Should().Contain(item => item.Label == "Multa FGTS estimada (40%)");
        result.Value.Explanation.Should().Contain("Demissão sem justa causa");
    }

    [Fact]
    public void Termination_Resignation_Should_Not_Include_Fgts_Fine()
    {
        var result = service.Calculate("rescisao-clt", BaseInput(TerminationReason.Resignation));

        result.IsSuccess.Should().BeTrue();
        result.Value.LineItems.Should().NotContain(item => item.Label == "Multa FGTS estimada (40%)");
        result.Value.LineItems.Should().Contain(item => item.Label == "Multa FGTS (40%)");
        result.Value.Explanation.Should().Contain("Pedido de demissão");
    }

    [Fact]
    public void Termination_Dismissal_Should_Pay_More_Than_Resignation()
    {
        var dismissal = service.Calculate("rescisao-clt", BaseInput(TerminationReason.DismissalWithoutCause));
        var resignation = service.Calculate("rescisao-clt", BaseInput(TerminationReason.Resignation));

        dismissal.Value!.EstimatedNetAmount.Amount.Should()
            .BeGreaterThan(resignation.Value!.EstimatedNetAmount.Amount);
    }

    [Fact]
    public void Termination_Resignation_1850_9Months_NoNotice_Should_Deduct_Notice_And_Be_Realistic()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 9,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Desconto aviso prévio (30 dias)");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(2715.66m, 1m);
        result.Value.EstimatedNetAmount.Amount.Should().BeLessThan(3500m);
    }

    [Fact]
    public void Termination_Resignation_With_Notice_Should_Not_Deduct_Notice()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 9,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: true);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().NotContain(item => item.Label == "Desconto aviso prévio (30 dias)");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(4565.66m, 1m);
    }
}
