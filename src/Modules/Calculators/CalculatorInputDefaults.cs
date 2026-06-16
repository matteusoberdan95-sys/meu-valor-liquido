namespace MeuValorLiquido.Modules.Calculators;

using MeuValorLiquido.Modules.Calculators.Tax;

public static class CalculatorInputDefaults
{
    public static CalculatorInput ForSlug(string slug) => slug.ToLowerInvariant() switch
    {
        "rescisao-clt" => new(
            Amount: 1850m,
            SecondaryAmount: 12m,
            Months: 10,
            TerminationMonth: 2,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false),
        "salario-liquido" => new(3000m, Dependents: 0),
        "ferias" => new(3000m, Dependents: 0),
        "decimo-terceiro" => new(3000m, Months: 9, Dependents: 0),
        "hora-extra" => new(25m, Hours: 10m, Rate: 50m),
        "juros-compostos" => new(1000m, Months: 12, Rate: 1m),
        "financiamento" => new(100_000m, Months: 360, Rate: 0.9m),
        "fgts" => new(3000m, Months: 24, TerminationReason: TerminationReason.DismissalWithoutCause),
        "simulador-mei" => new(5000m, MeiActivity: MeiActivityType.Services),
        "custo-funcionario" => new(4000m, SecondaryAmount: 500m),
        "multa-atraso" => new(1000m, SecondaryAmount: 45m, Rate: 1m, Hours: 2m),
        "conversor-salario" => new(3000m, SalaryBasis: SalaryConversionBasis.Monthly),
        _ => new(3000m, Months: 12, Rate: 50m)
    };
}
