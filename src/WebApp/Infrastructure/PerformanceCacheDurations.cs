namespace MeuValorLiquido.WebApp.Infrastructure;

public static class PerformanceCacheDurations
{
    public static readonly TimeSpan Catalog = TimeSpan.FromMinutes(10);

    public static readonly TimeSpan Content = TimeSpan.FromMinutes(10);

    public static readonly TimeSpan EditorialPages = TimeSpan.FromMinutes(10);

    public static readonly TimeSpan Sitemap = TimeSpan.FromHours(1);

    public static readonly TimeSpan StaticAssets = TimeSpan.FromDays(365);
}
