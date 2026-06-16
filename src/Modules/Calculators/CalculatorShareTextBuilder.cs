namespace MeuValorLiquido.Modules.Calculators;
public static class CalculatorShareTextBuilder
{
    public static string Build(CalculationResult result, string shareUrl)
    {
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
}
