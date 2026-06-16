namespace MeuValorLiquido.Modules.Calculators;
public sealed class CalculationEngine
{
    private static readonly string Disclaimer =
        "Resultado estimado para fins educativos (tabelas INSS/IRRF " + BrTaxTables2026.Year +
        ", Portaria MPS/MF nº 13/2026). Não substitui orientação jurídica, contábil, financeira ou conferência oficial.";

    private readonly IInssCalculator inssCalculator;
    private readonly IIrrfCalculator irrfCalculator;
    private readonly ITerminationTaxCalculator terminationTaxCalculator;
    private readonly NetSalaryCalculator netSalaryCalculator;
    private readonly CltPjComparisonCalculator cltPjComparisonCalculator;

    public CalculationEngine(
        IInssCalculator inssCalculator,
        IIrrfCalculator irrfCalculator,
        ITerminationTaxCalculator terminationTaxCalculator,
        NetSalaryCalculator netSalaryCalculator,
        CltPjComparisonCalculator cltPjComparisonCalculator)
    {
        this.inssCalculator = inssCalculator;
        this.irrfCalculator = irrfCalculator;
        this.terminationTaxCalculator = terminationTaxCalculator;
        this.netSalaryCalculator = netSalaryCalculator;
        this.cltPjComparisonCalculator = cltPjComparisonCalculator;
    }

    public CalculationResult? Calculate(CalculatorDefinition definition, CalculatorInput input)
    {
        return definition.Slug.ToLowerInvariant() switch
        {
            "salario-liquido" => CalculateNetSalary(definition, input),
            "salario-bruto-necessario" => CalculateRequiredGrossSalary(definition, input),
            "proposta-salarial" => CalculateSalaryProposal(definition, input),
            "ferias" => CalculateVacation(definition, input),
            "decimo-terceiro" => CalculateThirteenthSalary(definition, input),
            "rescisao-clt" => CalculateTermination(definition, input),
            "hora-extra" => CalculateOvertime(definition, input),
            "inss" => CalculateInssOnly(definition, input),
            "irrf" => CalculateIrrfOnly(definition, input),
            "pj-vs-clt" => CalculatePjVsClt(definition, input),
            "juros-compostos" => CalculateCompoundInterest(definition, input),
            "financiamento" => CalculateFinancing(definition, input),
            "fgts" => CalculateFgts(definition, input),
            "simulador-mei" => CalculateMei(definition, input),
            "custo-funcionario" => CalculateEmployeeCost(definition, input),
            "multa-atraso" => CalculateLatePenalty(definition, input),
            "conversor-salario" => CalculateSalaryConversion(definition, input),
            _ => null
        };
    }

    private CalculationResult CalculateNetSalary(CalculatorDefinition definition, CalculatorInput input)
    {
        var breakdown = netSalaryCalculator.Calculate(
            input.Amount,
            input.Dependents,
            input.TransportDiscount);

        return Build(definition, breakdown.Gross, breakdown.Net,
        [
            Discount("INSS", breakdown.Inss),
            Discount("IRRF", breakdown.Irrf),
            Discount("Vale-transporte/outros descontos", breakdown.TransportDiscount)
        ], "Salário líquido com INSS progressivo e IRRF com redução legal de " + BrTaxTables2026.Year + ".");
    }

