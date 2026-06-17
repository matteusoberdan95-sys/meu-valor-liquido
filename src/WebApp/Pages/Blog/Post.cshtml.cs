namespace MeuValorLiquido.WebApp.Pages.Blog;
public class PostModel : PageModel
{
    private readonly IContentService contentService;

    public PostModel(IContentService contentService)
    {
        this.contentService = contentService;
    }

    public BlogPost? Post { get; private set; }

    public IReadOnlyList<BlogPost> RelatedPosts { get; private set; } = [];

    public int ReadingMinutes { get; private set; }

    public IActionResult OnGet(string slug)
    {
        Post = contentService.GetBySlug(slug);
        if (Post is null)
        {
            return NotFound();
        }

        ReadingMinutes = BlogContentHelper.EstimateReadingMinutes(Post.Content);
        var sameCategory = contentService.GetPublishedPosts()
            .Where(p => p.Slug != Post.Slug && p.Category == Post.Category)
            .Take(3)
            .ToList();

        RelatedPosts = sameCategory.Count >= 2
            ? sameCategory
            : contentService.GetPublishedPosts()
                .Where(p => p.Slug != Post.Slug)
                .Take(3)
                .ToList();

        return Page();
    }
}
