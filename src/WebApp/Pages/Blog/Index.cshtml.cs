namespace MeuValorLiquido.WebApp.Pages.Blog;
public class IndexModel : PageModel
{
    private readonly IContentService contentService;

    public IndexModel(IContentService contentService)
    {
        this.contentService = contentService;
    }

    public IReadOnlyList<BlogPost> Posts { get; private set; } = [];

    public void OnGet()
    {
        Posts = contentService.GetPublishedPosts();
    }
}
