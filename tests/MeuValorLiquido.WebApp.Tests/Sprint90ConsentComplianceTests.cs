namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint90ConsentComplianceTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;
    private readonly HttpClient client;

    public Sprint90ConsentComplianceTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Clean_Visit_Should_Expose_Four_Categories_With_Optional_Choices_Unchecked()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("data-consent-version=\"2\"");
        html.Should().Contain("data-policy-version=\"2026-07-17\"");
        html.Should().Contain("Cookies essenciais");
        html.Should().Contain(">Analytics</strong>");
        html.Should().Contain(">Personalização</strong>");
        html.Should().Contain(">Publicidade</strong>");
        html.Should().Contain("data-cookie-consent-analytics");
        html.Should().Contain("data-cookie-consent-personalization");
        html.Should().Contain("data-cookie-consent-advertising");
        html.Should().NotContain("data-cookie-consent-analytics checked");
        html.Should().NotContain("data-cookie-consent-personalization checked");
        html.Should().NotContain("data-cookie-consent-advertising checked");
    }

    [Fact]
    public async Task Configured_Ads_Should_Remain_Hidden_And_Scriptless_Before_Consent()
    {
        using var configuredClient = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Ads:Enabled"] = "true",
                    ["Ads:PublisherId"] = "ca-pub-consent-test",
                    ["Ads:CalculatorTopSlotId"] = "top-slot"
                });
            });
        }).CreateClient();

        using var response = await configuredClient.GetAsync("/calculadoras/salario-liquido");
        var html = await response.Content.ReadAsStringAsync();

        html.Should().Contain("data-ads-slots-enabled=\"true\"");
        html.Should().Contain("data-ads-script-enabled=\"true\"");
        html.Should().Contain("data-ad-consent-required");
        html.Should().Contain("data-ad-slot=\"top-slot\"");
        html.Should().Contain("hidden");
        html.Should().NotContain(
            "src=\"https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-consent-test\"");
    }

    [Fact]
    public async Task Client_Scripts_Should_Enforce_Each_Optional_Category()
    {
        var consent = await client.GetStringAsync("/js/cookie-consent.js");
        var metrics = await client.GetStringAsync("/js/product-metrics.js");
        var panel = await client.GetStringAsync("/js/local-panel.js");
        var checklist = await client.GetStringAsync("/js/rescisao-checklist.js");

        consent.Should().Contain("consentVersion = 2");
        consent.Should().Contain("policyVersion = \"2026-07-17\"");
        consent.Should().Contain("saveCategories(false, false, false)");
        consent.Should().Contain("consent.advertising && adsScriptEnabled");
        consent.Should().Contain("clearPersonalizationStorage");
        metrics.Should().Contain("allows(\"analytics\")");
        panel.Should().Contain("allows(\"personalization\")");
        checklist.Should().Contain("allows(\"personalization\")");
    }

    [Fact]
    public async Task Legal_Pages_Should_Match_Implemented_Consent_Behavior()
    {
        var cookies = WebUtility.HtmlDecode(await client.GetStringAsync("/politica-de-cookies"));
        var privacy = WebUtility.HtmlDecode(await client.GetStringAsync("/politica-de-privacidade"));
        var terms = WebUtility.HtmlDecode(await client.GetStringAsync("/termos-de-uso"));
        var notice = WebUtility.HtmlDecode(await client.GetStringAsync("/aviso-legal"));

        cookies.Should().Contain("Nenhuma categoria opcional começa marcada");
        cookies.Should().Contain("versão 2");
        cookies.Should().Contain("17 de julho de 2026");
        privacy.Should().Contain("A verificação da conta usa somente uma meta tag");
        privacy.Should().Contain("base legal de consentimento");
        terms.Should().Contain("não substitui consentimento");
        notice.Should().Contain("só é carregado após consentimento");
    }

    [Fact]
    public async Task AdsTxt_Should_Contain_Only_A_Concrete_Google_Publisher_Record()
    {
        var adsTxt = (await client.GetStringAsync("/ads.txt")).Trim();

        adsTxt.Should().MatchRegex(
            "^google\\.com, pub-[0-9]+, DIRECT, f08c47fec0942fa0$");
        adsTxt.Should().NotContain("SEU_ID");
        adsTxt.Should().NotContain("test");
    }
}
