namespace MeuValorLiquido.Calculators.Tests;

public sealed class WhatIfScenarioCatalogTests
{
    private readonly CalculatorApplicationService service = CalculatorTestFactory.CreateService();

    [Fact]
    public void Catalog_Should_Define_Three_Scenarios()
    {
        WhatIfScenarioCatalog.GetAll().Should().HaveCount(3);
        WhatIfScenarioCatalog.GetAll().Should().OnlyHaveUniqueItems(s => s.Id);
    }

    [Theory]
    [InlineData(WhatIfScenarioCatalog.PedirDemissao)]
    [InlineData(WhatIfScenarioCatalog.AceitarPj)]
    [InlineData(WhatIfScenarioCatalog.VenderFerias)]
    public void Each_Scenario_Should_Calculate_Successfully(string scenarioId)
    {
        var scenario = WhatIfScenarioCatalog.TryGet(scenarioId);
        scenario.Should().NotBeNull();

        var result = service.Calculate(scenario!.CalculatorSlug, scenario.Input);
        result.IsSuccess.Should().BeTrue($"cenário {scenarioId} deveria calcular sem erro");
        result.Value.EstimatedNetAmount.Amount.Should().NotBe(0m);
    }

    [Fact]
    public void Desligamento_Hub_Should_Expose_Pedir_Demissao()
    {
        var scenarios = WhatIfScenarioCatalog.GetForHub(ThematicHubIds.Desligamento);
        scenarios.Should().ContainSingle(s => s.Id == WhatIfScenarioCatalog.PedirDemissao);
    }

    [Fact]
    public void Virar_Pj_Hub_Should_Expose_Aceitar_Pj()
    {
        var scenarios = WhatIfScenarioCatalog.GetForHub(ThematicHubIds.VirarPj);
        scenarios.Should().ContainSingle(s => s.Id == WhatIfScenarioCatalog.AceitarPj);
    }
}
