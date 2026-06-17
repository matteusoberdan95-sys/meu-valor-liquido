namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record CalculatorResultWarning(string Message, string Severity = "info", string Icon = "info");

public static class CalculatorResultWarningBuilder
{
    public static IReadOnlyList<CalculatorResultWarning> Build(
        string slug,
        CalculatorInput input,
        CalculationResult result)
    {
        if (slug.Equals("rescisao-clt", StringComparison.OrdinalIgnoreCase))
        {
            return BuildRescisaoWarnings(input);
        }

        if (slug.Equals("ferias", StringComparison.OrdinalIgnoreCase)
            || slug.Equals("decimo-terceiro", StringComparison.OrdinalIgnoreCase))
        {
            return BuildFeriasWarnings(slug);
        }

        if (slug.Equals("simulador-mei", StringComparison.OrdinalIgnoreCase))
        {
            return BuildMeiWarnings(result);
        }

        if (slug.Equals("salario-liquido", StringComparison.OrdinalIgnoreCase)
            || slug.Equals("salario-bruto-necessario", StringComparison.OrdinalIgnoreCase)
            || slug.Equals("proposta-salarial", StringComparison.OrdinalIgnoreCase))
        {
            return BuildHoleriteWarnings();
        }

        return [];
    }

    public static IReadOnlyList<CalculatorResultWarning> BuildPjVsCltWarnings(CltPjComparisonBreakdown breakdown) =>
    [
        new(
            "Comparativo de líquido mensal. Benefícios CLT (13º, férias + 1/3, FGTS, estabilidade) não entram no bolso todo mês — considere o custo total antes de decidir.",
            "warning",
            "balance"),
        new(
            breakdown.Pj.Revenue <= 0
                ? "Faturamento PJ estimado para equivaler ao líquido CLT. Ajuste alíquota do Simples e despesas conforme seu contador."
                : "Tributação PJ simplificada (Simples + pró-labore). Regimes Lucro Presumido ou Real podem alterar o resultado.",
            "info",
            "info")
    ];

    private static IReadOnlyList<CalculatorResultWarning> BuildRescisaoWarnings(CalculatorInput input)
    {
        var warnings = new List<CalculatorResultWarning>
        {
            new(
                "Estimativa educativa. O TRCT e o holerite oficial podem incluir médias de HE, comissões, adiantamentos de 13º ou descontos que você não informou aqui.",
                "warning",
                "warning"),
            new(
                "Confira cada verba no documento da empresa antes de planejar gastos ou prazos de saque do FGTS.",
                "info",
                "description")
        };

        if (input.ThirteenthAdvancePaid > 0m)
        {
            warnings.Add(new(
                "Adiantamento do 13º informado: o valor é descontado do 13º proporcional nesta simulação, como costuma ocorrer no TRCT.",
                "info",
                "receipt_long"));
        }

        if (input.SalaryAverageSupplement > 0m)
        {
            warnings.Add(new(
                "Média salarial complementar somada ao salário base. O RH pode usar critérios diferentes para médias de HE e comissão.",
                "info",
                "info"));
        }

        if (input.TerminationReason == TerminationReason.DismissalWithoutCause)
        {
            warnings.Add(new(
                "Demissão sem justa causa: pode haver direito ao seguro-desemprego (linha informativa no extrato) e saque do FGTS com multa de 40%. Confirme prazos na Caixa e no RH.",
                "info",
                "savings"));
        }
        else if (input.TerminationReason == TerminationReason.MutualAgreement)
        {
            warnings.Add(new(
                "Acordo 484-A: multa do FGTS de 20% e saque de 80% do saldo. Metade do aviso prévio indenizado e metade da multa podem ser pagas.",
                "info",
                "handshake"));
        }
        else if (input.TerminationReason == TerminationReason.Resignation
                 && input.NoticePeriod == NoticePeriodOption.NotFulfilledByEmployee)
        {
            warnings.Add(new(
                "Pedido de demissão sem cumprimento de aviso: a empresa pode descontar até 30 dias de salário no TRCT.",
                "warning",
                "schedule"));
        }

        return warnings;
    }

    private static IReadOnlyList<CalculatorResultWarning> BuildFeriasWarnings(string slug)
    {
        var label = slug.Equals("decimo-terceiro", StringComparison.OrdinalIgnoreCase) ? "13º" : "férias";
        return
        [
            new(
                $"O holerite de {label} pode diferir se houver média salarial de horas extras, comissões ou adiantamento já pago.",
                "info",
                "receipt_long"),
            new(
                "Estimativa com tabelas INSS/IRRF vigentes. Confirme valores com o RH antes de planejar gastos.",
                "info",
                "info")
        ];
    }

    private static IReadOnlyList<CalculatorResultWarning> BuildMeiWarnings(CalculationResult result)
    {
        var situation = result.LineItems
            .FirstOrDefault(i => i.Label.Equals("Situação", StringComparison.OrdinalIgnoreCase))
            ?.DisplayText;

        if (situation is null)
        {
            return [];
        }

        if (situation.Contains("Desenquadrado", StringComparison.OrdinalIgnoreCase))
        {
            return
            [
                new(
                    "Faturamento acima da tolerância MEI. Há risco de desenquadramento — consulte um contador para migrar ao ME ou Simples Nacional.",
                    "warning",
                    "error"),
                new(
                    "O DAS e o limite exibidos não se aplicam após o desenquadramento. Não use este resultado para declarar impostos.",
                    "warning",
                    "gavel")
            ];
        }

        if (situation.Contains("Acima do limite", StringComparison.OrdinalIgnoreCase))
        {
            return
            [
                new(
                    "Faturamento acima de R$ 81.000/ano, mas dentro da tolerância de 20%. Desenquadramento ocorre no ano seguinte — planeje a transição.",
                    "warning",
                    "trending_up")
            ];
        }

        return [];
    }

    private static IReadOnlyList<CalculatorResultWarning> BuildHoleriteWarnings() =>
    [
        new(
            "Holerite real pode divergir por arredondamento de centavos, faixa de IRRF ou descontos não informados (pensão, sindicato, plano).",
            "info",
            "info")
    ];
}
