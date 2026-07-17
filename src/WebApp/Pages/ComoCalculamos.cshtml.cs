namespace MeuValorLiquido.WebApp.Pages;

public class ComoCalculamosModel : PageModel
{
    public int TaxYear => BrTaxTables2026.Year;

    public decimal MinimumWage => BrTaxTables2026.MinimumWage;

    public decimal DependentDeduction => BrTaxTables2026.DependentDeduction;

    public InssBracket[] InssBrackets => BrTaxTables2026.InssBrackets;

    public IrrfBracket[] IrrfBrackets => BrTaxTables2026.IrrfBrackets;

    public IReadOnlyList<BrTaxTablePeriod> TaxTablePeriods => BrTaxTableCatalog.All;

    public int BenchmarkScenarioCount => CalculatorBenchmarkCatalog.All.Count;

    public IReadOnlyList<BenchmarkSourceSummary> BenchmarkSources =>
        CalculatorBenchmarkCatalog.All
            .GroupBy(scenario => new
            {
                scenario.SourceName,
                scenario.SourceUrl,
                scenario.CalibratedAt
            })
            .Select(group => new BenchmarkSourceSummary(
                group.Key.SourceName,
                group.Key.SourceUrl,
                group.Key.CalibratedAt,
                group.Count(),
                string.Join(", ", group.Select(scenario => scenario.Slug).Distinct().OrderBy(slug => slug))))
            .ToArray();

    public IReadOnlyList<MetodologiaCategorySection> CategorySections => MetodologiaCategoryCatalog.GetAll();

    public string LastCalibrationLabel => CalculatorBenchmarkHelper.FormatCalibrationDate();

    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Como calculamos — metodologia e fontes",
                "Entenda as tabelas INSS/IRRF, premissas das calculadoras e limitações dos resultados estimados.",
                "/como-calculamos"));
    }
}

public sealed record BenchmarkSourceSummary(
    string SourceName,
    string SourceUrl,
    DateOnly CalibratedAt,
    int ScenarioCount,
    string CoveredSlugs);
