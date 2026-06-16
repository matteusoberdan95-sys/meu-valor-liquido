namespace MeuValorLiquido.Modules.Calculators;

internal static class TerminationTenureHelper
{
    public static string MonthName(int month) => month switch
    {
        1 => "janeiro",
        2 => "fevereiro",
        3 => "março",
        4 => "abril",
        5 => "maio",
        6 => "junho",
        7 => "julho",
        8 => "agosto",
        9 => "setembro",
        10 => "outubro",
        11 => "novembro",
        12 => "dezembro",
        _ => "—"
    };

    public static int EstimateAdmissionMonth(int totalMonths, int terminationMonth)
    {
        if (terminationMonth is < 1 or > 12 || totalMonths < 1)
        {
            return 0;
        }

        var remainder = totalMonths % 12;
        if (remainder == 0)
        {
            remainder = 12;
        }

        var admissionMonth = terminationMonth - remainder + 1;
        while (admissionMonth <= 0)
        {
            admissionMonth += 12;
        }

        return admissionMonth;
    }

    public static bool IsAdmissionInPriorYear(
        int admissionMonth,
        int terminationMonth,
        bool admissionInPriorYear)
    {
        if (admissionInPriorYear)
        {
            return true;
        }

        return admissionMonth > terminationMonth;
    }

    public static int CountMonthsFromAdmissionToTermination(
        int admissionMonth,
        int terminationMonth,
        bool admissionInPriorYear)
    {
        if (admissionMonth is < 1 or > 12 || terminationMonth is < 1 or > 12)
        {
            return 0;
        }

        if (IsAdmissionInPriorYear(admissionMonth, terminationMonth, admissionInPriorYear))
        {
            return (12 - admissionMonth + 1) + terminationMonth;
        }

        return terminationMonth - admissionMonth + 1;
    }

    public static int CountThirteenthAvosInTerminationYear(
        int admissionMonth,
        int terminationMonth,
        bool admissionInPriorYear)
    {
        if (terminationMonth is < 1 or > 12)
        {
            return 0;
        }

        if (admissionMonth is < 1 or > 12)
        {
            return terminationMonth;
        }

        if (IsAdmissionInPriorYear(admissionMonth, terminationMonth, admissionInPriorYear))
        {
            return terminationMonth;
        }

        return terminationMonth - admissionMonth + 1;
    }

    public static (int Months, bool WasAdjusted) ResolveTenureMonths(CalculatorInput input, List<string> warnings)
    {
        var manualMonths = Math.Clamp(input.Months, 1, 240);
        var admissionMonth = input.AdmissionMonth;
        var terminationMonth = input.TerminationMonth;

        if (admissionMonth is not (>= 1 and <= 12) || terminationMonth is not (>= 1 and <= 12))
        {
            return (manualMonths, false);
        }

        var fromDates = CountMonthsFromAdmissionToTermination(
            admissionMonth,
            terminationMonth,
            input.AdmissionInPriorYear);

        if (fromDates < 1)
        {
            return (manualMonths, false);
        }

        if (fromDates != manualMonths)
        {
            warnings.Add(
                $"Tempo na empresa ajustado de {manualMonths} para {fromDates} meses conforme admissão em {MonthName(admissionMonth)} e saída em {MonthName(terminationMonth)}.");
            return (fromDates, true);
        }

        return (fromDates, false);
    }

    public static void AppendTenureWarnings(
        List<string> warnings,
        int totalMonths,
        int terminationMonth,
        int admissionMonth,
        int thirteenthMonths)
    {
        if (terminationMonth is < 1 or > 12)
        {
            return;
        }

        var estimatedAdmission = admissionMonth > 0
            ? admissionMonth
            : EstimateAdmissionMonth(totalMonths, terminationMonth);

        if (estimatedAdmission is < 1 or > 12)
        {
            return;
        }

        var admissionInPriorYear = admissionMonth > 0
            ? IsAdmissionInPriorYear(admissionMonth, terminationMonth, false)
            : totalMonths > terminationMonth;

        if (admissionMonth <= 0 && totalMonths > terminationMonth)
        {
            warnings.Add(
                $"Com {totalMonths} meses e saída em {MonthName(terminationMonth)}, a admissão provavelmente foi no ano anterior ({MonthName(estimatedAdmission)}). Marque \"Admissão no ano anterior\" se for o caso.");
        }

        if (admissionInPriorYear && admissionMonth > 0)
        {
            warnings.Add(
                $"Admissão em {MonthName(admissionMonth)} do ano anterior: o 13º usa só janeiro–{MonthName(terminationMonth)} ({thirteenthMonths} avos); férias usam o período completo ({totalMonths} meses).");
        }

        if (totalMonths <= 4)
        {
            warnings.Add(
                $"Com poucos meses na empresa ({totalMonths}), o líquido costuma ficar baixo (próximo de R$ 900 com salário ~R$ 1.850), principalmente pelo desconto do aviso prévio.");
        }
        else if (totalMonths >= 8 && thirteenthMonths >= 8)
        {
            warnings.Add(
                "Com vários meses no ano da saída, o líquido costuma ficar acima de R$ 3.000. Se você recebeu ~R$ 900, confira o mês de admissão, o mês da saída e os dias trabalhados no último mês.");
        }
    }
}
