namespace MeuValorLiquido.Modules.Calculators;

public static class TerminationDateHelper
{
    public static CalculatorInput ApplyDates(CalculatorInput input)
    {
        if (input.AdmissionDate is null || input.TerminationDate is null)
        {
            return input;
        }

        var admission = input.AdmissionDate.Value;
        var termination = input.TerminationDate.Value;
        if (termination < admission)
        {
            return input;
        }

        var months = CalculateTenureMonths(admission, termination);
        var workedDays = (decimal)Math.Clamp(termination.Day, 1, 31);

        return input with
        {
            Months = months,
            SecondaryAmount = input.SecondaryAmount > 0m ? input.SecondaryAmount : workedDays,
            AdmissionMonth = admission.Month,
            TerminationMonth = termination.Month,
            AdmissionInPriorYear = admission.Year < termination.Year || input.AdmissionInPriorYear
        };
    }

    /// <summary>
    /// Conta meses de vínculo: fração com 15 dias ou mais no mês conta como mês inteiro (regra usual em rescisão).
    /// </summary>
    public static int CalculateTenureMonths(DateOnly admission, DateOnly termination)
    {
        if (termination < admission)
        {
            return 1;
        }

        var total = 0;
        var cursor = new DateOnly(admission.Year, admission.Month, 1);

        while (cursor.Year < termination.Year
            || (cursor.Year == termination.Year && cursor.Month <= termination.Month))
        {
            var monthStart = cursor;
            var monthEnd = cursor.AddMonths(1).AddDays(-1);
            var periodStart = admission > monthStart ? admission : monthStart;
            var periodEnd = termination < monthEnd ? termination : monthEnd;

            if (periodStart <= periodEnd)
            {
                var days = periodEnd.DayNumber - periodStart.DayNumber + 1;
                if (days >= 15)
                {
                    total++;
                }
            }

            cursor = cursor.AddMonths(1);
        }

        return Math.Max(1, total);
    }

    public static int CountThirteenthAvos(DateOnly admission, DateOnly termination)
    {
        if (termination < admission)
        {
            return 0;
        }

        var yearStart = new DateOnly(termination.Year, 1, 1);
        var periodStart = admission > yearStart ? admission : yearStart;
        return CalculateTenureMonths(periodStart, termination);
    }

    public static int CountMonthsWorkedInYear(DateOnly? admission, DateOnly termination)
    {
        if (admission is null)
        {
            return Math.Clamp(termination.Month, 1, 12);
        }

        return CountThirteenthAvos(admission.Value, termination);
    }

    /// <summary>
    /// Meses do período aquisitivo incompleto com a regra dos 15 dias (fração ≥ 15 dias no mês conta como avo).
    /// </summary>
    public static int CountVacationProportionalAvos(DateOnly admission, DateOnly termination)
    {
        if (termination < admission)
        {
            return 0;
        }

        var totalTenure = CalculateTenureMonths(admission, termination);
        var completePeriods = totalTenure / 12;
        var periodStart = admission.AddMonths(completePeriods * 12);

        if (periodStart > termination)
        {
            periodStart = admission;
        }

        var monthsInCurrentPeriod = CalculateTenureMonths(periodStart, termination);

        if (monthsInCurrentPeriod == 0 && totalTenure >= 12 && totalTenure % 12 == 0)
        {
            return 0;
        }

        return Math.Clamp(monthsInCurrentPeriod, 0, 12);
    }
}
