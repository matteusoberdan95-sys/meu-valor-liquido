namespace MeuValorLiquido.WebApp.Infrastructure;

public static class StaticAssetCacheMiddleware
{
    private static readonly string[] CacheablePrefixes = ["/css/", "/js/", "/lib/", "/images/"];

    public static IApplicationBuilder UseStaticAssetCacheHeaders(this IApplicationBuilder app) =>
        app.Use(async (context, next) =>
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode != StatusCodes.Status200OK)
                {
                    return Task.CompletedTask;
                }

                var path = context.Request.Path.Value ?? string.Empty;
                if (path.Equals("/sitemap.xml", StringComparison.OrdinalIgnoreCase))
                {
                    var sitemapMaxAge = (int)PerformanceCacheDurations.Sitemap.TotalSeconds;
                    context.Response.Headers.CacheControl = $"public,max-age={sitemapMaxAge}";
                    return Task.CompletedTask;
                }

                if (!IsCacheableStaticAsset(path))
                {
                    return Task.CompletedTask;
                }

                var maxAge = (int)PerformanceCacheDurations.StaticAssets.TotalSeconds;
                context.Response.Headers.CacheControl = $"public,max-age={maxAge},immutable";
                return Task.CompletedTask;
            });

            await next();
        });

    private static bool IsCacheableStaticAsset(string path) =>
        CacheablePrefixes.Any(prefix => path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
}
