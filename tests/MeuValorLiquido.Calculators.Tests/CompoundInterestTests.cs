namespace MeuValorLiquido.Calculators.Tests;

public class CompoundInterestTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Compound_Interest_Should_Match_Standard_Formula()
    {
        var result = service.Calculate(
            "juros-compostos",
            new CalculatorInput(1000m, Months: 12, Rate: 3m));

        result.IsSuccess.Should().BeTrue();

        var expectedFinal = 1000m * (decimal)Math.Pow(1.03, 12);
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(expectedFinal, 0.01m);

        var juros = result.Value.LineItems.Single(item => item.Label == "Juros acumulados");
        juros.Amount.Amount.Should().BeApproximately(expectedFinal - 1000m, 0.01m);
    }

    [Fact]
    public void Compound_Interest_Should_Display_Months_And_Rate_As_Non_Money()
    {
        var result = service.Calculate(
            "juros-compostos",
            new CalculatorInput(1000m, Months: 12, Rate: 3m));

        result.IsSuccess.Should().BeTrue();

        result.Value.LineItems.Should().Contain(item =>
            item.Label == "Meses" && item.DisplayText == "12");
        result.Value.LineItems.Should().Contain(item =>
            item.Label == "Taxa mensal (%)" && item.DisplayText == "3%");
    }
}
