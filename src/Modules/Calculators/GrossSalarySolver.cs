namespace MeuValorLiquido.Modules.Calculators;

public static class GrossSalarySolver
{
    public const decimal Tolerance = 0.01m;
    private const decimal MaxGross = 1_000_000m;

    public static decimal Solve(
        NetSalaryCalculator calculator,
        decimal targetNet,
        int dependents,
        decimal transportDiscount,
        decimal mealVoucherDiscount,
        decimal otherDiscounts)
    {
        if (targetNet <= 0m)
        {
            return 0m;
        }

        var low = targetNet;
        var high = targetNet * 2m;

        while (calculator.Calculate(high, dependents, transportDiscount, mealVoucherDiscount, otherDiscounts).Net < targetNet
               && high < MaxGross)
        {
            high *= 2m;
        }

        if (calculator.Calculate(high, dependents, transportDiscount, mealVoucherDiscount, otherDiscounts).Net < targetNet)
        {
            return high;
        }

        while (high - low > Tolerance)
        {
            var mid = (low + high) / 2m;
            var net = calculator.Calculate(mid, dependents, transportDiscount, mealVoucherDiscount, otherDiscounts).Net;
            if (net < targetNet)
            {
                low = mid;
            }
            else
            {
                high = mid;
            }
        }

        return Math.Round((low + high) / 2m, 2, MidpointRounding.AwayFromZero);
    }
}
