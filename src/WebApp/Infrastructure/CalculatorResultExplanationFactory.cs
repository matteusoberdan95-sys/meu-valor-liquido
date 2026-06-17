namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record CalculatorRelatedLinkViewModel(
    string Slug,
    string Name,
    string Teaser,
    string Url);

public sealed record CalculatorResultExplanationViewModel(
    CalculatorSimpleExplanation Simple,
    IReadOnlyList<CalculatorRelatedLinkViewModel> RelatedLinks);

public sealed record CalculatorResultPanelViewModel(
    CalculationResult Result,
    CalculatorInput Input,
    string CalculatorSlug,
    CalculatorResultExplanationViewModel Explanation,
    bool ShowSimpleExplanation = true,
    TerminationResultSummary? TerminationSummary = null,
    IReadOnlyList<CalculatorResultWarning> Warnings = null!,
    CalculatorJourneyPanelViewModel? Journey = null)
{
    public IReadOnlyList<CalculatorResultWarning> Warnings { get; init; } = Warnings ?? [];
}

public sealed record SalaryBandResultPanelViewModel(
    int Gross,
    decimal Inss,
    decimal Irrf,
    decimal Net,
    CalculatorResultExplanationViewModel Explanation);

public static class CalculatorResultExplanationFactory
{
    public static CalculatorResultExplanationViewModel Build(
        string slug,
        CalculatorInput input,
        CalculationResult result,
        ICalculatorCatalogService catalogService)
    {
        var simple = CalculatorSimpleExplanationBuilder.Build(slug, input, result);
        var related = CalculatorRelatedLinksCatalog
            .GetForSlug(slug)
            .Select(link =>
            {
                var definition = catalogService.GetBySlug(link.Slug);
                return new CalculatorRelatedLinkViewModel(
                    link.Slug,
                    definition?.Name ?? link.Slug,
                    link.Teaser,
                    $"/calculadoras/{link.Slug}");
            })
            .ToList();

        return new CalculatorResultExplanationViewModel(simple, related);
    }

    public static CalculatorResultExplanationViewModel BuildForSalaryBand(
        int gross,
        NetSalaryBreakdown breakdown,
        ICalculatorCatalogService catalogService)
    {
        var simple = CalculatorSimpleExplanationBuilder.BuildForSalaryBand(gross, breakdown);
        var related = CalculatorRelatedLinksCatalog
            .GetForSlug("salario-liquido")
            .Select(link =>
            {
                var definition = catalogService.GetBySlug(link.Slug);
                return new CalculatorRelatedLinkViewModel(
                    link.Slug,
                    definition?.Name ?? link.Slug,
                    link.Teaser,
                    $"/calculadoras/{link.Slug}");
            })
            .ToList();

        return new CalculatorResultExplanationViewModel(simple, related);
    }
}
