namespace MeuValorLiquido.Modules.Calculators;

public sealed record HoleriteDiscountInput(
    decimal Transport,
    decimal MealVoucher,
    decimal HealthPlan,
    decimal AlimonyAmount,
    decimal AlimonyPercent,
    decimal Other)
{
    public decimal ResolveAlimony(decimal gross)
    {
        if (AlimonyPercent > 0m)
        {
            return decimal.Round(gross * AlimonyPercent / 100m, 2, MidpointRounding.AwayFromZero);
        }

        return AlimonyAmount;
    }
}

public static class HoleriteDiscountMapper
{
    public static HoleriteDiscountInput FromInput(CalculatorInput input, string slug)
    {
        var meal = input.MealVoucherDiscount;
        if (meal <= 0m
            && slug.Equals("salario-bruto-necessario", StringComparison.OrdinalIgnoreCase)
            && input.SecondaryAmount > 0m)
        {
            meal = input.SecondaryAmount;
        }

        return new HoleriteDiscountInput(
            input.TransportDiscount,
            meal,
            input.HealthPlanDiscount,
            input.AlimonyAmount,
            input.AlimonyPercent,
            input.OtherDiscounts);
    }
}
