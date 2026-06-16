namespace MeuValorLiquido.WebApp.Pages.Duvidas;
public class IndexModel : PageModel
{
    public IReadOnlyList<PopularQuestionDefinition> Questions { get; private set; } = [];

    public IReadOnlyList<string> Categories { get; private set; } = [];

    public void OnGet()
    {
        Questions = PopularQuestionsCatalog.GetAll();
        Categories = Questions.Select(q => q.Category).Distinct().OrderBy(c => c).ToList();

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Dúvidas populares sobre salário, CLT e impostos",
                "Respostas educativas sobre salário líquido, INSS, IRRF, férias, rescisão, PJ e MEI — com links para calculadoras.",
                "/duvidas"));
    }
}
