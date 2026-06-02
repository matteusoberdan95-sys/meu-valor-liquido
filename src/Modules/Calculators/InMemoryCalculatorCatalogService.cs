namespace MeuValorLiquido.Modules.Calculators;

public sealed class InMemoryCalculatorCatalogService : ICalculatorCatalogService
{
    private static readonly IReadOnlyList<CalculatorDefinition> Calculators = CalculatorSeedData.GetDefinitions();

    public IReadOnlyList<CalculatorDefinition> GetAll() => Calculators;

    public CalculatorDefinition? GetBySlug(string slug) =>
        Calculators.FirstOrDefault(c => c.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
}
