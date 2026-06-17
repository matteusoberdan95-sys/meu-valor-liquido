namespace MeuValorLiquido.Calculators.Tests;

public sealed class Sprint55TerminationTests
{
    private readonly ICalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Benchmark_Catalog_Should_Have_At_Least_15_Rescisao_Scenarios()
    {
        CalculatorBenchmarkCatalog.ForSlug("rescisao-clt")
            .Should()
            .HaveCountGreaterThanOrEqualTo(CalculatorBenchmarkCatalog.MinimumTerminationBenchmarkScenarios);
    }

    [Fact]
    public void Dismissal_Without_Cause_Should_Show_Unemployment_Insurance_As_Information_Line()
    {
        var result = service.Calculate(
            "rescisao-clt",
            new CalculatorInput(3000m, SecondaryAmount: 15m, Months: 12, TerminationReason: TerminationReason.DismissalWithoutCause));

        result.IsSuccess.Should().BeTrue();
        var line = result.Value.LineItems.Single(item =>
            item.Label.Equals("Seguro-desemprego", StringComparison.OrdinalIgnoreCase));
        line.Type.Should().Be(CalculationLineType.Information);
        line.DisplayText.Should().Be(UnemploymentInsuranceInfo.RescisaoDisplayText);
        line.Amount.Amount.Should().Be(0m);
    }

    [Fact]
    public void Rescisao_Should_Deduct_Thirteenth_Advance_From_Net()
    {
        var withoutAdvance = service.Calculate(
            "rescisao-clt",
            new CalculatorInput(4000m, SecondaryAmount: 15m, Months: 10, TerminationMonth: 10, TerminationReason: TerminationReason.DismissalWithoutCause));
        var withAdvance = service.Calculate(
            "rescisao-clt",
            new CalculatorInput(4000m, SecondaryAmount: 15m, Months: 10, TerminationMonth: 10, ThirteenthAdvancePaid: 2000m, TerminationReason: TerminationReason.DismissalWithoutCause));

        withoutAdvance.IsSuccess.Should().BeTrue();
        withAdvance.IsSuccess.Should().BeTrue();

        withAdvance.Value.EstimatedNetAmount.Amount
            .Should()
            .BeApproximately(withoutAdvance.Value.EstimatedNetAmount.Amount - 2000m, 0.02m);

        withAdvance.Value.LineItems.Should().Contain(item =>
            item.Label.Equals("Adiantamento do 13º já pago", StringComparison.OrdinalIgnoreCase)
            && item.Type == CalculationLineType.Discount);
    }

    [Fact]
    public void Rescisao_Should_Add_Salary_Supplement_To_Proportional_Benefits()
    {
        var baseline = service.Calculate(
            "rescisao-clt",
            new CalculatorInput(3000m, SecondaryAmount: 15m, Months: 12, TerminationReason: TerminationReason.DismissalWithoutCause));
        var withSupplement = service.Calculate(
            "rescisao-clt",
            new CalculatorInput(3000m, SalaryAverageSupplement: 600m, SecondaryAmount: 15m, Months: 12, TerminationReason: TerminationReason.DismissalWithoutCause));

        baseline.IsSuccess.Should().BeTrue();
        withSupplement.IsSuccess.Should().BeTrue();

        withSupplement.Value.GrossAmount.Amount.Should().BeGreaterThan(baseline.Value.GrossAmount.Amount);
        withSupplement.Value.LineItems.Should().Contain(item =>
            item.Label.Contains("Média salarial complementar", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Vacation_Proportional_Avos_Should_Use_15_Day_Rule_With_Dates()
    {
        var avos = TerminationDateHelper.CountVacationProportionalAvos(
            new DateOnly(2024, 3, 20),
            new DateOnly(2026, 6, 5));

        avos.Should().Be(2);

        var benefits = TerminationBenefitCalculator.Calculate(new CalculatorInput(
            4000m,
            AdmissionDate: new DateOnly(2024, 3, 20),
            TerminationDate: new DateOnly(2026, 6, 5),
            TerminationReason: TerminationReason.Resignation));

        benefits.ProportionalVacationMonths.Should().Be(2);
    }
}
