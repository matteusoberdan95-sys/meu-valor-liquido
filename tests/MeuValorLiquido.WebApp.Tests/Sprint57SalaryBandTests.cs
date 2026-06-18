namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint57SalaryBandTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint57SalaryBandTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public void Catalog_Should_Expose_At_Least_28_Bands()
    {
        SalaryBandCatalog.GetAll()
            .Should()
            .HaveCountGreaterThanOrEqualTo(SalaryBandCatalog.MinimumIndexedBands);
    }

    [Fact]
    public void Every_Band_Should_Have_Unique_Editorial_Angle()
    {
        var headlines = SalaryBandCatalog.GetAll()
            .Select(gross =>
            {
                SalaryBandEditorialCatalog.TryGetAngle(gross, out var angle).Should().BeTrue();
                return angle!.Headline;
            })
            .ToList();

        headlines.Should().OnlyHaveUniqueItems();
    }

    [Theory]
    [InlineData(2200)]
    [InlineData(4200)]
    [InlineData(11000)]
    public void New_Bands_Should_Have_Editorial_And_Fifth_Faq(int gross)
    {
        var breakdown = new NetSalaryCalculator(new InssCalculator(), new IrrfCalculator())
            .Calculate(gross, 0, 0m);
        var content = SalaryBandContentBuilder.Build(gross, breakdown);

        content.EditorialHtml.Should().NotBeNullOrWhiteSpace();
        content.FaqItems.Should().HaveCount(5);
        content.EditorialHtml.Should().Contain(SalaryBandCatalog.FormatCurrency(gross));
    }

    [Fact]
    public async Task Sitemap_Should_Include_New_Salary_Bands()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/salario-liquido/2200");
        xml.Should().Contain("/salario-liquido/4200");
        xml.Should().Contain("/salario-liquido/11000");
    }

    [Fact]
    public async Task Band_Page_Should_Render_Editorial_And_Widget_Cta()
    {
        var html = await client.GetStringAsync("/salario-liquido/4200");

        html.Should().Contain("Planejamento:");
        html.Should().Contain("Incorpore a calculadora no seu site");
        html.Should().Contain("/widget");
    }
}

public sealed class Sprint57WidgetCtaTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint57WidgetCtaTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/contato")]
    [InlineData("/salario-liquido")]
    [InlineData("/blog/o-que-e-salario-liquido")]
    public async Task Public_Pages_Should_Link_To_Widget_Hub(string path)
    {
        var html = await client.GetStringAsync(path);

        html.Should().Contain("/widget");
    }

    [Theory]
    [InlineData("/contato")]
    [InlineData("/salario-liquido")]
    public async Task Public_Pages_Should_Link_To_Incorporar_Alias(string path)
    {
        var html = await client.GetStringAsync(path);

        html.Should().Contain("/incorporar");
    }
}