    private CalculationResult CalculateRequiredGrossSalary(CalculatorDefinition definition, CalculatorInput input)
    {
        var targetNet = input.Amount;
        var transport = input.TransportDiscount;
        var meal = input.SecondaryAmount;
        var other = input.OtherDiscounts;

        var gross = GrossSalarySolver.Solve(
            netSalaryCalculator,
            targetNet,
            input.Dependents,
            transport,
            meal,
            other);

        var breakdown = netSalaryCalculator.Calculate(gross, input.Dependents, transport, meal, other);
        var difference = breakdown.Net - targetNet;

        var lines = new List<CalculationLineItem>
        {
            Information("Salário líquido desejado", targetNet),
            Discount("INSS", breakdown.Inss),
            Discount("IRRF", breakdown.Irrf)
        };

        if (breakdown.TransportDiscount > 0m)
        {
            lines.Add(Discount("Vale-transporte", breakdown.TransportDiscount));
        }

        if (breakdown.MealVoucherDiscount > 0m)
        {
            lines.Add(Discount("Vale-refeição/alimentação", breakdown.MealVoucherDiscount));
        }

        if (breakdown.OtherDiscounts > 0m)
        {
            lines.Add(Discount("Outros descontos", breakdown.OtherDiscounts));
        }

        lines.Add(Information("Diferença vs. líquido desejado", difference));

        var explanation =
            $"Para receber cerca de {Money.From(targetNet)} líquido, estimamos salário bruto de {Money.From(gross)}. " +
            "O cálculo usa busca binária sobre as mesmas regras de INSS progressivo e IRRF com redução legal de " +
            BrTaxTables2026.Year + ".";

        return Build(definition, gross, breakdown.Net, lines, explanation);
    }

