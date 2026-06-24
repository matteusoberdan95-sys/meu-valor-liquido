namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record WhatIfScenarioCardViewModel(
    string Id,
    string Title,
    string Teaser,
    string Url,
    string MaterialIcon,
    string CalculatorName);

public static class WhatIfScenarioCardBuilder
{
    public static IReadOnlyList<WhatIfScenarioCardViewModel> BuildForHub(
        string? hubId,
        ICalculatorCatalogService calculatorCatalog)
    {
        var scenarios = string.IsNullOrWhiteSpace(hubId)
            ? WhatIfScenarioCatalog.GetAll()
            : WhatIfScenarioCatalog.GetForHub(hubId);

        return Build(scenarios, calculatorCatalog);
    }

    public static IReadOnlyList<WhatIfScenarioCardViewModel> Build(
        IReadOnlyList<WhatIfScenarioDefinition> scenarios,
        ICalculatorCatalogService calculatorCatalog)
    {
        return scenarios
            .Select(scenario =>
            {
                var calculator = calculatorCatalog.GetBySlug(scenario.CalculatorSlug);
                return new WhatIfScenarioCardViewModel(
                    scenario.Id,
                    scenario.Title,
                    scenario.Teaser,
                    WhatIfScenarioLinkBuilder.BuildCalculatorUrl(scenario.CalculatorSlug, scenario.Input),
                    scenario.MaterialIcon,
                    calculator?.Name ?? scenario.CalculatorSlug);
            })
            .ToList();
    }
}

public static class WhatIfScenarioLinkBuilder
{
    public static string BuildCalculatorUrl(string slug, CalculatorInput input)
    {
        var token = CalculatorInputShareCodec.Encode(input);
        return $"/calculadoras/{slug}?r={Uri.EscapeDataString(token)}";
    }
}
