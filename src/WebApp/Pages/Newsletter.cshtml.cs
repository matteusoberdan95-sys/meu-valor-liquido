using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.RateLimiting;

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

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
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
}
