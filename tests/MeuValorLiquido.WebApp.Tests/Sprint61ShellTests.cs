namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint61ShellTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint61ShellTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Layout_Should_Render_Four_Column_Footer_With_Newsletter()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("valora-footer-stitch-col--newsletter");
        html.Should().Contain("valora-footer-stitch-newsletter-form");
        html.Should().Contain("Institucional");
        html.Should().Contain("Design minimalista");
        html.Should().NotContain(">Entrar<");
    }

    [Fact]
    public async Task Layout_Should_Use_Meus_Painel_Instead_Of_Login()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("Meu painel");
        html.Should().NotContain("asp-page=\"/Login\"");
    }
}
