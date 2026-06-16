namespace MeuValorLiquido.WebApp.Tests;

public class LocalPanelPageTests
{
    [Fact]
    public void SaveContextBuilder_Should_Format_Salary_Proposal_Summary()
    {
        var definition = new CalculatorDefinition(
            "proposta-salarial",
            "Proposta salarial",
            "Trabalhista",
            "Resumo",
            "SEO",
            "SEO desc",
            []);

        var result = new CalculationResult(
            "proposta-salarial",
            "Proposta salarial",
            Money.From(4000m),
            [],
            Money.From(3500m),
            "Explicação",
            "Disclaimer");

        var context = LocalPanelSaveContextBuilder.FromCalculation(
            definition,
            result,
            new CalculatorInput(4000m, SecondaryAmount: 4800m));

        context.Summary.Should().Contain("4.000");
        context.Summary.Should().Contain("4.800");
        context.CalculatorName.Should().Be("Proposta salarial");
    }

    [Fact]
    public async Task Panel_Page_Should_Load()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var response = await client.GetAsync("/meu-painel");

        response.IsSuccessStatusCode.Should().BeTrue();
        var html = await response.Content.ReadAsStringAsync();
        html.Should().Contain("Meu painel");
        html.Should().Contain("data-local-panel-page");
        html.Should().Contain("localStorage");
        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Painel_Alias_Should_Redirect()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient(
            new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var response = await client.GetAsync("/painel");

        response.StatusCode.Should().Be(HttpStatusCode.Redirect);
        response.Headers.Location?.ToString().Should().Contain("/meu-painel");
    }

    [Fact]
    public async Task Shared_Calculator_Should_Expose_Save_Button()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(3000m));
        var html = await client.GetStringAsync($"/calculadoras/salario-liquido?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("data-local-panel-save");
        html.Should().Contain("Salvar no painel");
        html.Should().Contain("/meu-painel");
    }

    [Fact]
    public async Task Salary_Band_Should_Expose_Save_Button()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var html = await client.GetStringAsync("/salario-liquido/3000");

        html.Should().Contain("data-local-panel-save");
        html.Should().Contain("salario-liquido-faixa");
    }

    [Fact]
    public async Task Sitemap_Should_Include_Local_Panel()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.WithWebHostBuilder(b => b.UseEnvironment("Testing")).CreateClient();

        var xml = await client.GetStringAsync("/sitemap.xml");

        xml.Should().Contain("/meu-painel");
    }
}
