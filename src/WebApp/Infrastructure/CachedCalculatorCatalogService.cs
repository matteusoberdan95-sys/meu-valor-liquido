namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class CachedCalculatorCatalogService : ICalculatorCatalogService
{
    private const string AllKey = "mvl:catalog:all";

    private readonly ICalculatorCatalogService inner;
    private readonly IMemoryCache cache;

    public CachedCalculatorCatalogService(ICalculatorCatalogService inner, IMemoryCache cache)
    {
        this.inner = inner;
        this.cache = cache;
    }

    public IReadOnlyList<CalculatorDefinition> GetAll() =>
        cache.GetOrCreate(AllKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = PerformanceCacheDurations.Catalog;
            return inner.GetAll();
        })!;

    public CalculatorDefinition? GetBySlug(string slug)
    {
        var key = $"mvl:catalog:slug:{slug}";
        return cache.GetOrCreate(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = PerformanceCacheDurations.Catalog;
            return inner.GetBySlug(slug);
        });
    }
}
