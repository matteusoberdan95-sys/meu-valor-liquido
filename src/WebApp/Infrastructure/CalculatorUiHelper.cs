namespace MeuValorLiquido.WebApp.Infrastructure;
public static class CalculatorUiHelper
{
    private static readonly Dictionary<string, string> Icons = new(StringComparer.OrdinalIgnoreCase)
    {
        ["salario-liquido"] = "payments",
        ["salario-bruto-necessario"] = "price_check",
        ["ferias"] = "beach_access",
        ["decimo-terceiro"] = "celebration",
        ["rescisao-clt"] = "logout",
        ["hora-extra"] = "schedule",
        ["inss"] = "account_balance",
        ["irrf"] = "account_balance_wallet",
        ["pj-vs-clt"] = "compare_arrows",
        ["juros-compostos"] = "trending_up",
        ["financiamento"] = "house",
        ["fgts"] = "savings",
        ["simulador-mei"] = "storefront",
        ["custo-funcionario"] = "groups",
        ["multa-atraso"] = "gavel",
        ["conversor-salario"] = "swap_horiz"
    };

    public static string GetIcon(string slug) =>
        Icons.TryGetValue(slug, out var icon) ? icon : "calculate";

    public static string GetBadgeClass(string category) => category.ToLowerInvariant() switch
    {
        "trabalhista" => "valora-badge-trabalhista",
        "fiscal" => "valora-badge-fiscal",
        "financeiro" => "valora-badge-financeiro",
        _ => "valora-badge-trabalhista"
    };
}
