namespace MeuValorLiquido.WebApp.Pages.Calculadoras;

public class IndexModel : PageModel
{
    private readonly ICalculatorCatalogService catalogService;

    public IndexModel(ICalculatorCatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    public IReadOnlyList<CalculatorDefinition> Calculators { get; private set; } = [];

    public void OnGet()
    {
        Calculators = catalogService.GetAll();
    }
}
