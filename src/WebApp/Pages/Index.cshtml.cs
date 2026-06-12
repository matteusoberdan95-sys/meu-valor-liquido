using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.Modules.Content;

namespace MeuValorLiquido.WebApp.Pages;

public class IndexModel : PageModel
{
    private readonly ICalculatorCatalogService catalogService;
    private readonly IContentService contentService;

    public IndexModel(ICalculatorCatalogService catalogService, IContentService contentService)
    {
        this.catalogService = catalogService;
        this.contentService = contentService;
    }

    public IReadOnlyList<CalculatorDefinition> Calculators { get; private set; } = [];

    public IReadOnlyList<BlogPost> RecentPosts { get; private set; } = [];

    public void OnGet()
    {
        Calculators = catalogService.GetAll();
        RecentPosts = contentService.GetPublishedPosts().Take(3).ToList();
    }
}
