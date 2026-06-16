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
        return new CltPjComparisonCalculator(netSalary, inss, irrf);
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
    public void Pj_Vs_Clt_Calculator_Should_Return_Detailed_Lines()
    {
        var result = service.Calculate("pj-vs-clt", new CalculatorInput(5000m, SecondaryAmount: 9000m, Rate: 6m));

        result.IsSuccess.Should().BeTrue();
        result.Value.LineItems.Should().Contain(item => item.Label == "CLT — líquido estimado");
        result.Value.LineItems.Should().Contain(item => item.Label == "PJ — líquido pessoal estimado");
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
