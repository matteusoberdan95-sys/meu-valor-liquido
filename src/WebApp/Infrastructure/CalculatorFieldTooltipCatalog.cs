namespace MeuValorLiquido.WebApp.Infrastructure;

public static class CalculatorFieldTooltipCatalog
{
    private static readonly Dictionary<string, string> SharedTooltips = new(StringComparer.OrdinalIgnoreCase)
    {
        [CalculatorFieldKeys.Dependents] =
            "Cada dependente legal reduz a base do IRRF em R$ 189,59 (2026). Não altera o INSS.",
        [CalculatorFieldKeys.AdmissionDate] =
            "Usada para tempo na empresa, 13º proporcional e férias. Se informar a data de saída, os meses são calculados automaticamente.",
        [CalculatorFieldKeys.TerminationDate] =
            "O dia da saída define os dias trabalhados no último mês quando você não informar manualmente.",
        [CalculatorFieldKeys.NoticePeriod] =
            "No pedido de demissão sem cumprimento, pode haver desconto de até 30 dias. Na demissão sem justa causa, aviso indenizado entra na rescisão.",
        [CalculatorFieldKeys.TerminationReason] =
            "Cada motivo muda verbas rescisórias, multa do FGTS e direito ao seguro-desemprego.",
        [CalculatorFieldKeys.FgtsBalance] =
            "Opcional. Informe o saldo atual para estimar a multa rescisória. Sem o valor, usamos depósitos estimados pelo tempo na empresa.",
        [CalculatorFieldKeys.Months] =
            "Avos trabalhados no período. Na regra dos 15 dias, mês com 15+ dias conta como avo completo.",
        [CalculatorFieldKeys.Rate] =
            "Taxa informada no período escolhido. Verifique se o contrato ou a CCT usa percentual mensal ou anual.",
        [CalculatorFieldKeys.MeiActivity] =
            "O DAS MEI varia conforme comércio, serviços ou indústria. O limite anual de faturamento é o mesmo para todas."
    };

