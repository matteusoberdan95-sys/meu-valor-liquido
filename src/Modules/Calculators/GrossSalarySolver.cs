namespace MeuValorLiquido.Modules.Calculators;

public sealed record GrossSalaryRange(decimal MinGross, decimal MaxGross, decimal MidGross);

public static class GrossSalarySolver
{
    public const decimal Tolerance = 0.01m;
    private const decimal MaxGross = 1_000_000m;
    private const decimal RangeSearchLimit = 100m;

    public static decimal Solve(
        NetSalaryCalculator calculator,
        decimal targetNet,
        int dependents,
        HoleriteDiscountInput discounts)
    {
        return SolveRange(calculator, targetNet, dependents, discounts).MidGross;
    }

    public static decimal Solve(
        NetSalaryCalculator calculator,
        decimal targetNet,
        int dependents,
        decimal transportDiscount,
        decimal mealVoucherDiscount,
        decimal otherDiscounts)
    {
        return Solve(
            calculator,
            targetNet,
            dependents,
            new HoleriteDiscountInput(transportDiscount, mealVoucherDiscount, 0m, 0m, 0m, otherDiscounts));
    }

    public static GrossSalaryRange SolveRange(
        NetSalaryCalculator calculator,
        decimal targetNet,
        int dependents,
        HoleriteDiscountInput discounts)
    {
        if (targetNet <= 0m)
        {
            return new GrossSalaryRange(0m, 0m, 0m);
        }

        var mid = SolveBinary(calculator, targetNet, dependents, discounts);
        var min = mid;
        var max = mid;

        for (var gross = mid - 0.01m; gross >= mid - RangeSearchLimit && gross > 0m; gross -= 0.01m)
        {
            var net = calculator.Calculate(gross, dependents, discounts).Net;
            if (Math.Abs(net - targetNet) <= Tolerance)
            {
                min = gross;
            }
            else if (net < targetNet - Tolerance)
            {
                break;
            }
        }

        for (var gross = mid + 0.01m; gross <= mid + RangeSearchLimit; gross += 0.01m)
        {
            var net = calculator.Calculate(gross, dependents, discounts).Net;
            if (Math.Abs(net - targetNet) <= Tolerance)
            {
                max = gross;
            }
            else if (net < targetNet - Tolerance)
            {
                break;
            }
        }

        return new GrossSalaryRange(
            decimal.Round(min, 2, MidpointRounding.AwayFromZero),
            decimal.Round(max, 2, MidpointRounding.AwayFromZero),
            mid);
    }

    private static decimal SolveBinary(
        NetSalaryCalculator calculator,
        decimal targetNet,
        int dependents,
        HoleriteDiscountInput discounts)
    {
        var low = targetNet;
        var high = targetNet * 2m;

        while (calculator.Calculate(high, dependents, discounts).Net < targetNet
               && high < MaxGross)
        {
            high *= 2m;
        }

        if (calculator.Calculate(high, dependents, discounts).Net < targetNet)
        {
            return decimal.Round(high, 2, MidpointRounding.AwayFromZero);
        }

        while (high - low > Tolerance)
        {
            var mid = (low + high) / 2m;
            var net = calculator.Calculate(mid, dependents, discounts).Net;
            if (net < targetNet)
            {
                low = mid;
            }
            else
            {
                high = mid;
            }
        }

        return decimal.Round((low + high) / 2m, 2, MidpointRounding.AwayFromZero);
    }
}
