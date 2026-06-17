using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.WebApp.Pages;

public class ComoCalculamosModel : PageModel
{
    public int TaxYear => BrTaxTables2026.Year;

    public decimal MinimumWage => BrTaxTables2026.MinimumWage;

    public decimal DependentDeduction => BrTaxTables2026.DependentDeduction;

    public InssBracket[] InssBrackets => BrTaxTables2026.InssBrackets;

    public IrrfBracket[] IrrfBrackets => BrTaxTables2026.IrrfBrackets;

    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Como calculamos — metodologia e fontes",
                "Entenda as tabelas INSS/IRRF, premissas das calculadoras e limitações dos resultados estimados.",
                "/como-calculamos"));
    }
}
