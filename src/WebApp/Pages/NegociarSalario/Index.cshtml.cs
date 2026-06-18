namespace MeuValorLiquido.WebApp.Pages.NegociarSalario;

public class IndexModel(ThematicHubLoader loader) : PageModel
{
    public ThematicHubPageViewModel HubPage { get; private set; } = null!;

    public IActionResult OnGet()
    {
        var result = loader.Load(this, ThematicHubCatalog.NegociarSalario, out var hubPage);
        HubPage = hubPage;
        return result;
    }
}
