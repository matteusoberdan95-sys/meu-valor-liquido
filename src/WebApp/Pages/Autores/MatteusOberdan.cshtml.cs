namespace MeuValorLiquido.WebApp.Pages.Autores;

public sealed class MatteusOberdanModel : PageModel
{
    private readonly IContentService contentService;

    public MatteusOberdanModel(IContentService contentService)
    {
        this.contentService = contentService;
    }

    public EditorialAuthorProfile Author => EditorialAuthorCatalog.Primary;

    public IReadOnlyList<BlogPost> PublishedPosts { get; private set; } = [];

    public void OnGet()
    {
        PublishedPosts = contentService.GetPublishedPosts()
            .Where(post => post.Author.Equals(Author.Name, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(post => post.PublishedAt)
            .ToList();

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Matteus Oberdan — responsável editorial",
                "Perfil de Matteus Oberdan, criador e responsável editorial do Meu Valor Líquido, com processo de pesquisa, revisão e artigos publicados.",
                Author.ProfilePath),
            ogType: "profile",
            ogImagePath: Author.ImagePath);
    }
}
