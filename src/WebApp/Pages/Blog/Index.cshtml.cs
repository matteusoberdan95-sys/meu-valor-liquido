namespace MeuValorLiquido.WebApp.Pages.Blog;

public class IndexModel : PageModel
{
    private readonly IContentService contentService;

    public IndexModel(IContentService contentService)
    {
        this.contentService = contentService;
    }

    [BindProperty(SupportsGet = true)]
    public string? Cat { get; set; }

    public BlogPost? FeaturedPost { get; private set; }

    public IReadOnlyList<BlogPost> FeedPosts { get; private set; } = [];

    public int TotalCount { get; private set; }

    public bool HasActiveFilter => !string.IsNullOrWhiteSpace(Cat);

    public void OnGet()
    {
        var all = contentService.GetPublishedPosts();
        var filtered = BlogHubHelper.FilterPosts(all, Cat);
        TotalCount = filtered.Count;
        FeaturedPost = filtered.FirstOrDefault();
        FeedPosts = filtered.Count <= 1 ? [] : filtered.Skip(1).ToList();
    }
}
