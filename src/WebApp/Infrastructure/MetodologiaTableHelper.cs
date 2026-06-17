using System.Globalization;
using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.WebApp.Infrastructure;

public static class MetodologiaTableHelper
{
    private static readonly CultureInfo PtBr = CultureInfo.GetCultureInfo("pt-BR");

    public static string FormatCurrency(decimal value) => value.ToString("N2", PtBr);

    public static string FormatInssSalaryRange(InssBracket bracket, int index)
    {
        if (index == 0)
        {
            return $"Até {FormatCurrency(bracket.To)}";
        }

        if (bracket.To >= BrTaxTables2026.InssCeiling - 0.01m)
        {
            return $"{FormatCurrency(bracket.From)} a {FormatCurrency(BrTaxTables2026.InssCeiling)} (teto)";
        }

        return $"{FormatCurrency(bracket.From)} a {FormatCurrency(bracket.To)}";
    }

    public static string FormatIrrfBaseRange(IrrfBracket bracket)
    {
        if (bracket.Rate == 0m)
        {
            return $"Até {FormatCurrency(bracket.To)}";
        }

        if (bracket.To == decimal.MaxValue)
        {
            return $"Acima de {FormatCurrency(bracket.From)}";
        }

        return $"{FormatCurrency(bracket.From)} a {FormatCurrency(bracket.To)}";
    }

    public static string FormatRate(decimal rate) =>
        rate == 0m ? "Isento" : $"{rate * 100m:0.#}%".Replace(".", ",");
}
