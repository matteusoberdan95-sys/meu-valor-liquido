namespace MeuValorLiquido.WebApp.Pages.Desligamento;

public class IndexModel(ThematicHubLoader loader) : PageModel
{
    public ThematicHubPageViewModel HubPage { get; private set; } = null!;

    public IReadOnlyList<FaqItem> FaqSchemaItems { get; private set; } = [];

    public IActionResult OnGet()
    {
        var result = loader.Load(this, ThematicHubCatalog.Desligamento, out var hubPage);
        HubPage = hubPage;
        FaqSchemaItems = ThematicHubFaqSchemaBuilder.Build(hubPage.Faqs);
        return result;
    }
}
