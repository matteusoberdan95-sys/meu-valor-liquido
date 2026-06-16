namespace MeuValorLiquido.WebApp.Tests;

public class CltPjPageTests
{
    [Fact]
    public void ContentBuilder_5000_Should_Mention_Equivalent_Pj()
    {
        var calculator = new CltPjComparisonCalculator(
            new NetSalaryCalculator(new InssCalculator(), new IrrfCalculator()),
            new InssCalculator(),
            new IrrfCalculator());
        var comparison = calculator.Compare(new CalculatorInput(5000m, Rate: 6m));
        var content = CltPjContentBuilder.Build(5000, comparison);

        content.Title.Should().Contain("5.000");
        content.Description.Should().Contain("faturamento PJ");
        content.FaqItems.Should().HaveCountGreaterThanOrEqualTo(4);
    }

    [Fact]
    public async Task Hub_Page_Should_Load()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var response = await client.GetAsync("/clt-pj");

        response.IsSuccessStatusCode.Should().BeTrue();
        var html = await response.Content.ReadAsStringAsync();
        html.Should().Contain("CLT x PJ");
        html.Should().Contain("calculadora PJ vs CLT");
    }

    [Fact]
    public async Task Comparison_Page_Should_Show_Panel()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var html = await client.GetStringAsync("/clt-pj/5000-clt-equivale-a-quanto-pj");

        html.Should().Contain("Comparativo estimado");
        html.Should().Contain("Faturamento PJ equivalente");
        html.Should().Contain("Calculadora completa");
    }
}
