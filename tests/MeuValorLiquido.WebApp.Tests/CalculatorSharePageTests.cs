namespace MeuValorLiquido.WebApp.Tests;
public class CalculatorSharePageTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public CalculatorSharePageTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Shared_Link_Should_Render_Result_And_Share_Actions()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(3000m));
        var html = await client.GetStringAsync($"/calculadoras/salario-liquido?r={Uri.EscapeDataString(token)}");

        (html.Contains("L&#xCD;QUIDO ESTIMADO") || html.Contains("valora-stitch-salario-result"))
            .Should()
            .BeTrue();
        html.Should().Contain("Compartilhar estimativa");
        html.Should().Contain("wa.me");
        html.Should().Contain("Explicação simples");
        html.Should().Contain("Continue explorando");
        html.Should().Contain("/calculadoras/inss");
    }

    [Fact]
    public async Task Salary_Band_Page_Should_Include_Simple_Explanation_And_Related_Links()
    {
        var html = await client.GetStringAsync("/salario-liquido/3000");

        html.Should().Contain("Explicação simples");
        html.Should().Contain("Entenda em passos");
        html.Should().Contain("Continue explorando");
        html.Should().Contain("data-share-copy");
        html.Should().Contain("Baixar PDF");
        html.Should().Contain("data-local-panel-save");
    }

    [Fact]
    public async Task Salary_Proposal_Shared_Link_Should_Render_Comparison()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(4000m, SecondaryAmount: 4800m));
        var html = await client.GetStringAsync($"/calculadoras/proposta-salarial?r={Uri.EscapeDataString(token)}");

        html.Should().Contain("valora-stitch-proposta");
        html.Should().Contain("Líquido proposto estimado");
        html.Should().Contain("Ganho no bolso");
        html.Should().Contain("Compartilhar estimativa");
        html.Should().Contain("Proposta salarial");
    }

    [Fact]
    public async Task Calculator_Pdf_Endpoint_Should_Return_Valid_Pdf()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(3000m));
        var response = await client.GetAsync(
            $"/calculadoras/salario-liquido/resultado.pdf?r={Uri.EscapeDataString(token)}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/pdf");

        var bytes = await response.Content.ReadAsByteArrayAsync();
        bytes.Length.Should().BeGreaterThan(100);
        Encoding.ASCII.GetString(bytes.AsSpan(0, 4)).Should().Be("%PDF");
    }

    [Fact]
    public async Task Salary_Band_Pdf_Endpoint_Should_Return_Valid_Pdf()
    {
        var response = await client.GetAsync("/salario-liquido/3000/resultado.pdf");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/pdf");

        var bytes = await response.Content.ReadAsByteArrayAsync();
        Encoding.ASCII.GetString(bytes.AsSpan(0, 4)).Should().Be("%PDF");
    }
}
