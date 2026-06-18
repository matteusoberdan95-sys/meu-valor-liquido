using MeuValorLiquido.Modules.Calculators;

namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record SalarioLiquidoDonutSegment(string Label, string Color, decimal Percent);

public sealed record SalarioLiquidoStitchResultViewModel(
    decimal Gross,
    decimal Net,
    decimal Inss,
    decimal Irrf,
    decimal NetPercentOfGross,
    decimal EffectiveTaxPercent,
    IReadOnlyList<SalarioLiquidoDonutSegment> DonutSegments);

public static class SalarioLiquidoStitchResultBuilder
{
    public static SalarioLiquidoStitchResultViewModel? TryBuild(CalculationResult result)
    {
        if (!result.Slug.Equals("salario-liquido", StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        var gross = result.GrossAmount.Amount;
        if (gross <= 0m)
        {
            return null;
        }

        var net = result.EstimatedNetAmount.Amount;
        var inss = SumDiscount(result, "INSS");
        var irrf = SumDiscount(result, "IRRF");
        var netPercent = Math.Round(net / gross * 100m, 1);
        var effectiveTax = Math.Round((inss + irrf) / gross * 100m, 1);

        var segments = BuildDonutSegments(gross, net, inss, irrf);

        return new SalarioLiquidoStitchResultViewModel(
            gross,
            net,
            inss,
            irrf,
            netPercent,
            effectiveTax,
            segments);
    }

    private static decimal SumDiscount(CalculationResult result, string labelPrefix) =>
        result.LineItems
            .Where(item => item.Type == CalculationLineType.Discount
                && item.Label.StartsWith(labelPrefix, StringComparison.OrdinalIgnoreCase))
            .Sum(item => item.Amount.Amount);

    private static IReadOnlyList<SalarioLiquidoDonutSegment> BuildDonutSegments(
        decimal gross,
        decimal net,
        decimal inss,
        decimal irrf)
    {
        var other = Math.Max(0m, gross - net - inss - irrf);
        var segments = new List<SalarioLiquidoDonutSegment>();

        void Add(string label, string color, decimal amount)
        {
            if (amount <= 0m)
            {
                return;
            }

            segments.Add(new SalarioLiquidoDonutSegment(label, color, Math.Round(amount / gross * 100m, 1)));
        }

        Add("Líquido", "#34D399", net);
        Add("INSS", "#3B82F6", inss);
        Add("IRRF", "#A855F7", irrf);
        Add("Outros", "#71717A", other);

        return segments;
    }
}
