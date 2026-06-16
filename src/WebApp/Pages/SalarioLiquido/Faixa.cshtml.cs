using System.Text.RegularExpressions;
using MeuValorLiquido.Modules.Ads;
using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.Shared.Seo;
using MeuValorLiquido.WebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuValorLiquido.WebApp.Pages.SalarioLiquido;

public partial class FaixaModel : PageModel
{
    private readonly NetSalaryCalculator netSalaryCalculator;
    private readonly IAdSlotProvider adSlotProvider;

    public FaixaModel(NetSalaryCalculator netSalaryCalculator, IAdSlotProvider adSlotProvider)
    {
        this.netSalaryCalculator = netSalaryCalculator;
        this.adSlotProvider = adSlotProvider;
    }

    public SalaryBandPageContent PageContent { get; private set; } = null!;

    public NetSalaryBreakdown Breakdown { get; private set; } = null!;

    public IReadOnlyList<FaqItem> FaqSchemaItems { get; private set; } = [];

    public AdSlotDefinition? TopAdSlot { get; private set; }

    public AdSlotDefinition? BottomAdSlot { get; private set; }

    public IActionResult OnGet(int valor)
    {
        if (!SalaryBandCatalog.IsValid(valor))
        {
            return NotFound();
        }

        Breakdown = netSalaryCalculator.Calculate(valor, dependents: 0, transportDiscount: 0m);
        PageContent = SalaryBandContentBuilder.Build(valor, Breakdown);
        FaqSchemaItems = PageContent.FaqItems
            .Select(f => new FaqItem(f.Question, HtmlTagRegex().Replace(f.Answer, " ").Trim()))
            .ToList();

        var slots = adSlotProvider.GetSlots();
        TopAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-top");
        BottomAdSlot = slots.FirstOrDefault(s => s.Key == "calculator-bottom");

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(PageContent.Title, PageContent.Description, SalaryBandCatalog.SlugPath(valor)));

        return Page();
    }

    [GeneratedRegex("<[^>]+>")]
    private static partial Regex HtmlTagRegex();
}
