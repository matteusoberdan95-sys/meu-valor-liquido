namespace MeuValorLiquido.Modules.Calculators;
public static class CalculatorInputDefaults
{
    public static CalculatorInput ForSlug(string slug) => slug.ToLowerInvariant() switch
    {
        "rescisao-clt" => new(
            Amount: 1850m,
            SecondaryAmount: 10m,
            Months: 9,
            AdmissionDate: new DateOnly(2026, 1, 10),
            TerminationDate: new DateOnly(2026, 10, 10),
            TerminationMonth: 10,
            AdmissionMonth: 1,
            TerminationReason: TerminationReason.Resignation,
            NoticePeriod: NoticePeriodOption.NotFulfilledByEmployee,
            CompletedNoticePeriod: false),
        "salario-liquido" => new(3000m, Dependents: 0),
        "salario-bruto-necessario" => new(3500m, Dependents: 0, TransportDiscount: 150m, SecondaryAmount: 50m, OtherDiscounts: 100m),
        "proposta-salarial" => new(4000m, SecondaryAmount: 4800m, Dependents: 0, TransportDiscount: 200m),
        "ferias" => new(3000m, Dependents: 0),
        "decimo-terceiro" => new(3000m, Months: 9, Dependents: 0),
        "hora-extra" => new(25m, Hours: 10m, Rate: 50m),
        "juros-compostos" => new(1000m, Months: 12, Rate: 1m),
        "financiamento" => new(100_000m, Months: 360, Rate: 0.9m),
        "fgts" => new(3000m, Months: 24, TerminationReason: TerminationReason.DismissalWithoutCause),
        "seguro-desemprego" => new(
            3000m,
            SecondaryAmount: 2900m,
            SalaryAverageSupplement: 2800m,
            Months: 24,
            MonthsWorkedInYear: 12,
            TerminationReason: TerminationReason.DismissalWithoutCause),
        "vale-transporte-hibrido" => new(4000m, SecondaryAmount: 16m, Months: 8, TransportDiscount: 240m),
        "simulador-mei" => new(5000m, MeiActivity: MeiActivityType.Services),
        "custo-funcionario" => new(4000m, SecondaryAmount: 500m),
        "multa-atraso" => new(1000m, SecondaryAmount: 45m, Rate: 1m, Hours: 2m),
        "conversor-salario" => new(3000m, SalaryBasis: SalaryConversionBasis.Monthly),
        "pj-vs-clt" => new(5000m, SecondaryAmount: 8000m, Dependents: 0, Rate: 6m),
        _ => new(3000m, Months: 12, Rate: 50m)
    };
}
