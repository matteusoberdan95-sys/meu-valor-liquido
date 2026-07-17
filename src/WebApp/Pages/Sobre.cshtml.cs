namespace MeuValorLiquido.WebApp.Pages;

using Microsoft.AspNetCore.OutputCaching;

[OutputCache(PolicyName = "editorial")]
public class SobreModel : PageModel
{
    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Sobre o Meu Valor Líquido",
                "Plataforma brasileira de calculadoras educativas sobre salário, impostos, CLT, PJ e planejamento financeiro.",
                "/sobre"));
    }
}
