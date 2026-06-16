namespace MeuValorLiquido.WebApp.Infrastructure;
public sealed class EfContactService : IContactService
{
    private readonly AppDbContext db;
    private readonly IEmailSender emailSender;

    public EfContactService(AppDbContext db, IEmailSender emailSender)
    {
        this.db = db;
        this.emailSender = emailSender;
    }

    public async Task SendAsync(ContactMessageRequest request, CancellationToken cancellationToken = default)
    {
        db.ContactMessages.Add(new ContactMessageEntity
        {
            Name = request.Name.Trim(),
            Email = request.Email.Trim(),
            Subject = request.Subject.Trim(),
            Message = request.Message.Trim(),
            CreatedAt = DateTimeOffset.UtcNow
        });

        await db.SaveChangesAsync(cancellationToken);

        await emailSender.SendAsync(
            new EmailMessage(
                "contato@meuvalorliquido.local",
                $"[Contato] {request.Subject}",
                $"Nome: {request.Name}\nE-mail: {request.Email}\n\n{request.Message}"),
            cancellationToken);
    }
}
