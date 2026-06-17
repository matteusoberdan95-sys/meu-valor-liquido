namespace MeuValorLiquido.Modules.Calculators;

public sealed record ThirteenthSalaryCalculationResult(
    decimal Gross,
    decimal Net,
    IReadOnlyList<CalculationLineItem> Lines,
    string Explanation);

public static class ThirteenthSalaryCalculator
{
    public static ThirteenthSalaryCalculationResult Calculate(
        CalculatorInput input,
        IInssCalculator inssCalculator,
        IIrrfCalculator irrfCalculator)
    {
        var months = Math.Clamp(input.Months, 1, 12);
        var grossTotal = decimal.Round(input.Amount * months / 12m, 2, MidpointRounding.AwayFromZero);
        var firstGross = decimal.Round(grossTotal / 2m, 2, MidpointRounding.AwayFromZero);
        var secondGross = grossTotal - firstGross;

        var inss = inssCalculator.Calculate(grossTotal);
        var irrf = irrfCalculator.Calculate(grossTotal - inss, input.Dependents);
        var advance = Math.Clamp(input.ThirteenthAdvancePaid, 0m, grossTotal);

        var secondNet = secondGross - inss - irrf;
        var totalNet = firstGross + secondNet - advance;

        var lines = new List<CalculationLineItem>
        {
            new("Meses considerados", Money.From(0m), CalculationLineType.Information, months.ToString()),
            new("Décimo terceiro proporcional", Money.From(grossTotal), CalculationLineType.Income),
            new("1ª parcela (sem INSS/IRRF)", Money.From(firstGross), CalculationLineType.Information),
            new("2ª parcela bruta", Money.From(secondGross), CalculationLineType.Information),
            new("INSS (na 2ª parcela)", Money.From(inss), CalculationLineType.Discount),
            new("IRRF (na 2ª parcela)", Money.From(irrf), CalculationLineType.Discount)
        };

        if (advance > 0m)
        {
            lines.Add(new CalculationLineItem(
                "Adiantamento já pago",
                Money.From(advance),
                CalculationLineType.Discount));
        }

        var explanation =
            $"Décimo terceiro proporcional ({months}/12 avos). A 1ª parcela costuma ser paga sem descontos; " +
            $"INSS e IRRF incidem sobre o valor integral na 2ª parcela ({BrTaxTables2026.Year}).";

        return new ThirteenthSalaryCalculationResult(grossTotal, totalNet, lines, explanation);
    }
}
