namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint56MetricsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint56MetricsTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Metrics_Page_Should_Support_7_Day_Period()
    {
        var html = await client.GetStringAsync("/metricas-internas?days=7");

        html.Should().Contain("7 dias");
        html.Should().Contain("30 dias");
        html.Should().Contain("valora-choice--active");
        html.Should().Contain("Ranking");
    }

    [Fact]
    public async Task Metrics_Page_Should_Show_Engagement_Rates()
    {
        var html = await client.GetStringAsync("/metricas-internas");

        html.Should().Contain("dos cálculos");
        html.Should().Contain("Mais compartilhadas");
        html.Should().Contain("Mais salvas no painel");
        html.Should().Contain("METRICS_ROUTINE.md");
    }

    [Fact]
    public async Task GetSummary_Should_Compute_Engagement_Rates()
    {
        var options = Options.Create(new MetricsOptions { Enabled = true });
        await using var db = CreateDbContext();
        var service = new EfProductMetricsService(db, options);

        await service.RecordAsync(ProductMetricEvents.CalculatorCalculation, "salario-liquido");
        await service.RecordAsync(ProductMetricEvents.CalculatorCalculation, "salario-liquido");
        await service.RecordAsync(ProductMetricEvents.ShareCopy, "salario-liquido");
        await service.RecordAsync(ProductMetricEvents.PdfDownload, "salario-liquido");

        var summary = await service.GetSummaryAsync(ProductMetricsPeriod.Week);

        summary.PeriodDays.Should().Be(7);
        summary.TotalCalculations.Should().Be(2);
        summary.SharePerCalculationPercent.Should().Be(50m);
        summary.PdfPerCalculationPercent.Should().Be(50m);
        summary.TopShareCopies.Should().ContainSingle(x => x.Label == "salario-liquido");
    }

    [Fact]
    public void ProductMetricsPeriod_Should_Normalize_To_7_Or_30()
    {
        ProductMetricsPeriod.Normalize(7).Should().Be(7);
        ProductMetricsPeriod.Normalize(30).Should().Be(30);
        ProductMetricsPeriod.Normalize(14).Should().Be(30);
        ProductMetricsPeriod.Normalize(null).Should().Be(30);
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }
}
