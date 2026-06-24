namespace MeuValorLiquido.WebApp.Tests;

public sealed class Sprint75WhatIfScenarioTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient client;

    public Sprint75WhatIfScenarioTests(WebApplicationFactory<Program> factory) =>
        client = factory.WithWebHostBuilder(builder => builder.UseEnvironment("Testing")).CreateClient();

    [Fact]
    public async Task Home_Should_Render_WhatIf_Scenarios()
    {
        var html = await client.GetStringAsync("/");

        html.Should().Contain("valora-stitch-whatif");
        html.Should().Contain("E se");
        html.Should().Contain("E se eu pedir demiss");
        html.Should().Contain("E se eu aceitar PJ?");
        html.Should().Contain("E se eu vender 1/3 das f");
        html.Should().Contain("/calculadoras/rescisao-clt?r=");
        html.Should().Contain("/calculadoras/pj-vs-clt?r=");
        html.Should().Contain("/calculadoras/ferias?r=");
    }

    [Fact]
    public async Task Desligamento_Hub_Should_Render_Pedir_Demissao_Scenario()
    {
        var html = await client.GetStringAsync("/desligamento");

        html.Should().Contain("valora-stitch-whatif");
        html.Should().Contain("E se eu pedir demiss");
        html.Should().NotContain("E se eu aceitar PJ?");
    }

    [Fact]
    public async Task Virar_Pj_Hub_Should_Render_Aceitar_Pj_Scenario()
    {
        var html = await client.GetStringAsync("/virar-pj");

        html.Should().Contain("E se eu aceitar PJ?");
        html.Should().NotContain("E se eu pedir demissão?");
    }

    [Fact]
    public async Task WhatIf_Link_Should_Open_Calculator_With_Result()
    {
        var scenario = WhatIfScenarioCatalog.TryGet(WhatIfScenarioCatalog.VenderFerias)!;
        var url = WhatIfScenarioLinkBuilder.BuildCalculatorUrl(scenario.CalculatorSlug, scenario.Input);
        var html = await client.GetStringAsync(url);

        html.Should().Contain("Abono pecuni");
        html.Should().Contain("Compartilhar estimativa");
    }
}
