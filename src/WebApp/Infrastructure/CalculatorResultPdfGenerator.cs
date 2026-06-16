namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class CalculatorResultPdfGenerator
{
    private const string BrandName = "Meu Valor Líquido";
    private static readonly string DefaultDisclaimer =
        "Estimativa educativa. Não substitui holerite, contracheque ou orientação profissional.";

    public byte[] Generate(CalculationResult result, string shareUrl, string siteUrl)
    {
        return Document.Create(document =>
        {
            document.Page(page =>
            {
                ConfigurePage(page);
                page.Header().Element(header => RenderHeader(header, result.Title));
                page.Content().Element(content => RenderCalculation(content, result));
                page.Footer().Element(footer =>
                    RenderFooter(footer, result.LegalDisclaimer, shareUrl, siteUrl));
            });
        }).GeneratePdf();
    }

    public byte[] GenerateSalaryBand(int gross, NetSalaryBreakdown breakdown, string shareUrl, string siteUrl)
    {
        var title = $"Salário bruto de {Money.From(gross)}";
        return Document.Create(document =>
        {
            document.Page(page =>
            {
                ConfigurePage(page);
                page.Header().Element(header => RenderHeader(header, title));
                page.Content().Column(column =>
                {
                    column.Spacing(6);
                    column.Item().Text($"Bruto: {Money.From(gross)}").SemiBold();
                    column.Item().Text($"INSS: - {Money.From(breakdown.Inss)}");
                    column.Item().Text($"IRRF: - {Money.From(breakdown.Irrf)}");
                    if (breakdown.TransportDiscount > 0m)
                    {
                        column.Item().Text($"Vale-transporte: - {Money.From(breakdown.TransportDiscount)}");
                    }

                    if (breakdown.MealVoucherDiscount > 0m)
                    {
                        column.Item().Text($"Vale-refeição: - {Money.From(breakdown.MealVoucherDiscount)}");
                    }

                    if (breakdown.OtherDiscounts > 0m)
                    {
                        column.Item().Text($"Outros descontos: - {Money.From(breakdown.OtherDiscounts)}");
                    }

                    column.Item().PaddingTop(8).Text($"Líquido estimado: {Money.From(breakdown.Net)}")
                        .Bold()
                        .FontSize(13);
                });
                page.Footer().Element(footer => RenderFooter(footer, DefaultDisclaimer, shareUrl, siteUrl));
            });
        }).GeneratePdf();
    }

    private static void ConfigurePage(PageDescriptor page)
    {
        page.Margin(40);
        page.DefaultTextStyle(style => style.FontSize(11));
    }

    private static void RenderHeader(IContainer container, string title)
    {
        container.Column(column =>
        {
            column.Spacing(4);
            column.Item().Text(BrandName).Bold().FontSize(18).FontColor(Colors.Teal.Darken2);
            column.Item().Text(title).SemiBold().FontSize(14);
            column.Item().Text($"Estimativa educativa ({BrTaxTables2026.Year})").FontSize(9).Italic();
        });
    }

    private static void RenderCalculation(IContainer container, CalculationResult result)
    {
        container.PaddingVertical(16).Column(column =>
        {
            column.Spacing(6);
            column.Item().Text($"Bruto: {result.GrossAmount}").SemiBold();

            foreach (var line in result.LineItems)
            {
                column.Item().Text(FormatLine(line));
            }

            column.Item().PaddingTop(8).Text($"Líquido estimado: {result.EstimatedNetAmount}")
                .Bold()
                .FontSize(13);
        });
    }

    private static void RenderFooter(IContainer container, string disclaimer, string shareUrl, string siteUrl)
    {
        container.Column(column =>
        {
            column.Spacing(4);
            column.Item().Text(disclaimer).FontSize(8);
            column.Item().Text($"Simule novamente: {shareUrl}").FontSize(8);
            column.Item().Text(siteUrl).FontSize(8).FontColor(Colors.Grey.Medium);
        });
    }

    private static string FormatLine(CalculationLineItem item)
    {
        if (!string.IsNullOrEmpty(item.DisplayText))
        {
            return $"{item.Label}: {item.DisplayText}";
        }

        return item.Type switch
        {
            CalculationLineType.Discount => $"{item.Label}: - {item.Amount}",
            CalculationLineType.Income => $"{item.Label}: + {item.Amount}",
            _ => $"{item.Label}: {item.Amount}"
        };
    }
}
