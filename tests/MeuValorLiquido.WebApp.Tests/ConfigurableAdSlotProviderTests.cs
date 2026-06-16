namespace MeuValorLiquido.WebApp.Tests;

public class ConfigurableAdSlotProviderTests
{
    [Fact]
    public void GetSlots_Should_Return_Placeholders_When_Ads_Disabled()
    {
        var options = Options.Create(new AdsOptions { Enabled = false });
        var provider = new ConfigurableAdSlotProvider(options);

        var slots = provider.GetSlots();

        slots.Should().HaveCount(2);
        slots.Should().OnlyContain(s => s.IsPlaceholder);
    }

    [Fact]
    public void GetSlots_Should_Return_Live_Slots_When_Ads_Enabled()
    {
        var options = Options.Create(new AdsOptions
        {
            Enabled = true,
            PublisherId = "ca-pub-test",
            CalculatorTopSlotId = "111",
            CalculatorBottomSlotId = "222"
        });
        var provider = new ConfigurableAdSlotProvider(options);

        var slots = provider.GetSlots();

        slots.Should().Contain(s => s.Key == "calculator-top" && s.AdSlotId == "111" && !s.IsPlaceholder);
        slots.Should().Contain(s => s.Key == "calculator-bottom" && s.AdSlotId == "222" && !s.IsPlaceholder);
    }
}
