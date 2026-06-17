namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record PjVsCltStitchResultsViewModel(
    MeuValorLiquido.Modules.Calculators.CltPjComparisonBreakdown Breakdown,
    CalculatorShareViewModel? Share);
