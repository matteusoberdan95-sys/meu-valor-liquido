namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record MetodologiaCategorySection(
    string Key,
    string Title,
    string Icon,
    string Summary,
    IReadOnlyList<string> Highlights,
    IReadOnlyList<MetodologiaCalculatorLink> Calculators);

public sealed record MetodologiaCalculatorLink(string Slug, string Name);

public static class MetodologiaCategoryCatalog
{
    public static IReadOnlyList<MetodologiaCategorySection> GetAll() =>
    [
        new(
            "trabalhista",
            "Calculadoras trabalhistas",
            "work",
            "Simulam verbas CLT com regras documentadas, extrato legível e avisos quando o holerite ou TRCT podem divergir.",
            [
                "Salário líquido usa INSS progressivo e IRRF 2026 sobre a base após dependentes e descontos informados.",
                "Rescisão estima verbas por tipo de desligamento, com regra dos 15 dias, adiantamento de 13º e média salarial opcional.",
                "Férias e 13º consideram proporcionalidade, abono e adiantamentos quando você preenche os campos."
            ],
            [
                Link("salario-liquido", "Salário líquido"),
                Link("rescisao-clt", "Rescisão CLT"),
                Link("ferias", "Férias"),
                Link("fgts", "FGTS"),
                Link("hora-extra", "Hora extra")
            ]),
        new(
            "fiscal",
            "Calculadoras fiscais",
            "account_balance",
            "Aplicam tabelas oficiais de INSS e IRRF e regras simplificadas de MEI, sempre como estimativa educativa.",
            [
                "INSS e IRRF usam tabelas oficiais de 2026 com data de calibração visível nesta página.",
                "IRRF considera dedução por dependente e isenção conforme legislação vigente.",
                "MEI alerta limite anual e desenquadramento — não substitui contador."
            ],
            [
                Link("inss", "INSS"),
                Link("irrf", "IRRF"),
                Link("simulador-mei", "Simulador MEI")
            ]),
        new(
            "financeiro",
            "Calculadoras financeiras",
            "trending_up",
            "Comparam cenários de crédito, investimento e regime de trabalho com premissas explícitas e limitações claras.",
            [
                "Financiamento compara Price e SAC; CET exibido é aproximado e informativo.",
                "PJ×CLT compara líquido mensal — benefícios CLT não entram no bolso todo mês.",
                "Juros compostos aceita taxa mensal; conversão anual é responsabilidade do usuário."
            ],
            [
                Link("financiamento", "Financiamento"),
                Link("pj-vs-clt", "PJ vs CLT"),
                Link("juros-compostos", "Juros compostos")
            ])
    ];

    private static MetodologiaCalculatorLink Link(string slug, string name) => new(slug, name);
}
