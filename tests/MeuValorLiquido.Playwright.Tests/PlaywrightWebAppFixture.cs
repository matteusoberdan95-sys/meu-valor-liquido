using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Playwright;

namespace MeuValorLiquido.Playwright.Tests;

public sealed class PlaywrightWebAppFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private IPlaywright? playwright;

    public IBrowser Browser { get; private set; } = null!;

    public string BaseUrl { get; private set; } = null!;

    public PlaywrightWebAppFixture()
    {
        UseKestrel(options => options.Listen(IPAddress.Loopback, 0));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.UseEnvironment("Testing");

    public async Task InitializeAsync()
    {
        StartServer();

        var addresses = Services.GetRequiredService<IServer>()
            .Features
            .Get<IServerAddressesFeature>()
            ?.Addresses;

        var address = addresses?.FirstOrDefault()
            ?? throw new InvalidOperationException("Não foi possível obter a URL do servidor de testes.");

        BaseUrl = address
            .Replace("[::1]", "127.0.0.1", StringComparison.Ordinal)
            .Replace("localhost", "127.0.0.1", StringComparison.OrdinalIgnoreCase)
            .TrimEnd('/');

        playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        Browser?.CloseAsync().GetAwaiter().GetResult();
        playwright?.Dispose();
        Dispose();
        return Task.CompletedTask;
    }
}

[CollectionDefinition(Name)]
public sealed class PlaywrightCollection : ICollectionFixture<PlaywrightWebAppFixture>
{
    public const string Name = "Playwright";
}
