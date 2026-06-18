namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record ProductMetricsInsight(string Severity, string Title, string Detail);

public static class ProductMetricsPrioritizationBuilder
{
    private const long ErrorAlertThreshold = 5;
    private const long CalculationVolumeThreshold = 10;
    private const decimal LowEngagementRateThreshold = 5m;
    private const decimal HighFailureRateThreshold = 3m;

    public static IReadOnlyList<ProductMetricsInsight> Build(
        ProductMetricsSummary summary,
        IReadOnlyDictionary<string, string> calculatorNames)
    {
        var insights = new List<ProductMetricsInsight>();

        if (summary.TotalHttp500 > 0)
        {
            insights.Add(new(
                "alert",
                "Erros 500 no período",
                $"{summary.TotalHttp500:N0} ocorrência(s). Revise logs do servidor e o checklist pós-deploy em docs/DEPLOY.md."));
        }

        if (summary.TotalHttp404 >= ErrorAlertThreshold)
        {
            insights.Add(new(
                "warning",
                "Muitos 404 agregados",
                $"{summary.TotalHttp404:N0} rotas não encontradas. Verifique links internos, sitemap e redirecionamentos."));
        }

        if (summary.CalculationFailureRatePercent >= HighFailureRateThreshold
            && summary.TotalCalculationFailures >= ErrorAlertThreshold)
        {
            insights.Add(new(
                "warning",
                "Taxa de falha em calculadoras",
                $"{ProductMetricsDisplayHelper.FormatRate(summary.CalculationFailureRatePercent)} dos envios falharam ({summary.TotalCalculationFailures:N0}×). Priorize validação e mensagens de erro."));
        }

        foreach (var row in summary.TopCalculationFailures.Take(3))
        {
            if (row.Count < 3)
            {
                continue;
            }

            var label = ProductMetricsDisplayHelper.ResolveCalculatorLabel(row.Label, calculatorNames);
            insights.Add(new(
                "info",
                $"Falhas em {label}",
                $"{row.Count:N0} envios com erro em `{row.Label}`. Teste o formulário com valores típicos."));
        }

        foreach (var row in summary.TopCalculations.Take(5))
        {
            if (row.Count < CalculationVolumeThreshold)
            {
                continue;
            }

            var shareCount = summary.TopShareCopies
                .FirstOrDefault(x => x.Label.Equals(row.Label, StringComparison.OrdinalIgnoreCase))?.Count ?? 0;
            var pdfCount = summary.TopPdfDownloads
                .FirstOrDefault(x => x.Label.Equals(row.Label, StringComparison.OrdinalIgnoreCase))?.Count ?? 0;
            var shareRate = row.Count > 0 ? shareCount * 100m / row.Count : 0m;
            var pdfRate = row.Count > 0 ? pdfCount * 100m / row.Count : 0m;

            if (shareRate < LowEngagementRateThreshold && pdfRate < LowEngagementRateThreshold)
            {
                var label = ProductMetricsDisplayHelper.ResolveCalculatorLabel(row.Label, calculatorNames);
                insights.Add(new(
                    "info",
                    $"{label}: alto volume, baixo engajamento",
                    $"{row.Count:N0} cálculos com share {ProductMetricsDisplayHelper.FormatRate(shareRate)} e PDF {ProductMetricsDisplayHelper.FormatRate(pdfRate)}. Reforce jornada, CTA ou artigo relacionado."));
            }
        }

        if (summary.TotalCalculations == 0 && summary.TotalWidgetViews > 0)
        {
            insights.Add(new(
                "info",
                "Widget sem cálculos no site",
                "Há views de embed, mas nenhum cálculo no domínio principal no período. Avalie conversão do widget para o site."));
        }

        if (insights.Count == 0)
        {
            insights.Add(new(
                "info",
                "Sem alertas críticos",
                "Nenhum sinal forte de erro ou priorização automática neste período. Continue a rotina semanal em docs/METRICS_ROUTINE.md."));
        }

        return insights;
    }
}
