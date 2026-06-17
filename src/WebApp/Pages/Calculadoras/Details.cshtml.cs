namespace MeuValorLiquido.WebApp.Pages.Calculadoras;
public class DetailsModel : PageModel
{
    private readonly ICalculatorApplicationService calculatorService;
    private readonly ICalculatorCatalogService catalogService;
    private readonly ICalculatorFieldProfileProvider fieldProfileProvider;
    private readonly IAdSlotProvider adSlotProvider;
    private readonly CalculatorShareLinkBuilder shareLinkBuilder;
    private readonly IProductMetricsService productMetricsService;
    private readonly CltPjComparisonCalculator cltPjComparisonCalculator;

    public DetailsModel(
        ICalculatorApplicationService calculatorService,
        ICalculatorCatalogService catalogService,
        ICalculatorFieldProfileProvider fieldProfileProvider,
        IAdSlotProvider adSlotProvider,
        CalculatorShareLinkBuilder shareLinkBuilder,
        IProductMetricsService productMetricsService,
        CltPjComparisonCalculator cltPjComparisonCalculator)
    {
        this.calculatorService = calculatorService;
        this.catalogService = catalogService;
        this.fieldProfileProvider = fieldProfileProvider;
        this.adSlotProvider = adSlotProvider;
        this.shareLinkBuilder = shareLinkBuilder;
        this.productMetricsService = productMetricsService;
        this.cltPjComparisonCalculator = cltPjComparisonCalculator;
    }

    public CalculatorDefinition? Definition { get; private set; }

    public CalculationResult? Result { get; private set; }

    public CalculatorFieldProfile FieldProfile { get; private set; } = new();

    public AdSlotDefinition? TopAdSlot { get; private set; }

    public AdSlotDefinition? BottomAdSlot { get; private set; }

    public CalculatorShareViewModel? Share { get; private set; }

    public CalculatorResultPanelViewModel? ResultPanel { get; private set; }

    public CltPjComparisonBreakdown? CltPjBreakdown { get; private set; }

    public bool IsEmbedMode { get; private set; }

    public bool IsPjVsCltStitch =>
        Definition?.Slug.Equals("pj-vs-clt", StringComparison.OrdinalIgnoreCase) == true && !IsEmbedMode;

    [BindProperty]
    public CalculatorInput Input { get; set; } = CalculatorInputDefaults.ForSlug("salario-liquido");

    public async Task<IActionResult> OnGetAsync(string slug)
    {
        if (!TryBeginRequest(slug, out var reject))
        {
            return reject!;
        }

        LoadPage(slug);
        if (Definition is not null)
        {
            Input = CalculatorInputDefaults.ForSlug(slug);
            if (slug.Equals("salario-liquido", StringComparison.OrdinalIgnoreCase)
                && decimal.TryParse(Request.Query["valor"], out var presetGross)
                && presetGross > 0m)
            {
                Input = Input with { Amount = presetGross };
            }
            else if (slug.Equals("pj-vs-clt", StringComparison.OrdinalIgnoreCase)
                && decimal.TryParse(Request.Query["valor"], out var cltPreset)
                && cltPreset > 0m)
            {
                Input = CalculatorInputDefaults.ForSlug(slug) with { Amount = cltPreset };
            }

            TryApplySharedCalculation(slug);
            if (IsEmbedMode)
            {
                await productMetricsService.RecordAsync(ProductMetricEvents.WidgetView, slug);
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string slug)
    {
        if (!TryBeginRequest(slug, out var reject))
        {
            return reject!;
        }

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

        await productMetricsService.RecordAsync(ProductMetricEvents.CalculatorCalculation, slug);

        var token = CalculatorInputShareCodec.Encode(Input);
        var embedQuery = IsEmbedMode ? "&embed=1" : string.Empty;
        return Redirect($"/calculadoras/{slug}?r={Uri.EscapeDataString(token)}{embedQuery}");
    }

    private bool TryBeginRequest(string slug, out IActionResult? reject)
    {
        reject = null;
        IsEmbedMode = string.Equals(Request.Query["embed"].ToString(), "1", StringComparison.Ordinal);
        if (!IsEmbedMode)
        {
            return true;
        }

        if (!EmbedWidgetCatalog.IsEmbeddable(slug))
        {
            reject = NotFound();
            return false;
        }

        ViewData["Robots"] = SeoMetadataHelper.NoIndexRobots;
        return true;
    }

    private void TryApplySharedCalculation(string slug)
    {
        if (!CalculatorInputShareCodec.TryDecode(Request.Query["r"], out var sharedInput))
        {
            return;
        }

        Input = sharedInput;
        var result = calculatorService.Calculate(slug, Input);
        if (result.IsFailure)
        {
            return;
        }

        Result = result.Value;
        BuildShare(slug);
        if (slug.Equals("pj-vs-clt", StringComparison.OrdinalIgnoreCase))
        {
            CltPjBreakdown = cltPjComparisonCalculator.Compare(Input);
        }
        else
        {
            ResultPanel = new CalculatorResultPanelViewModel(
                Result,
                Input,
                slug,
                CalculatorResultExplanationFactory.Build(slug, Input, Result, catalogService),
                ShowSimpleExplanation: !IsEmbedMode);
        }
    }

    private void BuildShare(string slug)
    {
        if (Result is null || IsEmbedMode)
        {
            return;
        }

        var shareUrl = shareLinkBuilder.BuildShareUrl(slug, Input, Request);
        var shareText = CalculatorShareTextBuilder.Build(Result, shareUrl);
        Share = new CalculatorShareViewModel(
            shareUrl,
            shareText,
            CalculatorShareLinkBuilder.BuildWhatsAppUrl(shareText),
            shareLinkBuilder.BuildPdfUrl(slug, Input),
            LocalPanelSaveContextBuilder.FromCalculation(Definition!, Result, Input));
    }

    private void LoadPage(string slug)
    {
        Definition = catalogService.GetBySlug(slug);
        if (Definition is null)
        {
            return;
        }

        FieldProfile = fieldProfileProvider.GetProfile(slug);
        if (IsEmbedMode)
        {
            TopAdSlot = null;
            BottomAdSlot = null;
            return;
        }

        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");
    }
}
