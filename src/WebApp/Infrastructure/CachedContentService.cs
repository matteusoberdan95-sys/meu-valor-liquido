namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class CachedContentService : IContentService
{
    private const string PostsKey = "mvl:content:posts";

    private readonly IContentService inner;
    private readonly IMemoryCache cache;

    public CachedContentService(IContentService inner, IMemoryCache cache)
    {
        this.inner = inner;
        this.cache = cache;
    }

    public IReadOnlyList<BlogPost> GetPublishedPosts() =>
        cache.GetOrCreate(PostsKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = PerformanceCacheDurations.Content;
            return inner.GetPublishedPosts();
        })!;

    public BlogPost? GetBySlug(string slug)
    {
        var key = $"mvl:content:slug:{slug}";
        return cache.GetOrCreate(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = PerformanceCacheDurations.Content;
            return inner.GetBySlug(slug);
        });
    }
}
