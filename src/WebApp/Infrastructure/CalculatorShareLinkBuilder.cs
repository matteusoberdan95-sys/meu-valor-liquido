namespace MeuValorLiquido.WebApp.Infrastructure;
public sealed class CalculatorShareLinkBuilder
{
    private readonly IConfiguration configuration;

    public CalculatorShareLinkBuilder(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string BuildShareUrl(string slug, CalculatorInput input, HttpRequest request)
    {
        var baseUrl = GetBaseUrl(request);
        var token = CalculatorInputShareCodec.Encode(input);
        return $"{baseUrl}/calculadoras/{slug}?r={Uri.EscapeDataString(token)}";
    }

    public string BuildPdfUrl(string slug, CalculatorInput input) =>
        $"/calculadoras/{slug}/resultado.pdf?r={Uri.EscapeDataString(CalculatorInputShareCodec.Encode(input))}";

    public static string BuildSalaryBandPdfUrl(int gross) =>
        $"/salario-liquido/{gross}/resultado.pdf";

    public string BuildAbsoluteUrl(string path, HttpRequest request)
    {
        var baseUrl = GetBaseUrl(request);
        var normalizedPath = path.StartsWith('/') ? path : $"/{path}";
        return $"{baseUrl}{normalizedPath}";
    }

    public static string BuildWhatsAppUrl(string shareText) =>
        $"https://wa.me/?text={Uri.EscapeDataString(shareText)}";

    private string GetBaseUrl(HttpRequest request) =>
        configuration["Site:BaseUrl"]?.TrimEnd('/')
        ?? $"{request.Scheme}://{request.Host}";
}

public sealed record CalculatorShareViewModel(
    string ShareUrl,
    string ShareText,
    string WhatsAppUrl,
    string? PdfUrl = null,
    LocalPanelSaveContext? LocalPanel = null,
    CalculatorJourneyPanelViewModel? Journey = null);
