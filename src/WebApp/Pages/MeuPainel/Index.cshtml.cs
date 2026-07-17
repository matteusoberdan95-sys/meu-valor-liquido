namespace MeuValorLiquido.WebApp.Pages.MeuPainel;

using MeuValorLiquido.Modules.Content;

public class IndexModel : PageModel
{
    private readonly IContentService contentService;

    public IndexModel(IContentService contentService)
    {
        this.contentService = contentService;
    }

    public IReadOnlyList<BlogPost> SuggestedPosts { get; private set; } = [];

    public void OnGet()
    {
        SuggestedPosts = contentService
            .GetPublishedPosts()
            .Take(2)
            .ToList();

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Meu painel — simulações salvas",
                "Revise estimativas salvas no seu navegador. Sem cadastro: os dados ficam só no seu dispositivo.",
                "/meu-painel",
                SeoMetadataHelper.NoIndexFollowRobots));
    }
}
