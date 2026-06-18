namespace MeuValorLiquido.Modules.Calculators;

public static class MeiAnnualRevenueProjector
{
    public static decimal ProjectAnnualRevenue(
        decimal monthlyRevenue,
        decimal annualAccumulated,
        DateOnly? referenceDate = null)
    {
        if (annualAccumulated <= 0m)
        {
            return monthlyRevenue * 12m;
        }

        var date = referenceDate ?? DateOnly.FromDateTime(DateTime.UtcNow);
        var monthsRemaining = Math.Clamp(13 - date.Month, 0, 12);
        return annualAccumulated + monthlyRevenue * monthsRemaining;
    }
}
