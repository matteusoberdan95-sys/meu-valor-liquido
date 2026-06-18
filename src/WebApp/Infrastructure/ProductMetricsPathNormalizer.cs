namespace MeuValorLiquido.WebApp.Infrastructure;

public static class ProductMetricsPathNormalizer
{
    public static string ForRequest(HttpRequest request)
    {
        var path = request.Path.Value ?? "/";
        return NormalizePath(path);
    }

    public static string NormalizePath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return "/";
        }

        var trimmed = path.Split('?', '#')[0].Trim();
        if (!trimmed.StartsWith('/'))
        {
            trimmed = "/" + trimmed;
        }

        var segments = trimmed
            .Split('/', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Take(3)
            .ToArray();

        return segments.Length == 0 ? "/" : "/" + string.Join('/', segments);
    }
}
