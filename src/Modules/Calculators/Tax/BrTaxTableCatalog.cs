namespace MeuValorLiquido.Modules.Calculators.Tax;

public sealed record BrTaxTablePeriod(
    int Year,
    DateOnly ValidFrom,
    DateOnly? ValidTo,
    string SourceName,
    string SourceUrl,
    decimal MinimumWage,
    decimal InssCeiling,
    decimal InssMaximumContribution,
    decimal DependentDeduction);

/// <summary>
/// Catálogo de tabelas fiscais versionadas. A resolução por data evita sobrescrever anos anteriores.
/// O motor de produção usa <see cref="BrTaxTables2026"/> enquanto a competência vigente for 2026.
/// </summary>
public static class BrTaxTableCatalog
{
    public static readonly DateOnly CurrentCompetence = new(2026, 1, 1);

    public static IReadOnlyList<BrTaxTablePeriod> All { get; } =
    [
        new(
            BrTaxTables2025.Year,
            BrTaxTables2025.ValidFrom,
            BrTaxTables2025.ValidTo,
            BrTaxTables2025.SourceName,
            BrTaxTables2025.SourceUrl,
            BrTaxTables2025.MinimumWage,
            BrTaxTables2025.InssCeiling,
            BrTaxTables2025.InssMaximumContribution,
            BrTaxTables2025.DependentDeduction),
        new(
            BrTaxTables2026.Year,
            BrTaxTables2026.ValidFrom,
            BrTaxTables2026.ValidTo,
            BrTaxTables2026.SourceName,
            BrTaxTables2026.SourceUrl,
            BrTaxTables2026.MinimumWage,
            BrTaxTables2026.InssCeiling,
            BrTaxTables2026.InssMaximumContribution,
            BrTaxTables2026.DependentDeduction)
    ];

    public static BrTaxTablePeriod Resolve(DateOnly competenceDate)
    {
        var match = All.SingleOrDefault(period =>
            competenceDate >= period.ValidFrom
            && (period.ValidTo is null || competenceDate <= period.ValidTo));

        if (match is null)
        {
            throw new InvalidOperationException(
                $"Nenhuma tabela fiscal versionada cobre a competência {competenceDate:yyyy-MM-dd}.");
        }

        return match;
    }

    public static BrTaxTablePeriod Current => Resolve(CurrentCompetence);
}
