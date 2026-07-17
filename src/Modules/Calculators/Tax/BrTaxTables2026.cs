namespace MeuValorLiquido.Modules.Calculators.Tax;
/// <summary>
/// Tabelas vigentes a partir de janeiro/2026 (Portaria Interministerial MPS/MF nº 13/2026).
/// IRRF com redução adicional conforme Lei nº 15.270/2025.
/// Mantém <see cref="BrTaxTables2025"/> intacta para regressão anual — não sobrescrever anos anteriores.
/// </summary>
public static class BrTaxTables2026
{
    public const int Year = 2026;

    public static readonly DateOnly ValidFrom = new(2026, 1, 1);

    /// <summary>Vigência aberta até nova portaria; o catálogo versionado preserva 2025 em paralelo.</summary>
    public static readonly DateOnly? ValidTo = null;

    public const string SourceName =
        "Portaria Interministerial MPS/MF n. 13/2026 e Lei n. 15.270/2025";

    public const string SourceUrl =
        "https://www.in.gov.br/en/web/dou/-/portaria-interministerial-mps/mf-n-13-de-9-de-janeiro-de-2026-680382603";

    public const decimal MinimumWage = 1621.00m;
    public const decimal DependentDeduction = 189.59m;
    public const decimal InssCeiling = 8475.55m;
    public const decimal InssMaximumContribution = 988.09m;
    public const decimal ProLaboreInssRate = 0.11m;
    public const decimal ProLaboreInssMaximumContribution = 932.31m;

    public static readonly InssBracket[] InssBrackets =
    [
        new(0m, 1621.00m, 0.075m),
        new(1621.01m, 2902.84m, 0.09m),
        new(2902.85m, 4354.27m, 0.12m),
        new(4354.28m, InssCeiling, 0.14m)
    ];

    public static readonly IrrfBracket[] IrrfBrackets =
    [
        new(0m, 2428.80m, 0m, 0m),
        new(2428.81m, 2826.65m, 0.075m, 182.16m),
        new(2826.66m, 3751.05m, 0.15m, 394.16m),
        new(3751.06m, 4664.68m, 0.225m, 675.49m),
        new(4664.69m, decimal.MaxValue, 0.275m, 908.73m)
    ];

    /// <summary>
    /// Redução adicional do IRRF (Lei 15.270/2025).
    /// Até R$ 5.000 de base tributável: isenção total. Entre R$ 5.000,01 e R$ 7.350: redução decrescente.
    /// </summary>
    public static decimal CalculateIrrfReduction(decimal taxableIncome, decimal grossIrrf)
    {
        if (taxableIncome <= 5000m)
        {
            return grossIrrf;
        }

        if (taxableIncome <= 7350m)
        {
            return Math.Max(0m, 978.62m - 0.133145m * taxableIncome);
        }

        return 0m;
    }
}

public readonly record struct InssBracket(decimal From, decimal To, decimal Rate);

public readonly record struct IrrfBracket(decimal From, decimal To, decimal Rate, decimal Deduction);

public interface IInssCalculator
{
    decimal Calculate(decimal grossSalary);
}

public interface IIrrfCalculator
{
    decimal Calculate(decimal taxableBasis, int dependents);
}

public sealed class InssCalculator : IInssCalculator
{
    public decimal Calculate(decimal grossSalary)
    {
        if (grossSalary <= 0m)
        {
            return 0m;
        }

        var capped = Math.Min(grossSalary, BrTaxTables2026.InssCeiling);
        var total = 0m;
        foreach (var bracket in BrTaxTables2026.InssBrackets)
        {
            if (capped <= bracket.From)
            {
                break;
            }

            var taxable = Math.Min(capped, bracket.To) - bracket.From;
            if (taxable > 0)
            {
                total += taxable * bracket.Rate;
            }
        }

        return MoneyRounding.Round(Math.Min(total, BrTaxTables2026.InssMaximumContribution));
    }
}

public sealed class IrrfCalculator : IIrrfCalculator
{
    public decimal Calculate(decimal taxableBasis, int dependents)
    {
        if (taxableBasis <= 0m)
        {
            return 0m;
        }

        var basis = Math.Max(0m, taxableBasis - dependents * BrTaxTables2026.DependentDeduction);
        var grossIrrf = 0m;
        foreach (var bracket in BrTaxTables2026.IrrfBrackets)
        {
            if (basis >= bracket.From && basis <= bracket.To)
            {
                grossIrrf = Math.Max(0m, basis * bracket.Rate - bracket.Deduction);
                break;
            }
        }

        var reduction = BrTaxTables2026.CalculateIrrfReduction(basis, grossIrrf);
        return MoneyRounding.Round(Math.Max(0m, grossIrrf - reduction));
    }
}

/// <summary>INSS sobre pró-labore de sócio: 11% fixo até o teto previdenciário (não usa tabela progressiva CLT).</summary>
public interface IProLaboreInssCalculator
{
    decimal Calculate(decimal proLaboreGross);
}

public sealed class ProLaboreInssCalculator : IProLaboreInssCalculator
{
    public decimal Calculate(decimal proLaboreGross)
    {
        if (proLaboreGross <= 0m)
        {
            return 0m;
        }

        var capped = Math.Min(proLaboreGross, BrTaxTables2026.InssCeiling);
        var contribution = capped * BrTaxTables2026.ProLaboreInssRate;
        return MoneyRounding.Round(
            Math.Min(contribution, BrTaxTables2026.ProLaboreInssMaximumContribution));
    }
}
