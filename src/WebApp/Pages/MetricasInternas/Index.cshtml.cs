namespace MeuValorLiquido.WebApp.Pages.MetricasInternas;

public class IndexModel : PageModel
{
    private readonly IProductMetricsService metricsService;

    public IndexModel(IProductMetricsService metricsService)
    {
        this.metricsService = metricsService;
    }

    public ProductMetricsSummary Summary { get; private set; } = null!;

    public async Task OnGetAsync()
    {
        Summary = await metricsService.GetSummaryAsync();

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Métricas internas agregadas",
                "Resumo agregado de uso das calculadoras — sem dados pessoais nem valores salariais.",
                "/metricas-internas",
                SeoMetadataHelper.NoIndexRobots));
    }
}
