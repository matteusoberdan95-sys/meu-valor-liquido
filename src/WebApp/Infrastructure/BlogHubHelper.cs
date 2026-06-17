using MeuValorLiquido.Modules.Content;

namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record BlogHubCategoryChip(
    string? FilterKey,
    string Label,
    string? MatchCategory = null,
    string? ContentKeyword = null,
    bool ExcludeKeywordMatch = false);

public static class BlogHubHelper
{
    public static readonly IReadOnlyList<BlogHubCategoryChip> CategoryChips =
    [
        new(null, "Tudo"),
        new("trabalhista", "CLT & Direitos", "Trabalhista"),
        new("financeiro", "PJ & MEI", "Financeiro", "invest", ExcludeKeywordMatch: true),
        new("fiscal", "Imposto de Renda", "Fiscal"),
        new("investimentos", "Investimentos", "Financeiro", "invest")
    ];

    public static IReadOnlyList<BlogPost> FilterPosts(IReadOnlyList<BlogPost> all, string? categoryKey)
    {
        if (string.IsNullOrWhiteSpace(categoryKey))
        {
            return all;
        }

        var chip = CategoryChips.FirstOrDefault(c =>
            c.FilterKey?.Equals(categoryKey, StringComparison.OrdinalIgnoreCase) == true);

        if (chip?.MatchCategory is null)
        {
            return all;
        }

        IEnumerable<BlogPost> items = all.Where(p =>
            p.Category?.Equals(chip.MatchCategory, StringComparison.OrdinalIgnoreCase) == true);

        if (!string.IsNullOrWhiteSpace(chip.ContentKeyword))
        {
            items = chip.ExcludeKeywordMatch
                ? items.Where(p => !MatchesKeyword(p, chip.ContentKeyword!))
                : items.Where(p => MatchesKeyword(p, chip.ContentKeyword!));
        }

        return items.ToList();
    }

    public static string GetFeaturedBorderClass(string? category) =>
        category?.ToLowerInvariant() switch
        {
            "fiscal" => "valora-stitch-blog-featured--fiscal",
            "financeiro" => "valora-stitch-blog-featured--financeiro",
            _ => "valora-stitch-blog-featured--trabalhista"
        };

    public static string GetVisualClass(string? category) =>
        category?.ToLowerInvariant() switch
        {
            "fiscal" => "valora-stitch-blog-visual--fiscal",
            "financeiro" => "valora-stitch-blog-visual--financeiro",
            _ => "valora-stitch-blog-visual--trabalhista"
        };

    public static string GetVisualIcon(string? category, string slug) =>
        slug.ToLowerInvariant() switch
        {
            "como-calcular-ferias" => "event_repeat",
            "planejamento-financeiro-com-salario" => "trending_up",
            "o-que-e-salario-liquido" => "payments",
            _ => category?.ToLowerInvariant() switch
            {
                "fiscal" => "receipt_long",
                "financeiro" => "business_center",
                _ => "article"
            }
        };

    private static bool MatchesKeyword(BlogPost post, string keyword) =>
        post.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
        || post.Summary.Contains(keyword, StringComparison.OrdinalIgnoreCase)
        || post.Slug.Contains(keyword, StringComparison.OrdinalIgnoreCase);
}
