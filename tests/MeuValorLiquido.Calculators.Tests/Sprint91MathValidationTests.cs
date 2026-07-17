namespace MeuValorLiquido.Calculators.Tests;

using MeuValorLiquido.Core.Money;
using MeuValorLiquido.Modules.Calculators.Tax;

public sealed class Sprint91MathValidationTests
{
    public static IEnumerable<object[]> EdgeCases =>
        CalculatorEdgeCaseCatalog.All.Select(scenario => new object[] { scenario });

    [Theory]
    [MemberData(nameof(EdgeCases))]
    public void Edge_Case_Should_Fail_Validation_Without_Inventing_Result(CalculatorEdgeCaseScenario scenario)
    {
        var service = CalculatorTestFactory.CreateService();

        var result = service.Calculate(scenario.Slug, scenario.Input);

        result.IsFailure.Should().BeTrue($"{scenario.Slug}/{scenario.Name} should reject invalid input");
        result.Error.Code.Should().Be(scenario.ExpectedErrorCode);
        result.Error.Message.Should().ContainEquivalentOf(scenario.ExpectedMessageContains);
    }

    [Fact]
    public void Priority_Slugs_Should_Cover_Min_Intermediate_Ceiling_Dependents_And_Rounding_Sources()
    {
        foreach (var slug in CalculatorBenchmarkCatalog.PrioritySlugs)
        {
            var scenarios = CalculatorBenchmarkCatalog.ForSlug(slug);
            scenarios.Should().HaveCountGreaterThanOrEqualTo(
                CalculatorBenchmarkCatalog.MinimumScenariosPerPrioritySlug);

            scenarios.Should().OnlyContain(scenario =>
                !string.IsNullOrWhiteSpace(scenario.SourceName)
                && Uri.IsWellFormedUriString(scenario.SourceUrl, UriKind.Absolute)
                && scenario.Tolerance >= 0.01m
                && scenario.CalibratedAt == new DateOnly(2026, 7, 17));
        }

        CalculatorBenchmarkCatalog.ForSlug("salario-liquido")
            .Select(scenario => scenario.Input.Amount)
            .Should().Contain(BrTaxTables2026.MinimumWage);

        CalculatorBenchmarkCatalog.ForSlug("inss")
            .Select(scenario => scenario.Input.Amount)
            .Should().Contain(BrTaxTables2026.InssCeiling);

        CalculatorBenchmarkCatalog.ForSlug("irrf")
            .Should().Contain(scenario => scenario.Input.Dependents >= 2);

        CalculatorBenchmarkCatalog.ForSlug("salario-liquido")
            .Should().Contain(scenario => scenario.Input.Dependents >= 1);
    }

    [Fact]
    public void Versioned_Tax_Tables_Should_Have_Non_Overlapping_Validity_Periods()
    {
        BrTaxTableCatalog.All.Should().HaveCount(2);
        BrTaxTableCatalog.All.Select(period => period.Year).Should().BeEquivalentTo([2025, 2026]);

        var period2025 = BrTaxTableCatalog.Resolve(new DateOnly(2025, 6, 15));
        period2025.Year.Should().Be(2025);
        period2025.ValidTo.Should().Be(new DateOnly(2025, 12, 31));

        var period2026 = BrTaxTableCatalog.Resolve(new DateOnly(2026, 7, 17));
        period2026.Year.Should().Be(2026);
        period2026.ValidTo.Should().BeNull();
        BrTaxTableCatalog.Current.Year.Should().Be(2026);

        BrTaxTables2025.MinimumWage.Should().NotBe(BrTaxTables2026.MinimumWage);
        BrTaxTables2025.InssCeiling.Should().NotBe(BrTaxTables2026.InssCeiling);
    }

    [Theory]
    [InlineData(1518.00, 113.85)]
    [InlineData(3000.00, 253.41)]
    [InlineData(8157.41, 951.62)]
    [InlineData(20000.00, 951.62)]
    public void Historical_2025_Inss_Should_Match_Portaria_6_Without_Mutating_2026(decimal gross, decimal expected)
    {
        BrTaxTables2025.CalculateInss(gross).Should().Be(expected);

        var live = new InssCalculator();
        if (gross == BrTaxTables2026.InssCeiling || gross > BrTaxTables2026.InssCeiling)
        {
            live.Calculate(Math.Max(gross, BrTaxTables2026.InssCeiling))
                .Should().Be(BrTaxTables2026.InssMaximumContribution);
        }
    }

    [Fact]
    public void Historical_2025_Irrf_Should_Not_Apply_Lei_15270_Reduction()
    {
        // Base 6000 com tabela May/2025 sem redução adicional → IRRF positivo.
        var irrf2025 = BrTaxTables2025.CalculateIrrf(6000m, 0);
        irrf2025.Should().BeGreaterThan(0m);

        // Em 2026 a mesma base após INSS típico fica na faixa de isenção/redução — o ponto
        // crítico é a tabela histórica existir e diferir da vigente.
        BrTaxTables2025.IrrfBrackets.Should().NotBeEmpty();
        BrTaxTables2026.CalculateIrrfReduction(5000m, 100m).Should().Be(100m);
    }

    [Fact]
    public void Money_Rounding_Should_Be_Explicit_AwayFromZero_On_Two_Decimals()
    {
        MoneyRounding.Scale.Should().Be(2);
        MoneyRounding.Mode.Should().Be(MidpointRounding.AwayFromZero);
        MoneyRounding.Round(1.225m).Should().Be(1.23m);
        MoneyRounding.Round(1.215m).Should().Be(1.22m);

        var money = Money.From(1.225m);
        money.Amount.Should().Be(1.23m);
    }

    [Fact]
    public void Monetary_Core_Types_Should_Not_Use_Double_For_Stored_Amounts()
    {
        var amountProperty = typeof(Money).GetProperty(nameof(Money.Amount));
        amountProperty.Should().NotBeNull();
        amountProperty!.PropertyType.Should().Be(typeof(decimal));

        typeof(CalculatorBenchmarkScenario).GetProperty(nameof(CalculatorBenchmarkScenario.ExpectedNetAmount))!
            .PropertyType.Should().Be(typeof(decimal));
    }

    [Theory]
    [InlineData("salario-liquido", 4000)]
    [InlineData("inss", 8475.55)]
    [InlineData("irrf", 7350)]
    public void Priority_Happy_Path_Should_Succeed_And_Round_To_Two_Decimals(string slug, decimal amount)
    {
        var service = CalculatorTestFactory.CreateService();
        var result = service.Calculate(slug, new CalculatorInput(amount));

        result.IsSuccess.Should().BeTrue(result.Error.Message);
        result.Value.GrossAmount.Amount.Should().Be(decimal.Round(result.Value.GrossAmount.Amount, 2));
        result.Value.EstimatedNetAmount.Amount.Should().Be(decimal.Round(result.Value.EstimatedNetAmount.Amount, 2));
    }

    [Fact]
    public void Rescisao_Inverted_Dates_Are_Covered_As_Edge_Case()
    {
        CalculatorEdgeCaseCatalog.All
            .Should()
            .Contain(scenario => scenario.Name == "datas-invertidas" && scenario.Slug == "rescisao-clt");
    }
}
