namespace MeuValorLiquido.WebApp.Infrastructure;

public static class ProductMetricsEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/api/metrics/collect", CollectMetric)
            .RequireRateLimiting("metrics-policy");
    }

    private static async Task<IResult> CollectMetric(
        ProductMetricCollectRequest request,
        IProductMetricsService metricsService,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Event) || !ProductMetricEvents.IsClientCollectible(request.Event))
        {
            return Results.BadRequest();
        }

        await metricsService.RecordAsync(request.Event, request.Dimension, cancellationToken);
        return Results.NoContent();
    }
}
