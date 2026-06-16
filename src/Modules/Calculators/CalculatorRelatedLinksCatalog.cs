namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorRelatedLinksCatalog
{
    private static readonly IReadOnlyDictionary<string, CalculatorRelatedLinkDefinition[]> Links =
        new Dictionary<string, CalculatorRelatedLinkDefinition[]>(StringComparer.OrdinalIgnoreCase)
        {
            ["salario-liquido"] =
            [
                new("inss", "Veja como o INSS progressivo incide sobre o bruto."),
                new("irrf", "Entenda a base e as faixas do imposto de renda."),
                new("salario-bruto-necessario", "Descubra o bruto para um líquido desejado."),
                new("conversor-salario", "Converta o valor entre mês, dia e hora."),
                new("fgts", "Estime depósitos e multa do FGTS.")
            ],
            ["salario-bruto-necessario"] =
            [
                new("salario-liquido", "Confira o extrato a partir do salário bruto."),
                new("inss", "Detalhe a contribuição previdenciária."),
                new("irrf", "Simule o IRRF com dependentes."),
                new("conversor-salario", "Compare o bruto em outras bases de tempo.")
            ],
            ["ferias"] =
            [
                new("salario-liquido", "Veja o líquido mensal habitual."),
                new("decimo-terceiro", "Estime o 13º no mesmo salário."),
                new("rescisao-clt", "Simule férias na rescisão completa."),
                new("inss", "Entenda o desconto previdenciário.")
            ],
            ["decimo-terceiro"] =
            [
                new("salario-liquido", "Compare com o salário mensal líquido."),
                new("ferias", "Calcule férias com o terço constitucional."),
                new("rescisao-clt", "Veja o 13º proporcional na rescisão."),
                new("irrf", "Simule o IRRF sobre a verba.")
            ],
            ["rescisao-clt"] =
            [
                new("fgts", "Estime saldo e multa do FGTS."),
                new("ferias", "Entenda férias proporcionais e vencidas."),
                new("decimo-terceiro", "Calcule o 13º isoladamente."),
                new("salario-liquido", "Compare com o salário mensal.")
            ],
            ["hora-extra"] =
            [
                new("conversor-salario", "Descubra o valor da hora normal."),
                new("salario-liquido", "Veja o impacto no holerite mensal."),
                new("rescisao-clt", "Horas extras podem entrar na rescisão.")
            ],
            ["inss"] =
            [
                new("irrf", "O IRRF usa a base após o INSS."),
                new("salario-liquido", "Monte o extrato completo do holerite."),
                new("salario-bruto-necessario", "Planeje o bruto para um líquido alvo.")
            ],
            ["irrf"] =
            [
                new("inss", "O imposto incide sobre a base após INSS."),
                new("salario-liquido", "Veja todos os descontos juntos."),
                new("decimo-terceiro", "IRRF também incide sobre o 13º.")
            ],
            ["pj-vs-clt"] =
            [
                new("salario-liquido", "Detalhe o lado CLT do comparativo."),
                new("simulador-mei", "Compare com custos de MEI."),
                new("custo-funcionario", "Veja o custo para a empresa na CLT.")
            ],
            ["fgts"] =
            [
                new("rescisao-clt", "Multa e saque na demissão."),
                new("salario-liquido", "FGTS não entra no líquido mensal."),
                new("custo-funcionario", "Depósito de 8% no custo da empresa.")
            ],
            ["simulador-mei"] =
            [
                new("pj-vs-clt", "Compare MEI com CLT e PJ."),
                new("multa-atraso", "Estime multa por atraso do DAS."),
                new("juros-compostos", "Projete reserva para impostos.")
            ],
            ["custo-funcionario"] =
            [
                new("salario-liquido", "Veja o que o funcionário recebe."),
                new("fgts", "Detalhe depósitos e multa."),
                new("pj-vs-clt", "Compare contratação CLT x PJ.")
            ],
            ["conversor-salario"] =
            [
                new("salario-liquido", "A partir do mensal, veja o líquido."),
                new("hora-extra", "Use o valor da hora nas extras."),
                new("salario-bruto-necessario", "Defina meta de líquido mensal.")
            ],
            ["financiamento"] =
            [
                new("juros-compostos", "Compare com investimento ou dívida."),
                new("multa-atraso", "Estime custo de atraso na parcela."),
                new("salario-liquido", "Veja se a parcela cabe no orçamento.")
            ],
            ["juros-compostos"] =
            [
                new("financiamento", "Compare com parcela de empréstimo."),
                new("multa-atraso", "Entenda juros em atrasos."),
                new("salario-liquido", "Relacione com sua renda líquida.")
            ],
            ["multa-atraso"] =
            [
                new("financiamento", "Simule parcelas de financiamento."),
                new("juros-compostos", "Projete crescimento de dívida."),
                new("simulador-mei", "MEI: evite atraso do DAS.")
            ]
        };

    private static readonly CalculatorRelatedLinkDefinition[] DefaultLinks =
    [
        new("salario-liquido", "Comece pelo extrato de salário líquido."),
        new("inss", "Entenda o desconto previdenciário."),
        new("irrf", "Veja como o imposto de renda é estimado.")
    ];

    public static IReadOnlyList<CalculatorRelatedLinkDefinition> GetForSlug(string slug) =>
        Links.TryGetValue(slug, out var items) ? items : DefaultLinks;
}
