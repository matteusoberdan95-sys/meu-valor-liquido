namespace MeuValorLiquido.WebApp.Pages.MeuPainel;

public class IndexModel : PageModel
{
    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Meu painel — simulações salvas",
                "Revise estimativas salvas no seu navegador. Sem cadastro: os dados ficam só no seu dispositivo.",
                "/meu-painel"));
    }
}
