namespace MeuValorLiquido.WebApp.Pages.VirarPj;

public class IndexModel(ThematicHubLoader loader) : PageModel
{
    public ThematicHubPageViewModel HubPage { get; private set; } = null!;

    public IActionResult OnGet()
    {
        var result = loader.Load(this, ThematicHubCatalog.VirarPj, out var hubPage);
        HubPage = hubPage;
        return result;
    }
}
