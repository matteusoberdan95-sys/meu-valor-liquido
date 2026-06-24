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
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Conferir holerite" })).ToBeVisibleAsync();
        });
    }

    [Fact]
    public async Task Header_Nav_Should_Open_Conferir_Holerite()
    {
        await WithPageAsync(async page =>
        {
            await page.GotoAsync("/");
            await page.GetByTestId("nav-conferir-holerite").ClickAsync();

            await page.WaitForURLAsync("**/conferir-holerite");
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
            await Assertions.Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Conferir holerite" })).ToBeVisibleAsync();
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
}
