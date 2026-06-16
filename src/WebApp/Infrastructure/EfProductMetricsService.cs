namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class EfProductMetricsService : IProductMetricsService
{
    private readonly AppDbContext db;
    private readonly MetricsOptions options;

    public EfProductMetricsService(AppDbContext db, IOptions<MetricsOptions> options)
    {
        this.db = db;
        this.options = options.Value;
    }

    public async Task RecordAsync(string eventType, string? dimension = null, CancellationToken cancellationToken = default)
    {
        if (!options.Enabled || !ProductMetricEvents.IsAllowed(eventType))
        {
            return;
        }

        var normalizedDimension = NormalizeDimension(dimension);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var metric = await db.AggregatedMetrics.FirstOrDefaultAsync(
            x => x.MetricDate == today && x.EventType == eventType && x.Dimension == normalizedDimension,
            cancellationToken);

        if (metric is null)
        {
            db.AggregatedMetrics.Add(new AggregatedMetricEntity
            {
                MetricDate = today,
                EventType = eventType,
                Dimension = normalizedDimension,
                Count = 1
            });
        }
        else
        {
            metric.Count++;
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProductMetricsSummary> GetSummaryAsync(int days = 30, CancellationToken cancellationToken = default)
    {
        var to = DateOnly.FromDateTime(DateTime.UtcNow);
        var from = to.AddDays(-(days - 1));

        var rows = await db.AggregatedMetrics
            .AsNoTracking()
            .Where(x => x.MetricDate >= from && x.MetricDate <= to)
            .ToListAsync(cancellationToken);

        long Sum(string eventType) => rows.Where(x => x.EventType == eventType).Sum(x => x.Count);

        var topCalculations = TopByDimension(rows, ProductMetricEvents.CalculatorCalculation, 10);
        var topPdfs = TopByDimension(rows, ProductMetricEvents.PdfDownload, 10);

        return new ProductMetricsSummary(
            from,
            to,
            Sum(ProductMetricEvents.CalculatorCalculation),
            Sum(ProductMetricEvents.PdfDownload),
            Sum(ProductMetricEvents.ShareCopy),
            Sum(ProductMetricEvents.PanelSave),
            Sum(ProductMetricEvents.WidgetView),
            topCalculations,
            topPdfs);
    }

    private static string NormalizeDimension(string? dimension)
    {
        if (string.IsNullOrWhiteSpace(dimension))
        {
            return string.Empty;
        }

        var trimmed = dimension.Trim();
        return trimmed.Length <= 80 ? trimmed : trimmed[..80];
    }

    private static IReadOnlyList<ProductMetricRow> TopByDimension(
        IReadOnlyList<AggregatedMetricEntity> rows,
        string eventType,
        int take)
    {
        return rows
            .Where(x => x.EventType == eventType && !string.IsNullOrEmpty(x.Dimension))
            .GroupBy(x => x.Dimension)
            .Select(group => new ProductMetricRow(group.Key, eventType, group.Sum(x => x.Count)))
            .OrderByDescending(x => x.Count)
            .Take(take)
            .ToList();
    }
}
