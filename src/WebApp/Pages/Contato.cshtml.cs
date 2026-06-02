using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.RateLimiting;

namespace MeuValorLiquido.WebApp.Pages;

[EnableRateLimiting("form-policy")]
public class ContatoModel : PageModel
{
    private readonly IContactService contactService;

    public ContatoModel(IContactService contactService)
    {
        this.contactService = contactService;
    }

    [BindProperty]
    public ContactFormInput Input { get; set; } = new();

    public bool Sent { get; private set; }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await contactService.SendAsync(new ContactMessageRequest(Input.Name, Input.Email, Input.Subject, Input.Message), cancellationToken);
        Sent = true;
        ModelState.Clear();
        Input = new ContactFormInput();
        return Page();
    }
}

public sealed class ContactFormInput
{
    [Required(ErrorMessage = "Informe seu nome.")]
    [StringLength(120, ErrorMessage = "O nome deve ter no máximo 120 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu e-mail.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o assunto.")]
    [StringLength(160, ErrorMessage = "O assunto deve ter no máximo 160 caracteres.")]
    public string Subject { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a mensagem.")]
    [StringLength(4000, ErrorMessage = "A mensagem deve ter no máximo 4000 caracteres.")]
    public string Message { get; set; } = string.Empty;
}
