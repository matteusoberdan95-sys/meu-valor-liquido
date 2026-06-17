namespace MeuValorLiquido.WebApp.Tests;

public class CalculatorFormFieldsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public CalculatorFormFieldsTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Theory]
    [InlineData("/calculadoras/salario-liquido", "Input_Amount", "Sal&#xE1;rio bruto")]
    [InlineData("/calculadoras/salario-bruto-necessario", "Input_Amount", "Sal&#xE1;rio l&#xED;quido desejado")]
    [InlineData("/calculadoras/salario-bruto-necessario", "Input_SecondaryAmount", "Desconto vale-refei&#xE7;&#xE3;o/alimenta&#xE7;&#xE3;o")]
    [InlineData("/calculadoras/proposta-salarial", "Input_Amount", "Sal&#xE1;rio bruto atual")]
    [InlineData("/calculadoras/proposta-salarial", "Input_SecondaryAmount", "Sal&#xE1;rio bruto proposto")]
    public async Task Calculator_Form_Should_Render_Primary_Fields(string url, string inputId, string encodedLabel)
    {
        var html = await client.GetStringAsync(url);

        html.Should().Contain($"id=\"{inputId}\"");
        html.Should().Contain(encodedLabel);
    }
}
