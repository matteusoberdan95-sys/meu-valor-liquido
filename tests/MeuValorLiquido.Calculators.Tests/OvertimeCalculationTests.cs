using FluentAssertions;
using MeuValorLiquido.Modules.Calculators;

namespace MeuValorLiquido.Calculators.Tests;

public class OvertimeCalculationTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Overtime_Should_Use_Weekly_Hours_Divisor()
    {
        var input40h = new CalculatorInput(
            Amount: 1m,
            SecondaryAmount: 4000m,
            Hours: 10m,
            Rate: 50m,
            WeeklyWorkHours: 40);

        var input44h = input40h with { WeeklyWorkHours = 0 };

        var result40 = service.Calculate("hora-extra", input40h);
        var result44 = service.Calculate("hora-extra", input44h);

        result40.Value!.EstimatedNetAmount.Amount.Should()
            .BeGreaterThan(result44.Value!.EstimatedNetAmount.Amount);
    }

    [Fact]
    public void Overtime_Sunday_Should_Apply_Minimum_100Percent()
    {
        var input = new CalculatorInput(
            Amount: 20m,
            Hours: 5m,
            Rate: 50m,
            OvertimeShiftType: OvertimeShiftType.SundayOrHoliday);

        var result = service.Calculate("hora-extra", input);

        result.IsSuccess.Should().BeTrue();
        result.Value!.LineItems.Should().Contain(item => item.Label.Contains("100%"));
    }

    [Fact]
    public void Overtime_CollectiveAgreement_Should_Use_Custom_Rate()
    {
        var standard = new CalculatorInput(Amount: 20m, Hours: 10m, Rate: 50m);
        var cct70 = standard with { Rate = 70m };

        var result50 = service.Calculate("hora-extra", standard);
        var result70 = service.Calculate("hora-extra", cct70);

        result70.Value!.EstimatedNetAmount.Amount.Should()
            .BeGreaterThan(result50.Value!.EstimatedNetAmount.Amount);
    }

    [Fact]
    public void Overtime_NightShift_Should_Add_20Percent()
    {
        var weekday = new CalculatorInput(Amount: 20m, Hours: 10m, Rate: 50m);
        var night = weekday with { OvertimeShiftType = OvertimeShiftType.NightWeekday };

        var resultDay = service.Calculate("hora-extra", weekday);
        var resultNight = service.Calculate("hora-extra", night);

        resultNight.Value!.EstimatedNetAmount.Amount.Should()
            .BeGreaterThan(resultDay.Value!.EstimatedNetAmount.Amount);
    }
}
