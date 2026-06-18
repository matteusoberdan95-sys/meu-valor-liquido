namespace MeuValorLiquido.WebApp.Infrastructure;

public static class ProductMetricsDisplayHelper
{
    public static string ResolveCalculatorLabel(string slug, IReadOnlyDictionary<string, string> namesBySlug)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return "—";
        }

        return namesBySlug.TryGetValue(slug, out var name) ? name : slug;
    }

    public static string FormatRate(decimal percent) => $"{percent:0.#}%";

    public static string FormatPeriodLabel(int days) =>
        days == ProductMetricsPeriod.Week ? "Últimos 7 dias" : "Últimos 30 dias";
}
