namespace MeuValorLiquido.WebApp.Infrastructure;

public static class EmbedFramePolicy
{
    public static bool AllowsEmbedding(PathString path, IQueryCollection query)
    {
        if (!string.Equals(query["embed"].ToString(), "1", StringComparison.Ordinal))
        {
            return false;
        }

        var segments = path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        return segments is { Length: 2 }
            && segments[0].Equals("calculadoras", StringComparison.OrdinalIgnoreCase)
            && EmbedWidgetCatalog.IsEmbeddable(segments[1]);
    }

    public static string BuildEmbedContentSecurityPolicy() =>
        "default-src 'self'; style-src 'self' 'unsafe-inline' https://fonts.googleapis.com; font-src 'self' https://fonts.gstatic.com; script-src 'self'; img-src 'self' data:; frame-ancestors *";
}
