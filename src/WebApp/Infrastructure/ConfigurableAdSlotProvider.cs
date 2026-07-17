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
            return [];
        }

        var slots = new List<AdSlotDefinition>();

        if (!string.IsNullOrWhiteSpace(options.CalculatorTopSlotId))
        {
            slots.Add(new(
                "calculator-top",
                "Publicidade",
                true,
                IsPlaceholder: false,
                AdSlotId: options.CalculatorTopSlotId));
        }

        if (!string.IsNullOrWhiteSpace(options.CalculatorBottomSlotId))
        {
            slots.Add(new(
                "calculator-bottom",
                "Publicidade",
                true,
                IsPlaceholder: false,
                AdSlotId: options.CalculatorBottomSlotId));
        }

        return slots;
    }
}
