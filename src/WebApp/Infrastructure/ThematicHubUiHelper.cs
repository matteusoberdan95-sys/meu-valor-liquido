namespace MeuValorLiquido.WebApp.Infrastructure;

public static class ThematicHubUiHelper
{
    private static readonly Dictionary<string, string> NavIcons = new(StringComparer.OrdinalIgnoreCase)
    {
        [ThematicHubCatalog.Desligamento] = "logout",
        [ThematicHubCatalog.NegociarSalario] = "handshake",
        [ThematicHubCatalog.VirarPj] = "compare_arrows"
    };

    public static string GetNavIcon(string hubId) =>
        NavIcons.TryGetValue(hubId, out var icon) ? icon : "route";

    public static string GetNavTeaser(ThematicHubDefinition hub) => hub.Id switch
    {
        ThematicHubCatalog.Desligamento =>
            "Checklist, simuladores de rescisão, FGTS e próximos passos após sair da empresa.",
        ThematicHubCatalog.NegociarSalario =>
            "Compare propostas pelo líquido, valide holerite e negocie com números claros.",
        ThematicHubCatalog.VirarPj =>
            "CLT x PJ, MEI e custo oculto dos benefícios antes de abrir CNPJ.",
        _ => hub.HeroLead
    };

    public static string GetNavCtaLabel(ThematicHubDefinition hub) => hub.Id switch
    {
        ThematicHubCatalog.Desligamento => "Ver guia de desligamento",
        ThematicHubCatalog.NegociarSalario => "Ver guia de negociação",
        ThematicHubCatalog.VirarPj => "Ver guia CLT x PJ",
        _ => "Ver guia completo"
    };
}
