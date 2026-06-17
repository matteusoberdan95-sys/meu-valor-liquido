namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record CalculatorJourneyNextStepViewModel(
    string Label,
    string Teaser,
    string Url);

public sealed record CalculatorJourneyPanelViewModel(
    string JourneyId,
    string Title,
    string Introduction,
    IReadOnlyList<CalculatorJourneyNextStepViewModel> Steps);

public sealed class CalculatorJourneyLinkBuilder
{
    private readonly CalculatorShareLinkBuilder shareLinkBuilder;
    private readonly ICalculatorCatalogService catalogService;

    public CalculatorJourneyLinkBuilder(
        CalculatorShareLinkBuilder shareLinkBuilder,
        ICalculatorCatalogService catalogService)
    {
        this.shareLinkBuilder = shareLinkBuilder;
        this.catalogService = catalogService;
    }

    public CalculatorJourneyPanelViewModel? Build(
        string? journeyId,
        string currentSlug,
        CalculatorInput input,
        CalculationResult result,
        HttpRequest request)
    {
        var journey = CalculatorJourneyCatalog.TryGet(journeyId)
            ?? CalculatorJourneyCatalog.TryGetByEntrySlug(currentSlug);
        if (journey is null)
        {
            return null;
        }

        var remaining = CalculatorJourneyCatalog.GetRemainingSteps(journey, currentSlug);
        if (remaining.Count == 0)
        {
            return null;
        }

        var steps = remaining
            .Select(step => TryBuildStep(journey, step, currentSlug, input, result, request))
            .Where(step => step is not null)
            .Cast<CalculatorJourneyNextStepViewModel>()
            .ToList();

        return steps.Count == 0
            ? null
            : new CalculatorJourneyPanelViewModel(journey.Id, journey.Title, journey.Introduction, steps);
    }

    private CalculatorJourneyNextStepViewModel? TryBuildStep(
        CalculatorJourneyDefinition journey,
        CalculatorJourneyStepDefinition step,
        string currentSlug,
        CalculatorInput input,
        CalculationResult result,
        HttpRequest request)
    {
        return step.Kind switch
        {
            CalculatorJourneyStepKind.Calculator => BuildCalculatorStep(journey, step, input, result, request),
            CalculatorJourneyStepKind.Faq => BuildFaqStep(step),
            CalculatorJourneyStepKind.SalaryBand => BuildSalaryBandStep(journey, input, result),
            _ => null
        };
    }

    private CalculatorJourneyNextStepViewModel? BuildCalculatorStep(
        CalculatorJourneyDefinition journey,
        CalculatorJourneyStepDefinition step,
        CalculatorInput input,
        CalculationResult result,
        HttpRequest request)
    {
        var mappedInput = CalculatorJourneyInputMapper.MapForCalculatorStep(
            journey.Id,
            step.Target,
            input,
            result);
        if (mappedInput is null)
        {
            return null;
        }

        var definition = catalogService.GetBySlug(step.Target);
        var shareUrl = shareLinkBuilder.BuildShareUrl(step.Target, mappedInput, request);
        var url = AppendJourneyQuery(shareUrl, journey.Id);

        return new CalculatorJourneyNextStepViewModel(
            definition?.Name ?? step.Target,
            step.Teaser,
            url);
    }

    private static CalculatorJourneyNextStepViewModel BuildFaqStep(CalculatorJourneyStepDefinition step) =>
        new(
            PopularQuestionsCatalog.GetBySlug(step.Target)?.Title ?? "Leia na central de dúvidas",
            step.Teaser,
            PopularQuestionsCatalog.SlugPath(step.Target));

    private CalculatorJourneyNextStepViewModel? BuildSalaryBandStep(
        CalculatorJourneyDefinition journey,
        CalculatorInput input,
        CalculationResult result)
    {
        if (!journey.Id.Equals(CalculatorJourneyCatalog.LiquidoDesejado, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var nearest = SalaryBandCatalog.ResolveNearestBand(result.GrossAmount.Amount);
        return new CalculatorJourneyNextStepViewModel(
            $"Salário de {SalaryBandCatalog.FormatCurrency(nearest)}",
            journey.Steps.Last().Teaser,
            SalaryBandCatalog.SlugPath(nearest));
    }

    public static string AppendJourneyQuery(string url, string journeyId)
    {
        var separator = url.Contains('?', StringComparison.Ordinal) ? '&' : '?';
        return $"{url}{separator}jornada={Uri.EscapeDataString(journeyId)}";
    }
}
