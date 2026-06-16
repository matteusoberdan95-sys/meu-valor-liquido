using MeuValorLiquido.Shared.Seo;
using MeuValorLiquido.WebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuValorLiquido.WebApp.Pages.SalarioLiquido;

public class IndexModel : PageModel
{
    public IReadOnlyList<int> Bands { get; private set; } = [];

    public void OnGet()
    {
        Bands = SalaryBandCatalog.GetAll();

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Salário líquido por valor bruto (2026)",
                "Consulte quanto sobra do salário bruto em valores comuns — de R$ 1.621 a R$ 20.000 — com INSS e IRRF estimados.",
                "/salario-liquido"));
    }
}
