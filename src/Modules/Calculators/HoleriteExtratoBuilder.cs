namespace MeuValorLiquido.Modules.Calculators;

public static class HoleriteExtratoBuilder
{
    public static IReadOnlyList<CalculationLineItem> Build(
        NetSalaryBreakdown breakdown,
        int dependents,
        bool includeGross = false)
    {
        var lines = new List<CalculationLineItem>();
        if (includeGross)
        {
            lines.Add(Income("Salário bruto", breakdown.Gross));
        }

        lines.AddRange(BuildDiscountLines(breakdown, dependents));
        return lines;
    }

    public static IReadOnlyList<CalculationLineItem> BuildDiscountLines(
        NetSalaryBreakdown breakdown,
        int dependents)
    {
        var lines = new List<CalculationLineItem>
        {
            Discount("INSS", breakdown.Inss),
            BuildIrrfLine(breakdown, dependents)
        };

        if (breakdown.TransportDiscount > 0m)
        {
            lines.Add(Discount("Vale-transporte", breakdown.TransportDiscount));
        }

        if (breakdown.MealVoucherDiscount > 0m)
        {
            lines.Add(Discount("Vale-refeição/alimentação", breakdown.MealVoucherDiscount));
        }

        if (breakdown.HealthPlanDiscount > 0m)
        {
            lines.Add(Discount("Plano de saúde", breakdown.HealthPlanDiscount));
        }

        if (breakdown.AlimonyDiscount > 0m)
        {
            lines.Add(Discount("Pensão alimentícia", breakdown.AlimonyDiscount));
        }

        if (breakdown.OtherDiscounts > 0m)
        {
            lines.Add(Discount("Outros descontos", breakdown.OtherDiscounts));
        }

        return lines;
    }

    private static CalculationLineItem BuildIrrfLine(NetSalaryBreakdown breakdown, int dependents)
    {
        var taxableBasis = Math.Max(
            0m,
            breakdown.Gross - breakdown.Inss - dependents * Tax.BrTaxTables2026.DependentDeduction);

        if (breakdown.Irrf == 0m && taxableBasis <= 5000m)
        {
            return new CalculationLineItem(
                "IRRF",
                Money.From(0m),
                CalculationLineType.Discount,
                "Isento (Lei 15.270/2025 — base até R$ 5.000)");
        }

        if (breakdown.Irrf == 0m)
        {
            return new CalculationLineItem(
                "IRRF",
                Money.From(0m),
                CalculationLineType.Discount,
                "Isento");
        }

        return Discount("IRRF", breakdown.Irrf);
    }

    private static CalculationLineItem Income(string label, decimal amount) =>
        new(label, Money.From(amount), CalculationLineType.Income);

    private static CalculationLineItem Discount(string label, decimal amount) =>
        new(label, Money.From(amount), CalculationLineType.Discount);
}
