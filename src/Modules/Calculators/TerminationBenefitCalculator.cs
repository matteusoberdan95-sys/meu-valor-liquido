namespace MeuValorLiquido.Modules.Calculators;
public sealed record TerminationBenefits(
    decimal SalaryBalance,
    decimal Thirteenth,
    decimal VacationProportional,
    decimal UnpaidVacation,
    int UnpaidVacationPeriods,
    int ProportionalVacationMonths,
    int ThirteenthMonths,
    decimal NoticeIndemnity,
    int NoticeDays,
    decimal NoticeDeduction,
    bool NoticeDeductionWasCapped,
    decimal FgtsFine,
    decimal FgtsFineRate,
    decimal FgtsBase,
    decimal FgtsWithdrawalRate,
    bool IncludeThirteenth,
    bool IncludeProportionalVacation,
    bool UnpaidVacationIgnored,
    IReadOnlyList<string> Warnings);

public static class TerminationBenefitCalculator
{
    public static TerminationBenefits Calculate(CalculatorInput input)
    {
        input = TerminationDateHelper.ApplyDates(input);
        var warnings = new List<string>();
        var salary = input.Amount;
        var (months, _) = TerminationTenureHelper.ResolveTenureMonths(input, warnings);
        months = Math.Clamp(months, 1, 240);
        var workedDays = input.SecondaryAmount <= 0 ? 15m : Math.Clamp(input.SecondaryAmount, 1m, 31m);
        var completeYears = input.CompleteYears > 0 ? input.CompleteYears : months / 12;
        var reason = input.TerminationReason;
        var isJustCause = TerminationReasonRules.IsJustCause(reason);

        var salaryBalance = salary / 30m * workedDays;
        var includeThirteenth = !isJustCause;
        var includeProportionalVacation = !isJustCause;

        var thirteenthMonths = ResolveThirteenthMonths(input, months, warnings);
        TerminationTenureHelper.AppendTenureWarnings(
            warnings,
            months,
            input.TerminationMonth,
            input.AdmissionMonth,
            thirteenthMonths);
        var thirteenth = includeThirteenth ? salary * thirteenthMonths / 12m : 0m;

        var completeVacationPeriods = months / 12;
        var proportionalVacationMonths = ResolveVacationProportionalMonths(input, months, includeProportionalVacation);
        var vacationBase = salary * proportionalVacationMonths / 12m;
        var vacationProportional = vacationBase + vacationBase / 3m;

        var unpaidVacationIgnored = input.HasUnpaidVacation && completeVacationPeriods == 0;
        if (unpaidVacationIgnored)
        {
            warnings.Add(
                "Férias vencidas só existem após períodos completos de 12 meses. Com menos de 12 meses, há apenas férias proporcionais.");
        }

        var unpaidVacationPeriods = input.HasUnpaidVacation && !unpaidVacationIgnored
            ? completeVacationPeriods
            : 0;
        var unpaidVacation = unpaidVacationPeriods > 0
            ? unpaidVacationPeriods * (salary + salary / 3m)
            : 0m;

        var noticeDays = ResolveNoticeDays(reason, completeYears);
        var payNoticeIndemnity = NoticePeriodResolver.ShouldPayIndemnifiedNotice(reason, input);
        var noticeIndemnity = payNoticeIndemnity && noticeDays > 0
            ? salary / 30m * noticeDays
            : 0m;

        var verbasBeforeNotice = salaryBalance + thirteenth + vacationProportional + unpaidVacation;
        var (noticeDeduction, noticeDeductionWasCapped) = ResolveNoticeDeduction(
            reason,
            input,
            salary,
            verbasBeforeNotice);

        if (noticeDeductionWasCapped)
        {
            warnings.Add(
                "O desconto por aviso prévio não pode ser maior que o total das verbas rescisórias devidas.");
        }

        var estimatedFgtsBalance = salary * months * 0.08m;
        var fgtsBase = input.FgtsBalance > 0m ? input.FgtsBalance : estimatedFgtsBalance;
        var fgtsFineRate = ResolveFgtsFineRate(reason);
        var fgtsFine = fgtsFineRate > 0m ? fgtsBase * fgtsFineRate : 0m;
        var fgtsWithdrawalRate = ResolveFgtsWithdrawalRate(reason);

        return new TerminationBenefits(
            salaryBalance,
            thirteenth,
            vacationProportional,
            unpaidVacation,
            unpaidVacationPeriods,
            proportionalVacationMonths,
            thirteenthMonths,
            noticeIndemnity,
            noticeDays,
            noticeDeduction,
            noticeDeductionWasCapped,
            fgtsFine,
            fgtsFineRate,
            fgtsBase,
            fgtsWithdrawalRate,
            includeThirteenth,
            includeProportionalVacation,
            unpaidVacationIgnored,
            warnings);
    }

    private static int ResolveThirteenthMonths(CalculatorInput input, int months, List<string> warnings)
    {
        if (input.MonthsWorkedInYear > 0)
        {
            return Math.Clamp(input.MonthsWorkedInYear, 1, 12);
        }

        if (input.AdmissionDate is not null && input.TerminationDate is not null)
        {
            return TerminationDateHelper.CountThirteenthAvos(
                input.AdmissionDate.Value,
                input.TerminationDate.Value);
        }

        if (input.TerminationDate is not null)
        {
            return TerminationDateHelper.CountMonthsWorkedInYear(input.AdmissionDate, input.TerminationDate.Value);
        }

        var terminationMonth = Math.Clamp(input.TerminationMonth, 0, 12);
        var admissionMonth = Math.Clamp(input.AdmissionMonth, 0, 12);

        if (terminationMonth > 0)
        {
            if (admissionMonth is >= 1 and <= 12)
            {
                return TerminationTenureHelper.CountThirteenthAvosInTerminationYear(
                    admissionMonth,
                    terminationMonth,
                    input.AdmissionInPriorYear);
            }

            return months > terminationMonth ? terminationMonth : months;
        }

        if (months > 12)
        {
            return 12;
        }

        warnings.Add(
            "Informe o mês da saída ou os meses trabalhados no ano da rescisão para calcular o 13º com precisão.");
        return months;
    }

