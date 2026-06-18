namespace MeuValorLiquido.WebApp.Pages;

public class PoliticaDeCookiesModel : PageModel
{
    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Política de Cookies",
                "Tipos de cookies usados no Meu Valor Líquido, finalidades, parceiros e como gerenciar seu consentimento (LGPD).",
                "/politica-de-cookies"));
    }
}
