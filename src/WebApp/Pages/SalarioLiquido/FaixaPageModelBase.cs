namespace MeuValorLiquido.WebApp.Pages.SalarioLiquido;

public abstract partial class FaixaPageModelBase : PageModel
{
    private readonly NetSalaryCalculator netSalaryCalculator;
    private readonly IAdSlotProvider adSlotProvider;
    private readonly CalculatorShareLinkBuilder shareLinkBuilder;
    private readonly ICalculatorCatalogService catalogService;

    protected FaixaPageModelBase(
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

    protected IActionResult LoadPage(int valor, string? variant)
    {
        if (!ProgrammaticDependentsCatalog.TryParseVariantSlug(variant, out var dependents)
            || !SalaryBandCatalog.IsValid(valor, dependents))
        {
            return NotFound();
        }

        Breakdown = netSalaryCalculator.Calculate(valor, dependents, transportDiscount: 0m);
        PageContent = SalaryBandContentBuilder.Build(valor, Breakdown, dependents);
        FaqSchemaItems = PageContent.FaqItems
            .Select(f => new FaqItem(f.Question, HtmlTagRegex().Replace(f.Answer, " ").Trim()))
            .ToList();

        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");

        var canonicalPath = SalaryBandCatalog.SlugPath(valor, dependents);
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(PageContent.Title, PageContent.Description, canonicalPath));

        var shareUrl = shareLinkBuilder.BuildAbsoluteUrl(canonicalPath, Request);
        var shareText =
            $"*Meu Valor Líquido* — Salário de {Money.From(valor)} ({ProgrammaticDependentsCatalog.SeoPhrase(dependents)})\n" +
            $"Líquido estimado: {Money.From(Breakdown.Net)}\n" +
            $"INSS: {Money.From(Breakdown.Inss)} | IRRF: {Money.From(Breakdown.Irrf)}\n\n" +
            $"Ver detalhes: {shareUrl}";
        Share = new CalculatorShareViewModel(
            shareUrl,
            shareText,
            CalculatorShareLinkBuilder.BuildWhatsAppUrl(shareText),
            CalculatorShareLinkBuilder.BuildSalaryBandPdfUrl(valor),
            LocalPanelSaveContextBuilder.FromSalaryBand(valor, Breakdown.Net));

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
