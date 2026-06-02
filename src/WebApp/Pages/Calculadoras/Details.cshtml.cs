namespace MeuValorLiquido.WebApp.Pages.Calculadoras;

public class DetailsModel : PageModel
{
    private readonly ICalculatorApplicationService calculatorService;
    private readonly ICalculatorCatalogService catalogService;

    public DetailsModel(ICalculatorApplicationService calculatorService, ICalculatorCatalogService catalogService)
    {
        this.calculatorService = calculatorService;
        this.catalogService = catalogService;
    }

    public CalculatorDefinition? Definition { get; private set; }

    public CalculationResult? Result { get; private set; }

    [BindProperty]
    public CalculatorInput Input { get; set; } = new(4000m, Months: 12, Rate: 50m);

    public IActionResult OnGet(string slug)
    {
        Definition = catalogService.GetBySlug(slug);
        return Page();
    }

    public IActionResult OnPost(string slug)
    {
        Definition = catalogService.GetBySlug(slug);
        if (Definition is null)
        {
            return Page();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var result = calculatorService.Calculate(slug, Input);
        if (result.IsFailure)
        {
            ModelState.AddModelError(string.Empty, result.Error.Message);
            return Page();
        }

        Result = result.Value;
        return Page();
    }
}
