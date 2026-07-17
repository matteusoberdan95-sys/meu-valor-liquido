namespace MeuValorLiquido.WebApp.Infrastructure;

public static class SitemapGenerator
{
    private static readonly DateOnly SeoBaselineLastModified = new(2026, 6, 29);

    public static async Task<string> BuildXmlAsync(AppDbContext db, IConfiguration configuration)
    {
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var baseUrl = configuration["Site:BaseUrl"]?.TrimEnd('/') ?? "https://meuvalorliquido.com";

        var urls = SeoRoutePolicyCatalog.IndexableStaticRoutes
            .Select(route => CreateUrl(ns, $"{baseUrl}{route.Path}", route.LastReviewedAt))
            .ToList();

        var calculators = await db.CalculatorCatalog.AsNoTracking().Where(x => x.IsActive).ToListAsync();
        urls.AddRange(
            calculators
                .GroupBy(calculator => calculator.Slug, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.First())
                .Select(calculator =>
                {
                    var lastModified = CalculatorEditorialCatalog.GetBySlug(calculator.Slug)?.LastReviewedAt
                        ?? SeoBaselineLastModified;
                    return CreateUrl(ns, $"{baseUrl}/calculadoras/{calculator.Slug}", lastModified);
                }));
        urls.AddRange(SalaryBandCatalog.GetAllIndexablePaths().Select(path => CreateUrl(ns, $"{baseUrl}{path}", SeoBaselineLastModified)));
        urls.AddRange(CltPjBandCatalog.GetAllIndexablePaths().Select(path => CreateUrl(ns, $"{baseUrl}{path}", SeoBaselineLastModified)));
        urls.AddRange(PopularQuestionsCatalog.GetAll().Select(q => CreateUrl(ns, $"{baseUrl}{PopularQuestionsCatalog.SlugPath(q.Slug)}", SeoBaselineLastModified)));

        var posts = await db.BlogPosts.AsNoTracking().Where(x => x.IsPublished).ToListAsync();
        urls.AddRange(
            posts
                .GroupBy(post => post.Slug, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderByDescending(post => post.PublishedAt).First())
                .Select(post => CreateUrl(ns, $"{baseUrl}/blog/{post.Slug}", post.PublishedAt)));

        var uniqueUrls = urls
            .GroupBy(url => url.Element(ns + "loc")!.Value, StringComparer.OrdinalIgnoreCase)
            .Select(group => group.First())
            .OrderBy(url => url.Element(ns + "loc")!.Value, StringComparer.Ordinal)
            .ToList();

        var document = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement(ns + "urlset", uniqueUrls));

        return document.ToString();
    }

    private static XElement CreateUrl(XNamespace ns, string location, DateOnly lastModified) =>
        new(
            ns + "url",
            new XElement(ns + "loc", location),
            new XElement(ns + "lastmod", lastModified.ToString("yyyy-MM-dd")));
}
