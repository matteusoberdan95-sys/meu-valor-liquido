namespace MeuValorLiquido.WebApp.Pages.Calculadoras;
public class IndexModel : PageModel
{
    private readonly ICalculatorCatalogService catalogService;

    public IndexModel(ICalculatorCatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    public IReadOnlyList<CalculatorDefinition> Calculators { get; private set; } = [];

    public IReadOnlyList<string> Categories { get; private set; } = [];

    [BindProperty(SupportsGet = true, Name = "categoria")]
    public string? Category { get; set; }

    public void OnGet()
    {
        var all = catalogService.GetAll();
        Categories = all.Select(c => c.Category).Distinct().OrderBy(c => c).ToList();

        Calculators = string.IsNullOrWhiteSpace(Category)
            ? all
            : all.Where(c => c.Category.Equals(Category, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
