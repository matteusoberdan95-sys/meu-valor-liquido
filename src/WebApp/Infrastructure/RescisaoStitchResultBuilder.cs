using MeuValorLiquido.Modules.Calculators;

namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record RescisaoStitchFgtsCard(string Label, decimal Amount, bool Highlight);

public sealed record RescisaoStitchResultViewModel(
    CalculationResult Result,
    TerminationResultSummary Summary,
    decimal NetAmount,
    decimal FgtsPackageTotal,
    RescisaoStitchFgtsCard? FgtsBalanceCard,
    RescisaoStitchFgtsCard? FgtsPenaltyCard,
    CalculatorShareViewModel? Share,
    IReadOnlyList<CalculatorResultWarning> Warnings);

public static class RescisaoStitchResultBuilder
{
    public static RescisaoStitchResultViewModel? TryBuild(
        CalculationResult result,
        TerminationResultSummary? summary,
        CalculatorShareViewModel? share,
        IReadOnlyList<CalculatorResultWarning> warnings)
    {
        if (!result.Slug.Equals("rescisao-clt", StringComparison.OrdinalIgnoreCase) || summary is null)
        {
            return null;
        }

        var (balance, penalty) = ResolveFgtsCards(summary.FgtsLines);
        var fgtsPackage = summary.FgtsLines
            .Where(item => item.Type == CalculationLineType.Income)
            .Sum(item => item.Amount.Amount);

        return new RescisaoStitchResultViewModel(
            result,
            summary,
            result.EstimatedNetAmount.Amount,
            fgtsPackage,
            balance,
            penalty,
            share,
            warnings);
    }

    private static (RescisaoStitchFgtsCard? Balance, RescisaoStitchFgtsCard? Penalty) ResolveFgtsCards(
        IReadOnlyList<CalculationLineItem> fgtsLines)
    {
        RescisaoStitchFgtsCard? balance = null;
        RescisaoStitchFgtsCard? penalty = null;

        foreach (var item in fgtsLines)
        {
            if (item.Label.StartsWith("Multa FGTS", StringComparison.Ordinal))
            {
                penalty = new RescisaoStitchFgtsCard(
                    item.Label.Contains('(') ? item.Label : "Multa rescisória FGTS",
                    item.Amount.Amount,
                    true);
                continue;
            }

            if (item.Label.Contains("Saldo FGTS", StringComparison.OrdinalIgnoreCase)
                || item.Label.Contains("Saque FGTS", StringComparison.OrdinalIgnoreCase))
            {
                balance = new RescisaoStitchFgtsCard("Saldo FGTS estimado", item.Amount.Amount, false);
            }
        }

        return (balance, penalty);
    }
}
