namespace MeuValorLiquido.WebApp.Infrastructure;

public static class CalculatorJourneyShareTextBuilder
{
    public static string AppendNextSteps(string shareText, CalculatorJourneyPanelViewModel? journey)
    {
        if (journey is null || journey.Steps.Count == 0)
        {
            return shareText;
        }

        var lines = new List<string>
        {
            shareText,
            string.Empty,
            "Próximo passo:"
        };

        foreach (var step in journey.Steps)
        {
            lines.Add($"• {step.Label} — {step.Url}");
        }

        return string.Join('\n', lines);
    }
}
