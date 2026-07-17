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
    private readonly NetSalaryCalculator netSalaryCalculator;

    public DetailsModel(
        ICalculatorApplicationService calculatorService,
        ICalculatorCatalogService catalogService,
        ICalculatorFieldProfileProvider fieldProfileProvider,
        IAdSlotProvider adSlotProvider,
        CalculatorShareLinkBuilder shareLinkBuilder,
        CalculatorJourneyLinkBuilder journeyLinkBuilder,
        IProductMetricsService productMetricsService,
        CltPjComparisonCalculator cltPjComparisonCalculator,
        NetSalaryCalculator netSalaryCalculator)
    {
        this.calculatorService = calculatorService;
        this.catalogService = catalogService;
        this.fieldProfileProvider = fieldProfileProvider;
        this.adSlotProvider = adSlotProvider;
        this.shareLinkBuilder = shareLinkBuilder;
        this.journeyLinkBuilder = journeyLinkBuilder;
        this.productMetricsService = productMetricsService;
        this.cltPjComparisonCalculator = cltPjComparisonCalculator;
        this.netSalaryCalculator = netSalaryCalculator;
    }

    public CalculatorDefinition? Definition { get; private set; }

    public IReadOnlyList<FaqItem> PageFaqItems { get; private set; } = [];

    public CalculationResult? Result { get; private set; }

    public CalculatorFieldProfile FieldProfile { get; private set; } = new();

    public AdSlotDefinition? TopAdSlot { get; private set; }

    public AdSlotDefinition? BottomAdSlot { get; private set; }

    public CalculatorShareViewModel? Share { get; private set; }

    public CalculatorResultPanelViewModel? ResultPanel { get; private set; }

    public CalculatorEditorialViewModel? EditorialContent { get; private set; }

    public CltPjComparisonBreakdown? CltPjBreakdown { get; private set; }

    public bool IsEmbedMode { get; private set; }

    public bool IsPjVsCltStitch =>
        Definition?.Slug.Equals("pj-vs-clt", StringComparison.OrdinalIgnoreCase) == true && !IsEmbedMode;

    public bool IsRescisaoStitch =>
        Definition?.Slug.Equals("rescisao-clt", StringComparison.OrdinalIgnoreCase) == true && !IsEmbedMode;

    public bool IsSalarioLiquidoStitch =>
        Definition?.Slug.Equals("salario-liquido", StringComparison.OrdinalIgnoreCase) == true && !IsEmbedMode;

    public bool IsPropostaSalarialStitch =>
        Definition?.Slug.Equals("proposta-salarial", StringComparison.OrdinalIgnoreCase) == true && !IsEmbedMode;

    public bool IsTemplateC1Stitch =>
        Definition is not null
        && CalculatorUiHelper.IsTemplateC1Slug(Definition.Slug)
        && !IsEmbedMode;

    public SalarioLiquidoStitchResultViewModel? SalarioLiquidoStitchResult { get; private set; }

    public RescisaoStitchResultViewModel? RescisaoStitchResult { get; private set; }

    public PropostaSalarialStitchResultsViewModel? PropostaStitchResult { get; private set; }

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

    public string CalcDetailModifierClass =>
        IsEmbedMode || IsPjVsCltStitch || IsRescisaoStitch || IsSalarioLiquidoStitch || IsPropostaSalarialStitch || Definition is null
            ? string.Empty
            : CalculatorUiHelper.GetStitchDetailModifierClass(Definition.Slug);

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
            var isSalarioLiquido = slug.Equals("salario-liquido", StringComparison.OrdinalIgnoreCase);
            var isRescisao = slug.Equals("rescisao-clt", StringComparison.OrdinalIgnoreCase);
            var isProposta = slug.Equals("proposta-salarial", StringComparison.OrdinalIgnoreCase);
            var warnings = CalculatorResultWarningBuilder.Build(slug, Input, Result);
            var explanation = CalculatorResultExplanationFactory.Build(slug, Input, Result, catalogService);

            if (isProposta && IsPropostaSalarialStitch)
            {
                PropostaStitchResult = SalaryProposalStitchResultBuilder.TryBuild(
                    Result,
                    Input,
                    netSalaryCalculator,
                    Share,
                    warnings,
                    journey,
                    explanation);
            }
            else
            {
                ResultPanel = new CalculatorResultPanelViewModel(
                    Result,
                    Input,
                    slug,
                    explanation,
                    ShowSimpleExplanation: !IsEmbedMode && !isSalarioLiquido && !(isRescisao && IsRescisaoStitch),
                    TerminationSummary: terminationSummary,
                    Warnings: warnings,
                    Journey: journey);
            }

            if (isSalarioLiquido)
            {
                SalarioLiquidoStitchResult = SalarioLiquidoStitchResultBuilder.TryBuild(Result);
            }
            else if (isRescisao && IsRescisaoStitch)
            {
                RescisaoStitchResult = RescisaoStitchResultBuilder.TryBuild(
                    Result,
                    terminationSummary,
                    Share,
                    warnings);
            }
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

        PageFaqItems = Definition.FaqItems
            .Concat(CalculatorEditorialCatalog.GetFaqs(slug))
            .DistinctBy(faq => faq.Question)
            .ToList();

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
        BuildEditorialContent(slug);
    }

    private void BuildEditorialContent(string slug)
    {
        var content = CalculatorEditorialCatalog.GetBySlug(slug);
        if (content is null)
        {
            return;
        }

        var exampleResult = calculatorService.Calculate(slug, content.Example.Input);
        if (exampleResult.IsFailure)
        {
            return;
        }

        var related = content.RelatedCalculatorSlugs
            .Select(catalogService.GetBySlug)
            .Where(definition => definition is not null)
            .Cast<CalculatorDefinition>()
            .ToList();

        EditorialContent = new CalculatorEditorialViewModel(content, exampleResult.Value, related);
    }
}
