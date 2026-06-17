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
    [InlineData("/calculadoras/salario-bruto-necessario", "Input_MealVoucherDiscount", "Vale-refei&#xE7;&#xE3;o/alimenta&#xE7;&#xE3;o")]
    [InlineData("/calculadoras/proposta-salarial", "Input_Amount", "Sal&#xE1;rio bruto atual")]
    [InlineData("/calculadoras/proposta-salarial", "Input_SecondaryAmount", "Sal&#xE1;rio bruto proposto")]
    public async Task Calculator_Form_Should_Render_Primary_Fields(string url, string inputId, string encodedLabel)
    {
        var html = await GetPageHtml(url);

        html.Should().Contain($"id=\"{inputId}\"");
        html.Should().Contain(encodedLabel);
    }

    [Theory]
    [InlineData("/calculadoras/ferias", "Input_VacationDayOption_0")]
    [InlineData("/calculadoras/rescisao-clt", "Input_TerminationReason_1")]
    [InlineData("/calculadoras/hora-extra", "Input_OvertimeShiftType_0")]
    [InlineData("/calculadoras/financiamento", "Input_FinancingAmortization_0")]
    [InlineData("/calculadoras/fgts", "Input_TerminationReason_fgts_0")]
    [InlineData("/calculadoras/simulador-mei", "Input_MeiActivity_1")]
    [InlineData("/calculadoras/conversor-salario", "Input_SalaryBasis_0")]
    public async Task Calculator_Radio_Groups_Should_Render_Default_Checked_Option(string url, string inputId)
    {
        var html = await GetPageHtml(url);

        html.Should().Contain($"id=\"{inputId}\"");
        var inputStart = html.IndexOf($"id=\"{inputId}\"", StringComparison.Ordinal);
        var inputMarkup = html.Substring(inputStart, Math.Min(300, html.Length - inputStart));
        inputMarkup.Should().Contain("checked=\"checked\"");
    }

    private async Task<string> GetPageHtml(string url)
    {
        var response = await client.GetAsync(url);
        var html = await response.Content.ReadAsStringAsync();

        response.IsSuccessStatusCode.Should().BeTrue(html);
        return html;
    }
}
