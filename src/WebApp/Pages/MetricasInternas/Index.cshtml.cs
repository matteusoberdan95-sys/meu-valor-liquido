namespace MeuValorLiquido.WebApp.Pages.MetricasInternas;

public class IndexModel : PageModel
{
    private readonly IProductMetricsService metricsService;
    private readonly ICalculatorCatalogService calculatorCatalog;

    public IndexModel(IProductMetricsService metricsService, ICalculatorCatalogService calculatorCatalog)
    {
        this.metricsService = metricsService;
        this.calculatorCatalog = calculatorCatalog;
    }

    [BindProperty(SupportsGet = true)]
    public int? Days { get; set; }

    public ProductMetricsSummary Summary { get; private set; } = null!;

    public IReadOnlyDictionary<string, string> CalculatorNames { get; private set; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public int SelectedPeriodDays => Summary?.PeriodDays ?? ProductMetricsPeriod.Month;

    public string CalculatorLabel(string slug) =>
        ProductMetricsDisplayHelper.ResolveCalculatorLabel(slug, CalculatorNames);

    public async Task OnGetAsync()
    {
        var periodDays = ProductMetricsPeriod.Normalize(Days);
        Summary = await metricsService.GetSummaryAsync(periodDays);
        CalculatorNames = calculatorCatalog.GetAll()
            .GroupBy(calculator => calculator.Slug, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.First().Name, StringComparer.OrdinalIgnoreCase);

        Summary = Summary with
        {
            PrioritizationInsights = ProductMetricsPrioritizationBuilder.Build(Summary, CalculatorNames)
        };

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Métricas internas agregadas",
                "Resumo agregado de uso das calculadoras — sem dados pessoais nem valores salariais.",
                "/metricas-internas",
                SeoMetadataHelper.NoIndexRobots));
    }
}
