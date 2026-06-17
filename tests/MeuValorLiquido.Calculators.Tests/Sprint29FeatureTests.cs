namespace MeuValorLiquido.Calculators.Tests;

public class FinancingCalculatorTests
{
    [Fact]
    public void Sac_Should_Have_Lower_Total_Interest_Than_Price()
    {
        var price = FinancingCalculator.CalculatePrice(100_000m, 360, 0.009m);
        var sac = FinancingCalculator.CalculateSac(100_000m, 360, 0.009m);

        sac.TotalInterest.Should().BeLessThan(price.TotalInterest);
        sac.FirstPayment.Should().BeGreaterThan(price.Payment);
        sac.LastPayment.Should().BeLessThan(price.Payment);
    }

    [Fact]
    public void Financing_Compare_Mode_Should_Show_Both_Systems()
    {
        var service = CalculatorTestFactory.CreateService();
        var result = service.Calculate(
            "financiamento",
            new CalculatorInput(
                Amount: 100_000m,
                Months: 360,
                Rate: 0.9m,
                FinancingAmortization: FinancingAmortizationSystem.Compare));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "Parcela Price (fixa)");
        result.Value.LineItems.Should().Contain(item => item.Label == "Primeira parcela SAC");
        result.Value.LineItems.Should().Contain(item => item.Label == "Economia de juros (SAC vs Price)");
    }
}

/// <summary>
/// Cenários de referência documentados para regressão (calculadoras externas e casos reais relatados).
/// </summary>
public class CalculatorBenchmarkTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Benchmark_Rescisao_Jan_Oct_2026_Reference_Site()
    {
        var result = service.Calculate("rescisao-clt", new CalculatorInput(
            Amount: 1850m,
            AdmissionDate: new DateOnly(2026, 1, 10),
            TerminationDate: new DateOnly(2026, 10, 10),
            TerminationReason: TerminationReason.Resignation,
            NoticePeriod: NoticePeriodOption.NotFulfilledByEmployee));

        result.Value!.EstimatedNetAmount.Amount.Should().BeApproximately(1853.86m, 2m);
    }

    [Fact]
    public void Benchmark_Rescisao_3_Months_Low_Payout()
    {
        var result = service.Calculate("rescisao-clt", new CalculatorInput(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 3,
            TerminationMonth: 8,
            AdmissionMonth: 6,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false));

        result.Value!.EstimatedNetAmount.Amount.Should().BeApproximately(902.30m, 1m);
    }

    [Fact]
    public void Benchmark_Employer_Death_Should_Include_Fgts_Fine()
    {
        var result = service.Calculate("rescisao-clt", new CalculatorInput(
            Amount: 3000m,
            SecondaryAmount: 20m,
            Months: 24,
            TerminationReason: TerminationReason.EmployerDeath,
            CompletedNoticePeriod: true));

        result.Value!.LineItems.Should().Contain(item => item.Label.StartsWith("Multa FGTS (40%)"));
    }

    [Fact]
    public void Benchmark_Fixed_Term_End_Should_Not_Include_Fgts_Fine()
    {
        var result = service.Calculate("rescisao-clt", new CalculatorInput(
            Amount: 2000m,
            SecondaryAmount: 15m,
            Months: 12,
            TerminationReason: TerminationReason.FixedTermContractEnd,
            CompletedNoticePeriod: true));

        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Multa FGTS" && item.Type == CalculationLineType.Information);
    }
}
