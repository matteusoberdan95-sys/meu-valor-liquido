namespace MeuValorLiquido.WebApp.Pages.Calculadoras;

public class DetailsModel : PageModel
{
    private readonly ICalculatorApplicationService calculatorService;
    private readonly ICalculatorCatalogService catalogService;
    private readonly ICalculatorFieldProfileProvider fieldProfileProvider;
    private readonly IAdSlotProvider adSlotProvider;

    public DetailsModel(
        ICalculatorApplicationService calculatorService,
        ICalculatorCatalogService catalogService,
        ICalculatorFieldProfileProvider fieldProfileProvider,
        IAdSlotProvider adSlotProvider)
    {
        this.calculatorService = calculatorService;
        this.catalogService = catalogService;
        this.fieldProfileProvider = fieldProfileProvider;
        this.adSlotProvider = adSlotProvider;
    }

    public CalculatorDefinition? Definition { get; private set; }

    public CalculationResult? Result { get; private set; }

    public CalculatorFieldProfile FieldProfile { get; private set; } = new();

    public AdSlotDefinition? TopAdSlot { get; private set; }

    public AdSlotDefinition? BottomAdSlot { get; private set; }

    [BindProperty]
    public CalculatorInput Input { get; set; } = new(4000m, Months: 12, Rate: 50m);

    public IActionResult OnGet(string slug)
    {
        LoadPage(slug);
        return Page();
    }

    public IActionResult OnPost(string slug)
    {
        LoadPage(slug);
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

    private void LoadPage(string slug)
    {
        Definition = catalogService.GetBySlug(slug);
        if (Definition is null)
        {
            return;
        }

        FieldProfile = fieldProfileProvider.GetProfile(slug);
        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");
    }
}
