namespace MeuValorLiquido.Modules.Calculators;

public sealed record CalculatorBenchmarkLineExpectation(
    string Label,
    decimal ExpectedAmount,
    decimal Tolerance = 0.02m);

public sealed record CalculatorBenchmarkScenario(
    string Slug,
    string Name,
    CalculatorInput Input,
    decimal ExpectedGrossAmount,
    decimal ExpectedNetAmount,
    decimal Tolerance,
    string SourceName,
    string SourceUrl,
    DateOnly CalibratedAt,
    IReadOnlyList<CalculatorBenchmarkLineExpectation> LineExpectations);

public static class CalculatorBenchmarkCatalog
{
    public const int MinimumScenariosPerPrioritySlug = 5;

    public const int MinimumTerminationBenchmarkScenarios = 15;

    private const string OfficialTaxSourceName =
        "Portaria Interministerial MPS/MF n. 13/2026 e Lei n. 15.270/2025";
    private const string OfficialTaxSourceUrl =
        "https://www.in.gov.br/en/web/dou/-/portaria-interministerial-mps/mf-n-13-de-9-de-janeiro-de-2026-680382603";
    private const string CltSourceName = "CLT e criterios internos documentados";
    private const string CltSourceUrl = "https://www.planalto.gov.br/ccivil_03/decreto-lei/del5452.htm";
    private static readonly DateOnly CalibrationDate = new(2026, 6, 17);

    public static readonly IReadOnlyList<string> PrioritySlugs =
    [
        "salario-liquido",
        "salario-bruto-necessario",
        "proposta-salarial",
        "ferias",
        "decimo-terceiro",
        "rescisao-clt",
        "inss",
        "irrf",
        "fgts",
        "hora-extra"
    ];

