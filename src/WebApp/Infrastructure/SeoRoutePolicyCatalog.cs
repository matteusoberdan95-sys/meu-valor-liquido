namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record IndexableStaticRoute(string Path, DateOnly LastReviewedAt);

public static class SeoRoutePolicyCatalog
{
    private static readonly DateOnly SeoBaselineLastModified = new(2026, 6, 29);

    public static IReadOnlyList<IndexableStaticRoute> IndexableStaticRoutes { get; } =
    [
        new("/", SeoBaselineLastModified),
        new("/calculadoras", SeoBaselineLastModified),
        new("/sobre", new DateOnly(2026, 7, 17)),
        new("/autores/matteus-oberdan", new DateOnly(2026, 7, 17)),
        new("/contato", SeoBaselineLastModified),
        new("/blog", SeoBaselineLastModified),
        new("/mapa-do-site", new DateOnly(2026, 7, 17)),
        new("/salario-liquido", SeoBaselineLastModified),
        new("/clt-pj", SeoBaselineLastModified),
        new("/duvidas", SeoBaselineLastModified),
        new("/desligamento", SeoBaselineLastModified),
        new("/negociar-salario", SeoBaselineLastModified),
        new("/virar-pj", SeoBaselineLastModified),
        new("/politica-de-privacidade", SeoBaselineLastModified),
        new("/politica-de-cookies", SeoBaselineLastModified),
        new("/politica-editorial", new DateOnly(2026, 7, 17)),
        new("/termos-de-uso", SeoBaselineLastModified),
        new("/aviso-legal", SeoBaselineLastModified),
        new("/como-calculamos", SeoBaselineLastModified),
        new("/conferir-holerite", SeoBaselineLastModified)
    ];

    public static IReadOnlyList<string> NoIndexPagePaths { get; } =
    [
        "/assistente",
        "/correcoes",
        "/meu-painel",
        "/metricas-internas",
        "/newsletter",
        "/widget",
        "/Error",
        "/NotFound"
    ];

    public static bool IsNoIndexPage(PathString path) =>
        NoIndexPagePaths.Any(route => path.Equals(route, StringComparison.OrdinalIgnoreCase));

    public static bool RequiresNoIndexHeader(PathString path)
    {
        var value = path.Value ?? string.Empty;
        return IsNoIndexPage(path)
            || value.StartsWith("/api/", StringComparison.OrdinalIgnoreCase)
            || value.Equals("/health", StringComparison.OrdinalIgnoreCase)
            || value.EndsWith("/resultado.pdf", StringComparison.OrdinalIgnoreCase);
    }
}
