namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorInputNormalizer
{
    public static CalculatorInput Normalize(string slug, CalculatorInput input)
    {
        return slug.ToLowerInvariant() switch
        {
            "rescisao-clt" => TerminationDateHelper.ApplyDates(input),
            "decimo-terceiro" => NormalizeThirteenth(input),
            "ferias" => NormalizeVacation(input),
            _ => input
        };
    }

    private static CalculatorInput NormalizeThirteenth(CalculatorInput input)
    {
        if (input.TerminationDate is null)
        {
            return input;
        }

        var months = input.AdmissionDate is not null
            ? TerminationDateHelper.CountThirteenthAvos(input.AdmissionDate.Value, input.TerminationDate.Value)
            : TerminationDateHelper.CountMonthsWorkedInYear(null, input.TerminationDate.Value);

        return input with { Months = Math.Clamp(months, 1, 12) };
    }

    private static CalculatorInput NormalizeVacation(CalculatorInput input)
    {
        if (input.AdmissionDate is null || input.TerminationDate is null)
        {
            return input;
        }

        var months = TerminationDateHelper.CalculateTenureMonths(
            input.AdmissionDate.Value,
            input.TerminationDate.Value);

        return input with { Months = Math.Clamp(months, 1, 12) };
    }
}
