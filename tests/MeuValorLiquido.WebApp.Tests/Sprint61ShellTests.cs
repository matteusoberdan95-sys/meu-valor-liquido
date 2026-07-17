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
        html.Should().Contain("Metodologia transparente");
        html.Should().Contain("Fontes oficiais");
        html.Should().NotContain(">Entrar<");
    }

    [Fact]
    public async Task Layout_Should_Use_Meus_Painel_Instead_Of_Login()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("Meu painel");
        html.Should().NotContain("asp-page=\"/Login\"");
    }

    [Fact]
    public async Task Header_Should_Use_Compact_Navigation()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain(">Holerite</a>");
        html.Should().Contain("href=\"/assistente\">Chat</a>");
        html.Should().Contain("aria-label=\"Meu painel\"");
        html.Should().Contain("placeholder=\"Buscar...\"");
    }

    [Fact]
    public async Task Home_Should_Not_Render_Redundant_Breadcrumb()
    {
        var html = await client.GetStringAsync("/");

        html.Should().NotContain("Página Inicial");
        html.Should().NotContain("valora-stitch-home-breadcrumb");
    }
}
