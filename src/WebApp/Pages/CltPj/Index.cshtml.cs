namespace MeuValorLiquido.WebApp.Pages.CltPj;
public class IndexModel : PageModel
{
    public IReadOnlyList<int> Bands { get; private set; } = [];

    public void OnGet()
    {
        Bands = CltPjBandCatalog.GetAll();

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "CLT x PJ: quanto faturar para equivaler ao salário CLT (2026)",
                "Compare salário CLT e faturamento PJ. Veja quanto faturar como PJ para equivaler ao líquido CLT em valores comuns, com Simples e pró-labore estimados.",
                "/clt-pj"));
    }
}
