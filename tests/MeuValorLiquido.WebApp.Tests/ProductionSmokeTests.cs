namespace MeuValorLiquido.WebApp.Tests;

/// <summary>
/// Smoke opcional contra produção ou staging. Defina <c>SMOKE_BASE_URL</c> (ex.: https://meuvalorliquido.com).
/// Sem a variável, os testes retornam imediatamente (não falham).
/// </summary>
public sealed class ProductionSmokeTests
{
    private static string? BaseUrl =>
        Environment.GetEnvironmentVariable("SMOKE_BASE_URL")?.TrimEnd('/');

    public static IEnumerable<object[]> AllCalculatorSlugs =>
        CalculatorSeedData.GetDefinitions().Select(definition => new object[] { definition.Slug });

    [Fact]
    public async Task Production_Health_Should_Be_Healthy_When_Configured()
    {
        if (!TryCreateClient(out var client))
        {
            return;
        }

        using (client)
        {
            var body = await client.GetStringAsync("/health");
            body.Should().Contain("Healthy");
        }
    }

    [Fact]
    public async Task Production_Sitemap_Should_Be_Available_When_Configured()
    {
        if (!TryCreateClient(out var client))
        {
            return;
        }

        using (client)
        {
            using var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, "/sitemap.xml"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

    [Theory]
    [MemberData(nameof(AllCalculatorSlugs))]
    public async Task Production_Calculator_Should_Return_200(string slug)
    {
        if (!TryCreateClient(out var client))
        {
            return;
        }

        using (client)
        {
            using var response = await client.GetAsync($"/calculadoras/{slug}");
            response.IsSuccessStatusCode.Should().BeTrue($"calculadora {slug} em {BaseUrl}");
        }
    }

    [Fact]
    public async Task Production_Metrics_Page_Should_Be_NoIndex_When_Configured()
    {
        if (!TryCreateClient(out var client))
        {
            return;
        }

        using (client)
        {
            var html = await client.GetStringAsync("/metricas-internas");
            html.Should().Contain("noindex,nofollow");
            html.Should().Contain("Priorização sugerida");
        }
    }

    private static bool TryCreateClient(out HttpClient client)
    {
        if (string.IsNullOrWhiteSpace(BaseUrl))
        {
            client = null!;
            return false;
        }

        client = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl + "/"),
            Timeout = TimeSpan.FromSeconds(45)
        };
        return true;
    }
}
