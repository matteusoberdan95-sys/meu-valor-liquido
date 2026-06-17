namespace MeuValorLiquido.WebApp.Infrastructure;

public static class CalculatorBenchmarkHelper
{
    public static bool IsPrioritySlug(string slug) =>
        CalculatorBenchmarkCatalog.PrioritySlugs.Contains(slug, StringComparer.OrdinalIgnoreCase);

    public static int ScenarioCountForSlug(string slug) =>
        CalculatorBenchmarkCatalog.All.Count(scenario =>
            scenario.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

    public static DateOnly LastCalibrationDate =>
        CalculatorBenchmarkCatalog.All.Max(scenario => scenario.CalibratedAt);

    public static string FormatCalibrationDate() =>
        LastCalibrationDate.ToString("dd/MM/yyyy");
}
