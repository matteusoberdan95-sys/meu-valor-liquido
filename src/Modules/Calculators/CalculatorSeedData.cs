namespace MeuValorLiquido.Modules.Calculators;

public static class CalculatorSeedData
{
    public static IReadOnlyList<CalculatorDefinition> GetDefinitions() =>
    [
        Create("salario-liquido", "Salário líquido", "Trabalhista", "Estime quanto sobra do salário após INSS, IRRF e descontos comuns."),
        Create("ferias", "Férias", "Trabalhista", "Calcule uma estimativa de férias com adicional constitucional de um terço."),
        Create("decimo-terceiro", "Décimo terceiro", "Trabalhista", "Estime o décimo terceiro proporcional ou integral com descontos."),
        Create("rescisao-clt", "Rescisão CLT", "Trabalhista", "Simule rescisão por demissão ou pedido de demissão com verbas proporcionais e multa FGTS quando aplicável."),
        Create("hora-extra", "Hora extra", "Trabalhista", "Calcule o valor estimado de horas extras com percentual adicional."),
        Create("inss", "INSS", "Fiscal", "Estime o desconto de INSS pela tabela progressiva de 2026."),
        Create("irrf", "IRRF", "Fiscal", "Estime o imposto de renda retido na fonte a partir da base informada."),
        Create("pj-vs-clt", "PJ vs CLT", "Financeiro", "Compare uma estimativa simples entre remuneração PJ e salário líquido CLT."),
        Create("juros-compostos", "Juros compostos", "Financeiro", "Projete o crescimento de um valor com taxa mensal e prazo."),
        Create("financiamento", "Financiamento", "Financeiro", "Estime uma parcela fixa usando a fórmula Price.")
    ];

    public static CalculatorDefinition Create(string slug, string name, string category, string summary)
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
