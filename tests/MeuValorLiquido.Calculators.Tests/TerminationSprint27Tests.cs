namespace MeuValorLiquido.Calculators.Tests;

public class TerminationDateHelperTests
{
    [Theory]
    [InlineData(2026, 1, 10, 2026, 10, 10, 9)]
    [InlineData(2024, 9, 15, 2025, 7, 20, 11)]
    [InlineData(2026, 6, 1, 2026, 8, 14, 2)]
    public void CalculateTenureMonths_Should_Count_Fifteen_Day_Rule(
        int ay, int am, int ad, int ty, int tm, int td, int expected)
    {
        var months = TerminationDateHelper.CalculateTenureMonths(
            new DateOnly(ay, am, ad),
            new DateOnly(ty, tm, td));

        months.Should().Be(expected);
    }

    [Fact]
    public void ApplyDates_Should_Set_Worked_Days_From_Termination_Day()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            AdmissionDate: new DateOnly(2026, 1, 10),
            TerminationDate: new DateOnly(2026, 10, 10));

        var applied = TerminationDateHelper.ApplyDates(input);

        applied.Months.Should().Be(9);
        applied.SecondaryAmount.Should().Be(10m);
        applied.AdmissionMonth.Should().Be(1);
        applied.TerminationMonth.Should().Be(10);
    }

    [Fact]
    public void CountThirteenthAvos_Should_Use_Fifteen_Day_Rule()
    {
        var avos = TerminationDateHelper.CountThirteenthAvos(
            new DateOnly(2026, 1, 10),
            new DateOnly(2026, 10, 10));

        avos.Should().Be(9);
    }
}

public class TerminationReferenceScenarioTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Reference_Site_Scenario_Jan_To_Oct_Resignation_Should_Match_Expected_Range()
    {
        var input = new CalculatorInput(
            Amount: 1850m,
            AdmissionDate: new DateOnly(2026, 1, 10),
            TerminationDate: new DateOnly(2026, 10, 10),
            TerminationReason: TerminationReason.Resignation,
            NoticePeriod: NoticePeriodOption.NotFulfilledByEmployee,
            CompletedNoticePeriod: false);

        var result = service.Calculate("rescisao-clt", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.EstimatedNetAmount.Amount.Should().BeApproximately(1853.86m, 2m);

        var summary = TerminationResultGrouper.Group(result.Value);
        summary.VerbasTotal.Should().BeApproximately(3854.17m, 2m);
        summary.VerbasTotal.Should().BeGreaterThan(0m);
        summary.DescontosTotal.Should().BeGreaterThan(0m);
        summary.FgtsTotal.Should().Be(0m);
    }
}

public class NoticePeriodOptionTests
{
    [Fact]
    public void Resignation_Not_Fulfilled_Should_Deduct_Notice()
    {
        NoticePeriodResolver.ShouldDeductOnResignation(new CalculatorInput(
            Amount: 1850m,
            NoticePeriod: NoticePeriodOption.NotFulfilledByEmployee)).Should().BeTrue();
    }

    [Fact]
    public void Dismissal_Worked_Should_Not_Pay_Indemnified_Notice()
    {
        NoticePeriodResolver.ShouldPayIndemnifiedNotice(
            TerminationReason.DismissalWithoutCause,
            new CalculatorInput(Amount: 5000m, NoticePeriod: NoticePeriodOption.Worked)).Should().BeFalse();
    }
}
