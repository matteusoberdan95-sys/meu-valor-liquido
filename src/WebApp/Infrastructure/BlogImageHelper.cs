namespace MeuValorLiquido.WebApp.Infrastructure;

/// <summary>
/// Convenção de imagens hero do blog: <c>wwwroot/images/blog/{slug}.webp</c> (16:9, ~1200×675).
/// </summary>
public static class BlogImageHelper
{
    public const string RelativeFolder = "images/blog";
    public const string FileExtension = ".webp";

    public static string GetPublicPath(string slug) =>
        $"/{RelativeFolder}/{slug}{FileExtension}";

    public static string GetPhysicalPath(IWebHostEnvironment environment, string slug) =>
        Path.Combine(environment.WebRootPath, RelativeFolder, $"{slug}{FileExtension}");

    public static bool Exists(IWebHostEnvironment environment, string slug) =>
        File.Exists(GetPhysicalPath(environment, slug));

    public static string GetAltText(string title, string? category) =>
        string.IsNullOrWhiteSpace(category)
            ? $"Ilustração editorial: {title} — Meu Valor Líquido"
            : $"Ilustração editorial ({category}): {title} — Meu Valor Líquido";
}

public interface IBlogHeroImageService
{
    bool Exists(string slug);

    string GetPublicPath(string slug);

    string GetAltText(string title, string? category);
}

public sealed class BlogHeroImageService : IBlogHeroImageService
{
    private readonly IWebHostEnvironment environment;

    public BlogHeroImageService(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    public bool Exists(string slug) => BlogImageHelper.Exists(environment, slug);

    public string GetPublicPath(string slug) => BlogImageHelper.GetPublicPath(slug);

    public string GetAltText(string title, string? category) =>
        BlogImageHelper.GetAltText(title, category);
}

public sealed record BlogHeroVisualModel(
    string Slug,
    string Title,
    string? Category,
    string WrapperClass,
    bool Decorative = true,
    bool ShowCategoryBadge = false);
