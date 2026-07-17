namespace MeuValorLiquido.WebApp.Pages;
[EnableRateLimiting("form-policy")]
public class NewsletterModel : PageModel
{
    private readonly INewsletterService newsletterService;

    public NewsletterModel(INewsletterService newsletterService)
    {
        this.newsletterService = newsletterService;
    }

    [BindProperty]
    [Required(ErrorMessage = "Informe seu e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    public bool Subscribed { get; private set; }

    public void OnGet()
    {
        ApplySeoMetadata();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        ApplySeoMetadata();

        if (!ModelState.IsValid)
        {
            return Page();
        }

        await newsletterService.SubscribeAsync(Email, cancellationToken);
        Subscribed = true;
        ModelState.Clear();
        Email = string.Empty;
        return Page();
    }

    private void ApplySeoMetadata()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Newsletter",
                "Receba novidades sobre calculadoras, conteúdos e atualizações do Meu Valor Líquido.",
                "/newsletter",
                SeoMetadataHelper.NoIndexFollowRobots));
    }
}
