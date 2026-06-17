namespace MeuValorLiquido.Modules.Calculators;
public static class CalculatorSeedData
{
    public static IReadOnlyList<CalculatorDefinition> GetDefinitions() =>
    [
        Create("salario-liquido", "Salário líquido", "Trabalhista", "Estime quanto sobra do salário após INSS, IRRF e descontos comuns."),
        CreateRequiredGrossSalary(),
        CreateSalaryProposal(),
        Create("ferias", "Férias", "Trabalhista", "Calcule uma estimativa de férias com adicional constitucional de um terço."),
        Create("decimo-terceiro", "Décimo terceiro", "Trabalhista", "Estime o décimo terceiro proporcional ou integral com descontos."),
        Create("rescisao-clt", "Rescisão CLT", "Trabalhista", "Simule todos os tipos de desligamento: demissão, pedido de demissão, acordo 484-A e justa causa."),
        Create("hora-extra", "Hora extra", "Trabalhista", "Calcule horas extras com adicional de CCT, jornada semanal, turno noturno e DSR."),
        Create("inss", "INSS", "Fiscal", "Estime o desconto de INSS pela tabela progressiva oficial de 2026 (Portaria MPS/MF nº 13)."),
        Create("irrf", "IRRF", "Fiscal", "Estime o IRRF com tabela progressiva e redução legal de 2026 (Lei 15.270/2025)."),
        CreateCltPj(),
        Create("juros-compostos", "Juros compostos", "Financeiro", "Projete o crescimento de um valor com taxa mensal e prazo."),
        Create("financiamento", "Financiamento", "Financeiro", "Estime parcelas no sistema Price ou SAC e compare o custo total de juros."),
        Create("fgts", "FGTS", "Trabalhista", "Calcule depósitos mensais de 8%, saldo acumulado e multa rescisória."),
        Create("simulador-mei", "Simulador MEI", "Fiscal", "Estime o DAS MEI, limite de faturamento e alertas de desenquadramento."),
        Create("custo-funcionario", "Custo de funcionário", "Trabalhista", "Estime o custo total da empresa com salário, encargos e provisões."),
        Create("multa-atraso", "Multa de atraso", "Financeiro", "Calcule multa e juros por atraso de pagamento."),
        Create("conversor-salario", "Conversor de salário", "Trabalhista", "Converta salário entre valor mensal, diário e por hora.")
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

    private static CalculatorDefinition CreateRequiredGrossSalary() =>
        new(
            "salario-bruto-necessario",
            "Salário bruto necessário",
            "Trabalhista",
            "Descubra quanto de salário bruto você precisa para receber o líquido desejado.",
            "Quanto preciso ganhar para receber X líquido? Calculadora 2026 | Meu Valor Líquido",
            "Informe o salário líquido desejado, dependentes e descontos. Estime o bruto necessário com INSS e IRRF 2026.",
            [
                new FaqItem(
                    "Como funciona a calculadora inversa de salário?",
                    "Ela estima o salário bruto usando busca binária sobre as mesmas regras da calculadora de salário líquido (INSS progressivo e IRRF 2026)."),
                new FaqItem(
                    "O resultado é exato?",
                    "É uma estimativa com precisão de centavos. Pequenas diferenças podem ocorrer por arredondamentos das tabelas."),
                new FaqItem(
                    "Posso usar para negociar salário?",
                    "Sim, como referência educativa. Confirme valores com RH ou contador antes de formalizar proposta."),
                new FaqItem("Os dados são salvos?", "Não. O cálculo é feito na hora e não fica armazenado no servidor.")
            ]);

    private static CalculatorDefinition CreateCltPj() =>
        new(
            "pj-vs-clt",
            "PJ vs CLT",
            "Financeiro",
            "Compare CLT e PJ com extrato detalhado, pró-labore, Simples Nacional e faturamento equivalente.",
            "PJ vs CLT: comparativo avançado de líquido | Meu Valor Líquido",
            "Simule salário CLT x faturamento PJ com INSS, IRRF, Simples e pró-labore. Descubra quanto faturar para equivaler ao líquido CLT.",
            [
                new FaqItem(
                    "Como o PJ é estimado nesta calculadora?",
                    "Usamos faturamento mensal, alíquota do Simples Nacional (padrão 6%), pró-labore de 28% sobre o faturamento, INSS e IRRF sobre o pró-labore, além de despesas fixas opcionais."),
                new FaqItem(
                    "O que é faturamento PJ equivalente?",
                    "É o valor de faturamento estimado para que o líquido pessoal na PJ fique próximo ao líquido CLT informado, com os mesmos parâmetros tributários."),
                new FaqItem(
                    "PJ sempre compensa mais que CLT?",
                    "Não necessariamente. Depende do faturamento, regime tributário, despesas, benefícios CLT (férias, 13º, FGTS) e estabilidade. Esta ferramenta compara apenas o líquido mensal estimado."),
                new FaqItem(
                    "Posso comparar com MEI?",
                    "Para MEI com DAS fixo, use o <a href=\"/calculadoras/simulador-mei\">simulador MEI</a>. Esta página foca em PJ no Simples com pró-labore.")
            ]);

    private static CalculatorDefinition CreateSalaryProposal() =>
        new(
            "proposta-salarial",
            "Proposta salarial",
            "Trabalhista",
            "Compare salário atual e proposta: veja quanto entra no bolso, diferença anual e compartilhe a simulação.",
            "Proposta salarial: compare bruto x líquido | Meu Valor Líquido",
            "Simule aumento ou redução salarial com INSS e IRRF 2026. Ideal para negociação com RH — compartilhe o resultado.",
            [
                new FaqItem(
                    "Por que o aumento no líquido é menor que no bruto?",
                    "INSS e IRRF são progressivos. Parte do aumento bruto vira contribuição e imposto, então o ganho real no bolso costuma ser menor que o percentual anunciado no bruto."),
                new FaqItem(
                    "Posso usar na negociação com a empresa?",
                    "Sim, como referência educativa. Mostre quanto a proposta representa no líquido mensal e anual. Confirme valores finais com RH ou contrato."),
                new FaqItem(
                    "Os descontos são os mesmos nos dois cenários?",
                    "Sim. Dependentes, vale-transporte e outros descontos informados são aplicados igualmente para comparar de forma justa."),
                new FaqItem(
                    "Como compartilhar com meu gestor?",
                    "Após calcular, use WhatsApp, copiar link ou baixar PDF. O link reproduz a mesma simulação sem salvar dados no servidor.")
            ]);
}
