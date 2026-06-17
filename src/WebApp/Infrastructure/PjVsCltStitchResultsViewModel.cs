namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record PjVsCltStitchResultsViewModel(
    MeuValorLiquido.Modules.Calculators.CltPjComparisonBreakdown Breakdown,
    CalculatorShareViewModel? Share,
    IReadOnlyList<CalculatorResultWarning> Warnings = null!)
{
    public IReadOnlyList<CalculatorResultWarning> Warnings { get; init; } = Warnings ?? [];
}
