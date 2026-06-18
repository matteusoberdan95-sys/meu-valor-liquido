using MeuValorLiquido.Modules.Calculators;

namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record PjVsCltStitchDisplayModel(
    decimal CltAnnualNet,
    decimal PjAnnualNet,
    decimal AdvantagePercent,
    bool PjWins,
    bool CltWins,
    int CltBarPercent,
    int PjBarPercent,
    decimal CltAnnualGross,
    decimal CltTaxesAnnual,
    decimal CltFgtsAnnual,
    decimal PjRevenueAnnual,
    decimal PjTaxesAnnual,
    decimal PjCostsAnnual);

public static class PjVsCltStitchDisplayBuilder
{
    public static PjVsCltStitchDisplayModel Build(CltPjComparisonBreakdown breakdown)
    {
        var cltAnnualNet = breakdown.Clt.Net * 12m;
        var pjAnnualNet = breakdown.Pj.Net * 12m;
        var pjWins = breakdown.NetDifference > 0;
        var cltWins = breakdown.NetDifference < 0;
        var baseNet = cltWins ? pjAnnualNet : cltAnnualNet;
        var advantage = baseNet > 0m
            ? Math.Round(Math.Abs(pjAnnualNet - cltAnnualNet) / baseNet * 100m, 0)
            : 0m;

        var maxAnnual = Math.Max(cltAnnualNet, pjAnnualNet);
        var cltBar = maxAnnual > 0m ? (int)Math.Round(cltAnnualNet / maxAnnual * 100m) : 0;
        var pjBar = maxAnnual > 0m ? (int)Math.Round(pjAnnualNet / maxAnnual * 100m) : 0;

        var cltAnnualGross = breakdown.Clt.Gross * 14m;
        var cltTaxesAnnual = (breakdown.Clt.Inss + breakdown.Clt.Irrf) * 12m;
        var cltFgtsAnnual = breakdown.Clt.HiddenBenefits.FgtsMonthly * 12m;
        var pjRevenueAnnual = breakdown.Pj.Revenue * 12m;
        var pjTaxesAnnual = breakdown.Pj.SimplesTax * 12m;
        var pjCostsAnnual = (breakdown.Pj.Inss + breakdown.Pj.Irrf + breakdown.Pj.Expenses) * 12m;

        return new PjVsCltStitchDisplayModel(
            cltAnnualNet,
            pjAnnualNet,
            advantage,
            pjWins,
            cltWins,
            cltBar,
            pjBar,
            cltAnnualGross,
            cltTaxesAnnual,
            cltFgtsAnnual,
            pjRevenueAnnual,
            pjTaxesAnnual,
            pjCostsAnnual);
    }
}
