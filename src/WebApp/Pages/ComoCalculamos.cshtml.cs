namespace MeuValorLiquido.WebApp.Pages;

public class ComoCalculamosModel : PageModel
{
    public int TaxYear => BrTaxTables2026.Year;

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
