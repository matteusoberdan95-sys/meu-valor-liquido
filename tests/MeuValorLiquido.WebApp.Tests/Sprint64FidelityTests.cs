using MeuValorLiquido.WebApp.Infrastructure;

namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint64FidelityTests : IClassFixture<WebApplicationFactory<Program>>
{
    private static readonly string[] TemplateC1Slugs =
    [
        "salario-bruto-necessario",
        "proposta-salarial",
        "ferias",
        "decimo-terceiro",
        "hora-extra",
        "inss",
        "irrf",
        "juros-compostos",
        "financiamento",
        "fgts",
        "simulador-mei",
        "custo-funcionario",
        "multa-atraso",
        "conversor-salario"
    ];

    private readonly HttpClient client;

    public Sprint64FidelityTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task MeuPainel_Should_Render_Bento_Dashboard()
    {
        var html = await client.GetStringAsync("/meu-painel");

        html.Should().Contain("valora-stitch-panel--bento");
        html.Should().Contain("Olá!");
        html.Should().Contain("Bem-vindo ao seu painel financeiro");
        html.Should().Contain("valora-stitch-panel-profile");
        html.Should().Contain("Painel local");
        html.Should().Contain("Sem login");
        html.Should().Contain("Cálculos salvos");
        html.Should().Contain("Leituras sugeridas");
        html.Should().Contain("valora-stitch-panel-newsletter");
        html.Should().Contain("Fique por dentro");
        html.Should().Contain("data-local-panel-page");
        html.Should().NotContain("Assinante Premium");
    }

    [Theory]
    [MemberData(nameof(TemplateC1SlugsData))]
    public async Task TemplateC1_Calculator_Should_Render_Stitch_Detail_Layout(string slug)
    {
        var html = await client.GetStringAsync($"/calculadoras/{slug}");

        html.Should().Contain("valora-stitch-calc-detail");
        html.Should().Contain("valora-stitch-calc-detail-grid");
        html.Should().Contain("valora-stitch-calc-detail-hero");
        html.Should().Contain("Calcular agora");
        html.Should().Contain(CalculatorUiHelper.GetStitchDetailModifierClass(slug).Trim());
    }

    [Fact]
    public async Task NotFound_Page_Should_Render_Stitch_Error_Layout()
    {
        using var response = await client.GetAsync("/rota-inexistente-teste-404");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var html = await response.Content.ReadAsStringAsync();
        html.Should().Contain("valora-stitch-error");
        html.Should().Contain("saiu da rota");
    }

    public static IEnumerable<object[]> TemplateC1SlugsData() =>
        TemplateC1Slugs.Select(slug => new object[] { slug });
}

public sealed class CalculatorUiHelperTemplateC1Tests
{
    [Fact]
    public void TemplateC1Slugs_Should_Count_Fourteen()
    {
        var slugs = new[]
        {
            "salario-bruto-necessario", "proposta-salarial", "ferias", "decimo-terceiro",
            "hora-extra", "inss", "irrf", "juros-compostos", "financiamento", "fgts",
            "simulador-mei", "custo-funcionario", "multa-atraso", "conversor-salario"
        };

        slugs.Should().OnlyContain(s => CalculatorUiHelper.IsTemplateC1Slug(s));
        slugs.Should().HaveCount(14);
    }
}
