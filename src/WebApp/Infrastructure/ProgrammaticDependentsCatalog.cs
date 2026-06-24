namespace MeuValorLiquido.WebApp.Infrastructure;

/// <summary>Variantes indexáveis de dependentes para páginas programáticas.</summary>
public static class ProgrammaticDependentsCatalog
{
    public static readonly int[] IndexedDependentCounts = [0, 1, 2];

    public static bool IsValidCount(int count) => count is >= 0 and <= 2;

    public static bool TryParseVariantSlug(string? variant, out int dependents)
    {
        dependents = 0;
        if (string.IsNullOrWhiteSpace(variant))
        {
            return true;
        }

        if (variant == "1-dependente")
        {
            dependents = 1;
            return true;
        }

        if (variant == "2-dependentes")
        {
            dependents = 2;
            return true;
        }

        return false;
    }

    public static string? VariantSlug(int dependents) => dependents switch
    {
        0 => null,
        1 => "1-dependente",
        2 => "2-dependentes",
        _ => throw new ArgumentOutOfRangeException(nameof(dependents))
    };

    public static string SeoPhrase(int dependents) => dependents switch
    {
        0 => "sem dependentes",
        1 => "com 1 dependente",
        2 => "com 2 dependentes",
        _ => throw new ArgumentOutOfRangeException(nameof(dependents))
    };

    public static string BreadcrumbLabel(int dependents) => dependents switch
    {
        0 => "Sem dependentes",
        1 => "1 dependente",
        2 => "2 dependentes",
        _ => throw new ArgumentOutOfRangeException(nameof(dependents))
    };
}
