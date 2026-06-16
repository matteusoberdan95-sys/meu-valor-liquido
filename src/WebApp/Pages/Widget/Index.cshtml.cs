namespace MeuValorLiquido.WebApp.Pages.Widget;

public class IndexModel : PageModel
{
    private readonly CalculatorShareLinkBuilder shareLinkBuilder;

    public IndexModel(CalculatorShareLinkBuilder shareLinkBuilder)
    {
        this.shareLinkBuilder = shareLinkBuilder;
    }

    public IReadOnlyList<EmbedWidgetDefinition> Widgets { get; private set; } = [];

    public string BaseUrl { get; private set; } = string.Empty;

    public void OnGet()
    {
        Widgets = EmbedWidgetCatalog.GetAll();
        BaseUrl = shareLinkBuilder.BuildAbsoluteUrl("/", Request).TrimEnd('/');

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Widget incorporável — calculadoras para seu site",
                "Incorpore calculadoras de salário, INSS e IRRF no seu blog ou portal com iframe gratuito e link de atribuição.",
                "/widget"));
    }
}
