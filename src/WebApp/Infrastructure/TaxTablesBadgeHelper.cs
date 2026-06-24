using System.Globalization;
using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.WebApp.Infrastructure;

public static class TaxTablesBadgeHelper
{
    private static readonly HashSet<string> TemplateC1InssIrrfSlugs = new(StringComparer.OrdinalIgnoreCase)
    {
        "salario-bruto-necessario",
        "proposta-salarial",
        "ferias",
        "decimo-terceiro",
        "hora-extra",
        "inss",
        "irrf",
        "simulador-mei",
        "custo-funcionario",
        "conversor-salario"
    };

    public static bool ShouldShowOnCalculator(string slug) =>
        CalculatorUiHelper.IsTemplateC1Slug(slug) && TemplateC1InssIrrfSlugs.Contains(slug);

    public static string FormatRevisionMonth() =>
        CultureInfo
            .GetCultureInfo("pt-BR")
            .DateTimeFormat.GetMonthName(CalculatorBenchmarkHelper.LastCalibrationDate.Month);

    public static string FormatBadgeLabel() =>
        $"INSS/IRRF {BrTaxTables2026.Year} · Revisado em {FormatRevisionMonth()}";
}
