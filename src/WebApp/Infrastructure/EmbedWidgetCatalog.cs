namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record EmbedWidgetDefinition(
    string Slug,
    string Name,
    string Summary,
    int RecommendedHeight);

public static class EmbedWidgetCatalog
{
    private static readonly IReadOnlyList<EmbedWidgetDefinition> All =
    [
        new("salario-liquido", "Salário líquido", "Estime INSS, IRRF e descontos a partir do bruto.", 560),
        new("salario-bruto-necessario", "Salário bruto necessário", "Descubra quanto pedir de bruto para um líquido alvo.", 580),
        new("proposta-salarial", "Proposta salarial", "Compare salário atual e proposto no bolso.", 600),
        new("inss", "INSS", "Desconto previdenciário por faixa em 2026.", 420),
        new("irrf", "IRRF", "Imposto de renda retido na fonte estimado.", 460),
        new("ferias", "Férias", "Valor de férias com adicional de 1/3.", 480),
        new("decimo-terceiro", "Décimo terceiro", "13º proporcional ou integral.", 460),
        new("pj-vs-clt", "PJ vs CLT", "Compare líquido CLT e cenário PJ.", 620)
    ];

    private static readonly HashSet<string> Slugs = All
        .Select(w => w.Slug)
        .ToHashSet(StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyList<EmbedWidgetDefinition> GetAll() => All;

    public static bool IsEmbeddable(string slug) => Slugs.Contains(slug);

    public static EmbedWidgetDefinition? GetBySlug(string slug) =>
        All.FirstOrDefault(w => w.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));

    public static string WidgetPath(string slug) => $"/widget/{slug}";
}
