namespace MeuValorLiquido.Modules.Calculators;

public sealed record SimpleExplanationStep(
    int Order,
    string Title,
    string Body,
    string? Highlight = null);

public sealed record CalculatorRelatedLinkDefinition(
    string Slug,
    string Teaser);

public sealed record CalculatorSimpleExplanation(
    IReadOnlyList<SimpleExplanationStep> Steps,
    string Summary);
