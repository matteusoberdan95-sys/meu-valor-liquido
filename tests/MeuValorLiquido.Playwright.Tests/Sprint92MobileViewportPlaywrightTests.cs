using Microsoft.Playwright;

namespace MeuValorLiquido.Playwright.Tests;

[Collection(PlaywrightCollection.Name)]
public sealed class Sprint92MobileViewportPlaywrightTests(PlaywrightWebAppFixture fixture)
{
    private async Task WithViewportPageAsync(int width, int height, Func<IPage, Task> action)
    {
        await using var context = await fixture.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = fixture.BaseUrl,
            ViewportSize = new ViewportSize { Width = width, Height = height }
        });

        var page = await context.NewPageAsync();
        page.SetDefaultTimeout(15_000);
        await action(page);
    }

    public static IEnumerable<object[]> MobileViewports =>
    [
        [360, 800],
        [390, 844],
        [412, 915]
    ];

    public static IEnumerable<object[]> MobileRoutes =>
    [
        ["/"],
        ["/calculadoras/salario-liquido"],
        ["/blog/o-que-e-salario-liquido"],
        ["/como-calculamos"]
    ];

    public static IEnumerable<object[]> ViewportRouteMatrix =>
        from viewport in MobileViewports
        from route in MobileRoutes
        select new object[] { (int)viewport[0], (int)viewport[1], (string)route[0] };

    [Theory]
    [MemberData(nameof(ViewportRouteMatrix))]
    public async Task Key_Pages_Should_Not_Overflow_On_Modest_Mobile_Viewports(int width, int height, string path)
    {
        await WithViewportPageAsync(width, height, async page =>
        {
            var consoleErrors = new List<string>();
            page.Console += (_, args) =>
            {
                if (args.Type == "error")
                {
                    consoleErrors.Add(args.Text);
                }
            };

            await page.GotoAsync(path, new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

            var hasHorizontalOverflow = await page.EvaluateAsync<bool>(
                "() => document.documentElement.scrollWidth > document.documentElement.clientWidth + 1");
            Assert.False(hasHorizontalOverflow, $"{path} overflowed at {width}x{height}");

            await Assertions.Expect(page.Locator("main")).ToBeVisibleAsync();
            Assert.DoesNotContain(consoleErrors, error =>
                error.Contains("Failed to load resource", StringComparison.OrdinalIgnoreCase)
                || error.Contains("Uncaught", StringComparison.OrdinalIgnoreCase));
        });
    }

    [Theory]
    [MemberData(nameof(MobileViewports))]
    public async Task Calculator_Primary_Actions_Should_Meet_Touch_Target_On_Mobile(int width, int height)
    {
        await WithViewportPageAsync(width, height, async page =>
        {
            await page.GotoAsync("/calculadoras/salario-liquido", new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

            var toggleBox = await page.Locator("[data-nav-toggle]").BoundingBoxAsync();
            Assert.NotNull(toggleBox);
            Assert.True(toggleBox!.Width >= 44 || toggleBox.Height >= 44);

            var bottomNavItem = await page.Locator(".valora-bottom-nav-item").First.BoundingBoxAsync();
            Assert.NotNull(bottomNavItem);
            Assert.True(bottomNavItem!.Height >= 44);
        });
    }
}
