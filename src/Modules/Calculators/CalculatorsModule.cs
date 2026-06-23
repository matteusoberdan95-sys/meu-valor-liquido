namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorsModule
{
    public static IServiceCollection AddCalculatorsModule(this IServiceCollection services)
    {
        services.AddScoped<IInssCalculator, InssCalculator>();
        services.AddScoped<IProLaboreInssCalculator, ProLaboreInssCalculator>();
        services.AddScoped<IIrrfCalculator, IrrfCalculator>();
        services.AddScoped<ITerminationTaxCalculator, TerminationTaxCalculator>();
        services.AddScoped<NetSalaryCalculator>();
        services.AddScoped<IPayslipValidationService, PayslipValidationService>();
        services.AddScoped<CltPjComparisonCalculator>();
        services.AddScoped<CalculationEngine>();
        services.AddSingleton<ICalculatorFieldProfileProvider, CalculatorFieldProfileProvider>();
        services.AddScoped<ICalculatorApplicationService, CalculatorApplicationService>();
        services.AddSingleton<IValidator<CalculatorInput>, CalculatorInputValidator>();
        return services;
    }
}

public sealed record CalculatorDefinition(
    string Slug,
    string Name,
    string Category,
    string Summary,
    string SeoTitle,
    string SeoDescription,
    IReadOnlyList<FaqItem> FaqItems,
    string? EducationalContent = null);

public sealed record FaqItem(string Question, string Answer);

public enum TerminationReason
{
    [Display(Name = "Demitido sem justa causa")]
    DismissalWithoutCause,

    [Display(Name = "Pediu demissão")]
    Resignation,

    [Display(Name = "Acordo comum (Art. 484-A)")]
    MutualAgreement,

    [Display(Name = "Demissão por justa causa")]
    DismissalForCause,

    [Display(Name = "Término de contrato de experiência (no prazo)")]
    ProbationContractCompleted,

    [Display(Name = "Término de contrato de experiência (antes do prazo)")]
    ProbationContractEarlyEnd,

    [Display(Name = "Aposentadoria")]
    Retirement,

    [Display(Name = "Falecimento do empregador")]
    EmployerDeath,

    [Display(Name = "Término de contrato por prazo determinado")]
    FixedTermContractEnd
}

public sealed record CalculatorInput(
    decimal Amount,
    decimal SecondaryAmount = 0m,
    int Months = 12,
    decimal Rate = 0m,
    decimal Hours = 0m,
    int Dependents = 0,
    decimal TransportDiscount = 0m,
    decimal MealVoucherDiscount = 0m,
    decimal HealthPlanDiscount = 0m,
    decimal AlimonyAmount = 0m,
    decimal AlimonyPercent = 0m,
    decimal OtherDiscounts = 0m,
    TerminationReason TerminationReason = TerminationReason.DismissalWithoutCause,
    bool CompletedNoticePeriod = false,
    int CompleteYears = 0,
    bool HasUnpaidVacation = false,
    decimal FgtsBalance = 0m,
    bool VacationTakenInCurrentPeriod = false,
    int MonthsSinceLastVacation = 0,
    int MonthsWorkedInYear = 0,
    int TerminationMonth = 0,
    int AdmissionMonth = 0,
    bool AdmissionInPriorYear = false,
    DateOnly? AdmissionDate = null,
    DateOnly? TerminationDate = null,
    NoticePeriodOption NoticePeriod = NoticePeriodOption.Automatic,
    int WeeklyWorkHours = 0,
    OvertimeShiftType OvertimeShiftType = OvertimeShiftType.Weekday,
    SalaryConversionBasis SalaryBasis = SalaryConversionBasis.Monthly,
    MeiActivityType MeiActivity = MeiActivityType.CommerceOrIndustry,
    FinancingAmortizationSystem FinancingAmortization = FinancingAmortizationSystem.Price,
    bool IrrfFromGrossSalary = false,
    VacationDayOption VacationDayOption = VacationDayOption.Automatic,
    bool SellVacationAllowance = false,
    bool DoubleVacationPayment = false,
    decimal ThirteenthAdvancePaid = 0m,
    decimal SalaryAverageSupplement = 0m,
    SimplesAnnex SimplesAnnex = SimplesAnnex.AnnexThree,
    decimal ProLaborePercent = 0m,
    decimal MeiAnnualAccumulated = 0m);

public sealed record CalculationLineItem(string Label, Money Amount, CalculationLineType Type, string? DisplayText = null);

public enum CalculationLineType
{
    Income,
    Discount,
    Information
}

public sealed record CalculationResult(
    string Slug,
    string Title,
    Money GrossAmount,
    IReadOnlyList<CalculationLineItem> LineItems,
    Money EstimatedNetAmount,
    string Explanation,
    string LegalDisclaimer);

