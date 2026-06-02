namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorsModule
{
    public static IServiceCollection AddCalculatorsModule(this IServiceCollection services)
    {
        services.AddSingleton<ICalculatorCatalogService, CalculatorCatalogService>();
        services.AddSingleton<ICalculatorApplicationService, CalculatorApplicationService>();
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
    IReadOnlyList<FaqItem> FaqItems);

public sealed record FaqItem(string Question, string Answer);

public sealed record CalculatorInput(
    decimal Amount,
    decimal SecondaryAmount = 0m,
    int Months = 12,
    decimal Rate = 0m,
    decimal Hours = 0m,
    int Dependents = 0,
    decimal TransportDiscount = 0m);

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

public sealed class CalculatorCatalogService : ICalculatorCatalogService
{
    private static readonly IReadOnlyList<CalculatorDefinition> Calculators =
    [
        Create("salario-liquido", "Salário líquido", "Trabalhista", "Estime quanto sobra do salário após INSS, IRRF e descontos comuns."),
        Create("ferias", "Férias", "Trabalhista", "Calcule uma estimativa de férias com adicional constitucional de um terço."),
        Create("decimo-terceiro", "Décimo terceiro", "Trabalhista", "Estime o décimo terceiro proporcional ou integral com descontos."),
        Create("rescisao-clt", "Rescisão CLT", "Trabalhista", "Simule uma rescisão CLT com saldo, proporcionais e multa estimada de FGTS."),
        Create("hora-extra", "Hora extra", "Trabalhista", "Calcule o valor estimado de horas extras com percentual adicional."),
        Create("inss", "INSS", "Fiscal", "Estime o desconto de INSS pela tabela progressiva usada no MVP."),
        Create("irrf", "IRRF", "Fiscal", "Estime o imposto de renda retido na fonte a partir da base informada."),
        Create("pj-vs-clt", "PJ vs CLT", "Financeiro", "Compare uma estimativa simples entre remuneração PJ e salário líquido CLT."),
        Create("juros-compostos", "Juros compostos", "Financeiro", "Projete o crescimento de um valor com taxa mensal e prazo."),
        Create("financiamento", "Financiamento", "Financeiro", "Estime uma parcela fixa usando a fórmula Price.")
    ];

    public IReadOnlyList<CalculatorDefinition> GetAll() => Calculators;

    public CalculatorDefinition? GetBySlug(string slug)
    {
        return Calculators.FirstOrDefault(calculator => calculator.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
    }

    private static CalculatorDefinition Create(string slug, string name, string category, string summary)
    {
        return new CalculatorDefinition(
            slug,
            name,
            category,
            summary,
            $"{name}: calculadora online | Meu Valor Líquido",
            $"{summary} Resultado em formato de extrato, com explicação simples e aviso legal.",
            [
                new FaqItem($"A calculadora de {name.ToLowerInvariant()} é oficial?", "Não. Ela oferece uma estimativa educativa e não substitui orientação jurídica, contábil ou financeira."),
                new FaqItem("Os dados são salvos?", "No MVP, os cálculos pessoais não são persistidos. O histórico fica previsto para uma fase futura com autenticação.")
            ]);
    }
}

public sealed class CalculatorApplicationService : ICalculatorApplicationService
{
    private const string Disclaimer = "Resultado estimado para fins educativos. Não substitui orientação jurídica, contábil, financeira ou conferência oficial.";
    private readonly ICalculatorCatalogService catalogService;
    private readonly IValidator<CalculatorInput> validator;

    public CalculatorApplicationService(ICalculatorCatalogService catalogService, IValidator<CalculatorInput> validator)
    {
        this.catalogService = catalogService;
        this.validator = validator;
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

        var result = slug.ToLowerInvariant() switch
        {
            "salario-liquido" => CalculateNetSalary(definition, input),
            "ferias" => CalculateVacation(definition, input),
            "decimo-terceiro" => CalculateThirteenthSalary(definition, input),
            "rescisao-clt" => CalculateTermination(definition, input),
            "hora-extra" => CalculateOvertime(definition, input),
            "inss" => CalculateInssOnly(definition, input),
            "irrf" => CalculateIrrfOnly(definition, input),
            "pj-vs-clt" => CalculatePjVsClt(definition, input),
            "juros-compostos" => CalculateCompoundInterest(definition, input),
            "financiamento" => CalculateFinancing(definition, input),
            _ => null
        };

        return result is null
            ? Result<CalculationResult>.Failure(new Error("Calculators.NotImplemented", "Calculadora ainda não implementada."))
            : Result<CalculationResult>.Success(result);
    }

    private static CalculationResult CalculateNetSalary(CalculatorDefinition definition, CalculatorInput input)
    {
        var gross = input.Amount;
        var inss = CalculateInss(gross);
        var irrf = CalculateIrrf(gross - inss, input.Dependents);
        var transport = Math.Min(input.TransportDiscount, gross);
        var net = gross - inss - irrf - transport;

        return Build(definition, gross, net,
        [
            Discount("INSS", inss),
            Discount("IRRF", irrf),
            Discount("Vale-transporte/outros descontos", transport)
        ], "O salário líquido considera os descontos estimados de INSS, IRRF e descontos informados.");
    }

    private static CalculationResult CalculateVacation(CalculatorDefinition definition, CalculatorInput input)
    {
        var salary = input.Amount;
        var vacationBonus = salary / 3m;
        var gross = salary + vacationBonus;
        var inss = CalculateInss(gross);
        var irrf = CalculateIrrf(gross - inss, input.Dependents);
        var net = gross - inss - irrf;

        return Build(definition, gross, net,
        [
            Income("Adicional de 1/3", vacationBonus),
            Discount("INSS estimado", inss),
            Discount("IRRF estimado", irrf)
        ], "A estimativa soma o salário de férias ao adicional de um terço e aplica descontos aproximados.");
    }

    private static CalculationResult CalculateThirteenthSalary(CalculatorDefinition definition, CalculatorInput input)
    {
        var months = Math.Clamp(input.Months, 1, 12);
        var gross = input.Amount * months / 12m;
        var inss = CalculateInss(gross);
        var irrf = CalculateIrrf(gross - inss, input.Dependents);
        var net = gross - inss - irrf;

        return Build(definition, gross, net,
        [
            Information("Meses considerados", months),
            Discount("INSS estimado", inss),
            Discount("IRRF estimado", irrf)
        ], "O décimo terceiro é proporcional aos meses informados e recebe descontos estimados.");
    }

    private static CalculationResult CalculateTermination(CalculatorDefinition definition, CalculatorInput input)
    {
        var salary = input.Amount;
        var months = Math.Clamp(input.Months, 1, 240);
        var workedDays = input.SecondaryAmount <= 0 ? 15m : Math.Clamp(input.SecondaryAmount, 1m, 30m);
        var salaryBalance = salary / 30m * workedDays;
        var thirteenth = salary * Math.Min(months, 12) / 12m;
        var vacation = thirteenth + thirteenth / 3m;
        var fgtsFine = salary * months * 0.08m * 0.40m;
        var gross = salaryBalance + thirteenth + vacation + fgtsFine;
        var inss = CalculateInss(salaryBalance + thirteenth + vacation);
        var net = gross - inss;

        return Build(definition, gross, net,
        [
            Income("Saldo de salário", salaryBalance),
            Income("13º proporcional", thirteenth),
            Income("Férias proporcionais + 1/3", vacation),
            Income("Multa FGTS estimada", fgtsFine),
            Discount("INSS estimado", inss)
        ], "A rescisão usa premissas simplificadas para saldo, proporcionais e multa de FGTS.");
    }

    private static CalculationResult CalculateOvertime(CalculatorDefinition definition, CalculatorInput input)
    {
        var hourlyRate = input.Amount;
        var hours = input.Hours <= 0 ? input.SecondaryAmount : input.Hours;
        var additionalRate = input.Rate <= 0 ? 50m : input.Rate;
        var total = hourlyRate * hours * (1m + additionalRate / 100m);

        return Build(definition, hourlyRate * hours, total,
        [
            Income("Adicional de hora extra", total - hourlyRate * hours),
            Information("Horas consideradas", hours)
        ], "O cálculo multiplica o valor da hora pela quantidade de horas e pelo adicional informado.");
    }

    private static CalculationResult CalculateInssOnly(CalculatorDefinition definition, CalculatorInput input)
    {
        var inss = CalculateInss(input.Amount);
        return Build(definition, input.Amount, input.Amount - inss,
        [
            Discount("INSS estimado", inss)
        ], "O desconto é calculado de forma progressiva pelas faixas parametrizadas no MVP.");
    }

    private static CalculationResult CalculateIrrfOnly(CalculatorDefinition definition, CalculatorInput input)
    {
        var irrf = CalculateIrrf(input.Amount, input.Dependents);
        return Build(definition, input.Amount, input.Amount - irrf,
        [
            Discount("IRRF estimado", irrf)
        ], "O IRRF considera a base informada e a dedução simplificada por dependente.");
    }

    private static CalculationResult CalculatePjVsClt(CalculatorDefinition definition, CalculatorInput input)
    {
        var cltGross = input.Amount;
        var pjGross = input.SecondaryAmount <= 0 ? input.Amount * 1.3m : input.SecondaryAmount;
        var cltNet = cltGross - CalculateInss(cltGross) - CalculateIrrf(cltGross - CalculateInss(cltGross), input.Dependents);
        var pjNet = pjGross * 0.86m;
        var difference = pjNet - cltNet;

        return Build(definition, Math.Max(cltGross, pjGross), Math.Max(cltNet, pjNet),
        [
            Information("CLT líquido estimado", cltNet),
            Information("PJ líquido estimado", pjNet),
            Information("Diferença estimada", difference)
        ], "A comparação usa uma retenção PJ simplificada de 14% e salário CLT líquido estimado.");
    }

    private static CalculationResult CalculateCompoundInterest(CalculatorDefinition definition, CalculatorInput input)
    {
        var months = Math.Clamp(input.Months, 1, 600);
        var rate = (double)(input.Rate / 100m);
        var finalAmount = input.Amount * (decimal)Math.Pow(1d + rate, months);
        var interest = finalAmount - input.Amount;

        return Build(definition, input.Amount, finalAmount,
        [
            Income("Juros acumulados", interest),
            Information("Meses considerados", months)
        ], "O valor final usa capitalização composta mensal pela taxa e prazo informados.");
    }

    private static CalculationResult CalculateFinancing(CalculatorDefinition definition, CalculatorInput input)
    {
        var months = Math.Clamp(input.Months, 1, 600);
        var monthlyRate = input.Rate / 100m;
        var payment = monthlyRate == 0
            ? input.Amount / months
            : input.Amount * monthlyRate / (1m - (decimal)Math.Pow((double)(1m + monthlyRate), -months));
        var total = payment * months;

        return Build(definition, input.Amount, payment,
        [
            Information("Parcela estimada", payment),
            Information("Total pago estimado", total),
            Information("Juros totais estimados", total - input.Amount)
        ], "A parcela estimada usa a fórmula Price com taxa mensal e prazo informados.");
    }

    private static CalculationResult Build(
        CalculatorDefinition definition,
        decimal gross,
        decimal net,
        IReadOnlyList<CalculationLineItem> lines,
        string explanation)
    {
        return new CalculationResult(
            definition.Slug,
            definition.Name,
            Money.From(gross),
            lines,
            Money.From(net),
            explanation,
            Disclaimer);
    }

    private static CalculationLineItem Income(string label, decimal amount) => new(label, Money.From(amount), CalculationLineType.Income);

    private static CalculationLineItem Discount(string label, decimal amount) => new(label, Money.From(amount), CalculationLineType.Discount);

    private static CalculationLineItem Information(string label, decimal amount) => new(label, Money.From(amount), CalculationLineType.Information);

    private static decimal CalculateInss(decimal salary)
    {
        var first = ProgressiveTaxSlice(salary, 0m, 1518m, 0.075m);
        var second = ProgressiveTaxSlice(salary, 1518.01m, 2793.88m, 0.09m);
        var third = ProgressiveTaxSlice(salary, 2793.89m, 4190.83m, 0.12m);
        var fourth = ProgressiveTaxSlice(salary, 4190.84m, 8157.41m, 0.14m);
        return decimal.Round(first + second + third + fourth, 2, MidpointRounding.AwayFromZero);
    }

    private static decimal CalculateIrrf(decimal basis, int dependents)
    {
        var dependentDeduction = dependents * 189.59m;
        var taxable = Math.Max(0m, basis - dependentDeduction);
        var (rate, deduction) = taxable switch
        {
            <= 2259.20m => (0m, 0m),
            <= 2826.65m => (0.075m, 169.44m),
            <= 3751.05m => (0.15m, 381.44m),
            <= 4664.68m => (0.225m, 662.77m),
            _ => (0.275m, 896m)
        };

        return decimal.Round(Math.Max(0m, taxable * rate - deduction), 2, MidpointRounding.AwayFromZero);
    }

    private static decimal ProgressiveTaxSlice(decimal amount, decimal from, decimal to, decimal rate)
    {
        if (amount <= from)
        {
            return 0m;
        }

        return (Math.Min(amount, to) - from) * rate;
    }
}
