namespace MeuValorLiquido.WebApp.Infrastructure;

public static class SitemapGenerator
{
    public static async Task<string> BuildXmlAsync(AppDbContext db, IConfiguration configuration)
    {
        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var baseUrl = configuration["Site:BaseUrl"]?.TrimEnd('/') ?? "https://meuvalorliquido.com";

        var urls = new List<XElement>
        {
            CreateUrl(ns, $"{baseUrl}/"),
            CreateUrl(ns, $"{baseUrl}/calculadoras"),
            CreateUrl(ns, $"{baseUrl}/sobre"),
            CreateUrl(ns, $"{baseUrl}/contato"),
            CreateUrl(ns, $"{baseUrl}/blog"),
            CreateUrl(ns, $"{baseUrl}/newsletter"),
            CreateUrl(ns, $"{baseUrl}/mapa-do-site"),
            CreateUrl(ns, $"{baseUrl}/salario-liquido"),
            CreateUrl(ns, $"{baseUrl}/clt-pj"),
            CreateUrl(ns, $"{baseUrl}/duvidas"),
            CreateUrl(ns, $"{baseUrl}/meu-painel"),
            CreateUrl(ns, $"{baseUrl}/widget"),
            CreateUrl(ns, $"{baseUrl}/politica-de-privacidade"),
            CreateUrl(ns, $"{baseUrl}/politica-de-cookies"),
            CreateUrl(ns, $"{baseUrl}/termos-de-uso"),
            CreateUrl(ns, $"{baseUrl}/aviso-legal"),
            CreateUrl(ns, $"{baseUrl}/como-calculamos")
        };

        var calculators = await db.CalculatorCatalog.AsNoTracking().Where(x => x.IsActive).ToListAsync();
        urls.AddRange(calculators.Select(c => CreateUrl(ns, $"{baseUrl}/calculadoras/{c.Slug}")));
        urls.AddRange(SalaryBandCatalog.GetAll().Select(b => CreateUrl(ns, $"{baseUrl}{SalaryBandCatalog.SlugPath(b)}")));
        urls.AddRange(CltPjBandCatalog.GetAll().Select(b => CreateUrl(ns, $"{baseUrl}{CltPjBandCatalog.SlugPath(b)}")));
        urls.AddRange(PopularQuestionsCatalog.GetAll().Select(q => CreateUrl(ns, $"{baseUrl}{PopularQuestionsCatalog.SlugPath(q.Slug)}")));

        var posts = await db.BlogPosts.AsNoTracking().Where(x => x.IsPublished).ToListAsync();
        urls.AddRange(posts.Select(p => CreateUrl(ns, $"{baseUrl}/blog/{p.Slug}")));

        var document = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement(ns + "urlset", urls));

        return document.ToString();
    }

    private static XElement CreateUrl(XNamespace ns, string location) =>
        new(ns + "url", new XElement(ns + "loc", location));
}
