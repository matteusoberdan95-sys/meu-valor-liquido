namespace MeuValorLiquido.WebApp.Infrastructure;
public sealed record BreadcrumbItem(string Name, string Path);

/// <summary>Centraliza metadados SEO para Razor Pages via ViewData.</summary>
public static class SeoMetadataHelper
{
    public const string SiteName = "Meu Valor Líquido";
    public const string DefaultOgImagePath = "/images/og-default.webp";
    public const string DefaultRobots = "index,follow";
    public const string NoIndexRobots = "noindex,nofollow";

    public static string BuildCanonicalUrl(string baseUrl, string canonicalPath)
    {
        var path = canonicalPath.StartsWith('/') ? canonicalPath : $"/{canonicalPath}";
        return $"{baseUrl.TrimEnd('/')}{path}";
    }

    public static string BuildOgImageUrl(string baseUrl, string? imagePath = null)
    {
        var path = imagePath ?? DefaultOgImagePath;
        if (path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            return path;
        }

        return BuildCanonicalUrl(baseUrl, path);
    }

    public static void Apply(
        IDictionary<string, object?> viewData,
        SeoMetadata metadata,
        string? ogType = null,
        string? ogImagePath = null)
    {
        viewData["Title"] = metadata.Title;
        viewData["Description"] = metadata.Description;
        if (!string.IsNullOrWhiteSpace(metadata.CanonicalPath))
        {
            viewData["CanonicalPath"] = metadata.CanonicalPath;
        }

        viewData["Robots"] = metadata.Robots;
        if (!string.IsNullOrWhiteSpace(ogType))
        {
            viewData["OgType"] = ogType;
        }

        if (!string.IsNullOrWhiteSpace(ogImagePath))
        {
            viewData["OgImagePath"] = ogImagePath;
        }
    }

    public static IReadOnlyList<BreadcrumbItem> CalculatorBreadcrumbs(string calculatorName, string slug) =>
    [
        new("Início", "/"),
        new("Calculadoras", "/calculadoras"),
        new(calculatorName, $"/calculadoras/{slug}")
    ];

    public static IReadOnlyList<BreadcrumbItem> BlogBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Blog", "/blog")
    ];

    public static IReadOnlyList<BreadcrumbItem> BlogPostBreadcrumbs(string postTitle, string slug) =>
    [
        new("Início", "/"),
        new("Blog", "/blog"),
        new(postTitle, $"/blog/{slug}")
    ];

    public static IReadOnlyList<BreadcrumbItem> SalaryBandHubBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Salário líquido por valor", "/salario-liquido")
    ];

    public static IReadOnlyList<BreadcrumbItem> SalaryBandBreadcrumbs(int gross) =>
    [
        new("Início", "/"),
        new("Salário líquido por valor", "/salario-liquido"),
        new(SalaryBandCatalog.FormatCurrency(gross), SalaryBandCatalog.SlugPath(gross))
    ];

    public static IReadOnlyList<BreadcrumbItem> CltPjHubBreadcrumbs() =>
    [
        new("Início", "/"),
        new("CLT x PJ", "/clt-pj")
    ];

    public static IReadOnlyList<BreadcrumbItem> CltPjBandBreadcrumbs(int cltGross) =>
    [
        new("Início", "/"),
        new("CLT x PJ", "/clt-pj"),
        new($"{CltPjBandCatalog.FormatCurrency(cltGross)} CLT", CltPjBandCatalog.SlugPath(cltGross))
    ];

    public static IReadOnlyList<BreadcrumbItem> PopularQuestionsHubBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Dúvidas populares", "/duvidas")
    ];

    public static IReadOnlyList<BreadcrumbItem> PopularQuestionBreadcrumbs(string title, string slug) =>
    [
        new("Início", "/"),
        new("Dúvidas populares", "/duvidas"),
        new(title, PopularQuestionsCatalog.SlugPath(slug))
    ];

    public static IReadOnlyList<BreadcrumbItem> LocalPanelBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Meu painel", "/meu-painel")
    ];

    public static IReadOnlyList<BreadcrumbItem> WidgetHubBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Widget incorporável", "/widget")
    ];

    public static IReadOnlyList<BreadcrumbItem> InternalMetricsBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Métricas internas", "/metricas-internas")
    ];

    public static IReadOnlyList<BreadcrumbItem> AboutBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Sobre", "/sobre")
    ];

    public static IReadOnlyList<BreadcrumbItem> HowWeCalculateBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Como calculamos", "/como-calculamos")
    ];

    public static IReadOnlyList<BreadcrumbItem> PrivacyPolicyBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Política de Privacidade", "/politica-de-privacidade")
    ];

    public static IReadOnlyList<BreadcrumbItem> TermsBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Termos de Uso", "/termos-de-uso")
    ];

    public static IReadOnlyList<BreadcrumbItem> LegalNoticeBreadcrumbs() =>
    [
        new("Início", "/"),
        new("Aviso Legal", "/aviso-legal")
    ];
}
