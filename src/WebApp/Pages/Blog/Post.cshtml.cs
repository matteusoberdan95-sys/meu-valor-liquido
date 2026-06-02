namespace MeuValorLiquido.WebApp.Pages.Blog;

public class PostModel : PageModel
{
    private readonly IContentService contentService;

    public PostModel(IContentService contentService)
    {
        this.contentService = contentService;
    }

    public BlogPost? Post { get; private set; }

    public void OnGet(string slug)
    {
        Post = contentService.GetBySlug(slug);
    }
}
