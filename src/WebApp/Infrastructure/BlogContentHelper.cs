namespace MeuValorLiquido.WebApp.Infrastructure;

public static class BlogContentHelper
{
    public static int EstimateReadingMinutes(string htmlContent)
    {
        var text = System.Text.RegularExpressions.Regex.Replace(htmlContent, "<[^>]+>", " ");
        var words = text.Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries).Length;
        return Math.Max(1, (int)Math.Ceiling(words / 200.0));
    }

    public static string GetCategoryLabel(string? category) => category ?? "Geral";

    public static string GetCategoryBadgeClass(string? category) =>
        category?.ToLowerInvariant() switch
        {
            "fiscal" => "valora-stitch-home-blog-badge--fiscal",
            "financeiro" => "valora-stitch-home-blog-badge--financeiro",
            _ => "valora-stitch-home-blog-badge--trabalhista"
        };

    public static string GetCategoryTextClass(string? category) =>
        category?.ToLowerInvariant() switch
        {
            "fiscal" => "valora-stitch-blog-cat-text--fiscal",
            "financeiro" => "valora-stitch-blog-cat-text--financeiro",
            _ => "valora-stitch-blog-cat-text--trabalhista"
        };
}
