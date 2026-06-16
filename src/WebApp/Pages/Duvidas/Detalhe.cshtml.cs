namespace MeuValorLiquido.WebApp.Pages.Duvidas;
public partial class DetalheModel : PageModel
{
    private readonly ICalculatorCatalogService calculatorCatalog;
    private readonly IAdSlotProvider adSlotProvider;

    public DetalheModel(ICalculatorCatalogService calculatorCatalog, IAdSlotProvider adSlotProvider)
    {
        this.calculatorCatalog = calculatorCatalog;
        this.adSlotProvider = adSlotProvider;
    }

    public PopularQuestionDefinition Question { get; private set; } = null!;

    public IReadOnlyList<FaqItem> FaqSchemaItems { get; private set; } = [];

    public IReadOnlyList<PopularQuestionDefinition> RelatedQuestions { get; private set; } = [];

    public CalculatorDefinition? RelatedCalculator { get; private set; }

    public AdSlotDefinition? TopAdSlot { get; private set; }

    public AdSlotDefinition? BottomAdSlot { get; private set; }

    public IActionResult OnGet(string slug)
    {
        var question = PopularQuestionsCatalog.GetBySlug(slug);
        if (question is null)
        {
            return NotFound();
        }

        Question = question;
        RelatedQuestions = PopularQuestionsCatalog.GetRelated(question);
        RelatedCalculator = string.IsNullOrEmpty(question.RelatedCalculatorSlug)
            ? null
            : calculatorCatalog.GetBySlug(question.RelatedCalculatorSlug);

        FaqSchemaItems = BuildFaqSchema(question);

        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                $"{question.Title} | Meu Valor Líquido",
                question.SeoDescription,
                PopularQuestionsCatalog.SlugPath(question.Slug)));

        return Page();
    }

    private static List<FaqItem> BuildFaqSchema(PopularQuestionDefinition question)
    {
        var items = new List<FaqItem>
        {
            new(question.Title, StripHtml(question.AnswerHtml))
        };

        items.AddRange(question.FaqItems.Select(f => new FaqItem(f.Question, StripHtml(f.AnswerHtml))));
        return items;
    }

    private static string StripHtml(string html) =>
        HtmlTagRegex().Replace(html, " ").Replace("&nbsp;", " ").Trim();

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTagRegex();
}
