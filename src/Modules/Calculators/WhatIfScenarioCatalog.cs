namespace MeuValorLiquido.Modules.Calculators;

public sealed record WhatIfScenarioDefinition(
    string Id,
    string Title,
    string Teaser,
    string CalculatorSlug,
    CalculatorInput Input,
    string MaterialIcon,
    IReadOnlyList<string> HubIds);

/// <summary>Cenários pré-montados para o simulador “E se…” (Sprint 75).</summary>
public static class WhatIfScenarioCatalog
{
    public const string PedirDemissao = "pedir-demissao";
    public const string AceitarPj = "aceitar-pj";
    public const string VenderFerias = "vender-ferias";

    private static readonly IReadOnlyList<WhatIfScenarioDefinition> All =
    [
        new(
            PedirDemissao,
            "E se eu pedir demissão?",
            "Estime verbas rescisórias, descontos de aviso e o que não recebe (FGTS, seguro-desemprego).",
            "rescisao-clt",
            new CalculatorInput(
                Amount: 4000m,
                SecondaryAmount: 15m,
                Months: 36,
                CompleteYears: 3,
                AdmissionDate: new DateOnly(2023, 1, 15),
                TerminationDate: new DateOnly(2026, 1, 15),
                TerminationMonth: 1,
                AdmissionMonth: 1,
                TerminationReason: TerminationReason.Resignation,
                NoticePeriod: NoticePeriodOption.Worked,
                CompletedNoticePeriod: true),
            "logout",
            [ThematicHubIds.Desligamento]),

        new(
            AceitarPj,
            "E se eu aceitar PJ?",
            "Compare líquido CLT com faturamento PJ, Simples 6% e pró-labore de 28% no mesmo cenário.",
            "pj-vs-clt",
            new CalculatorInput(
                Amount: 4500m,
                SecondaryAmount: 7500m,
                Dependents: 0,
                TransportDiscount: 180m,
                Rate: 6m,
                ProLaborePercent: 28m),
            "compare_arrows",
            [ThematicHubIds.VirarPj]),

        new(
            VenderFerias,
            "E se eu vender 1/3 das férias?",
            "Veja quanto entra no bolso com abono pecuniário (venda de até 10 dias) e o terço constitucional.",
            "ferias",
            new CalculatorInput(
                Amount: 4000m,
                Months: 12,
                Dependents: 0,
                SellVacationAllowance: true),
            "beach_access",
            [ThematicHubIds.NegociarSalario])
    ];

    private static readonly Dictionary<string, WhatIfScenarioDefinition> ById =
        All.ToDictionary(s => s.Id, StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyList<WhatIfScenarioDefinition> GetAll() => All;

    public static WhatIfScenarioDefinition? TryGet(string? id) =>
        string.IsNullOrWhiteSpace(id) || !ById.TryGetValue(id, out var scenario) ? null : scenario;

    public static IReadOnlyList<WhatIfScenarioDefinition> GetForHub(string? hubId)
    {
        if (string.IsNullOrWhiteSpace(hubId))
        {
            return All;
        }

        return All
            .Where(scenario => scenario.HubIds.Any(h => h.Equals(hubId, StringComparison.OrdinalIgnoreCase)))
            .ToList();
    }
}

/// <summary>Ids de hubs temáticos referenciados pelos cenários (espelha WebApp sem acoplar).</summary>
public static class ThematicHubIds
{
    public const string Desligamento = "desligamento";
    public const string NegociarSalario = "negociar-salario";
    public const string VirarPj = "virar-pj";
}
