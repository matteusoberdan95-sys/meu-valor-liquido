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