public interface ICalculatorCatalogService
{
    IReadOnlyList<CalculatorDefinition> GetAll();

    CalculatorDefinition? GetBySlug(string slug);
}

public interface ICalculatorApplicationService
{
    Result<CalculationResult> Calculate(string slug, CalculatorInput input);
}

public sealed class CalculatorInputValidator : AbstractValidator<CalculatorInput>
{
    public CalculatorInputValidator()
    {
        RuleFor(input => input.Amount)
            .GreaterThan(0)
            .WithMessage("Informe um valor maior que zero.");

        RuleFor(input => input.Months)
            .InclusiveBetween(1, 600)
            .WithMessage("Informe uma quantidade de meses válida.");

        RuleFor(input => input.Rate)
            .InclusiveBetween(0, 1000)
            .WithMessage("Informe uma taxa válida.");

        RuleFor(input => input.Dependents)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Informe uma quantidade de dependentes válida.");

        RuleFor(input => input.TransportDiscount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O desconto de vale-transporte não pode ser negativo.");

        RuleFor(input => input.SecondaryAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O valor secundário não pode ser negativo.");

        RuleFor(input => input.MealVoucherDiscount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O desconto de vale-refeição/alimentação não pode ser negativo.");

        RuleFor(input => input.HealthPlanDiscount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O desconto do plano de saúde não pode ser negativo.");

        RuleFor(input => input.AlimonyAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O valor da pensão alimentícia não pode ser negativo.");

        RuleFor(input => input.AlimonyPercent)
            .InclusiveBetween(0, 100)
            .WithMessage("Informe um percentual de pensão entre 0 e 100.");

        RuleFor(input => input.OtherDiscounts)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Outros descontos não podem ser negativos.");

        RuleFor(input => input.ThirteenthAdvancePaid)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O adiantamento do 13º não pode ser negativo.");

        RuleFor(input => input.SalaryAverageSupplement)
            .GreaterThanOrEqualTo(0)
            .WithMessage("A média salarial complementar não pode ser negativa.");

        RuleFor(input => input.ProLaborePercent)
            .InclusiveBetween(0, 100)
            .WithMessage("Informe um percentual de pró-labore entre 0 e 100.");

        RuleFor(input => input.MeiAnnualAccumulated)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O faturamento acumulado no ano não pode ser negativo.");

        RuleFor(input => input.MonthsSinceLastVacation)
            .InclusiveBetween(0, 12)
            .WithMessage("Informe meses válidos desde a última férias (0 a 12).");

        RuleFor(input => input.WeeklyWorkHours)
            .InclusiveBetween(0, 44)
            .WithMessage("Informe uma jornada semanal válida (0 = 44h padrão).");

        RuleFor(input => input.TerminationMonth)
            .InclusiveBetween(0, 12)
            .WithMessage("Informe o mês da saída (1 a 12) ou deixe 0.");

        RuleFor(input => input.AdmissionMonth)
            .InclusiveBetween(0, 12)
            .WithMessage("Informe o mês de admissão (1 a 12) ou deixe 0.");

        RuleFor(input => input)
            .Must(input => input.AdmissionDate is null
                || input.TerminationDate is null
                || input.TerminationDate >= input.AdmissionDate)
            .WithMessage("A data de afastamento deve ser igual ou posterior à data de admissão.");
    }
}

public sealed class CalculatorApplicationService : ICalculatorApplicationService
{
    private readonly ICalculatorCatalogService catalogService;
    private readonly IValidator<CalculatorInput> validator;
    private readonly CalculationEngine engine;

    public CalculatorApplicationService(
        ICalculatorCatalogService catalogService,
        IValidator<CalculatorInput> validator,
        CalculationEngine engine)
    {
        this.catalogService = catalogService;
        this.validator = validator;
        this.engine = engine;
    }

    public Result<CalculationResult> Calculate(string slug, CalculatorInput input)
    {
        var definition = catalogService.GetBySlug(slug);
        if (definition is null)
        {
            return Result<CalculationResult>.Failure(new Error("Calculators.NotFound", "Calculadora não encontrada."));
        }

        input = CalculatorInputNormalizer.Normalize(slug, input);

        var validation = validator.Validate(input);
        if (!validation.IsValid)
        {
            return Result<CalculationResult>.Failure(new Error("Calculators.InvalidInput", validation.Errors[0].ErrorMessage));
        }

        if (slug.Equals("proposta-salarial", StringComparison.OrdinalIgnoreCase) && input.SecondaryAmount <= 0m)
        {
            return Result<CalculationResult>.Failure(
                new Error("Calculators.InvalidInput", "Informe o salário bruto proposto."));
        }

        var result = engine.Calculate(definition, input);
        return result is null
            ? Result<CalculationResult>.Failure(new Error("Calculators.NotImplemented", "Calculadora ainda não implementada."))
            : Result<CalculationResult>.Success(result);
    }
}
