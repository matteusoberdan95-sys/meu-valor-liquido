namespace MeuValorLiquido.WebApp.Tests;

public class CachedCatalogServiceTests
{
    [Fact]
    public void GetAll_Should_Reuse_Memory_Cache()
    {
        var inner = new CountingCatalogService();
        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new CachedCalculatorCatalogService(inner, cache);

        var first = service.GetAll();
        var second = service.GetAll();

        first.Should().BeSameAs(second);
        inner.GetAllCalls.Should().Be(1);
    }

    [Fact]
    public void GetBySlug_Should_Cache_Per_Slug()
    {
        var inner = new CountingCatalogService();
        var cache = new MemoryCache(new MemoryCacheOptions());
        var service = new CachedCalculatorCatalogService(inner, cache);

        _ = service.GetBySlug("salario-liquido");
        _ = service.GetBySlug("salario-liquido");
        _ = service.GetBySlug("inss");

        inner.GetBySlugCalls.Should().Be(2);
    }

    private sealed class CountingCatalogService : ICalculatorCatalogService
    {
        private static readonly IReadOnlyList<CalculatorDefinition> Items =
        [
            new CalculatorDefinition(
                "salario-liquido",
                "Salário líquido",
                "Trabalhista",
                "Resumo",
                "SEO",
                "SEO",
                [])
        ];

        public int GetAllCalls { get; private set; }

        public int GetBySlugCalls { get; private set; }

        public IReadOnlyList<CalculatorDefinition> GetAll()
        {
            GetAllCalls++;
            return Items;
        }

        public CalculatorDefinition? GetBySlug(string slug)
        {
            GetBySlugCalls++;
            return Items.FirstOrDefault(c => c.Slug == slug);
        }
    }
}
