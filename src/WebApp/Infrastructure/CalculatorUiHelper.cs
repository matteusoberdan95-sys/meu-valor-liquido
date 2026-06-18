namespace MeuValorLiquido.WebApp.Infrastructure;
public static class CalculatorUiHelper
{
    private static readonly Dictionary<string, string> Icons = new(StringComparer.OrdinalIgnoreCase)
    {
        ["salario-liquido"] = "payments",
        ["salario-bruto-necessario"] = "price_check",
        ["proposta-salarial"] = "handshake",
        ["ferias"] = "event_repeat",
        ["decimo-terceiro"] = "celebration",
        ["rescisao-clt"] = "logout",
        ["hora-extra"] = "schedule",
        ["inss"] = "account_balance",
        ["irrf"] = "account_balance_wallet",
        ["pj-vs-clt"] = "compare_arrows",
        ["juros-compostos"] = "trending_up",
        ["financiamento"] = "house",
        ["fgts"] = "savings",
        ["simulador-mei"] = "request_quote",
        ["custo-funcionario"] = "groups",
        ["multa-atraso"] = "gavel",
        ["conversor-salario"] = "swap_horiz"
    };

    public static string GetIcon(string slug) =>
        Icons.TryGetValue(slug, out var icon) ? icon : "calculate";

    public static string GetSimulationBadge(string category) => category.ToLowerInvariant() switch
    {
        "trabalhista" => "Simulação CLT",
        "fiscal" => "Simulação fiscal",
        "financeiro" => "Simulação financeira",
        _ => "Simulação"
    };

    public static string GetBadgeClass(string category) => category.ToLowerInvariant() switch
    {
        "trabalhista" => "valora-badge-trabalhista",
        "fiscal" => "valora-badge-fiscal",
        "financeiro" => "valora-badge-financeiro",
        _ => "valora-badge-trabalhista"
    };

    public static string GetAccentCardClass(string category) => category.ToLowerInvariant() switch
    {
        "trabalhista" => "valora-card--accent-trabalhista",
        "fiscal" => "valora-card--accent-fiscal",
        "financeiro" => "valora-card--accent-financeiro",
        _ => "valora-card--accent-financeiro"
    };

    public static string GetBentoAccentClass(string category, string slug) =>
        slug.Equals("ferias", StringComparison.OrdinalIgnoreCase)
            ? "valora-stitch-bento-card--warning"
            : category.ToLowerInvariant() switch
            {
                "trabalhista" => "valora-stitch-bento-card--trabalhista",
                "fiscal" => "valora-stitch-bento-card--fiscal",
                "financeiro" => "valora-stitch-bento-card--financeiro",
                _ => "valora-stitch-bento-card--financeiro"
            };

    public static string GetBentoIconBoxClass(string category, string slug) =>
        slug.Equals("ferias", StringComparison.OrdinalIgnoreCase)
            ? "valora-bento-icon-box--primary"
            : category.ToLowerInvariant() switch
            {
                "trabalhista" => "valora-bento-icon-box--trabalhista",
                "fiscal" => "valora-bento-icon-box--fiscal",
                "financeiro" => "valora-bento-icon-box--financeiro",
                _ => "valora-bento-icon-box--financeiro"
            };

    public static string GetBentoIconColorClass(string category, string slug) =>
        slug.Equals("ferias", StringComparison.OrdinalIgnoreCase)
            ? "valora-bento-icon--warning"
            : category.ToLowerInvariant() switch
            {
                "trabalhista" => "valora-bento-icon--trabalhista",
                "fiscal" => "valora-bento-icon--fiscal",
                "financeiro" => "valora-bento-icon--financeiro",
                _ => "valora-bento-icon--financeiro"
            };

    public static string GetHubIconBgClass(string category, string slug) =>
        slug.Equals("decimo-terceiro", StringComparison.OrdinalIgnoreCase)
            ? "valora-hub-icon-bg--warning"
            : category.ToLowerInvariant() switch
            {
                "trabalhista" => "valora-hub-icon-bg--trabalhista",
                "fiscal" => "valora-hub-icon-bg--fiscal",
                "financeiro" => "valora-hub-icon-bg--financeiro",
                _ => "valora-hub-icon-bg--financeiro"
            };

    public static string GetHubCategoryBadgeClass(string category) => category.ToLowerInvariant() switch
    {
        "fiscal" => "valora-stitch-hub-row-badge--fiscal",
        "financeiro" => "valora-stitch-hub-row-badge--financeiro",
        _ => "valora-stitch-hub-row-badge--trabalhista"
    };

    private static readonly HashSet<string> TemplateC1Slugs = new(StringComparer.OrdinalIgnoreCase)
    {
        "salario-bruto-necessario",
        "proposta-salarial",
        "ferias",
        "decimo-terceiro",
        "hora-extra",
        "inss",
        "irrf",
        "juros-compostos",
        "financiamento",
        "fgts",
        "simulador-mei",
        "custo-funcionario",
        "multa-atraso",
        "conversor-salario"
    };

    public static bool IsTemplateC1Slug(string slug) => TemplateC1Slugs.Contains(slug);

    public static string GetStitchDetailModifierClass(string slug) => slug.ToLowerInvariant() switch
    {
        "inss" or "irrf" or "simulador-mei" => " valora-stitch-calc-detail--fiscal",
        "ferias" or "decimo-terceiro" => " valora-stitch-calc-detail--layered",
        "juros-compostos" or "financiamento" or "multa-atraso" => " valora-stitch-calc-detail--financial",
        _ when IsTemplateC1Slug(slug) => " valora-stitch-calc-detail--trabalhista",
        _ => string.Empty
    };

    public static string GetHubCardTag(string slug) => slug.ToLowerInvariant() switch
    {
        "pj-vs-clt" => "Novo layout",
        "ferias" => "Completa",
        "custo-funcionario" => "Empresarial",
        "decimo-terceiro" => "Essencial",
        "simulador-mei" => "MEI",
        "rescisao-clt" => "CLT",
        _ => "Popular"
    };

    public static string GetBentoSubtitle(string slug, string category) => slug.ToLowerInvariant() switch
    {
        "salario-liquido" => "CLT e descontos",
        "pj-vs-clt" => "Impostos e lucro",
        "simulador-mei" => "MEI e DAS",
        "ferias" => "Provisão e terço",
        "juros-compostos" => "Futuro do patrimônio",
        "rescisao-clt" => "Verbas e multa FGTS",
        "decimo-terceiro" => "Parcelas do benefício",
        "financiamento" => "Parcela e custo total",
        _ => category
    };

    public static string GetHomeDesktopCardSummary(string slug) => slug.ToLowerInvariant() switch
    {
        "salario-liquido" => "Cálculo completo de IRRF, INSS e descontos em folha com tabelas atualizadas.",
        "rescisao-clt" => "Calcule verbas rescisórias, férias, aviso prévio e multa do FGTS.",
        "pj-vs-clt" => "Qual modelo de contratação compensa mais para o seu perfil profissional?",
        "decimo-terceiro" => "Calcule o valor das parcelas do seu benefício de fim de ano.",
        "financiamento" => "Analise parcela, juros e se o financiamento cabe no orçamento.",
        _ => string.Empty
    };
}
