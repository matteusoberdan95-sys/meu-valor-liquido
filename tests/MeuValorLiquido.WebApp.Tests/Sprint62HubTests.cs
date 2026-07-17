namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint62HubTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint62HubTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task CalculadorasHub_Should_Render_Clt_Pj_Banner_Without_Usage_Claims()
    {
        var html = await client.GetStringAsync("/calculadoras");

        html.Should().Contain("valora-stitch-calc-premium-banner");
        html.Should().Contain("Comparação detalhada");
        html.Should().Contain("Calculadora CLT vs PJ");
        html.Should().NotContain("Mais usada esta semana");
        html.Should().Contain("Comparar agora");
        html.Should().Contain("/calculadoras/pj-vs-clt");
        html.Should().Contain("Qual cálculo você precisa fazer hoje?");
    }

    [Fact]
    public async Task BlogHub_Should_Render_Tip_Card_And_Newsletter()
    {
        var html = await client.GetStringAsync("/blog");

        html.Should().Contain("valora-stitch-blog-tip-card");
        html.Should().Contain("Dica rápida");
        html.Should().Contain("valora-stitch-blog-newsletter");
        html.Should().Contain("Mantenha seu valor líquido em dia");
        html.Should().Contain("valora-footer-stitch-newsletter-form");
    }

    [Fact]
    public async Task FaqHub_Should_Render_Stitch_Cta_Copy()
    {
        var html = await client.GetStringAsync("/duvidas");

        html.Should().Contain("Como podemos ajudar com sua vida financeira hoje?");
        html.Should().Contain("Ainda com dúvidas?");
        html.Should().Contain("Entrar em contato");
        html.Should().Contain("relate uma informação que precisa de revisão");
        html.Should().NotContain("Não encontrou sua resposta?");
        html.Should().NotContain("especialistas financeiros");
    }
}
