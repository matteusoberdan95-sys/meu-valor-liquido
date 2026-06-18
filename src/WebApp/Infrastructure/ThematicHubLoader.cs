namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class ThematicHubLoader
{
    private readonly ICalculatorCatalogService calculatorCatalog;
    private readonly IContentService contentService;

    public ThematicHubLoader(ICalculatorCatalogService calculatorCatalog, IContentService contentService)
    {
        this.calculatorCatalog = calculatorCatalog;
        this.contentService = contentService;
    }

    public IActionResult Load(PageModel page, string hubId, out ThematicHubPageViewModel hubPage)
    {
        hubPage = null!;
        var built = ThematicHubPageBuilder.Build(hubId, calculatorCatalog, contentService);
        if (built is null)
        {
            return page.NotFound();
        }

        hubPage = built;
        var hub = built.Hub;

        SeoMetadataHelper.Apply(
            page.ViewData,
            new SeoMetadata(hub.SeoTitle, hub.SeoDescription, hub.RoutePath),
            ogType: "website");

        return page.Page();
    }
}
