namespace MeuValorLiquido.Calculators.Tests;
public class TaxCalculatorTests
{
    private readonly InssCalculator inss = new();
    private readonly IrrfCalculator irrf = new();

    [Fact]
    public void Inss_Should_Apply_Progressive_Brackets_For_4000()
    {
        inss.Calculate(4000m).Should().Be(368.60m);
    }

    [Fact]
    public void Inss_Should_Respect_Ceiling_2026()
    {
        inss.Calculate(20_000m).Should().Be(BrTaxTables2026.InssMaximumContribution);
    }

    [Fact]
    public void Irrf_Should_Be_Zero_For_Taxable_Basis_Up_To_5000()
    {
        irrf.Calculate(4498.48m, 0).Should().Be(0m);
        irrf.Calculate(5000m, 0).Should().Be(0m);
    }

    [Fact]
    public void Irrf_Should_Apply_2026_Reduction_For_Low_Income()
    {
        irrf.Calculate(4000m, 0).Should().Be(0m);
    }

    [Fact]
    public void Irrf_Should_Respect_Dependent_Deduction()
    {
        var withoutDependents = irrf.Calculate(9000m, 0);
        var withDependents = irrf.Calculate(9000m, 2);

        withDependents.Should().BeLessThan(withoutDependents);
        withoutDependents.Should().BeGreaterThan(0m);
    }

    [Fact]
    public void TerminationTax_Should_Not_Tax_Vacation()
    {
        var calculator = new TerminationTaxCalculator(inss, irrf);
        var breakdown = calculator.Calculate(1850m, 1387.50m, 0);

        breakdown.InssOnSalaryBalance.Should().Be(142.18m);
        breakdown.InssOnThirteenth.Should().Be(104.06m);
        breakdown.TotalInss.Should().Be(246.24m);
        breakdown.TotalIrrf.Should().Be(0m);
    }

    [Theory]
    [InlineData(1621, 178.31)]
    [InlineData(3000, 330)]
    [InlineData(5000, 550)]
    [InlineData(8475.55, 932.31)]
    [InlineData(15000, 932.31)]
    public void ProLabore_Inss_Should_Be_11_Percent_Capped(decimal proLabore, decimal expected)
    {
        var calculator = new ProLaboreInssCalculator();
        calculator.Calculate(proLabore).Should().Be(expected);
    }

    [Fact]
    public void Net_Salary_5000_Should_Match_Receita_2026_Isenção()
    {
        var inssValue = inss.Calculate(5000m);
        inssValue.Should().Be(501.51m);

        var irrfValue = irrf.Calculate(5000m - inssValue, 0);
        irrfValue.Should().Be(0m);
    }

    [Fact]
    public void ProLabore_5000_Should_Have_Zero_Irrf_After_11_Percent_Inss()
    {
        var proLaboreInss = new ProLaboreInssCalculator();
        var inssValue = proLaboreInss.Calculate(5000m);
        inssValue.Should().Be(550m);

        irrf.Calculate(5000m - inssValue, 0).Should().Be(0m);
    }
}
