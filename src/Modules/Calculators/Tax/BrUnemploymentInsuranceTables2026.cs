namespace MeuValorLiquido.Modules.Calculators.Tax;

/// <summary>
/// Tabela de parcelas do seguro-desemprego vigente a partir de 11/01/2026 (MTE / Portaria MTP nº 1.905/2025).
/// </summary>
public static class BrUnemploymentInsuranceTables2026
{
    public const string SourceName = "Ministério do Trabalho e Emprego — tabela 2026";
    public const string SourceUrl =
        "https://www.gov.br/trabalho-e-emprego/pt-br/noticias-e-conteudo/2026/janeiro/mte-reajusta-valores-do-beneficio-seguro-desemprego";

    public const decimal FirstBracketLimit = 2222.17m;
    public const decimal SecondBracketLimit = 3703.99m;
    public const decimal SecondBracketBase = 1777.74m;
    public const decimal MaximumBenefit = 2518.65m;
    public const decimal MinimumBenefit = BrTaxTables2026.MinimumWage;

    public static decimal CalculateMonthlyBenefit(decimal averageSalary)
    {
        if (averageSalary <= 0m)
        {
            return MinimumBenefit;
        }

        var benefit = averageSalary <= FirstBracketLimit
            ? averageSalary * 0.80m
            : averageSalary <= SecondBracketLimit
                ? (averageSalary - FirstBracketLimit) * 0.50m + SecondBracketBase
                : MaximumBenefit;

        return decimal.Round(Math.Max(benefit, MinimumBenefit), 2, MidpointRounding.AwayFromZero);
    }

    public static int ResolveInstallmentCount(int monthsInLast36)
    {
        if (monthsInLast36 >= 24)
        {
            return 5;
        }

        if (monthsInLast36 >= 12)
        {
            return 4;
        }

        if (monthsInLast36 >= 6)
        {
            return 3;
        }

        return 0;
    }

    public static int RequiredQualifyingMonths(int previousRequests)
    {
        return previousRequests switch
        {
            0 => 12,
            1 => 9,
            _ => 6
        };
    }
}
