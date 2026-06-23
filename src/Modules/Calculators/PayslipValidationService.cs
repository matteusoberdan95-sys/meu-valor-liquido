namespace MeuValorLiquido.Modules.Calculators;

public sealed record PayslipValidationInput(
    decimal GrossSalary,
    int Dependents,
    decimal ReportedInss,
    decimal ReportedIrrf,
    decimal TransportDiscount = 0m,
    decimal MealVoucherDiscount = 0m,
    decimal HealthPlanDiscount = 0m,
    decimal AlimonyAmount = 0m,
    decimal AlimonyPercent = 0m,
    decimal OtherDiscounts = 0m,
    decimal? ReportedNet = null);

public enum PayslipValidationStatus
{
    Match,
    Mismatch
}

public sealed record PayslipValidationCheck(
    string Key,
    string Label,
    decimal Expected,
    decimal Reported,
    decimal Difference,
    PayslipValidationStatus Status,
    string Hint);

public sealed record PayslipValidationResult(
    NetSalaryBreakdown ExpectedBreakdown,
    IReadOnlyList<PayslipValidationCheck> Checks,
    bool AllMatched,
    string Summary);

public interface IPayslipValidationService
{
    PayslipValidationResult Validate(PayslipValidationInput input);
}

public sealed class PayslipValidationService : IPayslipValidationService
{
    public const decimal Tolerance = 1.00m;

    private readonly NetSalaryCalculator netSalaryCalculator;

    public PayslipValidationService(NetSalaryCalculator netSalaryCalculator)
    {
        this.netSalaryCalculator = netSalaryCalculator;
    }

    public PayslipValidationResult Validate(PayslipValidationInput input)
    {
        var discounts = new HoleriteDiscountInput(
            input.TransportDiscount,
            input.MealVoucherDiscount,
            input.HealthPlanDiscount,
            input.AlimonyAmount,
            input.AlimonyPercent,
            input.OtherDiscounts);

        var expected = netSalaryCalculator.Calculate(input.GrossSalary, input.Dependents, discounts);
        var checks = new List<PayslipValidationCheck>
        {
            BuildCheck(
                "inss",
                "INSS",
                expected.Inss,
                input.ReportedInss,
                "Confira se o RH aplicou a tabela progressiva de INSS 2026 — não é alíquota única sobre o bruto inteiro."),
            BuildCheck(
                "irrf",
                "IRRF",
                expected.Irrf,
                input.ReportedIrrf,
                "Verifique dependentes cadastrados, base após INSS e a isenção/redução de IRRF vigente em 2026.")
        };

        if (input.ReportedNet is decimal reportedNet)
        {
            checks.Add(BuildCheck(
                "net",
                "Salário líquido",
                expected.Net,
                reportedNet,
                "Diferenças no líquido costumam vir de consignado, sindicato, plano ou arredondamentos não informados."));
        }

        var allMatched = checks.All(check => check.Status == PayslipValidationStatus.Match);
        var summary = allMatched
            ? $"INSS e IRRF conferem com as tabelas de {BrTaxTables2026.Year} (tolerância de {Money.From(Tolerance)})."
            : BuildMismatchSummary(checks);

        return new PayslipValidationResult(expected, checks, allMatched, summary);
    }

    private static PayslipValidationCheck BuildCheck(
        string key,
        string label,
        decimal expected,
        decimal reported,
        string mismatchHint)
    {
        var difference = reported - expected;
        var status = Math.Abs(difference) <= Tolerance
            ? PayslipValidationStatus.Match
            : PayslipValidationStatus.Mismatch;

        var hint = status == PayslipValidationStatus.Match
            ? $"Valor dentro da tolerância de {Money.From(Tolerance)} em relação à simulação."
            : mismatchHint;

        return new PayslipValidationCheck(key, label, expected, reported, difference, status, hint);
    }

    private static string BuildMismatchSummary(IReadOnlyList<PayslipValidationCheck> checks)
    {
        var mismatches = checks
            .Where(check => check.Status == PayslipValidationStatus.Mismatch)
            .Select(check => check.Label)
            .ToList();

        return mismatches.Count switch
        {
            0 => "Revise os valores informados.",
            1 => $"Divergência em {mismatches[0]}. Compare com o holerite e, se necessário, solicite esclarecimento ao RH.",
            _ => $"Divergências em {string.Join(", ", mismatches)}. Use as dicas abaixo antes de questionar o RH."
        };
    }
}
