namespace MeuValorLiquido.Modules.Calculators;

public sealed record CltSideBreakdown(
    decimal Gross,
    decimal Inss,
    decimal Irrf,
    decimal Discounts,
    decimal Net);

public sealed record PjSideBreakdown(
    decimal Revenue,
    decimal SimplesTax,
    decimal ProLabore,
    decimal Inss,
    decimal Irrf,
    decimal Expenses,
    decimal Net);

public sealed record CltPjComparisonBreakdown(
    CltSideBreakdown Clt,
    PjSideBreakdown Pj,
    decimal NetDifference,
    decimal EquivalentPjRevenue,
    decimal SimplesRatePercent,
    decimal ProLaboreSharePercent);

public sealed class CltPjComparisonCalculator
{
    public const decimal ProLaboreShare = 0.28m;
    public const decimal DefaultSimplesRatePercent = 6m;

    private readonly NetSalaryCalculator netSalaryCalculator;
    private readonly IInssCalculator inssCalculator;
    private readonly IIrrfCalculator irrfCalculator;

    public CltPjComparisonCalculator(
        NetSalaryCalculator netSalaryCalculator,
        IInssCalculator inssCalculator,
        IIrrfCalculator irrfCalculator)
    {
        this.netSalaryCalculator = netSalaryCalculator;
        this.inssCalculator = inssCalculator;
        this.irrfCalculator = irrfCalculator;
    }

    public CltPjComparisonBreakdown Compare(CalculatorInput input)
    {
        var simplesRate = input.Rate > 0m ? input.Rate : DefaultSimplesRatePercent;
        var clt = netSalaryCalculator.Calculate(
            input.Amount,
            input.Dependents,
            input.TransportDiscount);

        var cltSide = new CltSideBreakdown(
            clt.Gross,
            clt.Inss,
            clt.Irrf,
            clt.TransportDiscount + clt.MealVoucherDiscount + clt.OtherDiscounts,
            clt.Net);

        var pjExpenses = Math.Max(0m, input.OtherDiscounts);
        var equivalentRevenue = SolveEquivalentPjRevenue(cltSide.Net, simplesRate, pjExpenses);
        var pjRevenue = input.SecondaryAmount > 0m ? input.SecondaryAmount : equivalentRevenue;
        var pjSide = CalculatePjSide(pjRevenue, simplesRate, pjExpenses);

        return new CltPjComparisonBreakdown(
            cltSide,
            pjSide,
            pjSide.Net - cltSide.Net,
            equivalentRevenue,
            simplesRate,
            ProLaboreShare * 100m);
    }

    public PjSideBreakdown CalculatePjSide(decimal revenue, decimal simplesRatePercent, decimal expenses)
    {
        if (revenue <= 0m)
        {
            return new PjSideBreakdown(0m, 0m, 0m, 0m, 0m, 0m, 0m);
        }

        var simples = revenue * simplesRatePercent / 100m;
        var proLabore = revenue * ProLaboreShare;
        var inss = inssCalculator.Calculate(proLabore);
        var irrf = irrfCalculator.Calculate(Math.Max(0m, proLabore - inss), dependents: 0);
        var allocatedExpenses = Math.Min(expenses, proLabore);
        var net = proLabore - inss - irrf - allocatedExpenses;

        return new PjSideBreakdown(revenue, simples, proLabore, inss, irrf, allocatedExpenses, net);
    }

    public decimal SolveEquivalentPjRevenue(decimal targetCltNet, decimal simplesRatePercent, decimal expenses)
    {
        if (targetCltNet <= 0m)
        {
            return 0m;
        }

        decimal low = targetCltNet;
        decimal high = targetCltNet * 4m;

        for (var i = 0; i < 48; i++)
        {
            var mid = (low + high) / 2m;
            var pjNet = CalculatePjSide(mid, simplesRatePercent, expenses).Net;
            if (pjNet < targetCltNet)
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
