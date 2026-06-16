namespace MeuValorLiquido.Modules.Calculators.Tax;
/// <summary>
/// INSS e IRRF na rescisão: cada verba salarial é calculada separadamente.
/// Férias (vencidas/proporcionais + 1/3) e aviso prévio indenizado são isentos (Instrução Normativa RFB).
/// </summary>
public sealed record TerminationTaxBreakdown(
    decimal InssOnSalaryBalance,
    decimal InssOnThirteenth,
    decimal IrrfOnSalaryBalance,
    decimal IrrfOnThirteenth)
{
    public decimal TotalInss => InssOnSalaryBalance + InssOnThirteenth;

    public decimal TotalIrrf => IrrfOnSalaryBalance + IrrfOnThirteenth;

    public decimal Total => TotalInss + TotalIrrf;
}

public interface ITerminationTaxCalculator
{
    TerminationTaxBreakdown Calculate(decimal salaryBalance, decimal thirteenth, int dependents);
}

public sealed class TerminationTaxCalculator : ITerminationTaxCalculator
{
    private readonly IInssCalculator inssCalculator;
    private readonly IIrrfCalculator irrfCalculator;

    public TerminationTaxCalculator(IInssCalculator inssCalculator, IIrrfCalculator irrfCalculator)
    {
        this.inssCalculator = inssCalculator;
        this.irrfCalculator = irrfCalculator;
    }

    public TerminationTaxBreakdown Calculate(decimal salaryBalance, decimal thirteenth, int dependents)
    {
        var inssSalary = inssCalculator.Calculate(salaryBalance);
        var inssThirteenth = inssCalculator.Calculate(thirteenth);
        var irrfSalary = irrfCalculator.Calculate(salaryBalance - inssSalary, dependents);
        var irrfThirteenth = irrfCalculator.Calculate(thirteenth - inssThirteenth, dependents);

        return new TerminationTaxBreakdown(inssSalary, inssThirteenth, irrfSalary, irrfThirteenth);
    }
}
