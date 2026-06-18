namespace MeuValorLiquido.Calculators.Tests;

using MeuValorLiquido.Modules.Calculators.Tax;

public sealed class Sprint67UnemploymentInsuranceTests
{
    private readonly ICalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Benchmark_Catalog_Should_Have_Five_Seguro_Desemprego_Scenarios()
    {
        CalculatorBenchmarkCatalog.ForSlug("seguro-desemprego")
            .Should()
            .HaveCountGreaterThanOrEqualTo(5);
    }

    [Fact]
    public void Dismissal_Without_Cause_Should_Estimate_Five_Installments_At_Medium_Salary()
    {
        var result = service.Calculate(
            "seguro-desemprego",
            new CalculatorInput(
                3000m,
                SecondaryAmount: 2900m,
                SalaryAverageSupplement: 2800m,
                Months: 24,
                MonthsWorkedInYear: 12,
                TerminationReason: TerminationReason.DismissalWithoutCause));

        result.IsSuccess.Should().BeTrue();
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(10583.30m, 0.02m);
        result.Value.LineItems.Should().Contain(item =>
            item.Label.Equals("Quantidade de parcelas", StringComparison.OrdinalIgnoreCase)
            && item.DisplayText == "5");
    }

    [Fact]
    public void Resignation_Should_Be_Ineligible_With_Zero_Total()
    {
        var result = service.Calculate(
            "seguro-desemprego",
            new CalculatorInput(
                3000m,
                Months: 24,
                MonthsWorkedInYear: 12,
                TerminationReason: TerminationReason.Resignation));

        result.IsSuccess.Should().BeTrue();
        result.Value.EstimatedNetAmount.Amount.Should().Be(0m);
        result.Value.LineItems.Should().Contain(item =>
            item.Label.Equals("Elegibilidade", StringComparison.OrdinalIgnoreCase)
            && item.DisplayText!.Contains("sem direito", StringComparison.OrdinalIgnoreCase));
    }

    [Theory]
    [InlineData(1621, 1621.00)]
    [InlineData(2000, 1621.00)]
    [InlineData(2900, 2116.66)]
    [InlineData(4000, 2518.65)]
    public void Monthly_Benefit_Should_Follow_Mte_2026_Table(double averageSalary, double expectedBenefit)
    {
        BrUnemploymentInsuranceTables2026.CalculateMonthlyBenefit((decimal)averageSalary)
            .Should()
            .BeApproximately((decimal)expectedBenefit, 0.02m);
    }
}
