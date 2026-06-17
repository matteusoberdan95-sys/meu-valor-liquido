namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint55WebTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint55WebTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Rescisao_Form_Should_Show_Advance_And_Salary_Supplement_Fields()
    {
        var html = await client.GetStringAsync("/calculadoras/rescisao-clt");

        html.Should().Contain("Input_ThirteenthAdvancePaid");
        html.Should().Contain("Input_SalaryAverageSupplement");
        html.Should().Contain("HE/comiss");
    }

    [Fact]
    public async Task Como_Calculamos_Should_Include_Rescisao_Section()
    {
        var html = await client.GetStringAsync("/como-calculamos");

        html.Should().Contain("Rescisão CLT");
        html.Should().Contain("Regra dos 15 dias");
        html.Should().Contain("Seguro-desemprego");
    }

    [Fact]
    public async Task Seguro_Desemprego_Faq_Should_Be_Published()
    {
        var html = await client.GetStringAsync("/duvidas/seguro-desemprego-quando-tem-direito");

        html.Should().Contain("linha informativa");
        html.Should().Contain("/calculadoras/rescisao-clt");
    }
}
