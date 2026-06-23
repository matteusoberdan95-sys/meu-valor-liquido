namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint69PdfTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint69PdfTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Sprint69_Calculator_Pdf_Should_Be_Larger_Than_Legacy_Minimum()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(5000m, Dependents: 1, TransportDiscount: 120m));
        var response = await client.GetAsync(
            $"/calculadoras/salario-liquido/resultado.pdf?r={Uri.EscapeDataString(token)}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var bytes = await response.Content.ReadAsByteArrayAsync();
        bytes.Length.Should().BeGreaterThan(4000, "PDF premium deve incluir logo e layout expandido");
        Encoding.ASCII.GetString(bytes.AsSpan(0, 4)).Should().Be("%PDF");
    }

    [Fact]
    public async Task Sprint69_Proposal_Pdf_Should_Return_Valid_File()
    {
        var token = CalculatorInputShareCodec.Encode(new CalculatorInput(4000m, SecondaryAmount: 4800m));
        var response = await client.GetAsync(
            $"/calculadoras/proposta-salarial/resultado.pdf?r={Uri.EscapeDataString(token)}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var bytes = await response.Content.ReadAsByteArrayAsync();
        bytes.Length.Should().BeGreaterThan(4000);
    }

    [Fact]
    public async Task Sprint69_Salary_Band_Pdf_Should_Return_Valid_File()
    {
        var response = await client.GetAsync("/salario-liquido/5000/resultado.pdf");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var bytes = await response.Content.ReadAsByteArrayAsync();
        bytes.Length.Should().BeGreaterThan(4000);
    }

    [Fact]
    public void Sprint69_Input_Summary_Should_Include_Holerite_Fields()
    {
        var builder = new CalculatorPdfInputSummaryBuilder(new CalculatorFieldProfileProvider());
        var fields = builder.Build(
            "salario-liquido",
            new CalculatorInput(5000m, Dependents: 2, TransportDiscount: 150m, MealVoucherDiscount: 80m));

        fields.Should().Contain(f => f.Label.Contains("bruto", StringComparison.OrdinalIgnoreCase));
        fields.Should().Contain(f => f.Label.Contains("Dependentes", StringComparison.OrdinalIgnoreCase) && f.Value == "2");
        fields.Should().Contain(f => f.Label.Contains("Vale-transporte", StringComparison.OrdinalIgnoreCase));
    }
}
