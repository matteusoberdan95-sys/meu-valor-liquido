namespace MeuValorLiquido.WebApp.Pages;
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public async Task OnGet([FromServices] IProductMetricsService metricsService)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        await metricsService.RecordAsync(ProductMetricEvents.HttpError500, "server");
    }
}

