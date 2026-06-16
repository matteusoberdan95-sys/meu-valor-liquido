namespace MeuValorLiquido.Modules.Calculators.Tax;
public enum MeiActivityType
{
    [Display(Name = "Comércio ou indústria")]
    CommerceOrIndustry,

    [Display(Name = "Prestação de serviços")]
    Services,

    [Display(Name = "Comércio e serviços")]
    CommerceAndServices
}

/// <summary>Parâmetros MEI vigentes em 2026 (DAS fixo sobre salário mínimo + ICMS/ISS).</summary>
public static class BrMeiTables2026
{
    public const decimal AnnualRevenueLimit = 81_000m;
    public const decimal MonthlyRevenueAverage = 6_750m;
    public const decimal ExcessTolerancePercent = 0.20m;

    public static decimal GetDas(MeiActivityType activity)
    {
        var inss = BrTaxTables2026.MinimumWage * 0.05m;
        return activity switch
        {
            MeiActivityType.CommerceOrIndustry => inss + 1m,
            MeiActivityType.Services => inss + 5m,
            MeiActivityType.CommerceAndServices => inss + 6m,
            _ => inss + 1m
        };
    }
}
