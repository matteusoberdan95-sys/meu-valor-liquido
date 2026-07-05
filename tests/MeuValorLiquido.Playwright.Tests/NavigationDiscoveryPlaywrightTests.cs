using Microsoft.Playwright;

namespace MeuValorLiquido.Playwright.Tests;

[Collection(PlaywrightCollection.Name)]
public sealed class NavigationDiscoveryPlaywrightTests(PlaywrightWebAppFixture fixture)
{
    private async Task WithPageAsync(Func<IPage, Task> action)
    {
        await using var context = await fixture.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = fixture.BaseUrl
        });

        var page = await context.NewPageAsync();
        await action(page);
    }

    private static async Task DismissCookieBannerAsync(IPage page)
    {
        var rejectButton = page.GetByRole(AriaRole.Button, new() { Name = "Rejeitar todos" });
        if (await rejectButton.CountAsync() > 0 && await rejectButton.IsVisibleAsync())
        {
            await rejectButton.ClickAsync();
        }
    }

    [Fact]
    public async Task Home_Should_Navigate_To_Desligamento_Hub()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/");

            var hubLink = page.GetByTestId("thematic-hub-desligamento");
            await hubLink.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await hubLink.ClickAsync();

            await page.WaitForURLAsync("**/desligamento");
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Saiu da empresa?" })).ToBeVisibleAsync();
        });
    }

    [Fact]
    public async Task Home_Should_Navigate_To_Conferir_Holerite()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/");

            var holeriteCard = page.GetByTestId("conferir-holerite-card");
            await holeriteCard.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await holeriteCard.ClickAsync();

            await page.WaitForURLAsync("**/conferir-holerite");
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Seu holerite está certo?" })).ToBeVisibleAsync();
        });
    }

    [Fact]
    public async Task Header_Nav_Should_Open_Conferir_Holerite()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/");
            await Task.WhenAll(
                page.WaitForURLAsync("**/conferir-holerite"),
                page.GetByTestId("nav-conferir-holerite").ClickAsync());

            await Assertions.Expect(page.Locator(".valora-stitch-payslip-form")).ToBeVisibleAsync();
        });
    }

    [Fact]
    public async Task Rescisao_Calculator_Should_Link_To_Desligamento_Hub()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/calculadoras/rescisao-clt");
            await page.GetByTestId("thematic-hub-promo-desligamento").ClickAsync();

            await page.WaitForURLAsync("**/desligamento");
            await Assertions.Expect(page.GetByText("Calculadoras recomendadas")).ToBeVisibleAsync();
        });
    }

    [Fact]
    public async Task Salario_Liquido_Calculator_Should_Link_To_Conferir_Holerite()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/calculadoras/salario-liquido");
            await page.GetByTestId("conferir-holerite-promo").ClickAsync();

            await page.WaitForURLAsync("**/conferir-holerite");
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Seu holerite está certo?" })).ToBeVisibleAsync();
        });
    }

    [Fact]
    public async Task Calculadoras_Hub_Should_Show_Jornadas_And_Holerite()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/calculadoras");

            await Assertions.Expect(page.GetByTestId("thematic-hub-desligamento")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByTestId("conferir-holerite-card")).ToBeVisibleAsync();
        });
    }

    [Theory]
    [InlineData(1366, 900)]
    [InlineData(430, 884)]
    public async Task Assistant_Page_Should_Be_Responsive_And_Submit_Guided_Question(int width, int height)
    {
        await WithPageAsync(async page =>
        {
            await page.SetViewportSizeAsync(width, height);
            await page.GotoAsync("/assistente");
            await DismissCookieBannerAsync(page);

            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Assistente Meu Valor Líquido" })).ToBeVisibleAsync();
            await Assertions.Expect(page.Locator("[data-assistant-chat]")).ToBeVisibleAsync();
            await Assertions.Expect(page.Locator("[data-assistant-input]")).ToBeVisibleAsync();

            var hasHorizontalOverflow = await page.EvaluateAsync<bool>("() => document.documentElement.scrollWidth > document.documentElement.clientWidth + 1");
            Assert.False(hasHorizontalOverflow);

            await page.Locator("[data-assistant-prompt='Quanto desconta de INSS?']").ClickAsync();
            await page.Locator("[data-assistant-chat-form]").EvaluateAsync("form => form.requestSubmit()");

            await Assertions.Expect(page.GetByText("Calcular INSS").Nth(0)).ToBeVisibleAsync();
        });
    }

    [Fact]
    public async Task Home_Assistant_Launcher_Should_Open_Invite()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/");

            await page.Locator("[data-assistant-launcher-toggle]").ClickAsync();

            await Assertions.Expect(page.GetByText("Quer tirar uma dúvida rápida?")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Link, new() { Name = "Iniciar chat" })).ToHaveAttributeAsync("href", "/assistente");
        });
    }

    [Fact]
    public async Task Desktop_Header_Should_Stay_Compact()
    {
        await WithPageAsync(async page =>
        {
            await page.SetViewportSizeAsync(1366, 768);
            await page.GotoAsync("/");

            var headerHeight = await page.Locator(".valora-header").EvaluateAsync<double>("header => header.getBoundingClientRect().height");
            Assert.True(headerHeight <= 82, $"Header ficou alto demais: {headerHeight}px.");

            var hasWrappedNavItem = await page.Locator(".valora-nav a").EvaluateAllAsync<bool>(
                "items => items.some(item => item.getBoundingClientRect().height > 44)");
            Assert.False(hasWrappedNavItem);

            await Assertions.Expect(page.GetByText("Página Inicial")).ToHaveCountAsync(0);
        });
    }

    [Fact]
    public async Task Cookie_Consent_Should_Offer_Choice_And_Customization()
    {
        await WithPageAsync(async page =>
        {
            await page.SetViewportSizeAsync(430, 884);
            await page.GotoAsync("/");

            await Assertions.Expect(page.GetByRole(AriaRole.Dialog, new() { Name = "Consentimento de cookies" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Aceitar todos" })).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Rejeitar todos" })).ToBeVisibleAsync();

            var compactHeight = await page.Locator(".valora-cookie-consent-content")
                .EvaluateAsync<double>("el => el.getBoundingClientRect().height");
            Assert.True(compactHeight <= 380, $"Cookie banner mobile ficou alto demais: {compactHeight}px.");

            await page.GetByRole(AriaRole.Button, new() { Name = "Personalizar" }).ClickAsync();

            await Assertions.Expect(page.GetByText("Cookies essenciais")).ToBeVisibleAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Salvar preferencias" })).ToBeVisibleAsync();

            var expandedHeight = await page.Locator(".valora-cookie-consent-content")
                .EvaluateAsync<double>("el => el.getBoundingClientRect().height");
            Assert.True(expandedHeight <= 380, $"Cookie banner mobile expandido ficou alto demais: {expandedHeight}px.");
        });
    }
}
