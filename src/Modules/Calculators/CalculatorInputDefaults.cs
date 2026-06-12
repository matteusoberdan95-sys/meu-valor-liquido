namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorInputDefaults
{
    public static CalculatorInput ForSlug(string slug) => slug.ToLowerInvariant() switch
    {
        "rescisao-clt" => new(
            Amount: 1850m,
            SecondaryAmount: 30m,
            Months: 9,
            TerminationReason: TerminationReason.Resignation,
            CompletedNoticePeriod: false),
        "salario-liquido" => new(3000m, Dependents: 0),
        "ferias" => new(3000m, Dependents: 0),
        "decimo-terceiro" => new(3000m, Months: 9, Dependents: 0),
        "hora-extra" => new(25m, Hours: 10m, Rate: 50m),
        "juros-compostos" => new(1000m, Months: 12, Rate: 1m),
        "financiamento" => new(100_000m, Months: 360, Rate: 0.9m),
        _ => new(3000m, Months: 12, Rate: 50m)
    };
}
