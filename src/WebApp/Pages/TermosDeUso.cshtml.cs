namespace MeuValorLiquido.WebApp.Pages;

public class TermosDeUsoModel : PageModel
{
    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Termos de Uso",
                "Condições de uso das calculadoras e conteúdos educativos do Meu Valor Líquido.",
                "/termos-de-uso"));
    }
}
