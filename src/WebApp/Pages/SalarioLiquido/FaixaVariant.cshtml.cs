namespace MeuValorLiquido.WebApp.Pages.SalarioLiquido;

public class FaixaVariantModel(
    NetSalaryCalculator netSalaryCalculator,
    IAdSlotProvider adSlotProvider,
    CalculatorShareLinkBuilder shareLinkBuilder,
    ICalculatorCatalogService catalogService)
    : FaixaPageModelBase(netSalaryCalculator, adSlotProvider, shareLinkBuilder, catalogService)
{
    public IActionResult OnGet(int valor, string variant) => LoadPage(valor, variant);
}
