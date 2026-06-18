namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record ThematicHubPageViewModel(
    ThematicHubDefinition Hub,
    CalculatorDefinition? PrimaryCalculator,
    IReadOnlyList<CalculatorDefinition> Calculators,
    IReadOnlyList<BlogPost> Articles,
    IReadOnlyList<PopularQuestionDefinition> Faqs,
    CalculatorJourneyPanelViewModel Journey);

public static class ThematicHubPageBuilder
{
    public static ThematicHubPageViewModel? Build(
        string hubId,
        ICalculatorCatalogService calculatorCatalog,
        IContentService contentService)
    {
        var hub = ThematicHubCatalog.TryGet(hubId);
        if (hub is null)
        {
            return null;
        }

        var allCalculators = calculatorCatalog.GetAll();
        var calculators = hub.CalculatorSlugs
            .Select(slug => allCalculators.FirstOrDefault(c => c.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase)))
            .Where(c => c is not null)
            .Cast<CalculatorDefinition>()
            .ToList();

        var postsBySlug = contentService.GetPublishedPosts()
            .GroupBy(post => post.Slug, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(group => group.Key, group => group.First(), StringComparer.OrdinalIgnoreCase);

        var articles = hub.BlogSlugs
            .Where(postsBySlug.ContainsKey)
            .Select(slug => postsBySlug[slug])
            .ToList();

        var faqs = hub.FaqSlugs
            .Select(PopularQuestionsCatalog.GetBySlug)
            .Where(faq => faq is not null)
            .Cast<PopularQuestionDefinition>()
            .ToList();

        var primaryCalculator = allCalculators.FirstOrDefault(c =>
            c.Slug.Equals(hub.PrimaryCalculatorSlug, StringComparison.OrdinalIgnoreCase));

        var journey = BuildJourneyPanel(hub, calculatorCatalog);

        return new ThematicHubPageViewModel(
            hub,
            primaryCalculator,
            calculators,
            articles,
            faqs,
            journey);
    }

    private static CalculatorJourneyPanelViewModel BuildJourneyPanel(
        ThematicHubDefinition hub,
        ICalculatorCatalogService calculatorCatalog)
    {
        var steps = hub.JourneySteps
            .Select(step => BuildJourneyStep(step, calculatorCatalog))
            .Where(step => step is not null)
            .Cast<CalculatorJourneyNextStepViewModel>()
            .ToList();

        return new CalculatorJourneyPanelViewModel(hub.Id, hub.JourneyTitle, hub.JourneyIntroduction, steps);
    }

    private static CalculatorJourneyNextStepViewModel? BuildJourneyStep(
        CalculatorJourneyStepDefinition step,
        ICalculatorCatalogService calculatorCatalog)
    {
        return step.Kind switch
        {
            CalculatorJourneyStepKind.Calculator => BuildCalculatorStep(step, calculatorCatalog),
            CalculatorJourneyStepKind.Faq => BuildFaqStep(step),
            CalculatorJourneyStepKind.SalaryBand => null,
            _ => null
        };
    }

    private static CalculatorJourneyNextStepViewModel BuildCalculatorStep(
        CalculatorJourneyStepDefinition step,
        ICalculatorCatalogService calculatorCatalog)
    {
        var definition = calculatorCatalog.GetBySlug(step.Target);
        return new CalculatorJourneyNextStepViewModel(
            definition?.Name ?? step.Target,
            step.Teaser,
            $"/calculadoras/{step.Target}");
    }

    private static CalculatorJourneyNextStepViewModel BuildFaqStep(CalculatorJourneyStepDefinition step) =>
        new(
            PopularQuestionsCatalog.GetBySlug(step.Target)?.Title ?? "Central de dúvidas",
            step.Teaser,
            PopularQuestionsCatalog.SlugPath(step.Target));
}
