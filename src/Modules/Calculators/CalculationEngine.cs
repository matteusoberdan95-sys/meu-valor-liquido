using MeuValorLiquido.Modules.Calculators.Tax;

namespace MeuValorLiquido.Modules.Calculators;

public sealed class CalculationEngine
{
    private static readonly string Disclaimer =
        "Resultado estimado para fins educativos (tabelas INSS/IRRF " + BrTaxTables2026.Year +
        "). Não substitui orientação jurídica, contábil, financeira ou conferência oficial.";

    private readonly IInssCalculator inssCalculator;
    private readonly IIrrfCalculator irrfCalculator;

    public CalculationEngine(IInssCalculator inssCalculator, IIrrfCalculator irrfCalculator)
    {
        this.inssCalculator = inssCalculator;
        this.irrfCalculator = irrfCalculator;
    }

    public CalculationResult? Calculate(CalculatorDefinition definition, CalculatorInput input)
    {
        return definition.Slug.ToLowerInvariant() switch
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
    }

    private CalculationResult CalculateNetSalary(CalculatorDefinition definition, CalculatorInput input)
    {
        var gross = input.Amount;
        var inss = inssCalculator.Calculate(gross);
        var irrf = irrfCalculator.Calculate(gross - inss, input.Dependents);
        var transport = Math.Min(input.TransportDiscount, gross);
        var net = gross - inss - irrf - transport;

        return Build(definition, gross, net,
        [
            Discount("INSS", inss),
            Discount("IRRF", irrf),
            Discount("Vale-transporte/outros descontos", transport)
        ], "Salário líquido com base nas tabelas progressivas de INSS e IRRF de " + BrTaxTables2026.Year + ".");
    }

    private CalculationResult CalculateVacation(CalculatorDefinition definition, CalculatorInput input)
    {
        var salary = input.Amount;
        var vacationBonus = salary / 3m;
        var gross = salary + vacationBonus;
        var inss = inssCalculator.Calculate(gross);
        var irrf = irrfCalculator.Calculate(gross - inss, input.Dependents);
        var net = gross - inss - irrf;

        return Build(definition, gross, net,
        [
            Income("Adicional de 1/3", vacationBonus),
            Discount("INSS estimado", inss),
            Discount("IRRF estimado", irrf)
        ], "Férias com adicional de um terço e descontos estimados (" + BrTaxTables2026.Year + ").");
    }

    private CalculationResult CalculateThirteenthSalary(CalculatorDefinition definition, CalculatorInput input)
    {
        var months = Math.Clamp(input.Months, 1, 12);
        var gross = input.Amount * months / 12m;
        var inss = inssCalculator.Calculate(gross);
        var irrf = irrfCalculator.Calculate(gross - inss, input.Dependents);
        var net = gross - inss - irrf;

        return Build(definition, gross, net,
        [
            Information("Meses considerados", months),
            Discount("INSS estimado", inss),
            Discount("IRRF estimado", irrf)
        ], "Décimo terceiro proporcional aos meses informados.");
    }

    private CalculationResult CalculateTermination(CalculatorDefinition definition, CalculatorInput input)
    {
        var salary = input.Amount;
        var months = Math.Clamp(input.Months, 1, 240);
        var workedDays = input.SecondaryAmount <= 0 ? 15m : Math.Clamp(input.SecondaryAmount, 1m, 31m);
        var proportionalMonths = Math.Min(months, 12);
        var proportionalBase = salary * proportionalMonths / 12m;

        var salaryBalance = salary / 30m * workedDays;
        var thirteenth = proportionalBase;
        var vacation = proportionalBase + proportionalBase / 3m;
        var fgtsBalance = salary * proportionalMonths * 0.08m;
        var fgtsFine = fgtsBalance * 0.40m;
        var isDismissal = input.TerminationReason == TerminationReason.DismissalWithoutCause;
        var noticeDeduction = !isDismissal && !input.CompletedNoticePeriod ? salary : 0m;

        var totalVerbas = salaryBalance + thirteenth + vacation + (isDismissal ? fgtsFine : 0m);
        var inss = inssCalculator.Calculate(salaryBalance + thirteenth + vacation);
        var net = totalVerbas - inss - noticeDeduction;

        var lines = new List<CalculationLineItem>
        {
            Income("Saldo de salário", salaryBalance),
            Income("13º proporcional", thirteenth),
            Income("Férias proporcionais + 1/3", vacation)
        };

        if (isDismissal)
        {
            lines.Add(Income("Multa FGTS estimada (40%)", fgtsFine));
        }
        else
        {
            lines.Add(Information("Multa FGTS (40%)", 0m));
            if (noticeDeduction > 0m)
            {
                lines.Add(Discount("Desconto aviso prévio (30 dias)", noticeDeduction));
            }
        }

        lines.Add(Discount("INSS estimado", inss));

        var explanation = isDismissal
            ? "Demissão sem justa causa: inclui multa de 40% sobre o saldo FGTS estimado. Pode haver direito ao seguro-desemprego, conforme regras da legislação."
            : noticeDeduction > 0m
                ? "Pedido de demissão sem cumprir aviso prévio: desconta-se até 30 dias de salário. Sem multa FGTS e sem seguro-desemprego."
                : "Pedido de demissão com aviso cumprido: sem multa FGTS e sem seguro-desemprego. Mantém saldo de salário e verbas proporcionais de férias e 13º.";

        return Build(definition, totalVerbas, net, lines, explanation);
    }

