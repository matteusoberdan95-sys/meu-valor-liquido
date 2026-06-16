namespace MeuValorLiquido.Modules.Contact;
public sealed record ContactMessageRequest(string Name, string Email, string Subject, string Message);

public interface IContactService
{
    Task SendAsync(ContactMessageRequest request, CancellationToken cancellationToken = default);
}

public sealed class ContactService : IContactService
{
    private readonly IEmailSender emailSender;

    public ContactService(IEmailSender emailSender)
    {
        this.emailSender = emailSender;
    }

    public Task SendAsync(ContactMessageRequest request, CancellationToken cancellationToken = default)
    {
        var body = $"Nome: {request.Name}\nEmail: {request.Email}\nMensagem:\n{request.Message}";
        return emailSender.SendAsync(new EmailMessage("contato@meuvalorliquido.com.br", request.Subject, body), cancellationToken);
    }
}
