namespace MeuValorLiquido.WebApp.Infrastructure;
public sealed record BreadcrumbItem(string Name, string Path);

/// <summary>Centraliza metadados SEO para Razor Pages via ViewData.</summary>
public static class SeoMetadataHelper
{
    public const string SiteName = "Meu Valor Líquido";
    public const string DefaultOgImagePath = "/images/og-default.svg";
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
}
