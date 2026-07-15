namespace MeuValorLiquido.WebApp.Infrastructure;

public static class SiteUtilityToolCatalog
{
    public const string ConferirHoleritePath = "/conferir-holerite";

    private static readonly HashSet<string> HoleritePromoCalculatorSlugs = new(StringComparer.OrdinalIgnoreCase)
    {
        "salario-liquido",
        "proposta-salarial",
        "vale-transporte-hibrido",
        "inss",
        "irrf"
    };

    public static bool ShouldPromoteConferirHolerite(string? calculatorSlug) =>
        !string.IsNullOrWhiteSpace(calculatorSlug)
        && HoleritePromoCalculatorSlugs.Contains(calculatorSlug);
}
