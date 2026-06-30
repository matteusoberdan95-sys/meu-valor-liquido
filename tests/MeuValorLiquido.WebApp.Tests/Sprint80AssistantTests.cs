namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint80AssistantTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint80AssistantTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Assistant_Page_Should_Render_Educational_Chat()
    {
        var html = await client.GetStringAsync("/assistente");

        html.Should().Contain("Assistente Meu Valor Líquido");
        html.Should().Contain("valora-assistant-page");
        html.Should().Contain("data-assistant-chat");
        html.Should().Contain("Quanto desconta de INSS?");
        html.Should().Contain("/calculadoras/inss");
        html.Should().Contain("Conteúdo educativo");
        html.Should().Contain("BreadcrumbList");
    }

    [Fact]
    public async Task Home_Should_Show_Assistant_Launcher()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("data-assistant-launcher");
        html.Should().Contain("Quer tirar uma dúvida rápida?");
        html.Should().Contain("/assistente");
    }

    [Fact]
    public async Task Assistant_Page_Should_Not_Show_Global_Launcher()
    {
        var html = await client.GetStringAsync("/assistente");

        html.Should().NotContain("data-assistant-launcher");
    }

    [Fact]
    public async Task Sitemap_And_Site_Map_Should_Include_Assistant()
    {
        var sitemap = await client.GetStringAsync("/sitemap.xml");
        var siteMap = await client.GetStringAsync("/mapa-do-site");

        sitemap.Should().Contain("https://meuvalorliquido.com/assistente");
        sitemap.Should().Contain("<lastmod>2026-06-29</lastmod>");
        siteMap.Should().Contain("/assistente");
        siteMap.Should().Contain("Assistente educativo");
    }

    [Fact]
    public async Task Site_Script_Should_Handle_Assistant_Interactions()
    {
        var script = await client.GetStringAsync("/js/site.js");

        script.Should().Contain("data-assistant-launcher");
        script.Should().Contain("data-assistant-chat-form");
        script.Should().Contain("Calcular INSS");
        script.Should().Contain("Conteúdo educativo. Não substitui orientação profissional.");
    }
}
