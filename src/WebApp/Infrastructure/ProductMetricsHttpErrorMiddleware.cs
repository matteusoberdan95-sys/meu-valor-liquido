namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class ProductMetricsHttpErrorMiddleware
{
    private readonly RequestDelegate next;

    public ProductMetricsHttpErrorMiddleware(RequestDelegate next) => this.next = next;

    public async Task InvokeAsync(HttpContext context, IProductMetricsService metrics)
    {
        await next(context);

        if (context.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            await metrics.RecordAsync(
                ProductMetricEvents.HttpError404,
                ProductMetricsPathNormalizer.ForRequest(context.Request));
        }
    }
}
