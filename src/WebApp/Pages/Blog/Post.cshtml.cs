namespace MeuValorLiquido.WebApp.Pages.Blog;

using Microsoft.AspNetCore.OutputCaching;

[OutputCache(PolicyName = "editorial")]
public class PostModel : PageModel
{
    private readonly IContentService contentService;
    private readonly IBlogHeroImageService blogHeroImages;
    private readonly ICalculatorCatalogService calculatorCatalog;

    public PostModel(
        IContentService contentService,
        IBlogHeroImageService blogHeroImages,
        ICalculatorCatalogService calculatorCatalog)
    {
        this.contentService = contentService;
        this.blogHeroImages = blogHeroImages;
        this.calculatorCatalog = calculatorCatalog;
    }

    public BlogPost? Post { get; private set; }

    public IReadOnlyList<BlogPost> RelatedPosts { get; private set; } = [];

    public BlogConversionPath? ConversionPath { get; private set; }

    public int ReadingMinutes { get; private set; }

    public IActionResult OnGet(string slug)
    {
        Post = contentService.GetBySlug(slug);
        if (Post is null)
        {
            return NotFound();
        }

        ReadingMinutes = BlogContentHelper.EstimateReadingMinutes(Post.Content);
        var relatedCalculator = string.IsNullOrEmpty(Post.RelatedCalculatorSlug)
            ? null
            : calculatorCatalog.GetBySlug(Post.RelatedCalculatorSlug);

        ConversionPath = BlogConversionPathCatalog.Build(Post, relatedCalculator);

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

        if (blogHeroImages.Exists(Post.Slug))
        {
            ViewData["OgImagePath"] = blogHeroImages.GetPublicPath(Post.Slug);
        }

        return Page();
    }
}
