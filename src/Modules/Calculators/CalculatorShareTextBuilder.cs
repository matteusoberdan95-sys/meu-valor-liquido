namespace MeuValorLiquido.Modules.Calculators;
public static class CalculatorShareTextBuilder
{
    public static string Build(CalculationResult result, string shareUrl)
    {
        if (result.Slug.Equals("proposta-salarial", StringComparison.OrdinalIgnoreCase))
        {
            return BuildSalaryProposalShareText(result, shareUrl);
        }

        var lines = new List<string>
        {
            $"*Meu Valor Líquido* — {result.Title}",
            $"Estimativa educativa ({BrTaxTables2026.Year})",
            string.Empty,
            $"Bruto: {result.GrossAmount}",
        };

        foreach (var item in result.LineItems)
        {
            if (!string.IsNullOrEmpty(item.DisplayText))
            {
                lines.Add($"{item.Label}: {item.DisplayText}");
                continue;
            }

            var prefix = item.Type switch
            {
                CalculationLineType.Discount => "- ",
                CalculationLineType.Income => "+ ",
                _ => string.Empty
            };

            lines.Add($"{item.Label}: {prefix}{item.Amount}");
        }

        lines.Add(string.Empty);
        lines.Add($"*Líquido estimado: {result.EstimatedNetAmount}*");
        lines.Add(string.Empty);
        lines.Add($"Simule o seu: {shareUrl}");
        lines.Add("Não substitui holerite ou orientação profissional.");

        return string.Join('\n', lines);
    }

    private static string BuildSalaryProposalShareText(CalculationResult result, string shareUrl)
    {
        decimal TryReadLine(string label)
        {
            var item = result.LineItems.FirstOrDefault(line =>
                line.Label.Equals(label, StringComparison.OrdinalIgnoreCase));
            return item?.Amount.Amount ?? 0m;
        }

        var currentGross = TryReadLine("Salário bruto atual");
        var proposedGross = TryReadLine("Salário bruto proposto");
        var currentNet = TryReadLine("Líquido atual estimado");
        var proposedNet = TryReadLine("Líquido proposto estimado");
        var monthlyGain = proposedNet - currentNet;
        var annualGain = monthlyGain * 12m;

        var lines = new List<string>
        {
            "*Meu Valor Líquido* — Proposta salarial",
            $"Estimativa educativa ({BrTaxTables2026.Year})",
            string.Empty,
            $"Atual: {Money.From(currentGross)} bruto → {Money.From(currentNet)} líquido",
            $"Proposta: {Money.From(proposedGross)} bruto → {Money.From(proposedNet)} líquido",
            string.Empty,
            monthlyGain >= 0m
                ? $"*Ganho líquido: +{Money.From(monthlyGain)}/mês (+{Money.From(annualGain)}/ano)*"
                : $"*Redução líquida: {Money.From(monthlyGain)}/mês ({Money.From(annualGain)}/ano)*",
            string.Empty,
            $"Ver simulação: {shareUrl}",
            "Referência educativa — confirme com RH ou contrato."
        };

        return string.Join('\n', lines);
    }
}
