namespace MeuValorLiquido.Modules.Calculators;

public sealed record TerminationResultSummary(
    decimal VerbasTotal,
    decimal DescontosTotal,
    decimal FgtsTotal,
    IReadOnlyList<CalculationLineItem> Verbas,
    IReadOnlyList<CalculationLineItem> Descontos,
    IReadOnlyList<CalculationLineItem> FgtsLines);

public static class TerminationResultGrouper
{
    private static readonly HashSet<string> GroupedCalculatorSlugs = new(StringComparer.OrdinalIgnoreCase)
    {
        "rescisao-clt",
        "decimo-terceiro",
        "ferias",
        "salario-liquido",
        "salario-bruto-necessario",
        "proposta-salarial"
    };

    private static readonly HashSet<string> HoleriteSlugs = new(StringComparer.OrdinalIgnoreCase)
    {
        "salario-liquido",
        "salario-bruto-necessario",
        "proposta-salarial"
    };

    public static bool SupportsGroupedSummary(string slug) =>
        GroupedCalculatorSlugs.Contains(slug);

    public static TerminationResultSummary? TryGroup(string slug, CalculationResult result) =>
        SupportsGroupedSummary(slug) ? Group(result) : null;
    private static readonly HashSet<string> FgtsLabels =
    [
        "Multa FGTS",
        "Saldo FGTS estimado (8% × meses)",
        "Saque FGTS permitido (estimado)"
    ];

    public static TerminationResultSummary Group(CalculationResult result)
    {
        var verbas = new List<CalculationLineItem>();
        var descontos = new List<CalculationLineItem>();
        var fgts = new List<CalculationLineItem>();

        foreach (var item in result.LineItems)
        {
            if (IsFgtsLine(item))
            {
                fgts.Add(item);
                continue;
            }

            if (item.Type == CalculationLineType.Discount)
            {
                descontos.Add(item);
                continue;
            }

            if (item.Type == CalculationLineType.Income)
            {
                verbas.Add(item);
            }
        }

        var verbasTotal = verbas.Sum(item => item.Amount.Amount);
        if (verbas.Count == 0 && HoleriteSlugs.Contains(result.Slug))
        {
            verbasTotal = result.GrossAmount.Amount;
        }

        var descontosTotal = descontos.Sum(item => item.Amount.Amount);
        var fgtsTotal = fgts
            .Where(item => item.Type == CalculationLineType.Income)
            .Sum(item => item.Amount.Amount);

        return new TerminationResultSummary(
            verbasTotal,
            descontosTotal,
            fgtsTotal,
            verbas,
            descontos,
            fgts);
    }

    private static bool IsFgtsLine(CalculationLineItem item)
    {
        if (item.Label.StartsWith("Multa FGTS", StringComparison.Ordinal))
        {
            return true;
        }

        return FgtsLabels.Contains(item.Label);
    }
}
