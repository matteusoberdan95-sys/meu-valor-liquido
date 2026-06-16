namespace MeuValorLiquido.WebApp.Pages;

public class PoliticaDePrivacidadeModel : PageModel
{
    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Política de Privacidade",
                "Como o Meu Valor Líquido trata dados pessoais, cookies, publicidade e simulações salvas no navegador.",
                "/politica-de-privacidade"));
    }
}
