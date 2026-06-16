namespace MeuValorLiquido.WebApp.Pages.CltPj;
public partial class ComparacaoModel : PageModel
{
    private readonly CltPjComparisonCalculator cltPjComparisonCalculator;
    private readonly IAdSlotProvider adSlotProvider;

    public ComparacaoModel(
        CltPjComparisonCalculator cltPjComparisonCalculator,
        IAdSlotProvider adSlotProvider)
    {
        this.cltPjComparisonCalculator = cltPjComparisonCalculator;
        this.adSlotProvider = adSlotProvider;
    }

    public CltPjPageContent PageContent { get; private set; } = null!;

    public CltPjComparisonBreakdown Comparison { get; private set; } = null!;

    public IReadOnlyList<FaqItem> FaqSchemaItems { get; private set; } = [];

    public AdSlotDefinition? TopAdSlot { get; private set; }

    public AdSlotDefinition? BottomAdSlot { get; private set; }

    public IActionResult OnGet(int valor)
    {
        if (!CltPjBandCatalog.IsValid(valor))
        {
            return NotFound();
        }

        Comparison = cltPjComparisonCalculator.Compare(new CalculatorInput(valor, Rate: 6m));
        PageContent = CltPjContentBuilder.Build(valor, Comparison);
        FaqSchemaItems = PageContent.FaqItems
            .Select(f => new FaqItem(f.Question, HtmlTagRegex().Replace(f.Answer, " ").Trim()))
            .ToList();

        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(PageContent.Title, PageContent.Description, CltPjBandCatalog.SlugPath(valor)));

        return Page();
    }

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTagRegex();
}
