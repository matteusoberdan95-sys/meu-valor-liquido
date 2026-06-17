namespace MeuValorLiquido.Calculators.Tests;

public class Sprint28FeatureTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Compound_Interest_With_Monthly_Contribution_Should_Exceed_Principal_Only()
    {
        var withContribution = service.Calculate(
            "juros-compostos",
            new CalculatorInput(1000m, SecondaryAmount: 100m, Months: 12, Rate: 1m));

        var principalOnly = service.Calculate(
            "juros-compostos",
            new CalculatorInput(1000m, Months: 12, Rate: 1m));

        withContribution.Value!.EstimatedNetAmount.Amount.Should()
            .BeGreaterThan(principalOnly.Value!.EstimatedNetAmount.Amount);
        withContribution.Value.LineItems.Should().Contain(item => item.Label == "Aporte mensal");
    }

    [Fact]
    public void Thirteenth_With_Dates_Should_Use_Nine_Avos_For_Jan_Oct()
    {
        var result = service.Calculate(
            "decimo-terceiro",
            new CalculatorInput(
                Amount: 1850m,
                AdmissionDate: new DateOnly(2026, 1, 10),
                TerminationDate: new DateOnly(2026, 10, 10)));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Meses considerados" && item.DisplayText == "9");
    }

    [Fact]
    public void Vacation_Proportional_With_Dates_Should_Use_Nine_Months()
    {
        var result = service.Calculate(
            "ferias",
            new CalculatorInput(
                Amount: 3000m,
                AdmissionDate: new DateOnly(2026, 1, 10),
                TerminationDate: new DateOnly(2026, 10, 10)));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Férias proporcionais");
        result.Value.LineItems.Should().Contain(item =>
            item.Label == "Meses de férias proporcionais" && item.DisplayText == "9");
    }

    [Fact]
    public void Probation_Early_End_Should_Include_Fgts_Fine()
    {
        var result = service.Calculate(
            "rescisao-clt",
            new CalculatorInput(
                Amount: 1850m,
                SecondaryAmount: 30m,
                Months: 3,
                TerminationReason: TerminationReason.ProbationContractEarlyEnd,
                CompletedNoticePeriod: true));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label.StartsWith("Multa FGTS"));
    }

    [Fact]
    public void Retirement_Should_Not_Include_Fgts_Fine()
    {
        var result = service.Calculate(
            "rescisao-clt",
            new CalculatorInput(
                Amount: 1850m,
                SecondaryAmount: 30m,
                Months: 24,
                TerminationReason: TerminationReason.Retirement,
                CompletedNoticePeriod: true));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().NotContain(item =>
            item.Label.StartsWith("Multa FGTS") && item.Type == CalculationLineType.Income);
    }
}
