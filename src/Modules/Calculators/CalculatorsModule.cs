using MeuValorLiquido.Modules.Calculators.Tax;

using System.ComponentModel.DataAnnotations;

namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorsModule
{
    public static IServiceCollection AddCalculatorsModule(this IServiceCollection services)
    {
        services.AddScoped<IInssCalculator, InssCalculator>();
        services.AddScoped<IIrrfCalculator, IrrfCalculator>();
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
    Resignation
}

public sealed record CalculatorInput(
    decimal Amount,
    decimal SecondaryAmount = 0m,
    int Months = 12,
    decimal Rate = 0m,
    decimal Hours = 0m,
    int Dependents = 0,
    decimal TransportDiscount = 0m,
    TerminationReason TerminationReason = TerminationReason.DismissalWithoutCause,
    bool CompletedNoticePeriod = false);

public sealed record CalculationLineItem(string Label, Money Amount, CalculationLineType Type);

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

        var validation = validator.Validate(input);
        if (!validation.IsValid)
        {
            return Result<CalculationResult>.Failure(new Error("Calculators.InvalidInput", validation.Errors[0].ErrorMessage));
        }

        var result = engine.Calculate(definition, input);
        return result is null
            ? Result<CalculationResult>.Failure(new Error("Calculators.NotImplemented", "Calculadora ainda não implementada."))
            : Result<CalculationResult>.Success(result);
    }
}
