namespace MeuValorLiquido.WebApp.Infrastructure;
public sealed class EfNewsletterService : INewsletterService
{
    private readonly AppDbContext db;
    private readonly IEmailSender emailSender;

    public EfNewsletterService(AppDbContext db, IEmailSender emailSender)
    {
        this.db = db;
        this.emailSender = emailSender;
    }

    public async Task<NewsletterSubscriber> SubscribeAsync(string email, CancellationToken cancellationToken = default)
    {
        var normalized = email.Trim().ToLowerInvariant();
        var existing = await db.NewsletterSubscribers.FirstOrDefaultAsync(x => x.Email == normalized, cancellationToken);
        if (existing is not null)
        {
            return new NewsletterSubscriber(existing.Email, existing.SubscribedAt, existing.IsConfirmed);
        }

        var entity = new NewsletterSubscriberEntity
        {
            Email = normalized,
            SubscribedAt = DateTimeOffset.UtcNow,
            IsConfirmed = false
        };

        db.NewsletterSubscribers.Add(entity);
        await db.SaveChangesAsync(cancellationToken);

        await emailSender.SendAsync(
            new EmailMessage(
                normalized,
                "Confirmação de newsletter (mock)",
                "Obrigado por se inscrever no Meu Valor Líquido. Esta é uma confirmação mockada para ambiente local."),
            cancellationToken);

        return new NewsletterSubscriber(entity.Email, entity.SubscribedAt, entity.IsConfirmed);
    }
}
