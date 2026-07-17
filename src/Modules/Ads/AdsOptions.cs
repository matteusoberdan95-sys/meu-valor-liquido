namespace MeuValorLiquido.Modules.Ads;

public sealed class AdsOptions
{
    public const string SectionName = "Ads";

    public bool Enabled { get; set; }

    public bool VerificationEnabled { get; set; }

    public string? PublisherId { get; set; }

    public string CalculatorTopSlotId { get; set; } = string.Empty;

    public string CalculatorBottomSlotId { get; set; } = string.Empty;

    public bool IsActive =>
        Enabled && !string.IsNullOrWhiteSpace(PublisherId);

    public bool ShouldRenderVerificationTag =>
        VerificationEnabled && !string.IsNullOrWhiteSpace(PublisherId);
}
