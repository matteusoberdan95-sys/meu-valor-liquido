namespace MeuValorLiquido.WebApp.Infrastructure;

internal static class CalculatorPdfEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapGet("/calculadoras/{slug}/resultado.pdf", DownloadCalculatorPdf);
        app.MapGet("/salario-liquido/{valor:int}/resultado.pdf", DownloadSalaryBandPdf);
    }

    private static IResult DownloadCalculatorPdf(
        string slug,
        string? r,
        ICalculatorCatalogService catalogService,
        ICalculatorApplicationService calculatorService,
        CalculatorResultPdfGenerator pdfGenerator,
        CalculatorShareLinkBuilder shareLinkBuilder,
        HttpContext httpContext)
    {
        if (string.IsNullOrWhiteSpace(r) || !CalculatorInputShareCodec.TryDecode(r, out var input))
        {
            return Results.BadRequest();
        }

        if (catalogService.GetBySlug(slug) is null)
        {
            return Results.NotFound();
        }

        var calculation = calculatorService.Calculate(slug, input);
        if (calculation.IsFailure)
        {
            return Results.BadRequest();
        }

        var shareUrl = shareLinkBuilder.BuildShareUrl(slug, input, httpContext.Request);
        var siteUrl = shareLinkBuilder.BuildAbsoluteUrl("/", httpContext.Request);
        var pdf = pdfGenerator.Generate(calculation.Value, shareUrl, siteUrl);

        return Results.File(pdf, "application/pdf", $"{slug}-resultado.pdf");
    }

    private static IResult DownloadSalaryBandPdf(
        int valor,
        NetSalaryCalculator netSalaryCalculator,
        CalculatorResultPdfGenerator pdfGenerator,
        CalculatorShareLinkBuilder shareLinkBuilder,
        HttpContext httpContext)
    {
        if (!SalaryBandCatalog.IsValid(valor))
        {
            return Results.NotFound();
        }

        var breakdown = netSalaryCalculator.Calculate(valor, dependents: 0, transportDiscount: 0m);
        var path = SalaryBandCatalog.SlugPath(valor);
        var shareUrl = shareLinkBuilder.BuildAbsoluteUrl(path, httpContext.Request);
        var siteUrl = shareLinkBuilder.BuildAbsoluteUrl("/", httpContext.Request);
        var pdf = pdfGenerator.GenerateSalaryBand(valor, breakdown, shareUrl, siteUrl);

        return Results.File(pdf, "application/pdf", $"salario-liquido-{valor}.pdf");
    }
}