    private static int ResolveVacationProportionalMonths(
        CalculatorInput input,
        int months,
        bool includeProportionalVacation)
    {
        if (!includeProportionalVacation || input.VacationTakenInCurrentPeriod)
        {
            return 0;
        }

        if (input.AdmissionDate is not null && input.TerminationDate is not null && months < 12)
        {
            return TerminationDateHelper.CalculateTenureMonths(
                input.AdmissionDate.Value,
                input.TerminationDate.Value);
        }

        var completePeriods = months / 12;
        var monthsInCurrentPeriod = months < 12 ? months : months % 12;

        if (monthsInCurrentPeriod == 0 && months >= 12)
        {
            monthsInCurrentPeriod = input.HasUnpaidVacation && completePeriods > 0 ? 0 : 12;
        }

        if (input.MonthsSinceLastVacation > 0)
        {
            monthsInCurrentPeriod = Math.Clamp(input.MonthsSinceLastVacation, 0, monthsInCurrentPeriod);
        }

        return monthsInCurrentPeriod;
    }

    private static (decimal Deduction, bool WasCapped) ResolveNoticeDeduction(
        TerminationReason reason,
        CalculatorInput input,
        decimal salary,
        decimal verbasBeforeNotice)
    {
        if (!TerminationReasonRules.AllowsNoticeDeduction(reason) || !NoticePeriodResolver.ShouldDeductOnResignation(input))
        {
            return (0m, false);
        }

        var maxDeduction = salary;
        if (verbasBeforeNotice <= 0m)
        {
            return (0m, false);
        }

        if (maxDeduction <= verbasBeforeNotice)
        {
            return (maxDeduction, false);
        }

        return (verbasBeforeNotice, true);
    }

    private static int ResolveNoticeDays(TerminationReason reason, int completeYears)
    {
        var fullNotice = Math.Min(30 + 3 * completeYears, 90);
        return reason switch
        {
            TerminationReason.DismissalWithoutCause or TerminationReason.ProbationContractEarlyEnd
                or TerminationReason.EmployerDeath => fullNotice,
            TerminationReason.MutualAgreement => fullNotice / 2,
            _ => 0
        };
    }

    private static decimal ResolveFgtsFineRate(TerminationReason reason) => ResolveFgtsFineRateForReason(reason);

    public static decimal ResolveFgtsFineRateForReason(TerminationReason reason) => reason switch
    {
        TerminationReason.DismissalWithoutCause or TerminationReason.ProbationContractEarlyEnd
            or TerminationReason.EmployerDeath => 0.40m,
        TerminationReason.MutualAgreement => 0.20m,
        _ => 0m
    };

    private static decimal ResolveFgtsWithdrawalRate(TerminationReason reason) => reason switch
    {
        TerminationReason.DismissalWithoutCause or TerminationReason.ProbationContractEarlyEnd
            or TerminationReason.EmployerDeath => 1.00m,
        TerminationReason.MutualAgreement => 0.80m,
        TerminationReason.Retirement => 1.00m,
        _ => 0m
    };

    public static string BuildExplanation(TerminationReason reason, TerminationBenefits benefits)
    {
        var baseText = reason switch
        {
            TerminationReason.DismissalWithoutCause =>
                "Demissão sem justa causa: aviso prévio indenizado, multa FGTS 40% e verbas proporcionais. INSS/IRRF apenas sobre saldo e 13º.",
            TerminationReason.MutualAgreement =>
                "Acordo comum (Art. 484-A): 50% do aviso prévio, multa FGTS 20%, saque de 80% do FGTS. Sem seguro-desemprego.",
            TerminationReason.DismissalForCause =>
                "Justa causa: direito ao saldo de salário e férias vencidas (se houver período completo de 12 meses). Sem 13º proporcional nem férias do período incompleto.",
            TerminationReason.Resignation when benefits.NoticeDeduction > 0m =>
                "Pedido de demissão sem aviso: desconto de até 30 dias, limitado ao total das verbas. Sem multa FGTS. Férias e 13º proporcionais conforme tempo na empresa.",
            TerminationReason.Resignation =>
                "Pedido de demissão com aviso cumprido: verbas proporcionais sem multa FGTS. Férias isentas de INSS/IRRF na rescisão.",
            TerminationReason.ProbationContractCompleted =>
                "Término de contrato de experiência no prazo: verbas proporcionais como pedido de demissão, sem multa FGTS.",
            TerminationReason.ProbationContractEarlyEnd =>
                "Rescisão antecipada do contrato de experiência pelo empregador: regras próximas à demissão sem justa causa (multa FGTS 40%).",
            TerminationReason.Retirement =>
                "Aposentadoria: verbas proporcionais e saque do FGTS (sem multa de 40%). Confirme regras no RH ou Caixa.",
            TerminationReason.EmployerDeath =>
                "Falecimento do empregador (pessoa física): verbas e multa FGTS 40% para dependentes, semelhante à demissão sem justa causa.",
            TerminationReason.FixedTermContractEnd =>
                "Término de contrato por prazo determinado: verbas proporcionais sem multa FGTS, como pedido de demissão.",
            _ => "Estimativa de verbas rescisórias conforme tipo de desligamento informado."
        };

        if (benefits.Warnings.Count == 0)
        {
            return baseText;
        }

        return baseText + " " + string.Join(" ", benefits.Warnings);
    }
}