    private static readonly Dictionary<string, Dictionary<string, string>> SlugTooltips =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["salario-liquido"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Salário bruto mensal antes de INSS, IRRF e descontos. Use o valor do contrato ou holerite.",
                [CalculatorFieldKeys.TransportDiscount] =
                    "Desconto de vale-transporte (até 6% do salário bruto, se a empresa oferecer). Não misture com VR, plano ou empréstimo.",
                [CalculatorFieldKeys.OtherDiscounts] =
                    "VR, VA, plano de saúde, pensão, empréstimo consignado e outros descontos que não são vale-transporte."
            },
            ["salario-bruto-necessario"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Quanto você quer receber no bolso após INSS, IRRF e os descontos informados.",
                [CalculatorFieldKeys.TransportDiscount] =
                    "Desconto mensal de vale-transporte que você costuma ter no holerite.",
                [CalculatorFieldKeys.SecondaryAmount] =
                    "Desconto de vale-refeição ou vale-alimentação, separado do vale-transporte.",
                [CalculatorFieldKeys.OtherDiscounts] =
                    "Plano de saúde, pensão, empréstimo e demais descontos fixos do holerite."
            },
            ["proposta-salarial"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] = "Salário bruto que você recebe hoje, antes dos descontos.",
                [CalculatorFieldKeys.SecondaryAmount] = "Novo salário bruto oferecido na proposta ou negociação.",
                [CalculatorFieldKeys.TransportDiscount] =
                    "Mantenha o mesmo desconto de VT nos dois cenários para comparar o ganho real no bolso.",
                [CalculatorFieldKeys.OtherDiscounts] =
                    "Descontos que tendem a permanecer iguais na proposta (plano, VR, empréstimo etc.)."
            },
            ["ferias"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Salário base usado no cálculo das férias. Se houver médias de HE ou comissão, o holerite pode divergir.",
                [CalculatorFieldKeys.Months] =
                    "12 meses = férias integrais. Com datas de admissão e saída, os meses são calculados automaticamente."
            },
            ["decimo-terceiro"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Months] =
                    "Avos trabalhados no ano (regra dos 15 dias). Use as datas ou informe manualmente."
            },
            ["rescisao-clt"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Último salário bruto, base para saldo de salário, aviso, férias e 13º na rescisão.",
                [CalculatorFieldKeys.TransportDiscount] =
                    "Descontos fixos do último mês (VT, empréstimo consignado etc.), não confundir com verbas rescisórias."
            },
            ["hora-extra"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Valor da hora normal. Se não souber, informe o salário bruto no campo opcional para calcular pelo divisor CLT.",
                [CalculatorFieldKeys.Rate] =
                    "Percentual de adicional da hora extra (50%, 100% ou o previsto na CCT)."
            },
            ["inss"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Salário de contribuição mensal. O INSS usa tabela progressiva — quem ganha mais paga alíquota maior sobre as faixas."
            },
            ["irrf"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Base de cálculo após INSS. Marque a opção abaixo se preferir informar o salário bruto."
            },
            ["pj-vs-clt"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Salário bruto CLT para comparar com o líquido PJ. Benefícios como 13º, férias e FGTS não entram no líquido mensal.",
                [CalculatorFieldKeys.SecondaryAmount] =
                    "Faturamento PJ mensal. Deixe em branco para estimar o valor equivalente ao líquido CLT.",
                [CalculatorFieldKeys.TransportDiscount] =
                    "Descontos típicos do holerite CLT (vale-transporte etc.).",
                [CalculatorFieldKeys.OtherDiscounts] =
                    "Despesas fixas da PJ: contador, software, aluguel de escritório etc.",
                [CalculatorFieldKeys.Rate] =
                    "Alíquota efetiva do Simples Nacional sobre o faturamento. Varia por anexo e faixa de receita."
            },
            ["juros-compostos"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Rate] = "Taxa de rendimento mensal. Para taxa anual, converta antes ou use a calculadora com o período em meses."
            },
            ["financiamento"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] = "Valor financiado após entrada, se houver.",
                [CalculatorFieldKeys.Rate] = "Taxa de juros mensal do contrato. CET real pode incluir seguros e tarifas não informadas aqui."
            },
            ["fgts"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Months] = "Tempo total com carteira assinada para estimar depósitos de 8% ao mês.",
                [CalculatorFieldKeys.TerminationReason] =
                    "Define a multa rescisória: 40% sem justa causa, 20% no acordo 484-A, 0% no pedido de demissão ou justa causa."
            },
            ["simulador-mei"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Faturamento médio mensal. O limite MEI é R$ 81.000/ano (tolerância de 20% até R$ 97.200)."
            },
            ["custo-funcionario"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Salário bruto mensal do funcionário, base para encargos patronais e provisões.",
                [CalculatorFieldKeys.SecondaryAmount] =
                    "VT, plano de saúde e outros benefícios pagos pela empresa além do salário."
            },
            ["conversor-salario"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] =
                    "Valor a converter conforme o tipo escolhido (mensal, diário ou hora)."
            },
            ["multa-atraso"] = new(StringComparer.OrdinalIgnoreCase)
            {
                [CalculatorFieldKeys.Amount] = "Valor principal em atraso, sem multa e juros.",
                [CalculatorFieldKeys.Rate] = "Juros de mora ao mês conforme contrato ou referência legal."
            }
        };

    public static string? GetTooltip(string slug, string fieldKey)
    {
        if (SlugTooltips.TryGetValue(slug, out var slugMap)
            && slugMap.TryGetValue(fieldKey, out var slugTooltip))
        {
            return slugTooltip;
        }

        return SharedTooltips.TryGetValue(fieldKey, out var shared) ? shared : null;
    }

    public static bool HasTooltip(string slug, string fieldKey) => GetTooltip(slug, fieldKey) is not null;
}
