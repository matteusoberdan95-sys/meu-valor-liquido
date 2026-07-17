namespace MeuValorLiquido.Modules.Calculators;

/// <summary>
/// Cenários de borda da Sprint 91: entradas inválidas devem falhar na validação,
/// sem inventar resultados monetários.
/// </summary>
public sealed record CalculatorEdgeCaseScenario(
    string Slug,
    string Name,
    CalculatorInput Input,
    string ExpectedErrorCode,
    string ExpectedMessageContains);

public static class CalculatorEdgeCaseCatalog
{
    public static IReadOnlyList<CalculatorEdgeCaseScenario> All { get; } =
    [
        new("salario-liquido", "valor-zero", new CalculatorInput(0m), "Calculators.InvalidInput", "maior que zero"),
        new("salario-liquido", "valor-negativo", new CalculatorInput(-100m), "Calculators.InvalidInput", "maior que zero"),
        new("salario-liquido", "desconto-negativo", new CalculatorInput(3000m, TransportDiscount: -10m), "Calculators.InvalidInput", "não pode ser negativo"),
        new("salario-liquido", "dependentes-negativos", new CalculatorInput(3000m, Dependents: -1), "Calculators.InvalidInput", "dependentes"),
        new("salario-bruto-necessario", "liquido-zero", new CalculatorInput(0m), "Calculators.InvalidInput", "maior que zero"),
        new("proposta-salarial", "proposta-sem-secundario", new CalculatorInput(4000m, SecondaryAmount: 0m), "Calculators.InvalidInput", "salário bruto proposto"),
        new("ferias", "meses-invalidos", new CalculatorInput(3000m, Months: 0), "Calculators.InvalidInput", "meses"),
        new("decimo-terceiro", "taxa-invalida", new CalculatorInput(3000m, Months: 12, Rate: -1m), "Calculators.InvalidInput", "taxa"),
        new("rescisao-clt", "datas-invertidas", new CalculatorInput(
            3000m,
            AdmissionDate: new DateOnly(2026, 6, 1),
            TerminationDate: new DateOnly(2026, 1, 1),
            TerminationReason: TerminationReason.Resignation), "Calculators.InvalidInput", "afastamento"),
        new("inss", "campo-principal-invalido", new CalculatorInput(-1m), "Calculators.InvalidInput", "maior que zero"),
        new("irrf", "base-zero", new CalculatorInput(0m), "Calculators.InvalidInput", "maior que zero"),
        new("fgts", "meses-acima-do-limite", new CalculatorInput(3000m, Months: 601), "Calculators.InvalidInput", "meses"),
        new("hora-extra", "jornada-acima-de-44h", new CalculatorInput(1m, SecondaryAmount: 3000m, Hours: 10m, WeeklyWorkHours: 45), "Calculators.InvalidInput", "jornada")
    ];
}
