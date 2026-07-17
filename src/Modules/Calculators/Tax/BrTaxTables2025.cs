namespace MeuValorLiquido.Modules.Calculators.Tax;

/// <summary>
/// Tabelas históricas de 2025 (não sobrescrever: a vigência atual está em <see cref="BrTaxTables2026"/>).
/// INSS: Portaria Interministerial MPS/MF nº 6/2025.
/// IRRF: faixas da MP nº 1.294/2025 (a partir de maio/2025); sem a redução adicional da Lei 15.270/2025.
/// </summary>
public static class BrTaxTables2025
{
    public const int Year = 2025;

    public static readonly DateOnly ValidFrom = new(2025, 1, 1);

    public static readonly DateOnly ValidTo = new(2025, 12, 31);

    public const string SourceName = "Portaria Interministerial MPS/MF n. 6/2025 e MP n. 1.294/2025";

    public const string SourceUrl =
        "https://www.in.gov.br/web/dou/-/portaria-interministerial-mps/mf-n-6-de-10-de-janeiro-de-2025-606526848";

    public const decimal MinimumWage = 1518.00m;

    public const decimal DependentDeduction = 189.59m;

    public const decimal InssCeiling = 8157.41m;

    /// <summary>Teto de contribuição progressiva publicado para empregados em 2025.</summary>
    public const decimal InssMaximumContribution = 951.62m;

    public const decimal ProLaboreInssRate = 0.11m;

    public const decimal ProLaboreInssMaximumContribution = 897.31m;

    public static readonly InssBracket[] InssBrackets =
    [
        new(0m, 1518.00m, 0.075m),
        new(1518.01m, 2793.88m, 0.09m),
        new(2793.89m, 4190.83m, 0.12m),
        new(4190.84m, InssCeiling, 0.14m)
    ];

    /// <summary>Tabela progressiva mensal vigente a partir de maio/2025 (MP 1.294/2025).</summary>
    public static readonly IrrfBracket[] IrrfBrackets =
    [
        new(0m, 2428.80m, 0m, 0m),
        new(2428.81m, 2826.65m, 0.075m, 182.16m),
        new(2826.66m, 3751.05m, 0.15m, 394.16m),
        new(3751.06m, 4664.68m, 0.225m, 675.49m),
        new(4664.69m, decimal.MaxValue, 0.275m, 908.73m)
    ];

    public static decimal CalculateInss(decimal grossSalary)
    {
        if (grossSalary <= 0m)
        {
            return 0m;
        }

        var capped = Math.Min(grossSalary, InssCeiling);
        var total = 0m;
        foreach (var bracket in InssBrackets)
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

        return MoneyRounding.Round(Math.Min(total, InssMaximumContribution));
    }

    public static decimal CalculateIrrf(decimal taxableBasis, int dependents)
    {
        if (taxableBasis <= 0m)
        {
            return 0m;
        }

        var basis = Math.Max(0m, taxableBasis - dependents * DependentDeduction);
        foreach (var bracket in IrrfBrackets)
        {
            if (basis >= bracket.From && basis <= bracket.To)
            {
                return MoneyRounding.Round(Math.Max(0m, basis * bracket.Rate - bracket.Deduction));
            }
        }

        return 0m;
    }
}
