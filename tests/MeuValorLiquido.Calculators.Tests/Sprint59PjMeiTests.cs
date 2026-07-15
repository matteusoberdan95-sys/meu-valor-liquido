namespace MeuValorLiquido.Calculators.Tests;

public sealed class Sprint59PjMeiTests
{
  private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

  [Fact]
  public void PjVsClt_Should_Use_Simples_Annex_Suggested_Rate_When_Rate_Is_Zero()
  {
    var result = service.Calculate(
      "pj-vs-clt",
      new CalculatorInput(5000m, SimplesAnnex: SimplesAnnex.AnnexFive));

    result.IsSuccess.Should().BeTrue();
    result.Value!.LineItems.Should().Contain(item =>
      item.Label.Contains("Simples Nacional", StringComparison.Ordinal)
      && item.Label.Contains("15", StringComparison.Ordinal));
  }

  [Fact]
  public void PjVsClt_Should_Show_ClT_Hidden_Benefits_Lines()
  {
    var result = service.Calculate("pj-vs-clt", new CalculatorInput(5000m, SecondaryAmount: 9000m, Rate: 6m));

    result.IsSuccess.Should().BeTrue();
    result.Value!.LineItems.Should().Contain(item => item.Label == "CLT — custo oculto FGTS (8%/mês)");
    result.Value.LineItems.Should().Contain(item => item.Label == "CLT — benefícios ocultos (mês)");
    result.Value.LineItems.Should().Contain(item => item.Label == "CLT — benefícios ocultos (ano)");
  }

  [Fact]
  public void PjVsClt_Should_Respect_Custom_ProLabore_Percent()
  {
    var result = service.Calculate(
      "pj-vs-clt",
      new CalculatorInput(6000m, SecondaryAmount: 10000m, Rate: 6m, ProLaborePercent: 35m));

    result.IsSuccess.Should().BeTrue();
    result.Value!.LineItems.Should().Contain(item =>
      item.Label == "PJ — pró-labore (35%)" && item.Amount.Amount == 3500m);
  }

  [Fact]
  public void Mei_Should_Project_Annual_Revenue_From_Accumulated_And_Monthly()
  {
    var result = service.Calculate(
      "simulador-mei",
      new CalculatorInput(5000m, MeiAnnualAccumulated: 50000m, MeiActivity: MeiActivityType.Services));

    result.IsSuccess.Should().BeTrue();
    var projected = MeiAnnualRevenueProjector.ProjectAnnualRevenue(
      5000m,
      50000m,
      DateOnly.FromDateTime(DateTime.UtcNow));
    result.Value!.LineItems.Should().Contain(item =>
      item.Label == "Faturamento anual projetado" && item.Amount.Amount == projected);
    result.Value.LineItems.Should().Contain(item => item.Label == "Faturamento já acumulado no ano");
  }

  [Fact]
  public void Benchmark_Catalog_Should_Have_Six_PjVsClt_Scenarios()
  {
    CalculatorBenchmarkCatalog.ForSlug("pj-vs-clt")
      .Should()
      .HaveCountGreaterThanOrEqualTo(CalculatorBenchmarkCatalog.MinimumPjVsCltBenchmarkScenarios);
  }

  [Fact]
  public void Hidden_Benefits_Should_Match_Employee_Cost_Provisions()
  {
    var gross = 4000m;
    var hidden = CltPjComparisonCalculator.CalculateHiddenBenefits(gross);

    hidden.FgtsMonthly.Should().Be(320m);
    hidden.ThirteenthProvision.Should().BeApproximately(gross / 12m, 0.01m);
    hidden.VacationProvision.Should().BeApproximately(gross * 4m / 36m, 0.01m);
    hidden.TotalMonthly.Should().Be(hidden.FgtsMonthly + hidden.ThirteenthProvision + hidden.VacationProvision);
  }
}
