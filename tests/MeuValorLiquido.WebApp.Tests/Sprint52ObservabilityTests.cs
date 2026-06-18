namespace MeuValorLiquido.WebApp.Tests;

using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

public sealed class Sprint52ObservabilityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint52ObservabilityTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
    }

    [Fact]
    public async Task Metrics_Page_Should_Show_Error_And_Prioritization_Sections()
    {
        var html = await client.GetStringAsync("/metricas-internas");

        html.Should().Contain("Erros 404");
        html.Should().Contain("Erros 500");
        html.Should().Contain("Falhas de cálculo");
        html.Should().Contain("Priorização sugerida");
        html.Should().Contain("Rotas 404 mais frequentes");
        html.Should().Contain("SEO_MONTHLY_REVIEW.md");
        html.Should().Contain("CALIBRATION_ROUTINE.md");
    }

    [Fact]
    public async Task NotFound_Should_Record_404_Metric()
    {
        var options = Options.Create(new MetricsOptions { Enabled = true });
        await using var db = CreateDbContext();
        var service = new EfProductMetricsService(db, options);

        using var factory = new WebApplicationFactory<Program>();
        using var httpClient = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                ReplaceMetricsService(services, service);
            });
        }).CreateClient();

        var response = await httpClient.GetAsync("/rota-inexistente-sprint52");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var summary = await service.GetSummaryAsync(ProductMetricsPeriod.Week);
        summary.TotalHttp404.Should().BeGreaterThan(0);
        summary.TopHttp404Routes.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Invalid_Calculator_Post_Should_Record_Calculation_Failure()
    {
        var options = Options.Create(new MetricsOptions { Enabled = true });
        await using var db = CreateDbContext();
        var service = new EfProductMetricsService(db, options);

        using var factory = new WebApplicationFactory<Program>();
        using var httpClient = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                ReplaceMetricsService(services, service);
            });
        }).CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var html = await httpClient.GetStringAsync("/calculadoras/salario-liquido");
        var token = Regex.Match(html, "name=\"__RequestVerificationToken\"[^>]*value=\"([^\"]+)\"").Groups[1].Value;

        using var postResponse = await httpClient.PostAsync(
            "/calculadoras/salario-liquido",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["__RequestVerificationToken"] = token,
                ["Input.Amount"] = "0"
            }));

        postResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var summary = await service.GetSummaryAsync(ProductMetricsPeriod.Week);
        summary.TotalCalculationFailures.Should().BeGreaterThan(0);
        summary.TopCalculationFailures.Should().Contain(x => x.Label == "salario-liquido");
    }

    [Fact]
    public void Prioritization_Builder_Should_Flag_High_404_Volume()
    {
        var summary = new ProductMetricsSummary(
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-6)),
            DateOnly.FromDateTime(DateTime.UtcNow),
            7,
            100,
            10,
            5,
            2,
            0,
            5m,
            10m,
            2m,
            12,
            0,
            0,
            0m,
            [],
            [],
            [],
            [],
            [new ProductMetricRow("/blog/antigo", ProductMetricEvents.HttpError404, 12)],
            [],
            []);

        var insights = ProductMetricsPrioritizationBuilder.Build(summary, new Dictionary<string, string>());

        insights.Should().Contain(i => i.Title.Contains("404", StringComparison.Ordinal));
    }

    [Fact]
    public void Path_Normalizer_Should_Truncate_Query_And_Segments()
    {
        ProductMetricsPathNormalizer.NormalizePath("/blog/post/foo?x=1")
            .Should().Be("/blog/post/foo");
        ProductMetricsPathNormalizer.NormalizePath("/a/b/c/d/e")
            .Should().Be("/a/b/c");
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private static void ReplaceMetricsService(IServiceCollection services, IProductMetricsService service)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IProductMetricsService));
        if (descriptor is not null)
        {
            services.Remove(descriptor);
        }

        services.AddScoped<IProductMetricsService>(_ => service);
    }
}