    public static IReadOnlyList<CalculatorBenchmarkScenario> All { get; } =
    [
        Scenario("salario-liquido", "salario-minimo-2026", new CalculatorInput(1621m), 1621.00m, 1499.42m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 121.58m)),
        Scenario("salario-liquido", "salario-3000-sem-dependentes", new CalculatorInput(3000m), 3000.00m, 2751.40m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 248.60m)),
        Scenario("salario-liquido", "salario-4000-com-vt-e-outros", new CalculatorInput(4000m, TransportDiscount: 240m, OtherDiscounts: 100m), 4000.00m, 3291.40m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 368.60m), Line("Vale-transporte", 240.00m), Line("Outros descontos", 100.00m)),
        Scenario("salario-liquido", "salario-5000-holerite-separado", new CalculatorInput(5000m, TransportDiscount: 200m, MealVoucherDiscount: 80m, HealthPlanDiscount: 120m, AlimonyPercent: 5m), 5000.00m, 3848.49m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 501.51m), Line("Vale-transporte", 200.00m), Line("Vale-refeição/alimentação", 80.00m), Line("Plano de saúde", 120.00m), Line("Pensão alimentícia", 250.00m)),
        Scenario("salario-liquido", "salario-6000-com-dependente-e-descontos", new CalculatorInput(6000m, Dependents: 1, TransportDiscount: 360m, OtherDiscounts: 150m), 6000.00m, 4626.18m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 641.51m), Line("IRRF", 222.31m)),
        Scenario("salario-liquido", "salario-9000-teto-inss", new CalculatorInput(9000m), 9000.00m, 6717.36m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 988.09m), Line("IRRF", 1294.55m)),

        Scenario("salario-bruto-necessario", "liquido-2000", new CalculatorInput(2000m), 2171.08m, 2000.00m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("salario-bruto-necessario", "liquido-3500-com-descontos", new CalculatorInput(3500m, TransportDiscount: 150m, MealVoucherDiscount: 50m, OtherDiscounts: 100m), 4191.59m, 3500.00m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("salario-bruto-necessario", "liquido-5000-com-dependente", new CalculatorInput(5000m, Dependents: 1, TransportDiscount: 300m), 6321.85m, 5000.00m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("salario-bruto-necessario", "liquido-7000-com-dependentes", new CalculatorInput(7000m, Dependents: 2, OtherDiscounts: 200m), 9521.88m, 7000.00m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("salario-bruto-necessario", "liquido-1200", new CalculatorInput(1200m), 1297.30m, 1200.00m, OfficialTaxSourceName, OfficialTaxSourceUrl),

        Scenario("proposta-salarial", "3000-para-3500", new CalculatorInput(3000m, SecondaryAmount: 3500m), 3500.00m, 3191.40m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("proposta-salarial", "4000-para-4800-com-vt", new CalculatorInput(4000m, SecondaryAmount: 4800m, TransportDiscount: 200m), 4800.00m, 4126.49m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("proposta-salarial", "6000-para-7000-com-dependente", new CalculatorInput(6000m, SecondaryAmount: 7000m, Dependents: 1, TransportDiscount: 300m, OtherDiscounts: 100m), 7000.00m, 5245.17m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("proposta-salarial", "9000-para-10000", new CalculatorInput(9000m, SecondaryAmount: 10000m), 10000.00m, 7442.36m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("proposta-salarial", "5000-para-4500", new CalculatorInput(5000m, SecondaryAmount: 4500m), 4500.00m, 4068.49m, OfficialTaxSourceName, OfficialTaxSourceUrl),

        Scenario("ferias", "integrais-3000", new CalculatorInput(3000m, Months: 12), 4000.00m, 3631.40m, CltSourceName, CltSourceUrl),
        Scenario("ferias", "proporcionais-6-avos-3000", new CalculatorInput(3000m, Months: 6), 2000.00m, 1844.32m, CltSourceName, CltSourceUrl),
        Scenario("ferias", "integrais-com-abono", new CalculatorInput(3000m, Months: 12, SellVacationAllowance: true), 5000.00m, 4498.49m, CltSourceName, CltSourceUrl),
        Scenario("ferias", "dobro-4000", new CalculatorInput(4000m, Months: 12, DoubleVacationPayment: true), 10666.67m, 7925.70m, CltSourceName, CltSourceUrl),
        Scenario("ferias", "reduzidas-20-dias", new CalculatorInput(2500m, Months: 12, VacationDayOption: VacationDayOption.Reduced20), 2222.22m, 2046.54m, CltSourceName, CltSourceUrl),

        Scenario("decimo-terceiro", "integral-3000", new CalculatorInput(3000m, Months: 12), 3000.00m, 2751.40m, CltSourceName, CltSourceUrl),
        Scenario("decimo-terceiro", "seis-avos-3000", new CalculatorInput(3000m, Months: 6), 1500.00m, 1387.50m, CltSourceName, CltSourceUrl),
        Scenario("decimo-terceiro", "integral-5000-com-dependente", new CalculatorInput(5000m, Months: 12, Dependents: 1), 5000.00m, 4498.49m, CltSourceName, CltSourceUrl),
        Scenario("decimo-terceiro", "integral-5000-com-adiantamento", new CalculatorInput(5000m, Months: 12, ThirteenthAdvancePaid: 2500m), 5000.00m, 1998.49m, CltSourceName, CltSourceUrl),
        Scenario("decimo-terceiro", "tres-avos-salario-minimo", new CalculatorInput(1621m, Months: 3), 405.25m, 374.86m, CltSourceName, CltSourceUrl),

        Scenario("rescisao-clt", "pedido-demissao-jan-out", new CalculatorInput(1850m, SecondaryAmount: 10m, Months: 9, AdmissionDate: new DateOnly(2026, 1, 10), TerminationDate: new DateOnly(2026, 10, 10), TerminationReason: TerminationReason.Resignation, NoticePeriod: NoticePeriodOption.NotFulfilledByEmployee), 3854.17m, 1853.86m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "demissao-sem-justa-causa-12-meses", new CalculatorInput(3000m, SecondaryAmount: 15m, Months: 12, TerminationReason: TerminationReason.DismissalWithoutCause), 12952.00m, 12590.90m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "acordo-484a-24-meses", new CalculatorInput(4000m, SecondaryAmount: 20m, Months: 24, CompleteYears: 2, FgtsBalance: 10000m, TerminationReason: TerminationReason.MutualAgreement), 16400.00m, 15815.72m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "justa-causa-com-ferias-vencidas", new CalculatorInput(3000m, SecondaryAmount: 10m, Months: 18, HasUnpaidVacation: true, TerminationReason: TerminationReason.DismissalForCause), 5000.00m, 4925.00m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "experiencia-antecipada", new CalculatorInput(2200m, SecondaryAmount: 15m, Months: 3, TerminationReason: TerminationReason.ProbationContractEarlyEnd), 4794.53m, 4670.78m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "aposentadoria-36-meses", new CalculatorInput(3500m, SecondaryAmount: 20m, Months: 36, CompleteYears: 3, TerminationReason: TerminationReason.Retirement), 10500.00m, 6505.72m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "falecimento-empregador", new CalculatorInput(2800m, SecondaryAmount: 12m, Months: 18, TerminationReason: TerminationReason.EmployerDeath), 10479.47m, 10167.79m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "contrato-prazo-determinado", new CalculatorInput(2500m, SecondaryAmount: 8m, Months: 8, TerminationReason: TerminationReason.FixedTermContractEnd), 4555.56m, 1879.88m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "experiencia-no-prazo", new CalculatorInput(2000m, SecondaryAmount: 10m, Months: 2, TerminationReason: TerminationReason.ProbationContractCompleted), 1444.44m, -75.00m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "pedido-demissao-com-aviso", new CalculatorInput(3200m, SecondaryAmount: 15m, Months: 14, TerminationReason: TerminationReason.Resignation, NoticePeriod: NoticePeriodOption.Worked), 5511.11m, 5118.51m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "demissao-24-meses-5000", new CalculatorInput(5000m, SecondaryAmount: 18m, Months: 24, CompleteYears: 2, TerminationReason: TerminationReason.DismissalWithoutCause), 24506.67m, 23756.56m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "demissao-com-adiantamento-13", new CalculatorInput(4000m, SecondaryAmount: 15m, Months: 10, TerminationMonth: 10, ThirteenthAdvancePaid: 2000m, TerminationReason: TerminationReason.DismissalWithoutCause), 15057.78m, 12613.50m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "demissao-com-media-he", new CalculatorInput(3000m, SalaryAverageSupplement: 600m, SecondaryAmount: 15m, Months: 12, TerminationReason: TerminationReason.DismissalWithoutCause), 15542.40m, 15084.12m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "rescisao-datas-regra-15-dias", new CalculatorInput(4000m, AdmissionDate: new DateOnly(2024, 3, 20), TerminationDate: new DateOnly(2026, 6, 5), TerminationReason: TerminationReason.Resignation), 3222.22m, -175.68m, CltSourceName, CltSourceUrl),
        Scenario("rescisao-clt", "acordo-484a-12-meses-saldo", new CalculatorInput(3000m, SecondaryAmount: 12m, Months: 12, FgtsBalance: 3500m, TerminationReason: TerminationReason.MutualAgreement), 10500.00m, 10161.40m, CltSourceName, CltSourceUrl),

        Scenario("inss", "salario-minimo", new CalculatorInput(1621m), 1621.00m, 1499.42m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 121.58m)),
        Scenario("inss", "faixa-3000", new CalculatorInput(3000m), 3000.00m, 2751.40m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 248.60m)),
        Scenario("inss", "faixa-4000", new CalculatorInput(4000m), 4000.00m, 3631.40m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 368.60m)),
        Scenario("inss", "teto-8475-55", new CalculatorInput(8475.55m), 8475.55m, 7487.46m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 988.09m)),
        Scenario("inss", "acima-do-teto", new CalculatorInput(20000m), 20000.00m, 19011.91m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("INSS", 988.09m)),

        Scenario("irrf", "base-4000", new CalculatorInput(4000m), 4000.00m, 4000.00m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("irrf", "base-5000", new CalculatorInput(5000m), 5000.00m, 5000.00m, OfficialTaxSourceName, OfficialTaxSourceUrl),
        Scenario("irrf", "base-6000", new CalculatorInput(6000m), 6000.00m, 5438.48m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("IRRF", 561.52m)),
        Scenario("irrf", "base-7350", new CalculatorInput(7350m), 7350.00m, 6237.48m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("IRRF", 1112.52m)),
        Scenario("irrf", "base-9000-com-2-dependentes", new CalculatorInput(9000m, Dependents: 2), 9000.00m, 7538.00m, OfficialTaxSourceName, OfficialTaxSourceUrl, Line("IRRF", 1462.00m)),

        Scenario("fgts", "12-meses-demissao", new CalculatorInput(3000m, Months: 12, TerminationReason: TerminationReason.DismissalWithoutCause), 2880.00m, 4032.00m, CltSourceName, CltSourceUrl),
        Scenario("fgts", "12-meses-acordo", new CalculatorInput(3000m, Months: 12, TerminationReason: TerminationReason.MutualAgreement), 2880.00m, 3456.00m, CltSourceName, CltSourceUrl),
        Scenario("fgts", "12-meses-pedido-demissao", new CalculatorInput(3000m, Months: 12, TerminationReason: TerminationReason.Resignation), 2880.00m, 2880.00m, CltSourceName, CltSourceUrl),
        Scenario("fgts", "saldo-informado-demissao", new CalculatorInput(5000m, Months: 24, FgtsBalance: 10000m, TerminationReason: TerminationReason.DismissalWithoutCause), 19600.00m, 27440.00m, CltSourceName, CltSourceUrl),
        Scenario("fgts", "seis-meses-salario-minimo", new CalculatorInput(1621m, Months: 6, TerminationReason: TerminationReason.DismissalWithoutCause), 778.08m, 1089.31m, CltSourceName, CltSourceUrl),

        Scenario("hora-extra", "hora-25-10h-50", new CalculatorInput(25m, Hours: 10m, Rate: 50m), 394.23m, 394.23m, CltSourceName, CltSourceUrl),
        Scenario("hora-extra", "mensal-3000-10h-50", new CalculatorInput(1m, SecondaryAmount: 3000m, Hours: 10m, Rate: 50m), 215.03m, 215.03m, CltSourceName, CltSourceUrl),
        Scenario("hora-extra", "domingo-3000-8h", new CalculatorInput(1m, SecondaryAmount: 3000m, Hours: 8m, OvertimeShiftType: OvertimeShiftType.SundayOrHoliday), 234.97m, 234.97m, CltSourceName, CltSourceUrl),
        Scenario("hora-extra", "noturna-4000-12h", new CalculatorInput(1m, SecondaryAmount: 4000m, Hours: 12m, Rate: 50m, OvertimeShiftType: OvertimeShiftType.NightWeekday), 394.41m, 394.41m, CltSourceName, CltSourceUrl),
        Scenario("hora-extra", "jornada-40h-cct-70", new CalculatorInput(1m, SecondaryAmount: 5000m, Hours: 5m, Rate: 70m, WeeklyWorkHours: 40), 225.96m, 225.96m, CltSourceName, CltSourceUrl)
    ];

    public static IReadOnlyList<CalculatorBenchmarkScenario> ForSlug(string slug) =>
        All.Where(scenario => scenario.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)).ToArray();

    private static CalculatorBenchmarkScenario Scenario(
        string slug,
        string name,
        CalculatorInput input,
        decimal expectedGrossAmount,
        decimal expectedNetAmount,
        string sourceName,
        string sourceUrl,
        params CalculatorBenchmarkLineExpectation[] lineExpectations)
    {
        return new CalculatorBenchmarkScenario(
            slug,
            name,
            input,
            expectedGrossAmount,
            expectedNetAmount,
            0.02m,
            sourceName,
            sourceUrl,
            CalibrationDate,
            lineExpectations);
    }

    private static CalculatorBenchmarkLineExpectation Line(string label, decimal expectedAmount) =>
        new(label, expectedAmount);
}
