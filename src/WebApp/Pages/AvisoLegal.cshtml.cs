namespace MeuValorLiquido.WebApp.Pages;

public class AvisoLegalModel : PageModel
{
    public void OnGet()
    {
        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Aviso Legal",
                "Limitações das estimativas, ausência de consultoria e orientações sobre uso responsável.",
                "/aviso-legal"));
    }
}
