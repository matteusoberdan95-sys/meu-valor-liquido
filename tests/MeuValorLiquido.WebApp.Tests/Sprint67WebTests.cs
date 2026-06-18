namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint67WebTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint67WebTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Seguro_Desemprego_Page_Should_Render_Form_And_Faq()
    {
        var html = await client.GetStringAsync("/calculadoras/seguro-desemprego");

        html.Should().Contain("Seguro-desemprego");
        html.Should().Contain("Input_Amount");
        html.Should().Contain("Input_TerminationReason");
        html.Should().Contain("tabela do MTE");
    }

    [Fact]
    public async Task Desligamento_Hub_Should_Link_Seguro_Desemprego_Calculator()
    {
        var html = await client.GetStringAsync("/desligamento");

        html.Should().Contain("/calculadoras/seguro-desemprego");
    }

    [Fact]
    public async Task Seguro_Desemprego_Blog_Article_Should_Link_Dedicated_Calculator()
    {
        var html = await client.GetStringAsync("/blog/seguro-desemprego-quem-tem-direito");

        html.Should().Contain("/calculadoras/seguro-desemprego");
    }
}
