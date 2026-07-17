namespace MeuValorLiquido.WebApp.Pages.Duvidas;
public class IndexModel : PageModel
{
    [FromQuery(Name = "q")]
    public string? Query { get; set; }

    [FromQuery(Name = "cat")]
    public string? Category { get; set; }

    public IReadOnlyList<PopularQuestionDefinition> Questions { get; private set; } = [];

    public IReadOnlyList<PopularQuestionDefinition> PopularQuestions { get; private set; } = [];

    public bool HasActiveFilter => !string.IsNullOrWhiteSpace(Query) || !string.IsNullOrWhiteSpace(Category);

    public void OnGet()
    {
        var all = PopularQuestionsCatalog.GetAll();
        Questions = FaqHubHelper.FilterQuestions(all, Query, Category);
        PopularQuestions = HasActiveFilter ? Questions.Take(6).ToList() : all.Take(4).ToList();

        ViewData["BottomNavTab"] = "help";

        SeoMetadataHelper.Apply(
            ViewData,
            new SeoMetadata(
                "Central de Ajuda — dúvidas sobre salário, CLT e impostos",
                "Respostas educativas sobre salário líquido, INSS, IRRF, férias, rescisão, PJ e MEI — com links para calculadoras.",
                "/duvidas",
                Request.QueryString.HasValue
                    ? SeoMetadataHelper.NoIndexFollowRobots
                    : SeoMetadataHelper.DefaultRobots));
    }
}