    private CalculationResult CalculateSalaryProposal(CalculatorDefinition definition, CalculatorInput input)
    {
        var currentGross = input.Amount;
        var proposedGross = input.SecondaryAmount;
        var current = netSalaryCalculator.Calculate(
            currentGross,
            input.Dependents,
            input.TransportDiscount,
            otherDiscounts: input.OtherDiscounts);
        var proposed = netSalaryCalculator.Calculate(
            proposedGross,
            input.Dependents,
            input.TransportDiscount,
            otherDiscounts: input.OtherDiscounts);

        var netDiff = proposed.Net - current.Net;
        var annualDiff = netDiff * 12m;
        var grossIncreasePercent = currentGross > 0m
            ? (proposedGross - currentGross) / currentGross * 100m
            : 0m;
        var netIncreasePercent = current.Net > 0m
            ? netDiff / current.Net * 100m
            : 0m;

        var lines = new List<CalculationLineItem>
        {
            Information("Salário bruto atual", currentGross),
            Information("Salário bruto proposto", proposedGross),
            Information("Líquido atual estimado", current.Net),
            Information("Líquido proposto estimado", proposed.Net),
            netDiff >= 0m
                ? Income("Ganho líquido mensal", netDiff)
                : Discount("Redução líquida mensal", Math.Abs(netDiff)),
            Information("Ganho ou perda anual (12 meses)", annualDiff),
            PercentInformation("Aumento no bruto", grossIncreasePercent),
            PercentInformation("Aumento no líquido", netIncreasePercent)
        };

        var explanation = netDiff >= 0m
            ? $"A proposta eleva o bruto em {grossIncreasePercent:0.#}% e o líquido em {netIncreasePercent:0.#}% " +
              $"(cerca de {Money.From(netDiff)} por mês, {Money.From(annualDiff)} no ano). " +
              "Impostos progressivos explicam por que o ganho no bolso é diferente do percentual no bruto."
            : $"A proposta reduz o líquido em {Money.From(Math.Abs(netDiff))} por mês " +
              $"({Money.From(Math.Abs(annualDiff))} no ano) em relação ao cenário atual.";

        return Build(definition, proposedGross, proposed.Net, lines, explanation);
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
            Discount("INSS", inss),
            Discount("IRRF", irrf)
        ], "Férias gozadas: salário + 1/3 constitucional com descontos de INSS e IRRF (" + BrTaxTables2026.Year + ").");
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
            Discount("INSS", inss),
            Discount("IRRF", irrf)
        ], "Décimo terceiro proporcional. INSS e IRRF calculados separadamente sobre a verba, conforme regra previdenciária.");
    }

    private CalculationResult CalculateTermination(CalculatorDefinition definition, CalculatorInput input)
    {
        var benefits = TerminationBenefitCalculator.Calculate(input);
        var reason = input.TerminationReason;

        var totalVerbas = benefits.SalaryBalance + benefits.Thirteenth + benefits.VacationProportional
            + benefits.UnpaidVacation + benefits.NoticeIndemnity + benefits.FgtsFine;

        var taxes = terminationTaxCalculator.Calculate(
            benefits.SalaryBalance,
            benefits.Thirteenth,
            input.Dependents);
        var otherDiscounts = Math.Max(0m, input.TransportDiscount);
        var net = totalVerbas - taxes.Total - benefits.NoticeDeduction - otherDiscounts;

        var lines = new List<CalculationLineItem>
        {
            Income("Saldo de salário", benefits.SalaryBalance)
        };

        if (benefits.IncludeThirteenth)
        {
            lines.Add(Income("13º proporcional", benefits.Thirteenth));
        }
        else
        {
            lines.Add(Information("13º proporcional", 0m));
        }

        if (benefits.IncludeProportionalVacation && benefits.VacationProportional > 0m)
        {
            lines.Add(Income("Férias proporcionais + 1/3", benefits.VacationProportional));
        }
        else if (benefits.IncludeProportionalVacation)
        {
            lines.Add(Information("Férias proporcionais + 1/3", 0m));
        }
        else
        {
            lines.Add(Information("Férias proporcionais + 1/3 (justa causa)", 0m));
        }

        if (benefits.UnpaidVacation > 0m)
        {
            lines.Add(Income("Férias vencidas + 1/3", benefits.UnpaidVacation));
        }

        if (benefits.NoticeIndemnity > 0m)
        {
            var noticeLabel = reason == TerminationReason.MutualAgreement
                ? $"Aviso prévio indenizado 50% ({benefits.NoticeDays} dias)"
                : $"Aviso prévio indenizado ({benefits.NoticeDays} dias)";
            lines.Add(Income(noticeLabel, benefits.NoticeIndemnity));
        }

        if (benefits.FgtsFine > 0m)
        {
            var finePercent = (benefits.FgtsFineRate * 100m).ToString("0");
            lines.Add(Income($"Multa FGTS ({finePercent}%)", benefits.FgtsFine));
            if (input.FgtsBalance <= 0m)
            {
                lines.Add(Information("Saldo FGTS estimado (8% × meses)", benefits.FgtsBase));
            }

            if (benefits.FgtsWithdrawalRate > 0m && benefits.FgtsWithdrawalRate < 1m)
            {
                lines.Add(Information("Saque FGTS permitido (estimado)", benefits.FgtsBase * benefits.FgtsWithdrawalRate));
            }
        }
        else if (reason != TerminationReason.DismissalForCause)
        {
            lines.Add(Information("Multa FGTS", 0m));
        }

        if (benefits.NoticeDeduction > 0m)
        {
            var noticeLabel = benefits.NoticeDeductionWasCapped
                ? "Desconto aviso prévio (limitado às verbas)"
                : "Desconto aviso prévio (30 dias)";
            lines.Add(Discount(noticeLabel, benefits.NoticeDeduction));
        }

        if (benefits.ThirteenthMonths > 0 && benefits.IncludeThirteenth)
        {
            lines.Add(CountInformation("Meses considerados no 13º", benefits.ThirteenthMonths));
        }

        if (benefits.ProportionalVacationMonths > 0 && benefits.IncludeProportionalVacation)
        {
            lines.Add(CountInformation("Meses de férias proporcionais", benefits.ProportionalVacationMonths));
        }

        if (benefits.UnpaidVacationPeriods > 0)
        {
            lines.Add(CountInformation("Períodos de férias vencidas", benefits.UnpaidVacationPeriods));
        }

        if (taxes.InssOnSalaryBalance > 0m)
        {
            lines.Add(Discount("INSS sobre saldo de salário", taxes.InssOnSalaryBalance));
        }

        if (taxes.InssOnThirteenth > 0m)
        {
            lines.Add(Discount("INSS sobre 13º proporcional", taxes.InssOnThirteenth));
        }

        if (taxes.IrrfOnSalaryBalance > 0m)
        {
            lines.Add(Discount("IRRF sobre saldo de salário", taxes.IrrfOnSalaryBalance));
        }

        if (taxes.IrrfOnThirteenth > 0m)
        {
            lines.Add(Discount("IRRF sobre 13º proporcional", taxes.IrrfOnThirteenth));
        }

        if (otherDiscounts > 0m)
        {
            lines.Add(Discount("Outros descontos", otherDiscounts));
        }

        if (reason == TerminationReason.DismissalWithoutCause)
        {
            lines.Add(Information("Seguro-desemprego", 0m));
        }

        var explanation = TerminationBenefitCalculator.BuildExplanation(reason, benefits);
        return Build(definition, totalVerbas, net, lines, explanation);
    }

    private CalculationResult CalculateOvertime(CalculatorDefinition definition, CalculatorInput input)
    {
        var divisor = CltWorkHourRules.GetMonthlyHourDivisor(input.WeeklyWorkHours);
        var additionalRate = CltWorkHourRules.ResolveOvertimeAdditionalPercent(input.Rate, input.OvertimeShiftType);
        var hourlyRate = input.SecondaryAmount > 0m
            ? input.SecondaryAmount / divisor
            : input.Amount;
        var hours = input.Hours <= 0 ? 10m : input.Hours;

        var basePay = hourlyRate * hours;
        var overtimePay = basePay * additionalRate / 100m;
        var total = basePay + overtimePay;

        const decimal workDays = 26m;
        const decimal restDays = 4m;
        var dsr = overtimePay * restDays / workDays;
        var grandTotal = total + dsr;

        var shiftLabel = input.OvertimeShiftType switch
        {
            OvertimeShiftType.SundayOrHoliday => "domingo/feriado",
            OvertimeShiftType.NightWeekday => "noturna",
            _ => "dia útil"
        };

        var lines = new List<CalculationLineItem>
        {
            Information("Divisor mensal de horas", divisor),
            Information("Valor da hora normal", hourlyRate),
            Income($"Horas extras {shiftLabel} ({additionalRate:0.#}%)", total),
            Income("Reflexo DSR estimado", dsr)
        };

        if (input.SecondaryAmount > 0m)
        {
            lines.Insert(1, Information("Salário mensal informado", input.SecondaryAmount));
        }

        if (input.Rate > 0m && input.Rate != additionalRate && input.OvertimeShiftType == OvertimeShiftType.Weekday)
        {
            lines.Add(Information("Adicional CCT informado (%)", input.Rate));
        }

        return Build(definition, grandTotal, grandTotal, lines,
            "Hora extra CLT com adicional de convenção coletiva, jornada configurável e DSR (Súmula 172 TST). Mínimos legais: 50% (dia útil), 100% (domingo/feriado), +20% noturno.");
    }

    private CalculationResult CalculateInssOnly(CalculatorDefinition definition, CalculatorInput input)
    {
        var inss = inssCalculator.Calculate(input.Amount);
        return Build(definition, input.Amount, input.Amount - inss,
        [Discount("INSS", inss)],
        "INSS progressivo (Portaria MPS/MF nº 13/2026). Teto: " + BrTaxTables2026.InssCeiling.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")) + ".");
    }

    private CalculationResult CalculateIrrfOnly(CalculatorDefinition definition, CalculatorInput input)
    {
        var irrf = irrfCalculator.Calculate(input.Amount, input.Dependents);
        return Build(definition, input.Amount, input.Amount - irrf,
        [Discount("IRRF", irrf)],
        "IRRF com tabela progressiva e redução legal de " + BrTaxTables2026.Year + " (Lei 15.270/2025). Dedução por dependente: R$ 189,59.");
    }

    private CalculationResult CalculatePjVsClt(CalculatorDefinition definition, CalculatorInput input)
    {
        var comparison = cltPjComparisonCalculator.Compare(input);
        var clt = comparison.Clt;
        var pj = comparison.Pj;
        var simplesLabel = $"{comparison.SimplesRatePercent:0.#}%";

        var lines = new List<CalculationLineItem>
        {
            Information("CLT — salário bruto", clt.Gross),
            Discount("CLT — INSS", clt.Inss),
            Discount("CLT — IRRF", clt.Irrf),
        };

        if (clt.Discounts > 0m)
        {
            lines.Add(Discount("CLT — outros descontos", clt.Discounts));
        }

        lines.Add(Information("CLT — líquido estimado", clt.Net));
        lines.Add(Information("PJ — faturamento mensal", pj.Revenue));
        lines.Add(Discount($"PJ — Simples Nacional ({simplesLabel})", pj.SimplesTax));
        lines.Add(Information($"PJ — pró-labore ({comparison.ProLaboreSharePercent:0.#}%)", pj.ProLabore));
        lines.Add(Discount("PJ — INSS sobre pró-labore", pj.Inss));
        lines.Add(Discount("PJ — IRRF sobre pró-labore", pj.Irrf));

        if (pj.Expenses > 0m)
        {
            lines.Add(Discount("PJ — despesas fixas", pj.Expenses));
        }

        lines.Add(Information("PJ — líquido pessoal estimado", pj.Net));
        lines.Add(Information("Faturamento PJ equivalente ao líquido CLT", comparison.EquivalentPjRevenue));
        lines.Add(Information("Diferença de líquido (PJ − CLT)", comparison.NetDifference));

        var explanation =
            $"Para um CLT de {Money.From(clt.Gross)} (líquido {Money.From(clt.Net)}), faturar cerca de " +
            $"{Money.From(comparison.EquivalentPjRevenue)} como PJ tende a equivaler ao bolso, " +
            $"com Simples de {simplesLabel} e pró-labore de {comparison.ProLaboreSharePercent:0.#}%. " +
            "Regime real, anexo do Simples, pró-labore mínimo e custos variam — use como referência educativa.";

        var winnerNet = Math.Max(clt.Net, pj.Net);
        return Build(definition, Math.Max(clt.Gross, pj.Revenue), winnerNet, lines, explanation);
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
        ], "Capitalização composta mensal (M = P × (1 + i)^n).");
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
        ], "Sistema Price (parcelas fixas): PMT = PV × i / (1 - (1 + i)^-n).");
    }

    private CalculationResult CalculateFgts(CalculatorDefinition definition, CalculatorInput input)
    {
        var salary = input.Amount;
        var months = Math.Clamp(input.Months, 1, 600);
        var deposits = salary * 0.08m * months;
        var balance = input.FgtsBalance > 0m ? input.FgtsBalance + deposits : deposits;
        var fineRate = TerminationBenefitCalculator.ResolveFgtsFineRateForReason(input.TerminationReason);
        var fine = fineRate > 0m ? balance * fineRate : 0m;

        var lines = new List<CalculationLineItem>
        {
            Income("Depósitos mensais estimados (8%)", salary * 0.08m),
            Information("Meses considerados", months),
            Income("Total depositado no período", deposits),
            Information("Saldo FGTS estimado", balance)
        };

        if (fineRate > 0m)
        {
            var finePercent = (fineRate * 100m).ToString("0");
            lines.Add(Income($"Multa rescisória ({finePercent}%)", fine));
        }

        return Build(definition, balance, balance + fine, lines,
            "FGTS: depósito mensal de 8% pelo empregador. Informe o tipo de desligamento para estimar multa rescisória (40% sem justa causa, 20% em acordo 484-A).");
    }

    private CalculationResult CalculateMei(CalculatorDefinition definition, CalculatorInput input)
    {
        var monthlyRevenue = input.Amount;
        var das = BrMeiTables2026.GetDas(input.MeiActivity);
        var annualRevenue = monthlyRevenue * 12m;
        var annualLimit = BrMeiTables2026.AnnualRevenueLimit;
        var toleranceLimit = annualLimit * (1m + BrMeiTables2026.ExcessTolerancePercent);
        var withinLimit = annualRevenue <= annualLimit;
        var withinTolerance = annualRevenue <= toleranceLimit;
        var net = monthlyRevenue - das;

        var lines = new List<CalculationLineItem>
        {
            Discount("DAS MEI mensal", das),
            Information("Faturamento anual projetado", annualRevenue),
            Information("Limite MEI anual", annualLimit),
            Information("Líquido após DAS", net)
        };

        if (!withinLimit && withinTolerance)
        {
            lines.Add(Information("Alerta: acima do limite", 0m));
        }
        else if (!withinTolerance)
        {
            lines.Add(Information("Risco de desenquadramento", 0m));
        }

        var explanation = withinLimit
            ? "MEI dentro do limite anual de R$ 81.000. DAS fixo conforme atividade (INSS 5% do salário mínimo + ICMS/ISS)."
            : withinTolerance
                ? "Faturamento acima de R$ 81.000/ano, mas dentro da tolerância de 20% (até R$ 97.200). Desenquadramento ocorre no ano seguinte."
                : "Faturamento acima da tolerância de 20%. Risco de desenquadramento retroativo para ME no Simples Nacional.";

        return Build(definition, monthlyRevenue, net, lines, explanation);
    }

    private CalculationResult CalculateEmployeeCost(CalculatorDefinition definition, CalculatorInput input)
    {
        var salary = input.Amount;
        var benefits = Math.Max(0m, input.SecondaryAmount);
        var fgts = salary * 0.08m;
        var inssPatronal = salary * 0.20m;
        var thirteenthProvision = salary / 12m;
        var vacationProvision = salary * 4m / 36m;
        var satEstimate = salary * 0.02m;
        var encargos = fgts + inssPatronal + thirteenthProvision + vacationProvision + satEstimate;
        var total = salary + benefits + encargos;

        return Build(definition, salary, total,
        [
            Income("Salário bruto", salary),
            Income("Benefícios informados", benefits),
            Income("FGTS (8%)", fgts),
            Income("INSS patronal (~20%)", inssPatronal),
            Income("Provisão 13º (1/12)", thirteenthProvision),
            Income("Provisão férias + 1/3", vacationProvision),
            Income("RAT/SAT estimado (2%)", satEstimate),
            Information("Custo mensal total empresa", total),
            Information("Multiplicador sobre salário", salary > 0m ? total / salary : 0m)
        ], "Custo total estimado da empresa: salário + encargos trabalhistas e provisões. Valores aproximados; consulte contador para folha real.");
    }

    private CalculationResult CalculateLatePenalty(CalculatorDefinition definition, CalculatorInput input)
    {
        var principal = input.Amount;
        var days = input.SecondaryAmount <= 0 ? 30m : Math.Clamp(input.SecondaryAmount, 1m, 3650m);
        var monthlyRate = input.Rate <= 0 ? 1m : input.Rate;
        var finePercent = input.Hours <= 0 ? 2m : input.Hours;
        var fine = principal * finePercent / 100m;
        var interest = principal * monthlyRate / 100m * days / 30m;
        var total = principal + fine + interest;

        return Build(definition, principal, total,
        [
            Income($"Multa ({finePercent:0.#}%)", fine),
            Income($"Juros ({monthlyRate:0.#}% a.m. × {days:0} dias)", interest),
            Information("Dias em atraso", days),
            Information("Total com acréscimos", total)
        ], "Multa e juros simples proporcionais (referência contratual comum: 2% de multa + 1% ao mês). Ajuste conforme contrato ou legislação aplicável.");
    }

    private CalculationResult CalculateSalaryConversion(CalculatorDefinition definition, CalculatorInput input)
    {
        var divisor = CltWorkHourRules.GetMonthlyHourDivisor(input.WeeklyWorkHours);
        var (monthly, daily, hourly) = CltWorkHourRules.ConvertSalary(input.Amount, input.SalaryBasis, divisor);

        return Build(definition, monthly, monthly,
        [
            Information("Salário mensal", monthly),
            Information("Salário diário (÷ 30)", daily),
            Information("Salário por hora", hourly),
            Information("Divisor de horas mensais", divisor)
        ], $"Conversão CLT com divisor mensal de {divisor:0} horas (jornada {input.WeeklyWorkHours switch { 40 => "40h", 36 => "36h", 30 => "30h", _ => "44h" }}). Dia calculado por 30 dias corridos.");
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

    private static CalculationLineItem CountInformation(string label, int count) =>
        new(label, Money.From(0m), CalculationLineType.Information, count.ToString());

    private static CalculationLineItem PercentInformation(string label, decimal percent) =>
        new(label, Money.From(0m), CalculationLineType.Information, $"{percent:0.##}%");
}
