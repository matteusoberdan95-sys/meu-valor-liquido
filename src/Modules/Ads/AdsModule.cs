namespace MeuValorLiquido.Modules.Ads;

public sealed record AdSlotDefinition(string Key, string Label, bool IsEnabled);

public interface IAdSlotProvider
{
    IReadOnlyList<AdSlotDefinition> GetSlots();
}

public sealed class PlaceholderAdSlotProvider : IAdSlotProvider
{
    private static readonly IReadOnlyList<AdSlotDefinition> Slots =
    [
        new("calculator-top", "Espaço reservado para anúncio futuro", true),
        new("calculator-bottom", "Espaço reservado para anúncio futuro", true)
    ];

    public IReadOnlyList<AdSlotDefinition> GetSlots() => Slots;
}