    private CalculationResult CalculateOvertime(CalculatorDefinition definition, CalculatorInput input)
    {
        var hourlyRate = input.Amount;
        var hours = input.Hours <= 0 ? input.SecondaryAmount : input.Hours;
        var additionalRate = input.Rate <= 0 ? 50m : input.Rate;
        var basePay = hourlyRate * hours;
        var total = basePay * (1m + additionalRate / 100m);

        return Build(definition, basePay, total,
        [
            Income("Adicional de hora extra", total - basePay),
            Information("Horas consideradas", hours),
            Information("Adicional aplicado (%)", additionalRate)
        ], "Hora extra com adicional percentual sobre o valor da hora.");
    }

    private CalculationResult CalculateInssOnly(CalculatorDefinition definition, CalculatorInput input)
    {
        var inss = inssCalculator.Calculate(input.Amount);
        return Build(definition, input.Amount, input.Amount - inss,
        [Discount("INSS", inss)],
        "INSS progressivo conforme faixas de " + BrTaxTables2026.Year + " (teto " + BrTaxTables2026.InssCeiling.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")) + ").");
    }

    private CalculationResult CalculateIrrfOnly(CalculatorDefinition definition, CalculatorInput input)
    {
        var irrf = irrfCalculator.Calculate(input.Amount, input.Dependents);
        return Build(definition, input.Amount, input.Amount - irrf,
        [Discount("IRRF", irrf)],
        "IRRF com dedução por dependente de " + BrTaxTables2026.DependentDeduction.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")) + ".");
    }

    private CalculationResult CalculatePjVsClt(CalculatorDefinition definition, CalculatorInput input)
    {
        var cltGross = input.Amount;
        var pjGross = input.SecondaryAmount <= 0 ? input.Amount * 1.3m : input.SecondaryAmount;
        var inss = inssCalculator.Calculate(cltGross);
        var cltNet = cltGross - inss - irrfCalculator.Calculate(cltGross - inss, input.Dependents);
        var pjNet = pjGross * 0.86m;

        return Build(definition, Math.Max(cltGross, pjGross), Math.Max(cltNet, pjNet),
        [
            Information("CLT líquido estimado", cltNet),
            Information("PJ líquido estimado (14% retenção simplificada)", pjNet),
            Information("Diferença estimada", pjNet - cltNet)
        ], "Comparação educativa; PJ real depende de tributação, pró-labore e despesas.");
    }

    private CalculationResult CalculateCompoundInterest(CalculatorDefinition definition, CalculatorInput input)
    {
        var months = Math.Clamp(input.Months, 1, 600);
        var rate = (double)(input.Rate / 100m);
        var finalAmount = input.Amount * (decimal)Math.Pow(1d + rate, months);

        return Build(definition, input.Amount, finalAmount,
        [
            Income("Juros acumulados", finalAmount - input.Amount),
            Information("Meses", months),
            Information("Taxa mensal (%)", input.Rate)
        ], "Capitalização composta mensal.");
    }

    private CalculationResult CalculateFinancing(CalculatorDefinition definition, CalculatorInput input)
    {
        var months = Math.Clamp(input.Months, 1, 600);
        var monthlyRate = input.Rate / 100m;
        var payment = monthlyRate == 0
            ? input.Amount / months
            : input.Amount * monthlyRate / (1m - (decimal)Math.Pow((double)(1m + monthlyRate), -months));
        var total = payment * months;

        return Build(definition, input.Amount, payment,
        [
            Information("Parcela (Price)", payment),
            Information("Total pago", total),
            Information("Juros totais", total - input.Amount)
        ], "Sistema Price com parcelas fixas.");
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

    private static CalculationLineItem Income(string label, decimal amount) =>
        new(label, Money.From(amount), CalculationLineType.Income);

    private static CalculationLineItem Discount(string label, decimal amount) =>
        new(label, Money.From(amount), CalculationLineType.Discount);

    private static CalculationLineItem Information(string label, decimal amount) =>
        new(label, Money.From(amount), CalculationLineType.Information);
}
