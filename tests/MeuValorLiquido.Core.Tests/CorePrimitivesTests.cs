using FluentAssertions;
using MeuValorLiquido.Core.Errors;
using MeuValorLiquido.Core.Results;
using DateRangeValue = MeuValorLiquido.Core.DateRange.DateRange;
using MoneyValue = MeuValorLiquido.Core.Money.Money;
using PercentageValue = MeuValorLiquido.Core.Percentage.Percentage;

namespace MeuValorLiquido.Core.Tests;

public class CorePrimitivesTests
{
    [Fact]
    public void Money_Should_Round_Using_Two_Decimals()
    {
        var money = MoneyValue.From(10.005m);

        money.Amount.Should().Be(10.01m);
        money.ToString().Should().Contain("10,01");
    }

    [Fact]
    public void Percentage_Should_Apply_Rate()
    {
        var percentage = PercentageValue.FromPercent(12.5m);

        percentage.ApplyTo(200m).Should().Be(25m);
    }

    [Fact]
    public void DateRange_Should_Count_Inclusive_Days()
    {
        var range = new DateRangeValue(new DateOnly(2026, 1, 1), new DateOnly(2026, 1, 31));

        range.Days.Should().Be(31);
    }

    [Fact]
    public void Result_Should_Expose_Success_Value()
    {
        var result = Result<int>.Success(42);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(42);
    }

    [Fact]
    public void Result_Should_Expose_Failure_Error()
    {
        var error = new Error("Test.Error", "Erro esperado.");

        var result = Result<int>.Failure(error);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(error);
    }
}
