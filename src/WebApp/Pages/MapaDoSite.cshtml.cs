using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.Modules.Content;
using MeuValorLiquido.WebApp.Infrastructure;
using MeuValorLiquido.Shared.Seo;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MeuValorLiquido.WebApp.Pages;

public class MapaDoSiteModel : PageModel
{
    private readonly ICalculatorCatalogService calculatorCatalog;
    private readonly IContentService contentService;

    public MapaDoSiteModel(ICalculatorCatalogService calculatorCatalog, IContentService contentService)
    {
        this.calculatorCatalog = calculatorCatalog;
        this.contentService = contentService;
    }

    public IReadOnlyList<CalculatorDefinition> Calculators { get; private set; } = [];

    public IReadOnlyList<BlogPost> Posts { get; private set; } = [];

    public void OnGet()
    {
        Calculators = calculatorCatalog.GetAll()
            .GroupBy(c => c.Slug, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .OrderBy(c => c.Name)
            .ToList();
        Posts = contentService.GetPublishedPosts()
            .GroupBy(p => p.Slug, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.First())
            .OrderByDescending(p => p.PublishedAt)
            .ToList();

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Mapa do site",
                "Navegue por todas as calculadoras, artigos e páginas do Meu Valor Líquido.",
                "/mapa-do-site"));
    }
}
