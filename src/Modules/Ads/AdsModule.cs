namespace MeuValorLiquido.Modules.Ads;
public sealed record AdSlotDefinition(
    string Key,
    string Label,
    bool IsEnabled,
    bool IsPlaceholder = true,
    string? AdSlotId = null);

public interface IAdSlotProvider
{
    IReadOnlyList<AdSlotDefinition> GetSlots();
}

public sealed class PlaceholderAdSlotProvider : IAdSlotProvider
{
    private static readonly IReadOnlyList<AdSlotDefinition> Slots =
    [
        new("calculator-top", "Espaço reservado para anúncio", true),
        new("calculator-bottom", "Espaço reservado para anúncio", true)
    ];

    public IReadOnlyList<AdSlotDefinition> GetSlots() => Slots;
}
