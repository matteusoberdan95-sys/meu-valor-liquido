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

        var hubSlugs = new[] { "pj-vs-clt", "ferias", "custo-funcionario", "decimo-terceiro" };
        HubSecondaryCalculators = hubSlugs
            .Select(slug => all.FirstOrDefault(c => c.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)))
            .Where(c => c is not null)
            .Cast<CalculatorDefinition>()
            .Where(c => Calculators.Any(x => x.Slug == c.Slug))
            .ToList();

        if (HubSecondaryCalculators.Count == 0)
        {
            HubSecondaryCalculators = Calculators
                .Where(c => FeaturedCalculator is null || c.Slug != FeaturedCalculator.Slug)
                .Take(4)
                .ToList();
        }
    }
}
