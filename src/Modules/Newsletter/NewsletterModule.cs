namespace MeuValorLiquido.Modules.Newsletter;
public sealed record NewsletterSubscriber(string Email, DateTimeOffset SubscribedAt, bool IsConfirmed);

public interface INewsletterService
{
    Task<NewsletterSubscriber> SubscribeAsync(string email, CancellationToken cancellationToken = default);
}

public sealed class InMemoryNewsletterService : INewsletterService
{
    public Task<NewsletterSubscriber> SubscribeAsync(string email, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(new NewsletterSubscriber(email.Trim().ToLowerInvariant(), DateTimeOffset.UtcNow, IsConfirmed: false));
    }
}
