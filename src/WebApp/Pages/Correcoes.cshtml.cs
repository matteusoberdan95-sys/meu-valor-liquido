namespace MeuValorLiquido.WebApp.Pages;

public sealed class CorrecoesModel : PageModel
{
    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Correções editoriais",
                "Processo de recebimento, análise e publicação de correções do Meu Valor Líquido.",
                "/correcoes",
                SeoMetadataHelper.NoIndexFollowRobots));
    }
}
