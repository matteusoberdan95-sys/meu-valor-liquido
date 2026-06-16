namespace MeuValorLiquido.Calculators.Tests;
public class CalculatorShareTests
{
    [Fact]
    public void ShareCodec_Should_Roundtrip_Input()
    {
        var original = new CalculatorInput(
            Amount: 4500m,
            Dependents: 2,
            TransportDiscount: 200m,
            SecondaryAmount: 50m,
            OtherDiscounts: 100m);

        var token = CalculatorInputShareCodec.Encode(original);
        CalculatorInputShareCodec.TryDecode(token, out var decoded).Should().BeTrue();
        decoded.Should().Be(original);
    }

    [Fact]
    public void ShareText_Should_Include_Net_And_Url()
    {
        var service = CalculatorTestFactory.CreateService();
        var result = service.Calculate("salario-liquido", new CalculatorInput(3000m));
        result.IsSuccess.Should().BeTrue();

        var text = CalculatorShareTextBuilder.Build(
            result.Value!,
            "https://meuvalorliquido.com.br/calculadoras/salario-liquido?r=abc");

        text.Should().Contain("Líquido estimado");
        text.Should().Contain("https://meuvalorliquido.com.br/calculadoras/salario-liquido?r=abc");
        text.Should().Contain("INSS");
    }
}
