namespace MeuValorLiquido.Modules.Calculators;

public sealed record NetSalaryBreakdown(
    decimal Gross,
    decimal Inss,
    decimal Irrf,
    decimal TransportDiscount,
    decimal MealVoucherDiscount,
    decimal HealthPlanDiscount,
    decimal AlimonyDiscount,
    decimal OtherDiscounts,
    decimal Net);

public sealed class NetSalaryCalculator
{
    private readonly IInssCalculator inssCalculator;
    private readonly IIrrfCalculator irrfCalculator;

    public NetSalaryCalculator(IInssCalculator inssCalculator, IIrrfCalculator irrfCalculator)
    {
        this.inssCalculator = inssCalculator;
        this.irrfCalculator = irrfCalculator;
    }

    public NetSalaryBreakdown Calculate(
        decimal gross,
        int dependents,
        decimal transportDiscount,
        decimal mealVoucherDiscount = 0m,
        decimal healthPlanDiscount = 0m,
        decimal alimonyAmount = 0m,
        decimal alimonyPercent = 0m,
        decimal otherDiscounts = 0m)
    {
        var discounts = new HoleriteDiscountInput(
            transportDiscount,
            mealVoucherDiscount,
            healthPlanDiscount,
            alimonyAmount,
            alimonyPercent,
            otherDiscounts);

        return Calculate(gross, dependents, discounts);
    }

    public NetSalaryBreakdown Calculate(decimal gross, int dependents, HoleriteDiscountInput discounts)
    {
        var inss = inssCalculator.Calculate(gross);
        var irrf = irrfCalculator.Calculate(gross - inss, dependents);
        var alimony = discounts.ResolveAlimony(gross);
        var transport = Math.Min(discounts.Transport, gross);
        var meal = Math.Min(discounts.MealVoucher, gross);
        var health = Math.Min(discounts.HealthPlan, gross);
        var other = Math.Min(discounts.Other, gross);
        var net = gross - inss - irrf - transport - meal - health - alimony - other;

        return new NetSalaryBreakdown(
            gross,
            inss,
            irrf,
            transport,
            meal,
            health,
            alimony,
            other,
            net);
    }
}
