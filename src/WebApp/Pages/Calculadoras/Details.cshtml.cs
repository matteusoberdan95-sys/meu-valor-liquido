namespace MeuValorLiquido.WebApp.Pages.Calculadoras;
public class DetailsModel : PageModel
{
    private readonly ICalculatorApplicationService calculatorService;
    private readonly ICalculatorCatalogService catalogService;
    private readonly ICalculatorFieldProfileProvider fieldProfileProvider;
    private readonly IAdSlotProvider adSlotProvider;
    private readonly CalculatorShareLinkBuilder shareLinkBuilder;
    private readonly CalculatorJourneyLinkBuilder journeyLinkBuilder;
    private readonly IProductMetricsService productMetricsService;
    private readonly CltPjComparisonCalculator cltPjComparisonCalculator;

    public DetailsModel(
        ICalculatorApplicationService calculatorService,
        ICalculatorCatalogService catalogService,
        ICalculatorFieldProfileProvider fieldProfileProvider,
        IAdSlotProvider adSlotProvider,
        CalculatorShareLinkBuilder shareLinkBuilder,
        CalculatorJourneyLinkBuilder journeyLinkBuilder,
        IProductMetricsService productMetricsService,
        CltPjComparisonCalculator cltPjComparisonCalculator)
    {
        this.calculatorService = calculatorService;
        this.catalogService = catalogService;
        this.fieldProfileProvider = fieldProfileProvider;
        this.adSlotProvider = adSlotProvider;
        this.shareLinkBuilder = shareLinkBuilder;
        this.journeyLinkBuilder = journeyLinkBuilder;
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

    public bool IsRescisaoStitch =>
        Definition?.Slug.Equals("rescisao-clt", StringComparison.OrdinalIgnoreCase) == true && !IsEmbedMode;

    public bool IsLayeredFiscalDetail =>
        Definition is not null
        && !IsEmbedMode
        && !IsPjVsCltStitch
        && !IsRescisaoStitch
        && (Definition.Slug.Equals("ferias", StringComparison.OrdinalIgnoreCase)
            || Definition.Slug.Equals("decimo-terceiro", StringComparison.OrdinalIgnoreCase)
            || Definition.Slug.Equals("inss", StringComparison.OrdinalIgnoreCase)
            || Definition.Slug.Equals("irrf", StringComparison.OrdinalIgnoreCase));

    public FieldLabelViewModel FieldLabel(string text, string? forId, string fieldKey) =>
        new(text, forId, CalculatorFieldTooltipCatalog.GetTooltip(Definition?.Slug ?? string.Empty, fieldKey));

    public string CalcDetailModifierClass
    {
        get
        {
            if (IsEmbedMode || IsPjVsCltStitch || IsRescisaoStitch || Definition is null)
            {
                return string.Empty;
            }

            if (Definition.Slug.Equals("inss", StringComparison.OrdinalIgnoreCase)
                || Definition.Slug.Equals("irrf", StringComparison.OrdinalIgnoreCase))
            {
                return " valora-stitch-calc-detail--fiscal";
            }

            if (Definition.Slug.Equals("ferias", StringComparison.OrdinalIgnoreCase)
                || Definition.Slug.Equals("decimo-terceiro", StringComparison.OrdinalIgnoreCase))
            {
                return " valora-stitch-calc-detail--layered";
            }

            return string.Empty;
        }
    }

    [BindProperty(SupportsGet = true)]
    public string? Jornada { get; set; }

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
            await productMetricsService.RecordAsync(ProductMetricEvents.CalculationFailed, slug);
            return Page();
        }

        var result = calculatorService.Calculate(slug, Input);
        if (result.IsFailure)
        {
            await productMetricsService.RecordAsync(ProductMetricEvents.CalculationFailed, slug);
            ModelState.AddModelError(string.Empty, result.Error.Message);
            return Page();
        }

        await productMetricsService.RecordAsync(ProductMetricEvents.CalculatorCalculation, slug);

        var token = CalculatorInputShareCodec.Encode(Input);
        var embedQuery = IsEmbedMode ? "&embed=1" : string.Empty;
        var jornadaQuery = string.IsNullOrWhiteSpace(Jornada)
            ? string.Empty
            : $"&jornada={Uri.EscapeDataString(Jornada)}";
        return Redirect($"/calculadoras/{slug}?r={Uri.EscapeDataString(token)}{embedQuery}{jornadaQuery}");
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
        BuildShare(slug, ResolveJourneyId(slug));
        if (slug.Equals("pj-vs-clt", StringComparison.OrdinalIgnoreCase))
        {
            CltPjBreakdown = cltPjComparisonCalculator.Compare(Input);
        }
        else
        {
            var journey = BuildJourneyPanel(slug, ResolveJourneyId(slug));
            var terminationSummary = TerminationResultGrouper.TryGroup(slug, Result);
            ResultPanel = new CalculatorResultPanelViewModel(
                Result,
                Input,
                slug,
                CalculatorResultExplanationFactory.Build(slug, Input, Result, catalogService),
                ShowSimpleExplanation: !IsEmbedMode,
                TerminationSummary: terminationSummary,
                Warnings: CalculatorResultWarningBuilder.Build(slug, Input, Result),
                Journey: journey);
        }
    }

    private string? ResolveJourneyId(string slug) =>
        string.IsNullOrWhiteSpace(Jornada)
            ? CalculatorJourneyCatalog.TryGetByEntrySlug(slug)?.Id
            : Jornada;

    private CalculatorJourneyPanelViewModel? BuildJourneyPanel(string slug, string? journeyId) =>
        Result is null
            ? null
            : journeyLinkBuilder.Build(journeyId, slug, Input, Result, Request);

    private void BuildShare(string slug, string? journeyId)
    {
        if (Result is null || IsEmbedMode)
        {
            return;
        }

        var journey = BuildJourneyPanel(slug, journeyId);
        var shareUrl = shareLinkBuilder.BuildShareUrl(slug, Input, Request);
        if (!string.IsNullOrWhiteSpace(journey?.JourneyId))
        {
            shareUrl = CalculatorJourneyLinkBuilder.AppendJourneyQuery(shareUrl, journey.JourneyId);
        }

        var shareText = CalculatorJourneyShareTextBuilder.AppendNextSteps(
            CalculatorShareTextBuilder.Build(Result, shareUrl),
            journey);
        Share = new CalculatorShareViewModel(
            shareUrl,
            shareText,
            CalculatorShareLinkBuilder.BuildWhatsAppUrl(shareText),
            shareLinkBuilder.BuildPdfUrl(slug, Input),
            LocalPanelSaveContextBuilder.FromCalculation(Definition!, Result, Input),
            journey);
    }

    private void BuildShare(string slug)
    {
        BuildShare(slug, ResolveJourneyId(slug));
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
