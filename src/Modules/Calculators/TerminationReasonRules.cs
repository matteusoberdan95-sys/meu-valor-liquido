namespace MeuValorLiquido.Modules.Calculators;

public static class TerminationReasonRules
{
    public static bool IsJustCause(TerminationReason reason) =>
        reason == TerminationReason.DismissalForCause;

    public static bool IsResignationLike(TerminationReason reason) => reason switch
    {
        TerminationReason.Resignation => true,
        TerminationReason.ProbationContractCompleted => true,
        TerminationReason.Retirement => true,
        _ => false
    };

    public static bool IsDismissalWithoutCauseLike(TerminationReason reason) => reason switch
    {
        TerminationReason.DismissalWithoutCause => true,
        TerminationReason.ProbationContractEarlyEnd => true,
        _ => false
    };

    public static bool AllowsNoticeDeduction(TerminationReason reason) =>
        IsResignationLike(reason);

    public static bool AllowsNoticeIndemnity(TerminationReason reason) =>
        IsDismissalWithoutCauseLike(reason) || reason == TerminationReason.MutualAgreement;

    public static bool AllowsFgtsFine(TerminationReason reason) =>
        reason is TerminationReason.DismissalWithoutCause or TerminationReason.MutualAgreement
            or TerminationReason.ProbationContractEarlyEnd;

    public static bool AllowsFgtsWithdrawalWithoutFine(TerminationReason reason) => reason switch
    {
        TerminationReason.DismissalWithoutCause => true,
        TerminationReason.MutualAgreement => true,
        TerminationReason.Retirement => true,
        _ => false
    };
}
