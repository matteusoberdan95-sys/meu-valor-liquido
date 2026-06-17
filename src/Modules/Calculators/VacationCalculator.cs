namespace MeuValorLiquido.Modules.Calculators;

public sealed record VacationCalculationResult(
    decimal Gross,
    decimal Net,
    IReadOnlyList<CalculationLineItem> Lines,
    string Explanation);

public static class VacationCalculator
{
    public static VacationCalculationResult Calculate(
        CalculatorInput input,
        IInssCalculator inssCalculator,
        IIrrfCalculator irrfCalculator)
    {
        var months = Math.Clamp(input.Months, 1, 12);
        var salary = input.Amount;
        var entitlementFactor = months / 12m;
        var entitledDays = ResolveEntitledDays(input.VacationDayOption, months);

        var vacationBase = ResolveVacationBase(salary, months, input.VacationDayOption);
        var constitutionalBonus = vacationBase / 3m;

        var abono = 0m;
        if (input.SellVacationAllowance)
        {
            var sellDays = Math.Min(10m, Math.Round(entitledDays / 3m, 0, MidpointRounding.AwayFromZero));
            abono = salary * (sellDays / 30m) * (months == 12 ? 1m : entitlementFactor);
        }

        var multiplier = input.DoubleVacationPayment ? 2m : 1m;
        var gross = (vacationBase + constitutionalBonus) * multiplier + abono;

        var inss = inssCalculator.Calculate(gross);
        var irrf = irrfCalculator.Calculate(gross - inss, input.Dependents);
        var net = gross - inss - irrf;

        var lines = BuildLines(
            months,
            vacationBase,
            constitutionalBonus,
            abono,
            entitledDays,
            input,
            multiplier,
            inss,
            irrf);

        var explanation = BuildExplanation(months, input, multiplier, abono > 0m);

        return new VacationCalculationResult(gross, net, lines, explanation);
    }

    private static decimal ResolveVacationBase(decimal salary, int months, VacationDayOption dayOption)
    {
        var entitlementFactor = months / 12m;

        return dayOption switch
        {
            VacationDayOption.Full30 => salary * entitlementFactor,
            VacationDayOption.Reduced20 when months == 12 => salary * (20m / 30m),
            VacationDayOption.Reduced20 => salary * entitlementFactor * (20m / 30m),
            _ => salary * entitlementFactor
        };
    }

    private static decimal ResolveEntitledDays(VacationDayOption dayOption, int months)
    {
        var proportionalDays = months / 12m * 30m;

        return dayOption switch
        {
            VacationDayOption.Full30 => months == 12 ? 30m : proportionalDays,
            VacationDayOption.Reduced20 => months == 12 ? 20m : proportionalDays * (20m / 30m),
            _ => proportionalDays
        };
    }

    private static List<CalculationLineItem> BuildLines(
        int months,
        decimal vacationBase,
        decimal constitutionalBonus,
        decimal abono,
        decimal entitledDays,
        CalculatorInput input,
        decimal multiplier,
        decimal inss,
        decimal irrf)
    {
        var lines = new List<CalculationLineItem>();

        if (months < 12)
        {
            lines.Add(new CalculationLineItem(
                "Meses de férias proporcionais",
                Money.From(0m),
                CalculationLineType.Information,
                months.ToString()));
            lines.Add(new CalculationLineItem(
                "Férias proporcionais",
                Money.From(vacationBase * multiplier),
                CalculationLineType.Income));
        }
        else
        {
            lines.Add(new CalculationLineItem(
                "Férias",
                Money.From(vacationBase * multiplier),
                CalculationLineType.Income));
        }

        lines.Add(new CalculationLineItem(
            "Adicional de 1/3",
            Money.From(constitutionalBonus * multiplier),
            CalculationLineType.Income));

        if (abono > 0m)
        {
            lines.Add(new CalculationLineItem(
                "Abono pecuniário",
                Money.From(abono),
                CalculationLineType.Income));
        }

        if (input.DoubleVacationPayment)
        {
            lines.Add(new CalculationLineItem(
                "Férias em dobro (informativo)",
                Money.From(0m),
                CalculationLineType.Information,
                "Remuneração dobrada por atraso na concessão"));
        }

        lines.Add(new CalculationLineItem(
            "Dias de férias considerados",
            Money.From(0m),
            CalculationLineType.Information,
            $"{entitledDays:0.#} dias"));

        lines.Add(new CalculationLineItem("INSS", Money.From(inss), CalculationLineType.Discount));
        lines.Add(new CalculationLineItem("IRRF", Money.From(irrf), CalculationLineType.Discount));

        return lines;
    }

    private static string BuildExplanation(int months, CalculatorInput input, decimal multiplier, bool hasAbono)
    {
        var parts = new List<string>();

        if (months < 12)
        {
            parts.Add($"Férias proporcionais ({months}/12)");
        }
        else
        {
            parts.Add("Férias gozadas");
        }

        parts.Add("com 1/3 constitucional");

        if (hasAbono)
        {
            parts.Add("e abono pecuniário (venda de até 1/3 dos dias)");
        }

        if (input.DoubleVacationPayment)
        {
            parts.Add("em dobro por atraso na concessão");
        }

        if (multiplier > 1m && !input.DoubleVacationPayment)
        {
            parts.Add("(remuneração majorada)");
        }

        return string.Join(" ", parts) + $" e descontos de INSS e IRRF ({BrTaxTables2026.Year}).";
    }
}
