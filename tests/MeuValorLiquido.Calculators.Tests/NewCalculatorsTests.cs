namespace MeuValorLiquido.Calculators.Tests;
public class NewCalculatorsTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Catalog_Should_Contain_Seventeen_Calculators()
    {
        new InMemoryCalculatorCatalogService().GetAll().Should().HaveCount(17);
    }

    [Fact]
    public void NetSalary_5000_Should_Have_Correct_Inss_And_Zero_Irrf()
    {
        var result = service.Calculate("salario-liquido", new CalculatorInput(5000m));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Single(i => i.Label == "INSS").Amount.Amount.Should().Be(501.51m);
        result.Value.LineItems.Single(i => i.Label == "IRRF").Amount.Amount.Should().Be(0m);
        result.Value.EstimatedNetAmount.Amount.Should().Be(4498.49m);
    }

    [Fact]
    public void Fgts_Should_Accumulate_Over_Full_Tenure()
    {
        var result = service.Calculate("fgts", new CalculatorInput(3000m, Months: 24));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Single(i => i.Label == "Total depositado no período").Amount.Amount
            .Should().Be(5760m);
    }

    [Fact]
    public void Fgts_Dismissal_Should_Include_40Percent_Fine()
    {
        var result = service.Calculate("fgts", new CalculatorInput(
            3000m,
            Months: 12,
            TerminationReason: TerminationReason.DismissalWithoutCause));

        result.Value!.LineItems.Should().Contain(i => i.Label == "Multa rescisória (40%)");
        result.Value.EstimatedNetAmount.Amount.Should().Be(4032m);
    }

    [Fact]
    public void SalaryConverter_Monthly_3000_Should_Match_Clt_Divisor()
    {
        var result = service.Calculate("conversor-salario", new CalculatorInput(
            3000m,
            SalaryBasis: SalaryConversionBasis.Monthly));

        result.Value!.LineItems.Single(i => i.Label == "Salário por hora").Amount.Amount
            .Should().BeApproximately(13.64m, 0.01m);
        result.Value.LineItems.Single(i => i.Label == "Salário diário (÷ 30)").Amount.Amount
            .Should().Be(100m);
    }

    [Fact]
    public void Mei_Services_Should_Use_2026_Das()
    {
        var result = service.Calculate("simulador-mei", new CalculatorInput(
            5000m,
            MeiActivity: MeiActivityType.Services));

        result.Value!.LineItems.Single(i => i.Label == "DAS MEI mensal").Amount.Amount.Should().Be(86.05m);
        result.Value.EstimatedNetAmount.Amount.Should().Be(4913.95m);
        result.Value.LineItems.Should().Contain(i => i.Label == "Líquido após DAS");
    }

    [Fact]
    public void Mei_Above_Tolerance_Should_Show_Excess_And_Disqualification()
    {
        var result = service.Calculate("simulador-mei", new CalculatorInput(
            900_000m,
            MeiActivity: MeiActivityType.CommerceOrIndustry));

        result.IsSuccess.Should().BeTrue();
        result.Value!.EstimatedNetAmount.Amount.Should().Be(0m);
        result.Value.LineItems.Single(i => i.Label == "Faturamento anual projetado").Amount.Amount.Should().Be(10_800_000m);
        result.Value.LineItems.Single(i => i.Label == "Excedente sobre o teto com tolerância").Amount.Amount.Should().Be(10_702_800m);
        result.Value.LineItems.Should().Contain(i => i.DisplayText == "Desenquadrado do MEI");
        result.Value.LineItems.Should().NotContain(i => i.Label == "Líquido após DAS");
        result.Value.Explanation.Should().Contain("não se aplica");
    }

    [Fact]
    public void Mei_Above_Limit_Within_Tolerance_Should_Show_Excess_Over_Limit()
    {
        var result = service.Calculate("simulador-mei", new CalculatorInput(
            8000m,
            MeiActivity: MeiActivityType.Services));

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Single(i => i.Label == "Excedente sobre o limite anual").Amount.Amount.Should().Be(15_000m);
        result.Value.LineItems.Should().Contain(i => i.DisplayText == "Acima do limite — desenquadramento no ano seguinte");
    }

    [Fact]
    public void EmployeeCost_Should_Include_Encargos()
    {
        var result = service.Calculate("custo-funcionario", new CalculatorInput(4000m, SecondaryAmount: 500m));

        result.Value!.EstimatedNetAmount.Amount.Should().BeGreaterThan(5500m);
        result.Value.LineItems.Should().Contain(i => i.Label == "FGTS (8%)");
    }

    [Fact]
    public void LatePenalty_Should_Apply_Fine_And_Interest()
    {
        var result = service.Calculate("multa-atraso", new CalculatorInput(
            1000m,
            SecondaryAmount: 30m,
            Rate: 1m,
            Hours: 2m));

        result.Value!.EstimatedNetAmount.Amount.Should().Be(1030m);
    }

    [Theory]
    [InlineData("fgts")]
    [InlineData("simulador-mei")]
    [InlineData("custo-funcionario")]
    [InlineData("multa-atraso")]
    [InlineData("conversor-salario")]
    [InlineData("salario-bruto-necessario")]
    public void New_Calculators_Should_Return_Result(string slug)
    {
        var result = service.Calculate(slug, CalculatorInputDefaults.ForSlug(slug));
        result.IsSuccess.Should().BeTrue();
        result.Value!.EstimatedNetAmount.Amount.Should().BeGreaterThan(0m);
    }
}
