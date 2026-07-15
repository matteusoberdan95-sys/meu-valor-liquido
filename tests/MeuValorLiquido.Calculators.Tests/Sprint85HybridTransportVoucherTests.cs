namespace MeuValorLiquido.Calculators.Tests;

public sealed class Sprint85HybridTransportVoucherTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Hybrid_Transport_Should_Use_Period_Cost_When_Lower_Than_Six_Percent()
    {
        var result = service.Calculate(
            "vale-transporte-hibrido",
            new CalculatorInput(4000m, SecondaryAmount: 16m, Months: 8, TransportDiscount: 240m));

        result.IsSuccess.Should().BeTrue();
        result.Value.GrossAmount.Amount.Should().Be(128m);
        result.Value.EstimatedNetAmount.Amount.Should().Be(128m);
        result.Value.LineItems.Single(item => item.Label == "Possivel desconto acima do esperado")
            .Amount.Amount.Should().Be(112m);
    }

    [Fact]
    public void Hybrid_Transport_Should_Cap_Employee_Discount_At_Six_Percent()
    {
        var result = service.Calculate(
            "vale-transporte-hibrido",
            new CalculatorInput(2000m, SecondaryAmount: 12m, Months: 22));

        result.IsSuccess.Should().BeTrue();
        result.Value.GrossAmount.Amount.Should().Be(264m);
        result.Value.EstimatedNetAmount.Amount.Should().Be(120m);
        result.Value.LineItems.Single(item => item.Label == "Parte estimada paga pela empresa")
            .Amount.Amount.Should().Be(144m);
    }

    [Fact]
    public void Hybrid_Transport_Should_Explain_When_Payslip_Discount_Matches()
    {
        var result = service.Calculate(
            "vale-transporte-hibrido",
            new CalculatorInput(5000m, SecondaryAmount: 20m, Months: 10, TransportDiscount: 200m));

        result.IsSuccess.Should().BeTrue();
        result.Value.EstimatedNetAmount.Amount.Should().Be(200m);
        result.Value.Explanation.Should().Contain("bate com a estimativa educativa");
    }
}
