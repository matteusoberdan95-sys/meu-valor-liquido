namespace MeuValorLiquido.Calculators.Tests;

using MeuValorLiquido.Modules.Calculators.Tax;

public class Sprint30FeatureTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Inss_Should_Show_Bracket_Line()
    {
        var result = service.Calculate("inss", new CalculatorInput(Amount: 4000m));

        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Faixa INSS aplicada" && item.DisplayText!.Contains("12"));
    }

    [Fact]
    public void Irrf_From_Gross_Should_Deduct_Inss_Before_Irrf()
    {
        var result = service.Calculate("irrf", new CalculatorInput(
            Amount: 5000m,
            IrrfFromGrossSalary: true));

        result.Value!.LineItems.Should().Contain(item => item.Label == "INSS (estimado)");
        result.Value.LineItems.Should().Contain(item => item.Label == "Faixa IRRF aplicada");
        result.Value.EstimatedNetAmount.Amount.Should().Be(4498.49m);
    }

    [Fact]
    public void TaxBracketDescriber_Should_Describe_Inss_Ceiling()
    {
        TaxBracketDescriber.DescribeInss(20_000m).Should().Contain("Teto");
    }
}
