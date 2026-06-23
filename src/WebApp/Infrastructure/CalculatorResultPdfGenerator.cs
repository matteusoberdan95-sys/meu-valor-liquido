namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class CalculatorResultPdfGenerator
{
    private const string BrandName = "Meu Valor Líquido";
    private const string MethodologyPath = "/como-calculamos";

    private static readonly Color BrandPrimary = Color.FromHex("#00a896");
    private static readonly Color BrandPrimaryDark = Color.FromHex("#003731");
    private static readonly Color BrandHighlight = Color.FromHex("#e6f7f4");
    private static readonly Color TextPrimary = Color.FromHex("#1c1c1f");
    private static readonly Color TextMuted = Color.FromHex("#6b7280");
    private static readonly Color BorderSubtle = Color.FromHex("#e5e7eb");

    private readonly byte[]? logoBytes;
    private readonly CalculatorPdfInputSummaryBuilder inputSummaryBuilder;

    public CalculatorResultPdfGenerator(
        IWebHostEnvironment environment,
        CalculatorPdfInputSummaryBuilder inputSummaryBuilder)
    {
        this.inputSummaryBuilder = inputSummaryBuilder;
        var logoPath = Path.Combine(environment.WebRootPath, "images", "brand", "logo-horizontal.png");
        logoBytes = File.Exists(logoPath) ? File.ReadAllBytes(logoPath) : null;
    }

    public byte[] Generate(CalculatorPdfReportContext context)
    {
        var inputFields = inputSummaryBuilder.Build(context.Slug, context.Input);
        return BuildDocument(
            context.CalculatorTitle,
            context.Result,
            inputFields,
            context.ShareUrl,
            context.SiteUrl,
            context.GeneratedAt);
    }

    public byte[] GenerateSalaryBand(SalaryBandPdfContext context)
    {
        var title = $"Salário bruto de {Money.From(context.Gross)}";
        var lines = new List<CalculationLineItem>
        {
            new("Salário bruto", Money.From(context.Gross), CalculationLineType.Income),
            new("INSS", Money.From(context.Breakdown.Inss), CalculationLineType.Discount),
            new("IRRF", Money.From(context.Breakdown.Irrf), CalculationLineType.Discount)
        };

        if (context.Breakdown.TransportDiscount > 0m)
        {
            lines.Add(new CalculationLineItem(
                "Vale-transporte",
                Money.From(context.Breakdown.TransportDiscount),
                CalculationLineType.Discount));
        }

        if (context.Breakdown.MealVoucherDiscount > 0m)
        {
            lines.Add(new CalculationLineItem(
                "Vale-refeição/alimentação",
                Money.From(context.Breakdown.MealVoucherDiscount),
                CalculationLineType.Discount));
        }

        if (context.Breakdown.OtherDiscounts > 0m)
        {
            lines.Add(new CalculationLineItem(
                "Outros descontos",
                Money.From(context.Breakdown.OtherDiscounts),
                CalculationLineType.Discount));
        }

        var result = new CalculationResult(
            "salario-liquido",
            title,
            Money.From(context.Gross),
            lines,
            Money.From(context.Breakdown.Net),
            $"Simulação educativa para salário bruto de {Money.From(context.Gross)} com tabelas de {BrTaxTables2026.Year}.",
            "Estimativa educativa. Não substitui holerite, contracheque ou orientação profissional.");

        var inputFields = new List<PdfInputField>
        {
            new("Salário bruto", Money.From(context.Gross).ToString()),
            new("Dependentes", "0")
        };

        return BuildDocument(title, result, inputFields, context.ShareUrl, context.SiteUrl, context.GeneratedAt);
    }

    private byte[] BuildDocument(
        string calculatorTitle,
        CalculationResult result,
        IReadOnlyList<PdfInputField> inputFields,
        string shareUrl,
        string siteUrl,
        DateTimeOffset generatedAt)
    {
        return Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(32);
                page.DefaultTextStyle(style => style.FontSize(10).FontColor(TextPrimary));

                page.Header().Element(header => RenderHeader(header, calculatorTitle, generatedAt));
                page.Content().PaddingVertical(12).Column(column =>
                {
                    column.Spacing(14);
                    column.Item().Element(container => RenderInputSection(container, inputFields));
                    column.Item().Element(container => RenderExtratoSection(container, result));
                    column.Item().Element(container => RenderNetHighlight(container, result.EstimatedNetAmount.ToString()));
                    column.Item().Element(container => RenderSummarySection(container, result.Explanation));
                });
                page.Footer().Element(footer =>
                    RenderFooter(footer, result.LegalDisclaimer, shareUrl, siteUrl));
            });
        }).GeneratePdf();
    }

    private void RenderHeader(IContainer container, string calculatorTitle, DateTimeOffset generatedAt)
    {
        container.Column(column =>
        {
            column.Item().Background(BrandPrimary).Padding(16).Row(row =>
            {
                if (logoBytes is { Length: > 0 })
                {
                    row.ConstantItem(150).Height(36).Image(logoBytes).FitArea();
                }
                else
                {
                    row.RelativeItem().Text(BrandName).Bold().FontSize(16).FontColor(Colors.White);
                }

                row.RelativeItem().AlignRight().Column(meta =>
                {
                    meta.Item().Text("Relatório de simulação").FontSize(9).FontColor(Colors.White);
                    meta.Item().Text($"Tabelas {BrTaxTables2026.Year}").FontSize(8).FontColor(Colors.White);
                });
            });

            column.Item().PaddingTop(10).Column(body =>
            {
                body.Item().Text(calculatorTitle).SemiBold().FontSize(14).FontColor(TextPrimary);
                body.Item().Text($"Gerado em {generatedAt.ToString("dd/MM/yyyy HH:mm", CultureInfo.GetCultureInfo("pt-BR"))} · Estimativa educativa")
                    .FontSize(8)
                    .FontColor(TextMuted);
            });
        });
    }

    private static void RenderInputSection(IContainer container, IReadOnlyList<PdfInputField> inputFields)
    {
        container.Column(column =>
        {
            column.Item().Text("Dados informados").SemiBold().FontSize(11).FontColor(BrandPrimaryDark);
            column.Item().PaddingTop(6);

            if (inputFields.Count == 0)
            {
                column.Item().Text("Parâmetros padrão da calculadora.").FontSize(9).FontColor(TextMuted);
                return;
            }

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1.4f);
                    columns.RelativeColumn(1f);
                });

                foreach (var field in inputFields)
                {
                    table.Cell().BorderBottom(0.5f).BorderColor(BorderSubtle).PaddingVertical(4).PaddingRight(8)
                        .Text(field.Label).FontSize(9).FontColor(TextMuted);
                    table.Cell().BorderBottom(0.5f).BorderColor(BorderSubtle).PaddingVertical(4)
                        .AlignRight().Text(field.Value).SemiBold().FontSize(9);
                }
            });
        });
    }

    private static void RenderExtratoSection(IContainer container, CalculationResult result)
    {
        container.Column(column =>
        {
            column.Item().Text("Extrato da simulação").SemiBold().FontSize(11).FontColor(BrandPrimaryDark);
            column.Item().PaddingTop(6);

            var income = result.LineItems.Where(x => x.Type == CalculationLineType.Income).ToList();
            var discounts = result.LineItems.Where(x => x.Type == CalculationLineType.Discount).ToList();
            var information = result.LineItems.Where(x => x.Type == CalculationLineType.Information).ToList();

            if (income.Count > 0)
            {
                column.Item().PaddingTop(4).Element(c => RenderLineGroup(c, "Proventos", income));
            }

            if (discounts.Count > 0)
            {
                column.Item().PaddingTop(8).Element(c => RenderLineGroup(c, "Descontos", discounts));
            }

            if (information.Count > 0)
            {
                column.Item().PaddingTop(8).Element(c => RenderLineGroup(c, "Detalhamento", information));
            }

            if (result.GrossAmount.Amount > 0m && income.Count == 0 && information.All(x => !x.Label.Contains("bruto", StringComparison.OrdinalIgnoreCase)))
            {
                column.Item().PaddingTop(4).Element(c =>
                    RenderLineGroup(c, "Base", [new CalculationLineItem("Valor base", result.GrossAmount, CalculationLineType.Information)]));
            }
        });
    }

    private static void RenderLineGroup(IContainer container, string title, IReadOnlyList<CalculationLineItem> items)
    {
        container.Column(column =>
        {
            column.Item().Background(BrandHighlight).PaddingHorizontal(8).PaddingVertical(4)
                .Text(title).SemiBold().FontSize(9).FontColor(BrandPrimaryDark);

            column.Item().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1.6f);
                    columns.RelativeColumn(1f);
                });

                foreach (var item in items)
                {
                    table.Cell().BorderBottom(0.5f).BorderColor(BorderSubtle).PaddingVertical(5).PaddingHorizontal(8)
                        .Text(item.Label).FontSize(9);
                    table.Cell().BorderBottom(0.5f).BorderColor(BorderSubtle).PaddingVertical(5).PaddingHorizontal(8)
                        .AlignRight().Text(FormatLineAmount(item)).SemiBold().FontSize(9);
                }
            });
        });
    }

    private static void RenderNetHighlight(IContainer container, string netAmount)
    {
        container.Background(BrandHighlight).Border(1).BorderColor(BrandPrimary).Padding(12).Row(row =>
        {
            row.RelativeItem().AlignMiddle().Text("Líquido estimado").SemiBold().FontSize(12).FontColor(BrandPrimaryDark);
            row.ConstantItem(160).AlignRight().Text(netAmount).Bold().FontSize(14).FontColor(BrandPrimary);
        });
    }

    private static void RenderSummarySection(IContainer container, string explanation)
    {
        var summary = TruncateExplanation(explanation);
        if (string.IsNullOrWhiteSpace(summary))
        {
            return;
        }

        container.Column(column =>
        {
            column.Item().Text("Resumo").SemiBold().FontSize(11).FontColor(BrandPrimaryDark);
            column.Item().PaddingTop(4).Text(summary).FontSize(9).LineHeight(1.35f).FontColor(TextPrimary);
        });
    }

    private void RenderFooter(IContainer container, string disclaimer, string shareUrl, string siteUrl)
    {
        var methodologyUrl = CombineUrl(siteUrl, MethodologyPath);

        container.Column(column =>
        {
            column.Item().LineHorizontal(0.5f).LineColor(BorderSubtle);
            column.Item().PaddingTop(6).Text(disclaimer).FontSize(7.5f).FontColor(TextMuted);
            column.Item().PaddingTop(4).Text($"Simule novamente: {shareUrl}").FontSize(7.5f).FontColor(TextMuted);
            column.Item().Text($"{siteUrl} · Metodologia: {methodologyUrl}")
                .FontSize(7.5f)
                .FontColor(TextMuted);
            column.Item().AlignRight().Text(text =>
            {
                text.Span("Gerado por ").FontSize(7).FontColor(TextMuted);
                text.Span(BrandName).SemiBold().FontSize(7).FontColor(BrandPrimary);
            });
        });
    }

    private static string FormatLineAmount(CalculationLineItem item)
    {
        if (!string.IsNullOrWhiteSpace(item.DisplayText))
        {
            return item.DisplayText;
        }

        return item.Type switch
        {
            CalculationLineType.Discount => $"- {item.Amount}",
            CalculationLineType.Income => $"+ {item.Amount}",
            _ => item.Amount.ToString()
        };
    }

    private static string TruncateExplanation(string explanation)
    {
        if (string.IsNullOrWhiteSpace(explanation))
        {
            return string.Empty;
        }

        var normalized = Regex.Replace(explanation.Trim(), @"\s+", " ");
        return normalized.Length <= 320 ? normalized : normalized[..317] + "...";
    }

    private static string CombineUrl(string siteUrl, string path)
    {
        var baseUrl = siteUrl.TrimEnd('/');
        return $"{baseUrl}{path}";
    }
}
