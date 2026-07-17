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

    public int TotalCount { get; private set; }

    public IReadOnlyDictionary<string, int> CategoryCounts { get; private set; } =
        new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyList<CalculatorDefinition> PopularCalculators { get; private set; } = [];

    public void OnGet()
    {
        var all = catalogService.GetAll();
        TotalCount = all.Count;
        Categories = all.Select(c => c.Category).Distinct().OrderBy(c => c).ToList();
        CategoryCounts = all
            .GroupBy(c => c.Category, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.Count(), StringComparer.OrdinalIgnoreCase);

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

        var popularSlugs = new[] { "salario-liquido", "pj-vs-clt", "juros-compostos", "simulador-mei", "rescisao-clt" };
        PopularCalculators = popularSlugs
            .Select(slug => all.FirstOrDefault(c => c.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)))
            .Where(c => c is not null)
            .Cast<CalculatorDefinition>()
            .Where(c => Calculators.Any(x => x.Slug.Equals(c.Slug, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (Request.QueryString.HasValue)
        {
            ViewData["Robots"] = SeoMetadataHelper.NoIndexFollowRobots;
        }
    }
}
