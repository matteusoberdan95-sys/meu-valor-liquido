namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint91MathValidationWebTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint91MathValidationWebTests(WebApplicationFactory<Program> factory)
    {
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();
    }

    [Fact]
    public async Task Como_Calculamos_Should_Expose_Versioned_Tax_Table_Validity()
    {
        var html = await client.GetStringAsync("/como-calculamos");

        html.Should().Contain("Vigência das tabelas versionadas");
        html.Should().Contain("BrTaxTables2025");
        html.Should().Contain("CalculatorEdgeCaseCatalog");
        html.Should().Contain("8.157,41");
        html.Should().Contain("8.475,55");
    }
}
