namespace MeuValorLiquido.WebApp.Pages.SalarioLiquido;
public partial class FaixaModel : PageModel
{
    private readonly NetSalaryCalculator netSalaryCalculator;
    private readonly IAdSlotProvider adSlotProvider;
    private readonly CalculatorShareLinkBuilder shareLinkBuilder;
    private readonly ICalculatorCatalogService catalogService;

    public FaixaModel(
        NetSalaryCalculator netSalaryCalculator,
        IAdSlotProvider adSlotProvider,
        CalculatorShareLinkBuilder shareLinkBuilder,
        ICalculatorCatalogService catalogService)
    {
        this.netSalaryCalculator = netSalaryCalculator;
        this.adSlotProvider = adSlotProvider;
        this.shareLinkBuilder = shareLinkBuilder;
        this.catalogService = catalogService;
    }

    public SalaryBandPageContent PageContent { get; private set; } = null!;

    public NetSalaryBreakdown Breakdown { get; private set; } = null!;

    public IReadOnlyList<FaqItem> FaqSchemaItems { get; private set; } = [];

    public AdSlotDefinition? TopAdSlot { get; private set; }

    public AdSlotDefinition? BottomAdSlot { get; private set; }

    public CalculatorShareViewModel? Share { get; private set; }

    public SalaryBandResultPanelViewModel ResultPanel { get; private set; } = null!;

    public IActionResult OnGet(int valor)
    {
        if (!SalaryBandCatalog.IsValid(valor))
        {
            return NotFound();
        }

        Breakdown = netSalaryCalculator.Calculate(valor, dependents: 0, transportDiscount: 0m);
        PageContent = SalaryBandContentBuilder.Build(valor, Breakdown);
        FaqSchemaItems = PageContent.FaqItems
            .Select(f => new FaqItem(f.Question, HtmlTagRegex().Replace(f.Answer, " ").Trim()))
            .ToList();

        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(PageContent.Title, PageContent.Description, SalaryBandCatalog.SlugPath(valor)));

        var shareUrl = shareLinkBuilder.BuildAbsoluteUrl(SalaryBandCatalog.SlugPath(valor), Request);
        var shareText =
            $"*Meu Valor Líquido* — Salário de {Money.From(valor)}\n" +
            $"Líquido estimado: {Money.From(Breakdown.Net)}\n" +
            $"INSS: {Money.From(Breakdown.Inss)} | IRRF: {Money.From(Breakdown.Irrf)}\n\n" +
            $"Ver detalhes: {shareUrl}";
        Share = new CalculatorShareViewModel(
            shareUrl,
            shareText,
            CalculatorShareLinkBuilder.BuildWhatsAppUrl(shareText),
            CalculatorShareLinkBuilder.BuildSalaryBandPdfUrl(valor));

        var explanation = CalculatorResultExplanationFactory.BuildForSalaryBand(valor, Breakdown, catalogService);
        ResultPanel = new SalaryBandResultPanelViewModel(
            valor,
            Breakdown.Inss,
            Breakdown.Irrf,
            Breakdown.Net,
            explanation);

        return Page();
    }

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTagRegex();
}
