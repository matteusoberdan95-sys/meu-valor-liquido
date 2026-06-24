namespace MeuValorLiquido.WebApp.Pages.CltPj;

public class ComparacaoVariantModel(CltPjComparisonCalculator cltPjComparisonCalculator, IAdSlotProvider adSlotProvider)
    : ComparacaoPageModelBase(cltPjComparisonCalculator, adSlotProvider)
{
    public IActionResult OnGet(int valor, string variant) => LoadPage(valor, variant);
}
