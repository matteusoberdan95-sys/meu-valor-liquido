namespace MeuValorLiquido.Calculators.Tests;
public class CalculatorApplicationServiceTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Catalog_Should_Contain_Seventeen_Calculators()
    {
        var catalog = new InMemoryCalculatorCatalogService();

        catalog.GetAll().Should().HaveCount(17);
        catalog.GetBySlug("salario-liquido").Should().NotBeNull();
        catalog.GetBySlug("salario-bruto-necessario").Should().NotBeNull();
        catalog.GetBySlug("proposta-salarial").Should().NotBeNull();
    }

    [Fact]
    public void NetSalary_Should_Return_Extract_With_Discounts()
    {
        var result = service.Calculate("salario-liquido", new CalculatorInput(4000m, Dependents: 0, TransportDiscount: 240m));

        result.IsSuccess.Should().BeTrue();
        result.Value.LineItems.Should().Contain(item => item.Label == "INSS");
        result.Value.LineItems.Should().Contain(item => item.Label == "IRRF");
        result.Value.EstimatedNetAmount.Amount.Should().BeLessThan(4000m);
        result.Value.LegalDisclaimer.Should().Contain("estimado");
    }

    [Fact]
    public void NetSalary_Should_Subtract_Other_Discounts()
    {
        var withoutOtherDiscounts = service.Calculate(
            "salario-liquido",
            new CalculatorInput(4000m, Dependents: 0, TransportDiscount: 240m));
        var withOtherDiscounts = service.Calculate(
            "salario-liquido",
            new CalculatorInput(4000m, Dependents: 0, TransportDiscount: 240m, OtherDiscounts: 100m));

        withoutOtherDiscounts.IsSuccess.Should().BeTrue();
        withOtherDiscounts.IsSuccess.Should().BeTrue();
        withOtherDiscounts.Value.LineItems.Single(item => item.Label == "Outros descontos").Amount.Amount
            .Should().Be(100m);
        withOtherDiscounts.Value.EstimatedNetAmount.Amount
            .Should().Be(withoutOtherDiscounts.Value.EstimatedNetAmount.Amount - 100m);
    }

    [Theory]
    [InlineData("ferias")]
    [InlineData("decimo-terceiro")]
    [InlineData("rescisao-clt")]
    [InlineData("hora-extra")]
    [InlineData("inss")]
    [InlineData("irrf")]
    [InlineData("pj-vs-clt")]
    [InlineData("juros-compostos")]
    [InlineData("financiamento")]
    public void Mvp_Calculator_Should_Return_Result(string slug)
    {
        var input = new CalculatorInput(4000m, SecondaryAmount: 5000m, Months: 12, Rate: 2m, Hours: 10m);

        var result = service.Calculate(slug, input);

        result.IsSuccess.Should().BeTrue();
        result.Value.EstimatedNetAmount.Amount.Should().BeGreaterThan(0m);
    }

    [Fact]
    public void Calculator_Should_Reject_Invalid_Input()
    {
        var result = service.Calculate("salario-liquido", new CalculatorInput(0m));

        result.IsFailure.Should().BeTrue();
        result.Error.Code.Should().Be("Calculators.InvalidInput");
    }

    [Fact]
    public void Calculator_Should_Handle_Random_Positive_Amounts()
    {
        var faker = new Faker();
        var amount = faker.Random.Decimal(1500m, 20000m);

        var result = service.Calculate("inss", new CalculatorInput(amount));

        result.IsSuccess.Should().BeTrue();
        result.Value.EstimatedNetAmount.Amount.Should().BeLessThan(amount);
    }
}
