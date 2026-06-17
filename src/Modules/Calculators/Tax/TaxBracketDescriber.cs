using System.Globalization;

namespace MeuValorLiquido.Modules.Calculators.Tax;

public static class TaxBracketDescriber
{
    private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");

    public static string DescribeInss(decimal grossSalary)
    {
        if (grossSalary <= 0m)
        {
            return "Sem incidência";
        }

        if (grossSalary > BrTaxTables2026.InssCeiling)
        {
            return $"Teto previdenciário ({BrTaxTables2026.InssCeiling.ToString("C", PtBr)})";
        }

        var capped = grossSalary;
        InssBracket? bracket = null;
        foreach (var candidate in BrTaxTables2026.InssBrackets)
        {
            if (capped >= candidate.From)
            {
                bracket = candidate;
            }
        }

        return bracket is null
            ? "Isento"
            : $"Até {bracket.Value.To.ToString("C", PtBr)} — {bracket.Value.Rate.ToString("P1", PtBr)} no patamar";
    }

    public static string DescribeIrrf(decimal taxableBasis, int dependents)
    {
        var basis = Math.Max(0m, taxableBasis - dependents * BrTaxTables2026.DependentDeduction);
        if (basis <= 0m)
        {
            return "Base zerada após dependentes";
        }

        foreach (var bracket in BrTaxTables2026.IrrfBrackets)
        {
            if (basis >= bracket.From && basis <= bracket.To)
            {
                if (bracket.Rate == 0m)
                {
                    return $"Isento (base até {bracket.To.ToString("C", PtBr)})";
                }

                var upper = bracket.To == decimal.MaxValue
                    ? "acima"
                    : $"até {bracket.To.ToString("C", PtBr)}";
                return $"Faixa {upper} — {bracket.Rate.ToString("P1", PtBr)}";
            }
        }

        return "Fora das faixas da tabela";
    }
}
