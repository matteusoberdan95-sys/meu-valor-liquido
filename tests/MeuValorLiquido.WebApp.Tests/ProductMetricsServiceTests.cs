namespace MeuValorLiquido.WebApp.Tests;

public class ProductMetricsServiceTests
{
    [Fact]
    public async Task RecordAsync_Should_Increment_Daily_Counter()
    {
        var options = Options.Create(new MetricsOptions { Enabled = true });
        await using var db = CreateDbContext();
        var service = new EfProductMetricsService(db, options);

        await service.RecordAsync(ProductMetricEvents.CalculatorCalculation, "salario-liquido");
        await service.RecordAsync(ProductMetricEvents.CalculatorCalculation, "salario-liquido");

        var summary = await service.GetSummaryAsync();
        summary.TotalCalculations.Should().Be(2);
        summary.TopCalculations.Should().ContainSingle(x => x.Label == "salario-liquido" && x.Count == 2);
    }

    [Fact]
    public async Task RecordAsync_Should_Ignore_Disallowed_Events()
    {
        var options = Options.Create(new MetricsOptions { Enabled = true });
        await using var db = CreateDbContext();
        var service = new EfProductMetricsService(db, options);

        await service.RecordAsync("salary_amount_logged", "3000");

        (await db.AggregatedMetrics.CountAsync()).Should().Be(0);
    }

    [Fact]
    public async Task Collect_Endpoint_Should_Accept_Client_Event()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        using var response = await client.PostAsJsonAsync(
            "/api/metrics/collect",
            new ProductMetricCollectRequest(ProductMetricEvents.PanelSave, "salario-liquido"));

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Collect_Endpoint_Should_Reject_Server_Only_Event()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        using var response = await client.PostAsJsonAsync(
            "/api/metrics/collect",
            new ProductMetricCollectRequest(ProductMetricEvents.CalculatorCalculation, "salario-liquido"));

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Internal_Metrics_Page_Should_Be_NoIndex()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var html = await client.GetStringAsync("/metricas-internas");

        html.Should().Contain("noindex,nofollow");
        html.Should().Contain("Métricas internas");
        html.Should().Contain("sem IP");
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }
}
