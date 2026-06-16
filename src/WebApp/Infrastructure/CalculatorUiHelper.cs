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
}
