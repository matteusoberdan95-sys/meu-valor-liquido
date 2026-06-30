namespace MeuValorLiquido.WebApp.Infrastructure;

public static class SitemapGenerator
{
    private static readonly DateOnly SeoBaselineLastModified = new(2026, 6, 29);

    public static async Task<string> BuildXmlAsync(AppDbContext db, IConfiguration configuration)
    {
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var baseUrl = configuration["Site:BaseUrl"]?.TrimEnd('/') ?? "https://meuvalorliquido.com";

        var urls = new List<XElement>
        {
            CreateUrl(ns, $"{baseUrl}/", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/calculadoras", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/sobre", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/contato", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/blog", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/newsletter", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/mapa-do-site", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/salario-liquido", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/clt-pj", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/duvidas", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/assistente", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/desligamento", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/negociar-salario", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/virar-pj", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/meu-painel", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/widget", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/politica-de-privacidade", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/politica-de-cookies", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/termos-de-uso", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/aviso-legal", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/como-calculamos", SeoBaselineLastModified),
            CreateUrl(ns, $"{baseUrl}/conferir-holerite", SeoBaselineLastModified)
        };

        var calculators = await db.CalculatorCatalog.AsNoTracking().Where(x => x.IsActive).ToListAsync();
        urls.AddRange(calculators.Select(c => CreateUrl(ns, $"{baseUrl}/calculadoras/{c.Slug}", SeoBaselineLastModified)));
        urls.AddRange(SalaryBandCatalog.GetAllIndexablePaths().Select(path => CreateUrl(ns, $"{baseUrl}{path}", SeoBaselineLastModified)));
        urls.AddRange(CltPjBandCatalog.GetAllIndexablePaths().Select(path => CreateUrl(ns, $"{baseUrl}{path}", SeoBaselineLastModified)));
        urls.AddRange(PopularQuestionsCatalog.GetAll().Select(q => CreateUrl(ns, $"{baseUrl}{PopularQuestionsCatalog.SlugPath(q.Slug)}", SeoBaselineLastModified)));

        var posts = await db.BlogPosts.AsNoTracking().Where(x => x.IsPublished).ToListAsync();
        urls.AddRange(posts.Select(p => CreateUrl(ns, $"{baseUrl}/blog/{p.Slug}", p.PublishedAt)));

        var document = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement(ns + "urlset", urls));

        return document.ToString();
    }

    private static XElement CreateUrl(XNamespace ns, string location, DateOnly lastModified) =>
        new(
            ns + "url",
            new XElement(ns + "loc", location),
            new XElement(ns + "lastmod", lastModified.ToString("yyyy-MM-dd")));
}
