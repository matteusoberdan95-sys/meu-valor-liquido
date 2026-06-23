namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record SalaryProposalStitchResultViewModel(
    decimal CurrentGross,
    decimal ProposedGross,
    decimal CurrentNet,
    decimal ProposedNet,
    decimal CurrentInss,
    decimal ProposedInss,
    decimal CurrentIrrf,
    decimal ProposedIrrf,
    decimal NetGainMonthly,
    decimal NetGainAnnual,
    decimal GrossIncreasePercent,
    decimal NetIncreasePercent,
    bool IsGain,
    string Explanation);

public sealed record PropostaSalarialStitchResultsViewModel(
    SalaryProposalStitchResultViewModel Comparison,
    CalculatorShareViewModel? Share,
    IReadOnlyList<CalculatorResultWarning> Warnings,
    CalculatorJourneyPanelViewModel? Journey,
    CalculatorResultExplanationViewModel? Explanation);

public static class SalaryProposalStitchResultBuilder
{
    public static PropostaSalarialStitchResultsViewModel? TryBuild(
        CalculationResult result,
        CalculatorInput input,
        NetSalaryCalculator netSalaryCalculator,
        CalculatorShareViewModel? share,
        IReadOnlyList<CalculatorResultWarning> warnings,
        CalculatorJourneyPanelViewModel? journey,
        CalculatorResultExplanationViewModel? explanation)
    {
        if (!result.Slug.Equals("proposta-salarial", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (input.SecondaryAmount <= 0m)
        {
            return null;
        }

        var discounts = HoleriteDiscountMapper.FromInput(input, result.Slug);
        var current = netSalaryCalculator.Calculate(input.Amount, input.Dependents, discounts);
        var proposed = netSalaryCalculator.Calculate(input.SecondaryAmount, input.Dependents, discounts);

        var netGainLine = result.LineItems.FirstOrDefault(item =>
            item.Label is "Ganho líquido mensal" or "Redução líquida mensal");
        var annualLine = result.LineItems.FirstOrDefault(item =>
            item.Label == "Ganho ou perda anual (12 meses)");
        var grossPctLine = result.LineItems.FirstOrDefault(item => item.Label == "Aumento no bruto");
        var netPctLine = result.LineItems.FirstOrDefault(item => item.Label == "Aumento no líquido");

        var isGain = netGainLine?.Label == "Ganho líquido mensal";
        var netGainMonthly = netGainLine?.Amount.Amount ?? 0m;
        var netGainAnnual = annualLine?.Amount.Amount ?? netGainMonthly * 12m;
        var grossIncreasePercent = ParsePercent(grossPctLine?.DisplayText);
        var netIncreasePercent = ParsePercent(netPctLine?.DisplayText);

        var comparison = new SalaryProposalStitchResultViewModel(
            input.Amount,
            input.SecondaryAmount,
            current.Net,
            proposed.Net,
            current.Inss,
            proposed.Inss,
            current.Irrf,
            proposed.Irrf,
            netGainMonthly,
            netGainAnnual,
            grossIncreasePercent,
            netIncreasePercent,
            isGain,
            result.Explanation);

        return new PropostaSalarialStitchResultsViewModel(
            comparison,
            share,
            warnings,
            journey,
            explanation);
    }

    private static decimal ParsePercent(string? displayText)
    {
        if (string.IsNullOrWhiteSpace(displayText))
        {
            return 0m;
        }

        var normalized = displayText.Replace("%", string.Empty, StringComparison.Ordinal).Trim();
        return decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out var value)
            ? value
            : 0m;
    }
}
