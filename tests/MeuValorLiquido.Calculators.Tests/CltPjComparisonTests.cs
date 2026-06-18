namespace MeuValorLiquido.Calculators.Tests;

public class CltPjComparisonTests
{
    private readonly CltPjComparisonCalculator calculator = CreateCalculator();
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    private static CltPjComparisonCalculator CreateCalculator()
    {
        var inss = new InssCalculator();
        var irrf = new IrrfCalculator();
        var netSalary = new NetSalaryCalculator(inss, irrf);
        return new CltPjComparisonCalculator(netSalary, new ProLaboreInssCalculator(), irrf);
    }

    [Fact]
    public void Compare_Should_Calculate_Equivalent_Pj_Revenue()
    {
        var comparison = calculator.Compare(new CalculatorInput(5000m, Rate: 6m));

        comparison.Clt.Gross.Should().Be(5000m);
        comparison.Clt.Net.Should().BeGreaterThan(0m);
        comparison.EquivalentPjRevenue.Should().BeGreaterThan(comparison.Clt.Net);
        comparison.Pj.Revenue.Should().Be(comparison.EquivalentPjRevenue);
    }

    [Fact]
    public void Compare_With_Custom_Pj_Revenue_Should_Keep_Equivalent_Reference()
    {
        var comparison = calculator.Compare(new CalculatorInput(5000m, SecondaryAmount: 12000m, Rate: 6m));

        comparison.Pj.Revenue.Should().Be(12000m);
        comparison.EquivalentPjRevenue.Should().BeGreaterThan(0m);
        comparison.NetDifference.Should().Be(comparison.Pj.Net - comparison.Clt.Net);
    }

    [Fact]
    public void PjSide_Should_Use_11_Percent_Inss_On_ProLabore()
    {
        var side = calculator.CalculatePjSide(10_000m, 6m, 0m);

        side.ProLabore.Should().Be(2800m);
        side.Inss.Should().Be(308m);
        side.SimplesTax.Should().Be(600m);
        side.RevenueAfterSimples.Should().Be(9400m);
        side.CompanyRetained.Should().Be(6600m);
        side.Net.Should().Be(2492m);
    }

    [Fact]
    public void PjSide_Should_Cap_ProLabore_Inss_At_Ceiling()
    {
        var proLabore = 10_000m;
        var side = calculator.CalculatePjSide(proLabore / CltPjComparisonCalculator.DefaultProLaboreShare, 6m, 0m);

        side.Inss.Should().Be(BrTaxTables2026.ProLaboreInssMaximumContribution);
    }

    [Fact]
    public void Pj_Vs_Clt_Calculator_Should_Return_Detailed_Lines()
    {
        var result = service.Calculate("pj-vs-clt", new CalculatorInput(5000m, SecondaryAmount: 9000m, Rate: 6m));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label == "CLT — líquido estimado");
        result.Value.LineItems.Should().Contain(item => item.Label == "PJ — líquido pessoal (pró-labore)");
        result.Value.LineItems.Should().Contain(item => item.Label == "PJ — INSS 11% (pró-labore)");
        result.Value.LineItems.Should().Contain(item => item.Label == "Faturamento PJ equivalente ao líquido CLT");
    }

    [Fact]
    public void Share_Text_Should_Mention_Equivalent_Revenue()
    {
        var result = service.Calculate("pj-vs-clt", new CalculatorInput(5000m, SecondaryAmount: 9000m, Rate: 6m));
        var text = CalculatorShareTextBuilder.Build(result.Value, "https://example.com");

        text.Should().Contain("PJ equivalente ao CLT");
        text.Should().Contain("CLT líquido");
    }
}
