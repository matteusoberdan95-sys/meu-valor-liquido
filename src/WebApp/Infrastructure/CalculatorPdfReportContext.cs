namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record CalculatorPdfReportContext(
    string Slug,
    string CalculatorTitle,
    CalculatorInput Input,
    CalculationResult Result,
    string ShareUrl,
    string SiteUrl,
    DateTimeOffset GeneratedAt);

public sealed record PdfInputField(string Label, string Value);

public sealed record SalaryBandPdfContext(
    int Gross,
    NetSalaryBreakdown Breakdown,
    string ShareUrl,
    string SiteUrl,
    DateTimeOffset GeneratedAt);
