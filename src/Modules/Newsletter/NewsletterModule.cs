namespace MeuValorLiquido.Modules.Newsletter;

public sealed record NewsletterSubscriber(string Email, DateTimeOffset SubscribedAt, bool IsConfirmed);

public interface INewsletterService
{
    NewsletterSubscriber Subscribe(string email);
}

public sealed class InMemoryNewsletterService : INewsletterService
{
    public NewsletterSubscriber Subscribe(string email)
    {
        return new NewsletterSubscriber(email.Trim().ToLowerInvariant(), DateTimeOffset.UtcNow, IsConfirmed: false);
    }
}
