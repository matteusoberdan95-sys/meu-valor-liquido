namespace MeuValorLiquido.Modules.Calculators;

using MeuValorLiquido.Modules.Calculators.Tax;

public sealed record UnemploymentInsuranceResult(
    bool IsEligible,
    string EligibilitySummary,
    decimal AverageSalary,
    decimal MonthlyBenefit,
    int InstallmentCount,
    decimal TotalBenefit);

public static class UnemploymentInsuranceCalculator
{
  private static readonly HashSet<TerminationReason> EligibleReasons =
  [
      TerminationReason.DismissalWithoutCause,
      TerminationReason.FixedTermContractEnd,
      TerminationReason.ProbationContractCompleted,
      TerminationReason.EmployerDeath
  ];

  public static UnemploymentInsuranceResult Calculate(CalculatorInput input)
  {
    var previousRequests = Math.Clamp(input.CompleteYears, 0, 2);
    var qualifyingMonths = input.MonthsWorkedInYear > 0
        ? input.MonthsWorkedInYear
        : RequiredQualifyingMonths(previousRequests);
    var monthsInLast36 = Math.Clamp(input.Months, 1, 36);
    var averageSalary = ResolveAverageSalary(input);
    var monthlyBenefit = BrUnemploymentInsuranceTables2026.CalculateMonthlyBenefit(averageSalary);
    var installmentCount = BrUnemploymentInsuranceTables2026.ResolveInstallmentCount(monthsInLast36);
    var requiredMonths = BrUnemploymentInsuranceTables2026.RequiredQualifyingMonths(previousRequests);

    if (!EligibleReasons.Contains(input.TerminationReason))
    {
      return Ineligible(
          averageSalary,
          monthlyBenefit,
          "Motivo de desligamento sem direito ao seguro-desemprego na regra geral (ex.: pedido de demissão, acordo 484-A ou justa causa).");
    }

    if (qualifyingMonths < requiredMonths)
    {
      return Ineligible(
          averageSalary,
          monthlyBenefit,
          $"Tempo de carteira insuficiente: exige pelo menos {requiredMonths} meses para a {(previousRequests + 1)}ª solicitação (informados {qualifyingMonths}).");
    }

    if (installmentCount == 0)
    {
      return Ineligible(
          averageSalary,
          monthlyBenefit,
          "Menos de 6 meses com carteira nos últimos 36 meses — em geral não há direito às parcelas.");
    }

    var total = decimal.Round(monthlyBenefit * installmentCount, 2, MidpointRounding.AwayFromZero);
    return new UnemploymentInsuranceResult(
        true,
        $"Direito estimado às {installmentCount} parcelas (demissão sem justa causa ou término elegível, com carência atendida).",
        averageSalary,
        monthlyBenefit,
        installmentCount,
        total);
  }

  private static int RequiredQualifyingMonths(int previousRequests) =>
      BrUnemploymentInsuranceTables2026.RequiredQualifyingMonths(previousRequests);

  private static decimal ResolveAverageSalary(CalculatorInput input)
  {
    var salaries = new List<decimal> { input.Amount };
    if (input.SecondaryAmount > 0m)
    {
      salaries.Add(input.SecondaryAmount);
    }

    if (input.SalaryAverageSupplement > 0m)
    {
      salaries.Add(input.SalaryAverageSupplement);
    }

    return decimal.Round(salaries.Average(), 2, MidpointRounding.AwayFromZero);
  }

  private static UnemploymentInsuranceResult Ineligible(
      decimal averageSalary,
      decimal monthlyBenefit,
      string summary) =>
      new(false, summary, averageSalary, monthlyBenefit, 0, 0m);
}
