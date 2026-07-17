namespace MeuValorLiquido.WebApp.Pages.Assistente;
public class IndexModel : PageModel
{
    private readonly IAdSlotProvider adSlotProvider;

    public IndexModel(IAdSlotProvider adSlotProvider)
    {
        this.adSlotProvider = adSlotProvider;
    }

    public IReadOnlyList<AssistantPrompt> Prompts { get; private set; } = [];

    public IReadOnlyList<AssistantShortcut> Shortcuts { get; private set; } = [];

    public AdSlotDefinition? AdSlot { get; private set; }

    public void OnGet()
    {
        ViewData["BottomNavTab"] = "assistant";
        ViewData["HideAssistantLauncher"] = true;

        Prompts =
        [
            new("Quanto desconta de INSS?", "inss"),
            new("Como calcular IRRF?", "irrf"),
            new("CLT ou PJ vale mais?", "clt-pj"),
            new("Quanto recebo na rescisão?", "rescisao"),
            new("Como conferir meu holerite?", "holerite")
        ];

        Shortcuts =
        [
            new("Salário líquido", "/calculadoras/salario-liquido", "calculate", "Simule bruto, INSS, IRRF e descontos."),
            new("INSS", "/calculadoras/inss", "account_balance", "Veja o desconto progressivo."),
            new("IRRF", "/calculadoras/irrf", "receipt_long", "Entenda imposto de renda no salário."),
            new("Rescisão CLT", "/calculadoras/rescisao-clt", "work_off", "Estime verbas de saída."),
            new("Conferir holerite", "/conferir-holerite", "fact_check", "Compare desconto esperado e informado.")
        ];

        AdSlot = adSlotProvider.GetSlots().FirstOrDefault(s => s.Key == "calculator-bottom");

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Assistente educativo de salário, CLT e descontos",
                "Tire dúvidas educativas sobre salário líquido, INSS, IRRF, rescisão, férias e CLT vs PJ com links para calculadoras do Meu Valor Líquido.",
                "/assistente",
                SeoMetadataHelper.NoIndexFollowRobots));
    }
}

public sealed record AssistantPrompt(string Text, string Intent);

public sealed record AssistantShortcut(string Title, string Href, string Icon, string Summary);
