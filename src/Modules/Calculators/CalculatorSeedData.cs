namespace MeuValorLiquido.Modules.Calculators;
public static class CalculatorSeedData
{
    public static IReadOnlyList<CalculatorDefinition> GetDefinitions() =>
    [
        Create("salario-liquido", "Salário líquido", "Trabalhista", "Estime quanto sobra do salário após INSS, IRRF e descontos comuns."),
        CreateRequiredGrossSalary(),
        CreateSalaryProposal(),
        CreateVacation(),
        CreateThirteenth(),
        Create("rescisao-clt", "Rescisão CLT", "Trabalhista", "Simule todos os tipos de desligamento: demissão, pedido de demissão, acordo 484-A e justa causa."),
        Create("hora-extra", "Hora extra", "Trabalhista", "Calcule horas extras com adicional de CCT, jornada semanal, turno noturno e DSR."),
        Create("inss", "INSS", "Fiscal", "Estime o desconto de INSS pela tabela progressiva oficial de 2026 (Portaria MPS/MF nº 13)."),
        Create("irrf", "IRRF", "Fiscal", "Estime o IRRF com tabela progressiva e redução legal de 2026 (Lei 15.270/2025)."),
        CreateCltPj(),
        Create("juros-compostos", "Juros compostos", "Financeiro", "Projete o crescimento de um valor com taxa mensal e prazo."),
        Create("financiamento", "Financiamento", "Financeiro", "Estime parcelas no sistema Price ou SAC e compare o custo total de juros."),
        Create("fgts", "FGTS", "Trabalhista", "Calcule depósitos mensais de 8%, saldo acumulado e multa rescisória."),
        CreateSeguroDesemprego(),
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

    private static CalculatorDefinition CreateVacation() =>
        new(
            "ferias",
            "Férias",
            "Trabalhista",
            "Calcule férias integrais ou proporcionais com 1/3 constitucional, abono pecuniário e descontos de INSS e IRRF.",
            "Calculadora de férias CLT com 1/3 e abono | Meu Valor Líquido",
            "Estime férias gozadas ou proporcionais, abono pecuniário (venda de 1/3), férias em dobro e líquido após INSS/IRRF 2026.",
            [
                new FaqItem(
                    "O que é abono pecuniário?",
                    "É a conversão de até 1/3 dos dias de férias em dinheiro. Você goza o restante e recebe o abono junto com as férias + 1/3."),
                new FaqItem(
                    "Férias proporcionais usam quantos meses?",
                    "Informe os meses trabalhados ou as datas de admissão e saída. Cada mês completo ou fração acima de 15 dias conta como avo."),
                new FaqItem(
                    "Quando as férias são pagas em dobro?",
                    "Quando o empregador atrasa a concessão além do prazo legal. A calculadora dobra férias + 1/3 como estimativa educativa."),
                new FaqItem(
                    "Como isso se relaciona com a rescisão?",
                    "Na rescisão, férias proporcionais e vencidas entram nas verbas. Use a <a href=\"/calculadoras/rescisao-clt\">calculadora de rescisão</a> para o pacote completo.")
            ]);

    private static CalculatorDefinition CreateThirteenth() =>
        new(
            "decimo-terceiro",
            "Décimo terceiro",
            "Trabalhista",
            "Estime o 13º proporcional com 1ª e 2ª parcela, adiantamento já pago e descontos de INSS e IRRF.",
            "Calculadora de décimo terceiro 2026 | Meu Valor Líquido",
            "Simule 13º salário proporcional, parcelas de novembro e dezembro, adiantamento e líquido com tabelas 2026.",
            [
                new FaqItem(
                    "Como funciona a 1ª e a 2ª parcela?",
                    "A 1ª parcela (até 30/11) costuma ser 50% do 13º sem descontos. A 2ª parcela recebe INSS e IRRF sobre o valor integral do 13º."),
                new FaqItem(
                    "O que é adiantamento do 13º?",
                    "Algumas empresas pagam parte do 13º antes de novembro. Informe o valor já recebido para estimar o que ainda falta no ano."),
                new FaqItem(
                    "Quantos avos são considerados?",
                    "Cada mês trabalhado no ano vale 1/12. A regra dos 15 dias também se aplica quando você informa datas completas."),
                new FaqItem(
                    "O 13º na rescisão é o mesmo cálculo?",
                    "A lógica de avos é parecida, mas na rescisão entram outras verbas. Confira na <a href=\"/calculadoras/rescisao-clt\">calculadora de rescisão CLT</a>.")
            ]);

    private static CalculatorDefinition CreateSeguroDesemprego() =>
        new(
            "seguro-desemprego",
            "Seguro-desemprego",
            "Trabalhista",
            "Estime o valor das parcelas do seguro-desemprego com base no salário médio e tempo de carteira.",
            "Calculadora de seguro-desemprego 2026 | Meu Valor Líquido",
            "Simule parcelas do seguro-desemprego após demissão sem justa causa. Tabela MTE 2026, carência e quantidade de parcelas.",
            [
                new FaqItem(
                    "Como o valor da parcela é calculado?",
                    "Usamos a média dos últimos salários brutos e a tabela do MTE vigente em 2026: até R$ 2.222,17 (80% da média), faixa intermediária com 50% do excedente + R$ 1.777,74, e teto de R$ 2.518,65. O piso é o salário mínimo (R$ 1.621,00)."),
                new FaqItem(
                    "Quantas parcelas posso receber?",
                    "Depende dos meses com carteira nos últimos 36 meses: 3 parcelas (6 a 11 meses), 4 parcelas (12 a 23) ou 5 parcelas (24 ou mais). A carência mínima varia conforme solicitações anteriores."),
                new FaqItem(
                    "Pedido de demissão dá direito?",
                    "Em regra, não. O benefício é típico da demissão sem justa causa pelo empregador, com requisitos de tempo de vínculo e ausência de renda própria."),
                new FaqItem(
                    "O resultado é oficial?",
                    "Não. É estimativa educativa. O valor definitivo só o governo calcula na solicitação (Caixa ou gov.br), após análise dos requisitos legais.")
            ],
            """
            <p>O <strong>seguro-desemprego</strong> é pago pelo governo ao trabalhador CLT dispensado sem justa causa (e em alguns términos de contrato), desde que cumpra carência de tempo de serviço e não tenha renda suficiente para se manter.</p>
            <p>Informe os últimos salários brutos, o tempo de carteira e se já solicitou o benefício antes. A simulação usa a <strong>tabela do MTE de 2026</strong> (vigente desde 11/01/2026) e não substitui a análise oficial na Caixa ou no portal gov.br.</p>
            """);
}
