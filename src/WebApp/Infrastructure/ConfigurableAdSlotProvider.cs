namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class ConfigurableAdSlotProvider : IAdSlotProvider
{
    private readonly AdsOptions options;

    public ConfigurableAdSlotProvider(IOptions<AdsOptions> options)
    {
        this.options = options.Value;
    }

    public IReadOnlyList<AdSlotDefinition> GetSlots()
    {
        if (!options.IsActive)
        {
            return new PlaceholderAdSlotProvider().GetSlots();
        }

        return
        [
            new(
                "calculator-top",
                "Espaço reservado para anúncio",
                true,
                IsPlaceholder: false,
                AdSlotId: options.CalculatorTopSlotId),
            new(
                "calculator-bottom",
                "Espaço reservado para anúncio",
                true,
                IsPlaceholder: false,
                AdSlotId: options.CalculatorBottomSlotId)
        ];
    }
}
