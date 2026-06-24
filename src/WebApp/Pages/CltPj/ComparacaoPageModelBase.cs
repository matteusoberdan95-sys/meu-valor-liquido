namespace MeuValorLiquido.WebApp.Pages.CltPj;

public abstract partial class ComparacaoPageModelBase : PageModel
{
    private readonly CltPjComparisonCalculator cltPjComparisonCalculator;
    private readonly IAdSlotProvider adSlotProvider;

    protected ComparacaoPageModelBase(
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

    protected IActionResult LoadPage(int valor, string? variant)
    {
        if (!ProgrammaticDependentsCatalog.TryParseVariantSlug(variant, out var dependents)
            || !CltPjBandCatalog.IsValid(valor, dependents))
        {
            return NotFound();
        }

        Comparison = cltPjComparisonCalculator.Compare(new CalculatorInput(valor, Dependents: dependents, Rate: 6m));
        PageContent = CltPjContentBuilder.Build(valor, Comparison, dependents);
        FaqSchemaItems = PageContent.FaqItems
            .Select(f => new FaqItem(f.Question, HtmlTagRegex().Replace(f.Answer, " ").Trim()))
            .ToList();

        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                PageContent.Title,
                PageContent.Description,
                CltPjBandCatalog.SlugPath(valor, dependents)));

        return Page();
    }

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTagRegex();
}
