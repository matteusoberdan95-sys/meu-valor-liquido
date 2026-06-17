namespace MeuValorLiquido.Calculators.Tests;

public class Sprint31FeatureTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Vacation_Full_Year_Should_Include_Constitutional_Bonus()
    {
        var result = service.Calculate("ferias", new CalculatorInput(Amount: 3000m, Months: 12));

        result.Value!.LineItems.Should().Contain(item => item.Label == "Férias");
        result.Value.LineItems.Should().Contain(item => item.Label == "Adicional de 1/3");
        result.Value.GrossAmount.Amount.Should().Be(4000m);
    }

    [Fact]
    public void Vacation_Proportional_Six_Months_Should_Halve_Base()
    {
        var result = service.Calculate("ferias", new CalculatorInput(Amount: 3000m, Months: 6));

        result.Value!.LineItems.Should().Contain(item => item.Label == "Férias proporcionais");
        result.Value.GrossAmount.Amount.Should().Be(2000m);
    }

    [Fact]
    public void Vacation_With_Allowance_Should_Add_Abono_Line()
    {
        var result = service.Calculate("ferias", new CalculatorInput(
            Amount: 3000m,
            Months: 12,
            SellVacationAllowance: true));

        result.Value!.LineItems.Should().Contain(item => item.Label == "Abono pecuniário");
        result.Value.GrossAmount.Amount.Should().Be(5000m);
    }

    [Fact]
    public void Vacation_Double_Payment_Should_Double_Base_And_Bonus()
    {
        var without = service.Calculate("ferias", new CalculatorInput(Amount: 3000m, Months: 12));
        var withDouble = service.Calculate("ferias", new CalculatorInput(
            Amount: 3000m,
            Months: 12,
            DoubleVacationPayment: true));

        withDouble.Value!.GrossAmount.Amount.Should().Be(without.Value!.GrossAmount.Amount * 2);
        withDouble.Value.LineItems.Should().Contain(item => item.Label == "Férias em dobro (informativo)");
    }

    [Fact]
    public void Vacation_Reduced_20_Days_Should_Lower_Base()
    {
        var result = service.Calculate("ferias", new CalculatorInput(
            Amount: 3000m,
            Months: 12,
            VacationDayOption: VacationDayOption.Reduced20));

        result.Value!.GrossAmount.Amount.Should().BeApproximately(2666.67m, 0.05m);
    }

    [Fact]
    public void Thirteenth_Full_Year_Should_Split_Installments()
    {
        var result = service.Calculate("decimo-terceiro", new CalculatorInput(Amount: 3000m, Months: 12));

        result.Value!.LineItems.Should().Contain(item => item.Label == "1ª parcela (sem INSS/IRRF)");
        result.Value.LineItems.Should().Contain(item => item.Label == "2ª parcela bruta");
        result.Value.GrossAmount.Amount.Should().Be(3000m);
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(2751.40m, 0.01m);
    }

    [Fact]
    public void Thirteenth_Six_Months_Should_Be_Proportional()
    {
        var result = service.Calculate("decimo-terceiro", new CalculatorInput(Amount: 3000m, Months: 6));

        result.Value!.GrossAmount.Amount.Should().Be(1500m);
    }

    [Fact]
    public void Thirteenth_Advance_Should_Reduce_Net()
    {
        var result = service.Calculate("decimo-terceiro", new CalculatorInput(
            Amount: 3000m,
            Months: 12,
            ThirteenthAdvancePaid: 500m));

        result.Value!.LineItems.Should().Contain(item => item.Label == "Adiantamento já pago");
        result.Value.EstimatedNetAmount.Amount.Should().BeApproximately(2251.40m, 0.01m);
    }

    [Fact]
    public void Thirteenth_With_Dates_Should_Keep_Nine_Avos()
    {
        var result = service.Calculate("decimo-terceiro", new CalculatorInput(
            Amount: 1850m,
            AdmissionDate: new DateOnly(2026, 1, 10),
            TerminationDate: new DateOnly(2026, 10, 10)));

        result.Value!.LineItems.Should().Contain(item =>
            item.Label == "Meses considerados" && item.DisplayText == "9");
    }

    [Fact]
    public void Vacation_And_Thirteenth_Should_Support_Grouped_Summary()
    {
        TerminationResultGrouper.SupportsGroupedSummary("ferias").Should().BeTrue();
        TerminationResultGrouper.SupportsGroupedSummary("decimo-terceiro").Should().BeTrue();
    }
}
