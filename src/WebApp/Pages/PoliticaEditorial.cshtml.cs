namespace MeuValorLiquido.WebApp.Pages;

using Microsoft.AspNetCore.OutputCaching;

[OutputCache(PolicyName = "editorial")]
public class PoliticaEditorialModel : PageModel
{
    public EditorialAuthorProfile Author => EditorialAuthorCatalog.Primary;

    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Política Editorial",
                "Conheça os critérios editoriais, fontes, revisão e correções do Meu Valor Líquido.",
                "/politica-editorial"));
    }
}
