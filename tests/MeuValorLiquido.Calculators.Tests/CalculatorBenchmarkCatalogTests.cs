namespace MeuValorLiquido.Calculators.Tests;

public sealed class CalculatorBenchmarkCatalogTests
{
    public static IEnumerable<object[]> BenchmarkScenarios =>
        CalculatorBenchmarkCatalog.All.Select(scenario => new object[] { scenario });

    [Theory]
    [MemberData(nameof(BenchmarkScenarios))]
    public void Benchmark_Scenario_Should_Match_Expected_Amounts(CalculatorBenchmarkScenario scenario)
    {
        var service = CalculatorTestFactory.CreateService();

        var result = service.Calculate(scenario.Slug, scenario.Input);

        result.IsSuccess.Should().BeTrue(result.Error.Message);
        result.Value.GrossAmount.Amount.Should().BeApproximately(
            scenario.ExpectedGrossAmount,
            scenario.Tolerance,
            $"{scenario.Slug}/{scenario.Name} gross amount should match the benchmark");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(
            scenario.ExpectedNetAmount,
            scenario.Tolerance,
            $"{scenario.Slug}/{scenario.Name} net amount should match the benchmark");

        foreach (var lineExpectation in scenario.LineExpectations)
        {
            var line = result.Value.LineItems.SingleOrDefault(item =>
                item.Label.Equals(lineExpectation.Label, StringComparison.OrdinalIgnoreCase));

            line.Should().NotBeNull($"{scenario.Slug}/{scenario.Name} should include line '{lineExpectation.Label}'");
            line!.Amount.Amount.Should().BeApproximately(
                lineExpectation.ExpectedAmount,
                lineExpectation.Tolerance,
                $"{scenario.Slug}/{scenario.Name} line '{lineExpectation.Label}' should match the benchmark");
        }
    }

    [Fact]
    public void Benchmark_Catalog_Should_Cover_All_Priority_Slugs()
    {
        foreach (var slug in CalculatorBenchmarkCatalog.PrioritySlugs)
        {
            CalculatorBenchmarkCatalog.ForSlug(slug).Should().HaveCountGreaterThanOrEqualTo(
                CalculatorBenchmarkCatalog.MinimumScenariosPerPrioritySlug,
                $"{slug} should have benchmark coverage");
        }
    }

    [Fact]
    public void Benchmark_Catalog_Should_Have_Source_Metadata()
    {
        CalculatorBenchmarkCatalog.All.Should().OnlyContain(scenario =>
            !string.IsNullOrWhiteSpace(scenario.SourceName)
            && Uri.IsWellFormedUriString(scenario.SourceUrl, UriKind.Absolute)
            && scenario.CalibratedAt.Year >= 2026);
    }
}
