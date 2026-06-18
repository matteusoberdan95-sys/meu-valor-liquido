namespace MeuValorLiquido.WebApp.Infrastructure;

public static class ProductMetricEvents
{
    public const string CalculatorCalculation = "calculator_calculation";
    public const string PdfDownload = "pdf_download";
    public const string ShareCopy = "share_copy";
    public const string PanelSave = "panel_save";
    public const string WidgetView = "widget_view";
    public const string HttpError404 = "http_error_404";
    public const string HttpError500 = "http_error_500";
    public const string CalculationFailed = "calculation_failed";

    private static readonly HashSet<string> All =
    [
        CalculatorCalculation,
        PdfDownload,
        ShareCopy,
        PanelSave,
        WidgetView,
        HttpError404,
        HttpError500,
        CalculationFailed
    ];

    private static readonly HashSet<string> ClientCollectible =
    [
        ShareCopy,
        PanelSave
    ];

    public static bool IsAllowed(string eventType) => All.Contains(eventType);

    public static bool IsClientCollectible(string eventType) => ClientCollectible.Contains(eventType);
}

public sealed class MetricsOptions
{
    public const string SectionName = "Metrics";

    public bool Enabled { get; set; } = true;
}

public sealed record ProductMetricCollectRequest(string Event, string? Dimension);

public sealed record ProductMetricRow(string Label, string EventType, long Count);

public sealed record ProductMetricsSummary(
    DateOnly From,
    DateOnly To,
    int PeriodDays,
    long TotalCalculations,
    long TotalPdfDownloads,
    long TotalShareCopies,
    long TotalPanelSaves,
    long TotalWidgetViews,
    decimal SharePerCalculationPercent,
    decimal PdfPerCalculationPercent,
    decimal PanelSavePerCalculationPercent,
    long TotalHttp404,
    long TotalHttp500,
    long TotalCalculationFailures,
    decimal CalculationFailureRatePercent,
    IReadOnlyList<ProductMetricRow> TopCalculations,
    IReadOnlyList<ProductMetricRow> TopPdfDownloads,
    IReadOnlyList<ProductMetricRow> TopShareCopies,
    IReadOnlyList<ProductMetricRow> TopPanelSaves,
    IReadOnlyList<ProductMetricRow> TopHttp404Routes,
    IReadOnlyList<ProductMetricRow> TopCalculationFailures,
    IReadOnlyList<ProductMetricsInsight> PrioritizationInsights);

public static class ProductMetricsPeriod
{
    public const int Week = 7;
    public const int Month = 30;

    public static int Normalize(int? days) =>
        days == Week ? Week : Month;
}

public interface IProductMetricsService
{
    Task RecordAsync(string eventType, string? dimension = null, CancellationToken cancellationToken = default);

    Task<ProductMetricsSummary> GetSummaryAsync(int days = ProductMetricsPeriod.Month, CancellationToken cancellationToken = default);
}
