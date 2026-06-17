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

    [BindProperty(SupportsGet = true, Name = "q")]
    public string? Query { get; set; }

    public CalculatorDefinition? FeaturedCalculator { get; private set; }

    public IReadOnlyList<CalculatorDefinition> HubSecondaryCalculators { get; private set; } = [];

    public void OnGet()
    {
        var all = catalogService.GetAll();
        Categories = all.Select(c => c.Category).Distinct().OrderBy(c => c).ToList();

        var filtered = all.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(Category))
        {
            filtered = filtered.Where(c => c.Category.Equals(Category, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(Query))
        {
            filtered = filtered.Where(c =>
                c.Name.Contains(Query, StringComparison.OrdinalIgnoreCase)
                || c.Summary.Contains(Query, StringComparison.OrdinalIgnoreCase)
                || c.Slug.Contains(Query, StringComparison.OrdinalIgnoreCase));
        }

        Calculators = filtered.ToList();

        FeaturedCalculator = all.FirstOrDefault(c => c.Slug.Equals("salario-liquido", StringComparison.OrdinalIgnoreCase))
            ?? Calculators.FirstOrDefault();

        HubSecondaryCalculators = Calculators
            .Where(c => FeaturedCalculator is null || !c.Slug.Equals(FeaturedCalculator.Slug, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
