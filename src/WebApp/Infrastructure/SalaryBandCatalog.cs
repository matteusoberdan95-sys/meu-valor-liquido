namespace MeuValorLiquido.WebApp.Infrastructure;
/// <summary>Valores brutos indexáveis para páginas programáticas de salário líquido.</summary>
public static class SalaryBandCatalog
{
    public const int MinGross = 1621;
    public const int MaxGross = 20000;

    private static readonly int[] Bands =
    [
        1621, 1800, 2000, 2200, 2400, 2500, 2600, 2800, 3000, 3200, 3300, 3500, 3600, 3800, 4000,
        4200, 4400, 4500, 4600, 4800, 5000, 5200, 5500, 5800, 6000, 6200, 6500, 6800, 7000, 7200,
        7500, 8000, 8500, 9000, 9500, 10000, 11000, 12000, 13000, 14000, 15000, 16000, 17000, 18000,
        19000, 20000
    ];

    public const int MinimumIndexedBands = 40;

    /// <summary>
    /// Faixas de maior demanda mantidas no sitemap (Tier 1).
    /// Demais faixas continuam acessíveis, mas com noindex para reduzir thin content em escala.
    /// </summary>
    private static readonly HashSet<int> SitemapIndexableBands =
    [
        1621, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5500, 6000,
        6500, 7000, 8000, 9000, 10000, 12000, 15000, 20000
    ];

    public static IReadOnlyList<int> GetAll() => Bands;

    public static IReadOnlyCollection<int> GetSitemapIndexableBands() => SitemapIndexableBands;

    public static bool IsValid(int gross) => Bands.Contains(gross);

    public static bool IsValid(int gross, int dependents) =>
        IsValid(gross) && ProgrammaticDependentsCatalog.IsValidCount(dependents);

    public static bool IsSitemapIndexable(int gross, int dependents = 0) =>
        IsValid(gross, dependents) && SitemapIndexableBands.Contains(gross);

    public static int ResolveNearestBand(decimal gross)
    {
        var rounded = (int)decimal.Round(gross, 0, MidpointRounding.AwayFromZero);
        if (IsValid(rounded))
        {
            return rounded;
        }

        return Bands
            .OrderBy(band => Math.Abs(band - rounded))
            .First();
    }

    public static string FormatCurrency(int amount) =>
        amount.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));

    public static string SlugPath(int gross, int dependents = 0)
    {
        if (dependents == 0)
        {
            return $"/salario-liquido/{gross}";
        }

        var variant = ProgrammaticDependentsCatalog.VariantSlug(dependents)
            ?? throw new ArgumentOutOfRangeException(nameof(dependents));

        return $"/salario-liquido/{gross}/{variant}";
    }

    public static IEnumerable<string> GetAllIndexablePaths() =>
        from gross in Bands
        where IsSitemapIndexable(gross)
        from dependents in ProgrammaticDependentsCatalog.IndexedDependentCounts
        select SlugPath(gross, dependents);
}
