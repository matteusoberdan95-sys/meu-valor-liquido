namespace MeuValorLiquido.WebApp.Pages.CltPj;

public class ComparacaoModel(CltPjComparisonCalculator cltPjComparisonCalculator, IAdSlotProvider adSlotProvider)
    : ComparacaoPageModelBase(cltPjComparisonCalculator, adSlotProvider)
{
    public IActionResult OnGet(int valor) => LoadPage(valor, null);
}
