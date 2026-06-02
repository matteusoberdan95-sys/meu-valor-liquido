using FluentAssertions;
using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.Calculators.Tests;

public class TaxCalculatorTests
{
    private readonly InssCalculator inss = new();
    private readonly IrrfCalculator irrf = new();

    [Fact]
    public void Inss_Should_Apply_Progressive_Brackets_For_4000()
    {
        inss.Calculate(4000m).Should().Be(373.41m);
    }

    [Fact]
    public void Irrf_Should_Respect_Dependent_Deduction()
    {
        var withoutDependents = irrf.Calculate(3500m, 0);
        var withDependents = irrf.Calculate(3500m, 2);

        withDependents.Should().BeLessThan(withoutDependents);
    }
}
