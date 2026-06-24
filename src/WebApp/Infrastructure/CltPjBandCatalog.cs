namespace MeuValorLiquido.WebApp.Infrastructure;

/// <summary>URLs indexáveis CLT x PJ (reutiliza faixas de salário bruto).</summary>
public static class CltPjBandCatalog
{
    public static IReadOnlyList<int> GetAll() => SalaryBandCatalog.GetAll();

    public static bool IsValid(int cltGross) => SalaryBandCatalog.IsValid(cltGross);

    public static bool IsValid(int cltGross, int dependents) =>
        SalaryBandCatalog.IsValid(cltGross, dependents);

    public static string SlugPath(int cltGross, int dependents = 0)
    {
        if (dependents == 0)
        {
            return $"/clt-pj/{cltGross}-clt-equivale-a-quanto-pj";
        }

        var variant = ProgrammaticDependentsCatalog.VariantSlug(dependents)
            ?? throw new ArgumentOutOfRangeException(nameof(dependents));

        return $"/clt-pj/{cltGross}/{variant}";
    }

    public static string FormatCurrency(int amount) => SalaryBandCatalog.FormatCurrency(amount);

    public static IEnumerable<string> GetAllIndexablePaths() =>
        from gross in GetAll()
        from dependents in ProgrammaticDependentsCatalog.IndexedDependentCounts
        select SlugPath(gross, dependents);
}
