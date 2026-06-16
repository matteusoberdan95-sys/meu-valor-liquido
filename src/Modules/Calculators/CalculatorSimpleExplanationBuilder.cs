namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorSimpleExplanationBuilder
{
    public static CalculatorSimpleExplanation Build(string slug, CalculatorInput input, CalculationResult result)
    {
        var steps = new List<SimpleExplanationStep>();
        var order = 1;

        foreach (var opener in GetOpeners(slug, input, result))
        {
            steps.Add(new SimpleExplanationStep(order++, opener.Title, opener.Body, opener.Highlight));
        }

        foreach (var line in result.LineItems)
        {
            if (!TryCreateStepFromLine(line, ref order, out var step))
            {
                continue;
            }

            if (steps.Any(existing => existing.Title.Equals(step.Title, StringComparison.OrdinalIgnoreCase)))
            {
                continue;
            }

            steps.Add(step);
        }

        steps.Add(new SimpleExplanationStep(
            order,
            "O que isso significa na prática",
            result.Explanation,
            result.EstimatedNetAmount.ToString()));

        return new CalculatorSimpleExplanation(steps, result.Explanation);
    }

    public static CalculatorSimpleExplanation BuildForSalaryBand(int gross, NetSalaryBreakdown breakdown)
    {
        var steps = new List<SimpleExplanationStep>
        {
            new(
                1,
                "Salário bruto de referência",
                $"Partimos de {Money.From(gross)} como base mensal, sem dependentes nem outros descontos.",
                Money.From(gross).ToString()),
            new(
                2,
                "Desconto de INSS",
                "A contribuição previdenciária segue a tabela progressiva de " + BrTaxTables2026.Year + ".",
                $"- {Money.From(breakdown.Inss)}"),
            new(
                3,
                "Desconto de IRRF",
                "O imposto de renda incide sobre a base após o INSS, com redução legal quando aplicável.",
                $"- {Money.From(breakdown.Irrf)}"),
            new(
                4,
                "O que sobra no mês",
                $"Estimativa educativa para quem ganha {Money.From(gross)} brutos neste cenário simplificado.",
                Money.From(breakdown.Net).ToString())
        };

        var summary =
            $"Com salário bruto de {Money.From(gross)}, o líquido estimado é {Money.From(breakdown.Net)} " +
            $"após INSS ({Money.From(breakdown.Inss)}) e IRRF ({Money.From(breakdown.Irrf)}).";

        return new CalculatorSimpleExplanation(steps, summary);
    }

    private static IEnumerable<(string Title, string Body, string? Highlight)> GetOpeners(
        string slug,
        CalculatorInput input,
        CalculationResult result)
    {
        return slug.ToLowerInvariant() switch
        {
            "salario-liquido" =>
            [
                (
                    "Ponto de partida: salário bruto",
                    "É o valor acordado no contrato, antes de descontos obrigatórios e opcionais.",
                    result.GrossAmount.ToString())
            ],
            "salario-bruto-necessario" =>
            [
                (
                    "Meta: salário líquido desejado",
                    "Informamos quanto de bruto costuma ser necessário para chegar perto desse líquido.",
                    Money.From(input.Amount).ToString())
            ],
            "rescisao-clt" =>
            [
                (
                    "Tipo de desligamento",
                    input.TerminationReason.GetDisplayName() + ". Cada modalidade muda verbas, multas e descontos.",
                    null)
            ],
            "pj-vs-clt" =>
            [
                (
                    "Comparativo simplificado",
                    "Mostramos estimativas lado a lado para apoiar conversa com contador ou RH — não é recomendação automática.",
                    result.GrossAmount.ToString())
            ],
            "simulador-mei" =>
            [
                (
                    "Faturamento informado",
                    "O DAS e o limite anual do MEI dependem da atividade e do volume de receita.",
                    Money.From(input.Amount).ToString())
            ],
            _ =>
            [
                (
                    result.Title,
                    "Seguimos o mesmo extrato da aba ao lado, explicado passo a passo em linguagem direta.",
                    result.GrossAmount.ToString())
            ]
        };
    }

    private static bool TryCreateStepFromLine(CalculationLineItem line, ref int order, out SimpleExplanationStep step)
    {
        step = default!;

        if (line.Type == CalculationLineType.Information && string.IsNullOrEmpty(line.DisplayText) && line.Amount.Amount <= 0m)
        {
            return false;
        }

        var amount = FormatLineAmount(line);
        if (line.Type == CalculationLineType.Discount && line.Amount.Amount <= 0m)
        {
            return false;
        }

        if (line.Type == CalculationLineType.Income && line.Amount.Amount <= 0m && string.IsNullOrEmpty(line.DisplayText))
        {
            return false;
        }

        var (title, body) = DescribeLine(line);
        step = new SimpleExplanationStep(order++, title, body, amount);
        return true;
    }

    private static (string Title, string Body) DescribeLine(CalculationLineItem line)
    {
        if (!string.IsNullOrEmpty(line.DisplayText))
        {
            return (line.Label, $"Valor considerado nesta etapa: {line.DisplayText}.");
        }

        return line.Type switch
        {
            CalculationLineType.Income => (
                line.Label,
                "Esta verba entra no total bruto da simulação e pode gerar descontos depois."),
            CalculationLineType.Discount => (
                line.Label,
                "Desconto estimado nesta etapa, conforme tabelas e regras vigentes."),
            _ => (
                line.Label,
                "Informação usada para compor o resultado final.")
        };
    }

    private static string? FormatLineAmount(CalculationLineItem line)
    {
        if (!string.IsNullOrEmpty(line.DisplayText))
        {
            return line.DisplayText;
        }

        if (line.Amount.Amount <= 0m && line.Type == CalculationLineType.Information)
        {
            return null;
        }

        return line.Type switch
        {
            CalculationLineType.Discount => $"- {line.Amount}",
            CalculationLineType.Income => $"+ {line.Amount}",
            _ => line.Amount.ToString()
        };
    }
}
