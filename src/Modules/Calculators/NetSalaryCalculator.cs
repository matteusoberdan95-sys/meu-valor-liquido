using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.Modules.Calculators;

public sealed record NetSalaryBreakdown(
    decimal Gross,
    decimal Inss,
    decimal Irrf,
    decimal TransportDiscount,
    decimal MealVoucherDiscount,
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
        decimal otherDiscounts = 0m)
    {
        var inss = inssCalculator.Calculate(gross);
        var irrf = irrfCalculator.Calculate(gross - inss, dependents);
        var transport = Math.Min(transportDiscount, gross);
        var meal = Math.Min(mealVoucherDiscount, gross);
        var other = Math.Min(otherDiscounts, gross);
        var net = gross - inss - irrf - transport - meal - other;

        return new NetSalaryBreakdown(gross, inss, irrf, transport, meal, other, net);
    }
}
