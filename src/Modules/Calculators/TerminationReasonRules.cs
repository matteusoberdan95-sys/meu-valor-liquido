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
        TerminationReason.FixedTermContractEnd => true,
        _ => false
    };

    public static bool IsDismissalWithoutCauseLike(TerminationReason reason) => reason switch
    {
        TerminationReason.DismissalWithoutCause => true,
        TerminationReason.ProbationContractEarlyEnd => true,
        TerminationReason.EmployerDeath => true,
        _ => false
    };

    public static bool AllowsNoticeDeduction(TerminationReason reason) =>
        IsResignationLike(reason);

    public static bool AllowsNoticeIndemnity(TerminationReason reason) =>
        IsDismissalWithoutCauseLike(reason) || reason == TerminationReason.MutualAgreement;

    public static bool AllowsFgtsFine(TerminationReason reason) =>
        reason is TerminationReason.DismissalWithoutCause or TerminationReason.MutualAgreement
            or TerminationReason.ProbationContractEarlyEnd or TerminationReason.EmployerDeath;

    public static bool AllowsFgtsWithdrawalWithoutFine(TerminationReason reason) => reason switch
    {
        TerminationReason.DismissalWithoutCause or TerminationReason.ProbationContractEarlyEnd
            or TerminationReason.EmployerDeath => true,
        TerminationReason.MutualAgreement => true,
        TerminationReason.Retirement => true,
        _ => false
    };
}
