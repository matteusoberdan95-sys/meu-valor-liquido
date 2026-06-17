namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record FaqHubCategoryCard(
    string? FilterCategory,
    string Title,
    string Summary,
    string Icon,
    string AccentClass,
    string? Href = null);

public static class FaqHubHelper
{
    public static readonly IReadOnlyList<FaqHubCategoryCard> CategoryCards =
    [
        new("Trabalhista", "Regime CLT", "Leis trabalhistas e benefícios.", "work", "valora-stitch-faq-cat--trabalhista"),
        new("Financeiro", "Regime PJ & MEI", "Gestão e formalização.", "business_center", "valora-stitch-faq-cat--pj"),
        new("Fiscal", "Impostos", "Cálculos e retenções.", "receipt_long", "valora-stitch-faq-cat--fiscal"),
        new(null, "Sobre Nós", "Nossa missão e valores.", "info", "valora-stitch-faq-cat--about", "/sobre")
    ];

    public static IReadOnlyList<PopularQuestionDefinition> FilterQuestions(
        IReadOnlyList<PopularQuestionDefinition> all,
        string? query,
        string? category)
    {
        IEnumerable<PopularQuestionDefinition> items = all;

        if (!string.IsNullOrWhiteSpace(category))
        {
            items = items.Where(q => q.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var term = query.Trim();
            items = items.Where(q =>
                q.Title.Contains(term, StringComparison.OrdinalIgnoreCase)
                || q.Category.Contains(term, StringComparison.OrdinalIgnoreCase)
                || q.SeoDescription.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        return items.ToList();
    }
}
