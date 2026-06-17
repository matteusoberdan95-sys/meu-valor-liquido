namespace MeuValorLiquido.Modules.Calculators;

public enum NoticePeriodOption
{
    [Display(Name = "Automático")]
    Automatic = 0,

    [Display(Name = "Trabalhado")]
    Worked,

    [Display(Name = "Indenizado pelo empregador")]
    IndemnifiedByEmployer,

    [Display(Name = "Não cumprido pelo empregado")]
    NotFulfilledByEmployee,

    [Display(Name = "Dispensado")]
    Waived
}

public static class NoticePeriodResolver
{
    public static bool ShouldDeductOnResignation(CalculatorInput input) =>
        input.NoticePeriod switch
        {
            NoticePeriodOption.NotFulfilledByEmployee => true,
            NoticePeriodOption.Worked or NoticePeriodOption.Waived or NoticePeriodOption.IndemnifiedByEmployer => false,
            _ => !input.CompletedNoticePeriod
        };

    public static bool ShouldPayIndemnifiedNotice(TerminationReason reason, CalculatorInput input)
    {
        if (!TerminationReasonRules.AllowsNoticeIndemnity(reason))
        {
            return false;
        }

        return input.NoticePeriod switch
        {
            NoticePeriodOption.Worked or NoticePeriodOption.Waived => false,
            NoticePeriodOption.IndemnifiedByEmployer => true,
            NoticePeriodOption.NotFulfilledByEmployee => false,
            _ => true
        };
    }
}
