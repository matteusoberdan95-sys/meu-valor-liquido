using System.Text.Json;

namespace MeuValorLiquido.WebApp.Pages.Widget;

public sealed record WidgetHubItem(
    string Slug,
    string Name,
    string Summary,
    int RecommendedHeight,
    string WidgetPath,
    string IframeCode);

public class IndexModel : PageModel
{
    private static readonly JsonSerializerOptions CatalogJsonOptions = new(JsonSerializerDefaults.Web);

    private readonly CalculatorShareLinkBuilder shareLinkBuilder;

    public IndexModel(CalculatorShareLinkBuilder shareLinkBuilder)
    {
        this.shareLinkBuilder = shareLinkBuilder;
    }

    public IReadOnlyList<WidgetHubItem> WidgetItems { get; private set; } = [];

    public string WidgetCatalogJson { get; private set; } = "[]";

    public string BaseUrl { get; private set; } = string.Empty;

    public string DefaultSlug { get; private set; } = "salario-liquido";

    public void OnGet()
    {
        var widgets = EmbedWidgetCatalog.GetAll();
        BaseUrl = shareLinkBuilder.BuildAbsoluteUrl("/", Request).TrimEnd('/');
        DefaultSlug = widgets[0].Slug;

        WidgetItems = widgets
            .Select(widget => new WidgetHubItem(
                widget.Slug,
                widget.Name,
                widget.Summary,
                widget.RecommendedHeight,
                EmbedWidgetCatalog.WidgetPath(widget.Slug),
                EmbedCodeBuilder.BuildIframeHtml(BaseUrl, widget)))
            .ToList();

        WidgetCatalogJson = JsonSerializer.Serialize(WidgetItems, CatalogJsonOptions);

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Widget incorporável — calculadoras para seu site",
                "Incorpore calculadoras de salário, INSS e IRRF no seu blog ou portal com iframe gratuito, sem anúncios e com link de atribuição.",
                "/widget"));
    }
}
