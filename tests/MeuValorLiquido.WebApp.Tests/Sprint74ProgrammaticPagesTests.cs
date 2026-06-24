namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint74ProgrammaticPagesTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint74ProgrammaticPagesTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public void Salary_Band_Catalog_Should_Expose_At_Least_40_Bands()
    {
        SalaryBandCatalog.GetAll().Should().HaveCountGreaterThanOrEqualTo(SalaryBandCatalog.MinimumIndexedBands);
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
    [InlineData(2400)]
    [InlineData(5800)]
    [InlineData(14000)]
    public void New_Bands_Should_Have_Editorial_Content(int gross)
    {
        var breakdown = new NetSalaryCalculator(new InssCalculator(), new IrrfCalculator())
            .Calculate(gross, 0, 0m);
        var content = SalaryBandContentBuilder.Build(gross, breakdown);

        content.EditorialHtml.Should().NotBeNullOrWhiteSpace();
        content.FaqItems.Should().HaveCountGreaterThanOrEqualTo(5);
    }

    [Theory]
    [InlineData(5000, 0, "/salario-liquido/5000")]
    [InlineData(5000, 1, "/salario-liquido/5000/1-dependente")]
    [InlineData(5000, 2, "/salario-liquido/5000/2-dependentes")]
    public void Salary_Band_SlugPath_Should_Support_Dependent_Variants(int gross, int dependents, string expectedPath)
    {
        SalaryBandCatalog.SlugPath(gross, dependents).Should().Be(expectedPath);
    }

    [Theory]
    [InlineData(6000, 0, "/clt-pj/6000-clt-equivale-a-quanto-pj")]
    [InlineData(6000, 1, "/clt-pj/6000/1-dependente")]
    public void CltPj_SlugPath_Should_Support_Dependent_Variants(int gross, int dependents, string expectedPath)
    {
        CltPjBandCatalog.SlugPath(gross, dependents).Should().Be(expectedPath);
    }

    [Fact]
    public async Task Salary_Band_Dependent_Page_Should_Render_Unique_Seo()
    {
        var html = await client.GetStringAsync("/salario-liquido/6000/1-dependente");

        html.Should().Contain("com 1 dependente");
        html.Should().Contain("1 dependente");
        html.Should().Contain("/salario-liquido/6000");
    }

    [Fact]
    public async Task CltPj_Dependent_Page_Should_Render()
    {
        var html = await client.GetStringAsync("/clt-pj/6000/1-dependente");

        html.Should().Contain("com 1 dependente");
        html.Should().Contain("Simples");
    }

    [Fact]
    public async Task Invalid_Dependent_Variant_Should_Return_NotFound()
    {
        using var response = await client.GetAsync("/salario-liquido/5000/3-dependentes");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Sitemap_Should_Include_Dependent_Variant_Urls()
    {
        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/salario-liquido/5800");
        xml.Should().Contain("/salario-liquido/6000/1-dependente");
        xml.Should().Contain("/clt-pj/6000/1-dependente");
    }

    [Fact]
    public void Indexable_Url_Count_Should_Match_Bands_Times_Variants()
    {
        var bandCount = SalaryBandCatalog.GetAll().Count;
        var variantCount = ProgrammaticDependentsCatalog.IndexedDependentCounts.Length;

        SalaryBandCatalog.GetAllIndexablePaths().Should().HaveCount(bandCount * variantCount);
        CltPjBandCatalog.GetAllIndexablePaths().Should().HaveCount(bandCount * variantCount);
    }
}
