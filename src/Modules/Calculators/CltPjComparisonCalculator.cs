namespace MeuValorLiquido.Modules.Calculators;

public sealed record CltHiddenBenefitsBreakdown(
    decimal FgtsMonthly,
    decimal ThirteenthProvision,
    decimal VacationProvision,
    decimal TotalMonthly,
    decimal TotalAnnual);

public sealed record CltSideBreakdown(
    decimal Gross,
    decimal Inss,
    decimal Irrf,
    decimal Discounts,
    decimal Net,
    CltHiddenBenefitsBreakdown HiddenBenefits);

public sealed record PjSideBreakdown(
    decimal Revenue,
    decimal SimplesTax,
    decimal RevenueAfterSimples,
    decimal ProLabore,
    decimal Inss,
    decimal Irrf,
    decimal Expenses,
    /// <summary>Líquido pessoal: pró-labore após INSS 11% e IRRF.</summary>
    decimal Net,
    /// <summary>Faturamento menos Simples e pró-labore (estimativa retida na empresa).</summary>
    decimal CompanyRetained);

public sealed record CltPjComparisonBreakdown(
    CltSideBreakdown Clt,
    PjSideBreakdown Pj,
    decimal NetDifference,
    decimal EquivalentPjRevenue,
    decimal SimplesRatePercent,
    decimal ProLaboreSharePercent,
    SimplesAnnex SimplesAnnex);

public sealed class CltPjComparisonCalculator
{
    public const decimal DefaultProLaboreShare = 0.28m;
    public const decimal DefaultSimplesRatePercent = 6m;

    private readonly NetSalaryCalculator netSalaryCalculator;
    private readonly IProLaboreInssCalculator proLaboreInssCalculator;
    private readonly IIrrfCalculator irrfCalculator;

    public CltPjComparisonCalculator(
        NetSalaryCalculator netSalaryCalculator,
        IProLaboreInssCalculator proLaboreInssCalculator,
        IIrrfCalculator irrfCalculator)
    {
        this.netSalaryCalculator = netSalaryCalculator;
        this.proLaboreInssCalculator = proLaboreInssCalculator;
        this.irrfCalculator = irrfCalculator;
    }

    public CltPjComparisonBreakdown Compare(CalculatorInput input)
    {
        var proLaboreShare = ResolveProLaboreShare(input.ProLaborePercent);
        var simplesRate = ResolveSimplesRate(input);
        var clt = netSalaryCalculator.Calculate(
            input.Amount,
            input.Dependents,
            input.TransportDiscount);

        var hiddenBenefits = CalculateHiddenBenefits(clt.Gross);
        var cltSide = new CltSideBreakdown(
            clt.Gross,
            clt.Inss,
            clt.Irrf,
            clt.TransportDiscount + clt.MealVoucherDiscount + clt.OtherDiscounts,
            clt.Net,
            hiddenBenefits);

        var pjExpenses = Math.Max(0m, input.OtherDiscounts);
        var equivalentRevenue = SolveEquivalentPjRevenue(cltSide.Net, simplesRate, pjExpenses, proLaboreShare);
        var pjRevenue = input.SecondaryAmount > 0m ? input.SecondaryAmount : equivalentRevenue;
        var pjSide = CalculatePjSide(pjRevenue, simplesRate, pjExpenses, proLaboreShare);

        return new CltPjComparisonBreakdown(
            cltSide,
            pjSide,
            pjSide.Net - cltSide.Net,
            equivalentRevenue,
            simplesRate,
            proLaboreShare * 100m,
            input.SimplesAnnex);
    }

    public static decimal ResolveProLaboreShare(decimal proLaborePercent) =>
        proLaborePercent > 0m ? proLaborePercent / 100m : DefaultProLaboreShare;

    public static decimal ResolveSimplesRate(CalculatorInput input) =>
        input.Rate > 0m
            ? input.Rate
            : SimplesNationalAnnexCatalog.GetSuggestedRatePercent(input.SimplesAnnex);

    public static CltHiddenBenefitsBreakdown CalculateHiddenBenefits(decimal grossSalary)
    {
        if (grossSalary <= 0m)
        {
            return new CltHiddenBenefitsBreakdown(0m, 0m, 0m, 0m, 0m);
        }

        var fgts = grossSalary * 0.08m;
        var thirteenth = grossSalary / 12m;
        var vacation = grossSalary * 4m / 36m;
        var totalMonthly = fgts + thirteenth + vacation;

        return new CltHiddenBenefitsBreakdown(
            fgts,
            thirteenth,
            vacation,
            totalMonthly,
            totalMonthly * 12m);
    }

    public PjSideBreakdown CalculatePjSide(
        decimal revenue,
        decimal simplesRatePercent,
        decimal expenses,
        decimal? proLaboreShare = null)
    {
        var share = proLaboreShare ?? DefaultProLaboreShare;

        if (revenue <= 0m)
        {
            return new PjSideBreakdown(0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m);
        }

        var simples = revenue * simplesRatePercent / 100m;
        var revenueAfterSimples = revenue - simples;
        var proLabore = revenue * share;
        var inss = proLaboreInssCalculator.Calculate(proLabore);
        var irrf = irrfCalculator.Calculate(Math.Max(0m, proLabore - inss), dependents: 0);
        var allocatedExpenses = Math.Min(expenses, proLabore);
        var net = proLabore - inss - irrf - allocatedExpenses;
        var companyRetained = revenueAfterSimples - proLabore;

        return new PjSideBreakdown(
            revenue,
            simples,
            revenueAfterSimples,
            proLabore,
            inss,
            irrf,
            allocatedExpenses,
            net,
            companyRetained);
    }

    public decimal SolveEquivalentPjRevenue(
        decimal targetCltNet,
        decimal simplesRatePercent,
        decimal expenses,
        decimal? proLaboreShare = null)
    {
        if (targetCltNet <= 0m)
        {
            return 0m;
        }

        var share = proLaboreShare ?? DefaultProLaboreShare;
        decimal low = targetCltNet;
        decimal high = targetCltNet * 4m;

        for (var i = 0; i < 48; i++)
        {
            var mid = (low + high) / 2m;
            var pjNet = CalculatePjSide(mid, simplesRatePercent, expenses, share).Net;
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
