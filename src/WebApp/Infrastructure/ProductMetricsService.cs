namespace MeuValorLiquido.WebApp.Infrastructure;

public static class ProductMetricEvents
{
    public const string CalculatorCalculation = "calculator_calculation";
    public const string PdfDownload = "pdf_download";
    public const string ShareCopy = "share_copy";
    public const string PanelSave = "panel_save";
    public const string WidgetView = "widget_view";

    private static readonly HashSet<string> All =
    [
        CalculatorCalculation,
        PdfDownload,
        ShareCopy,
        PanelSave,
        WidgetView
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
    IReadOnlyList<ProductMetricRow> TopCalculations,
    IReadOnlyList<ProductMetricRow> TopPdfDownloads,
    IReadOnlyList<ProductMetricRow> TopShareCopies,
    IReadOnlyList<ProductMetricRow> TopPanelSaves);

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
