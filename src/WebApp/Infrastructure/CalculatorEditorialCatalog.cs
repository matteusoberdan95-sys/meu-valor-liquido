namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record CalculatorEditorialExample(
    string Title,
    string InputSummary,
    CalculatorInput Input,
    string Interpretation);

public sealed record OfficialSource(string Name, string Url);

public sealed record CalculatorEditorialContent(
    string Slug,
    string Summary,
    string HowItWorks,
    IReadOnlyList<string> IncludedItems,
    IReadOnlyList<string> ExcludedItems,
    CalculatorEditorialExample Example,
    string HowToInterpret,
    IReadOnlyList<string> CommonErrors,
    IReadOnlyList<OfficialSource> Sources,
    DateOnly LastReviewedAt,
    string ReviewedBy,
    IReadOnlyList<string> RelatedCalculatorSlugs,
    string EducationalNotice);

public sealed record CalculatorEditorialViewModel(
    CalculatorEditorialContent Content,
    CalculationResult ExampleResult,
    IReadOnlyList<CalculatorDefinition> RelatedCalculators);

public static class CalculatorEditorialCatalog
{
    private const string ReviewedBy = "Matteus Oberdan — responsável editorial";
    private static readonly DateOnly ReviewDate = new(2026, 7, 17);

    private static readonly OfficialSource CltSource = new(
        "Consolidação das Leis do Trabalho (Decreto-Lei nº 5.452/1943)",
        "https://www.planalto.gov.br/ccivil_03/decreto-lei/del5452.htm");

    private static readonly OfficialSource TaxTablesSource = new(
        "Portaria Interministerial MPS/MF nº 13/2026",
        "https://www.in.gov.br/en/web/dou/-/portaria-interministerial-mps/mf-n-13-de-9-de-janeiro-de-2026-680382603");

    private static readonly OfficialSource IrrfSource = new(
        "Lei nº 15.270/2025 — regras do IRPF",
        "https://www.planalto.gov.br/ccivil_03/_ato2023-2026/2025/lei/l15270.htm");

    private static readonly OfficialSource FgtsSource = new(
        "Lei nº 8.036/1990 — Fundo de Garantia do Tempo de Serviço",
        "https://www.planalto.gov.br/ccivil_03/leis/l8036consol.htm");

    private static readonly OfficialSource ThirteenthSource = new(
        "Lei nº 4.090/1962 — gratificação de Natal",
        "https://www.planalto.gov.br/ccivil_03/leis/l4090.htm");

    private static readonly OfficialSource SimplesSource = new(
        "Lei Complementar nº 123/2006 — Simples Nacional e MEI",
        "https://www.planalto.gov.br/ccivil_03/leis/lcp/lcp123.htm");

    private static readonly OfficialSource BcbSource = new(
        "Banco Central do Brasil — Calculadora do Cidadão",
        "https://www3.bcb.gov.br/CALCIDADAO/jsp/index.jsp");

    private static readonly IReadOnlyDictionary<string, CalculatorEditorialContent> Items =
        new Dictionary<string, CalculatorEditorialContent>(StringComparer.OrdinalIgnoreCase)
        {
            ["salario-liquido"] = new(
                "salario-liquido",
                "A calculadora estima quanto pode sobrar do salário bruto depois de INSS, IRRF e descontos informados. O resultado ajuda a conferir a ordem das deduções, mas não substitui o holerite.",
                "Primeiro o motor calcula o INSS de forma progressiva. Depois determina a base estimada do IRRF, aplica dependentes e a regra tributária vigente e, por fim, subtrai vale-transporte, alimentação, plano de saúde, pensão e outros descontos que você informar.",
                [
                    "Salário bruto mensal informado.",
                    "INSS progressivo e IRRF estimado com tabelas de 2026.",
                    "Dedução por dependentes quando aplicável.",
                    "Descontos opcionais preenchidos no formulário."
                ],
                [
                    "Regras específicas de convenção coletiva ou folha da empresa.",
                    "Adiantamentos, empréstimos e rubricas que não forem informados.",
                    "Benefícios pagos fora do holerite e ajustes retroativos."
                ],
                new(
                    "Exemplo: salário bruto de R$ 3.500,00, sem dependentes",
                    "Salário bruto de R$ 3.500,00, sem dependentes e sem descontos opcionais.",
                    new CalculatorInput(3500m),
                    "Compare o líquido e cada desconto com as rubricas do holerite. Diferenças podem surgir por benefícios, arredondamento da folha ou eventos de outros períodos."),
                "O valor principal é uma estimativa do líquido mensal. Use o extrato para identificar quanto foi destinado ao INSS, ao IRRF e a descontos particulares, em vez de avaliar apenas o total final.",
                [
                    "Informar salário líquido no campo de salário bruto.",
                    "Somar benefícios recebidos fora da folha ao salário tributável.",
                    "Esquecer descontos recorrentes ao comparar com o holerite."
                ],
                [TaxTablesSource, IrrfSource],
                ReviewDate,
                ReviewedBy,
                ["inss", "irrf", "conferir-holerite"],
                "Estimativa educativa. Confirme rubricas, competência e bases de cálculo no holerite ou com o RH."
            ),
            ["rescisao-clt"] = new(
                "rescisao-clt",
                "A calculadora organiza as principais verbas de encerramento de um contrato CLT conforme motivo, período trabalhado e opções de aviso prévio.",
                "O motor estima saldo de salário, 13º e férias proporcionais ou vencidas, aviso prévio e multa do FGTS quando cabíveis. Em seguida aplica os descontos tributários e valores informados, respeitando o motivo do desligamento selecionado.",
                [
                    "Motivo do desligamento, datas e dias trabalhados no mês.",
                    "Férias, 13º, aviso prévio e multa rescisória aplicáveis ao cenário.",
                    "INSS e IRRF estimados sobre verbas tributáveis.",
                    "Saldo de FGTS e adiantamentos quando informados."
                ],
                [
                    "Horas extras, comissões ou adicionais não informados.",
                    "Multas, indenizações judiciais e regras de convenção coletiva.",
                    "Saldo oficial da conta FGTS quando ele não for preenchido."
                ],
                new(
                    "Exemplo: demissão sem justa causa após 12 meses",
                    "Salário de R$ 3.000,00, 15 dias trabalhados no mês, 12 meses no período e demissão sem justa causa.",
                    new CalculatorInput(
                        3000m,
                        SecondaryAmount: 15m,
                        Months: 12,
                        TerminationReason: TerminationReason.DismissalWithoutCause),
                    "Leia cada verba separadamente e confira se o motivo e o aviso prévio correspondem ao TRCT. O total não confirma sozinho que todas as rubricas estão corretas."),
                "O total líquido reúne verbas com naturezas diferentes. Confira saldo de salário, férias, 13º, aviso e FGTS em linhas separadas e valide datas e motivo antes de comparar com o documento da empresa.",
                [
                    "Selecionar pedido de demissão quando houve dispensa pela empresa.",
                    "Usar meses aproximados quando as datas exatas estão disponíveis.",
                    "Tratar o saldo estimado de FGTS como extrato oficial da Caixa."
                ],
                [CltSource, FgtsSource, ThirteenthSource],
                ReviewDate,
                ReviewedBy,
                ["ferias", "decimo-terceiro", "fgts"],
                "Estimativa educativa. Rescisões podem depender de contrato, convenção coletiva e fatos que exigem análise profissional."
            ),
            ["ferias"] = new(
                "ferias",
                "A calculadora estima férias integrais ou proporcionais, adicional de um terço, abono pecuniário e descontos tributários conforme as opções selecionadas.",
                "O valor-base é ajustado pela quantidade de avos ou dias de férias. O motor acrescenta o terço constitucional, inclui abono ou pagamento em dobro quando selecionado e estima INSS e IRRF sobre as parcelas tratadas pelo modelo.",
                [
                    "Salário, meses adquiridos e quantidade de dias de férias.",
                    "Terço constitucional.",
                    "Venda de até um terço e férias em dobro quando selecionadas.",
                    "INSS e IRRF estimados."
                ],
                [
                    "Médias variáveis não preenchidas, como comissão e hora extra.",
                    "Regras específicas de acordo ou convenção coletiva.",
                    "Descontos particulares lançados pela empresa."
                ],
                new(
                    "Exemplo: férias integrais sobre salário de R$ 3.000,00",
                    "Salário de R$ 3.000,00, 12 avos, 30 dias, sem venda de dias e sem dependentes.",
                    new CalculatorInput(3000m, Months: 12),
                    "O bruto inclui férias e um terço; o líquido desconta tributos estimados. Compare com o recibo de férias e verifique médias e adiantamentos."),
                "Diferencie valor bruto, descontos e líquido. Se houver abono, observe a linha correspondente; se as férias forem proporcionais, confirme a quantidade de avos considerada.",
                [
                    "Confundir férias gozadas com férias proporcionais de rescisão.",
                    "Informar 12 avos sem ter completado o período aquisitivo.",
                    "Ignorar médias de remuneração variável."
                ],
                [CltSource],
                ReviewDate,
                ReviewedBy,
                ["salario-liquido", "decimo-terceiro", "rescisao-clt"],
                "Estimativa educativa. Datas do período aquisitivo, médias e regras coletivas podem alterar o pagamento."
            ),
            ["decimo-terceiro"] = new(
                "decimo-terceiro",
                "A calculadora estima o décimo terceiro integral ou proporcional, separando avos, adiantamento e descontos que normalmente recaem na parcela final.",
                "O motor multiplica a remuneração pela fração de meses computados no ano. Depois considera o adiantamento informado e estima INSS e IRRF sobre a base aplicável.",
                [
                    "Salário, médias adicionais, avos e dependentes.",
                    "Primeira parcela ou adiantamento já recebido.",
                    "INSS e IRRF estimados na apuração anual do benefício."
                ],
                [
                    "Médias de comissão, adicionais ou horas extras não informadas.",
                    "Ajustes feitos pela empresa depois do fechamento da folha.",
                    "Regras específicas de afastamento não representadas pelos campos."
                ],
                new(
                    "Exemplo: décimo terceiro integral de R$ 3.000,00",
                    "Salário de R$ 3.000,00, 12 avos, sem dependentes e sem adiantamento informado.",
                    new CalculatorInput(3000m, Months: 12),
                    "O resultado mostra o benefício bruto e o líquido estimado. Se a primeira parcela já foi paga, informe-a para estimar apenas o saldo restante."),
                "Confira os avos antes do total. A primeira parcela costuma ser adiantada sem os descontos finais; por isso, o valor da segunda parcela não corresponde simplesmente à metade do bruto.",
                [
                    "Contar como avo um mês com menos de 15 dias trabalhados.",
                    "Não informar a primeira parcela já recebida.",
                    "Comparar o líquido anual com apenas uma das parcelas."
                ],
                [ThirteenthSource, TaxTablesSource, IrrfSource],
                ReviewDate,
                ReviewedBy,
                ["salario-liquido", "ferias", "rescisao-clt"],
                "Estimativa educativa. A folha pode ajustar médias e afastamentos no fechamento do décimo terceiro."
            ),
            ["inss"] = new(
                "inss",
                "A calculadora estima a contribuição previdenciária do empregado usando as faixas progressivas vigentes em 2026.",
                "Cada parcela do salário é tributada pela alíquota da faixa correspondente. O motor soma as parcelas até o teto contributivo; não aplica uma única alíquota sobre todo o salário.",
                [
                    "Salário de contribuição informado.",
                    "Faixas progressivas e teto previdenciário de 2026.",
                    "Valor total estimado da contribuição do empregado."
                ],
                [
                    "Contribuição patronal ou encargos pagos pela empresa.",
                    "Regras de contribuinte individual, facultativo ou regime próprio.",
                    "Múltiplos vínculos e ajustes de outras folhas."
                ],
                new(
                    "Exemplo: salário de contribuição de R$ 3.000,00",
                    "Base mensal de R$ 3.000,00 em um único vínculo.",
                    new CalculatorInput(3000m),
                    "A linha INSS mostra a soma das parcelas progressivas. A diferença entre bruto e resultado principal representa apenas essa contribuição nesta ferramenta."),
                "A alíquota efetiva é menor que a maior alíquota nominal atingida, porque cada faixa incide somente sobre uma parte da base. Acima do teto, a contribuição deixa de crescer.",
                [
                    "Multiplicar todo o salário pela última alíquota da tabela.",
                    "Usar salário líquido como base.",
                    "Ignorar outro vínculo empregatício no mesmo mês."
                ],
                [TaxTablesSource],
                ReviewDate,
                ReviewedBy,
                ["salario-liquido", "irrf", "conferir-holerite"],
                "Estimativa educativa para empregado em um vínculo. Confirme a base e eventuais ajustes no eSocial ou holerite."
            ),
            ["irrf"] = new(
                "irrf",
                "A calculadora estima o imposto de renda retido na fonte a partir da base tributável ou do salário bruto, conforme a opção selecionada.",
                "Quando você informa salário bruto, o motor estima o INSS antes de chegar à base do IRRF. Depois aplica dependentes, faixas progressivas, parcela a deduzir e a redução legal vigente em 2026.",
                [
                    "Base tributável ou salário bruto, conforme o modo escolhido.",
                    "INSS estimado no modo de salário bruto.",
                    "Dependentes, faixas progressivas e redução legal de 2026."
                ],
                [
                    "Outras deduções legais não representadas no formulário.",
                    "Ajuste anual da declaração de imposto de renda.",
                    "Rendimentos de outras fontes pagadoras."
                ],
                new(
                    "Exemplo: base tributável de R$ 6.000,00",
                    "Base mensal de IRRF de R$ 6.000,00, sem dependentes, informada diretamente.",
                    new CalculatorInput(6000m),
                    "O resultado principal é a base menos o IRRF estimado, não o salário líquido completo. Para considerar INSS e descontos de folha, use o modo bruto ou a calculadora de salário líquido."),
                "Observe se o formulário está no modo base tributável ou salário bruto. A faixa exibida não significa que toda a renda foi tributada por aquela alíquota.",
                [
                    "Informar salário bruto no modo de base tributável.",
                    "Aplicar a alíquota máxima sobre todo o valor.",
                    "Confundir retenção mensal com imposto definitivo da declaração anual."
                ],
                [IrrfSource, TaxTablesSource],
                ReviewDate,
                ReviewedBy,
                ["salario-liquido", "inss", "conferir-holerite"],
                "Estimativa educativa de retenção mensal. A declaração anual e outras fontes de renda podem produzir resultado diferente."
            ),
            ["hora-extra"] = new(
                "hora-extra",
                "A calculadora estima remuneração de horas extras a partir do salário ou valor-hora, jornada semanal, adicional e tipo de turno.",
                "O motor encontra o divisor mensal da jornada, calcula o valor da hora normal, aplica o adicional informado ou mínimo do turno e acrescenta um reflexo estimado de descanso semanal remunerado.",
                [
                    "Salário mensal ou valor da hora.",
                    "Quantidade de horas extras e jornada semanal.",
                    "Adicional de dia útil, domingo/feriado, noturno ou CCT.",
                    "Reflexo estimado de DSR."
                ],
                [
                    "Reflexos completos em férias, 13º, FGTS e rescisão.",
                    "Banco de horas e compensações.",
                    "Regras específicas da convenção coletiva."
                ],
                new(
                    "Exemplo: 10 horas extras sobre salário de R$ 3.000,00",
                    "Salário mensal de R$ 3.000,00, 10 horas extras em dia útil, adicional de 50% e jornada padrão.",
                    new CalculatorInput(1m, SecondaryAmount: 3000m, Hours: 10m, Rate: 50m),
                    "Confira o divisor, o adicional e o DSR exibidos. Se a categoria tiver percentual superior, altere o adicional conforme a convenção coletiva."),
                "O resultado principal soma a remuneração das horas e o DSR estimado. Ele não representa necessariamente todo o impacto dessas horas nas demais verbas da folha.",
                [
                    "Usar o salário mensal como se fosse valor-hora.",
                    "Ignorar a jornada semanal que define o divisor.",
                    "Não conferir adicional noturno ou percentual da convenção."
                ],
                [CltSource],
                ReviewDate,
                ReviewedBy,
                ["salario-liquido", "decimo-terceiro", "ferias"],
                "Estimativa educativa. Jornada, banco de horas e convenção coletiva devem ser conferidos com o contrato e o RH."
            ),
            ["fgts"] = new(
                "fgts",
                "A calculadora estima depósitos mensais de FGTS, saldo do período e multa rescisória conforme o motivo de desligamento.",
                "O motor aplica 8% sobre o salário informado em cada mês, soma o saldo anterior opcional e calcula multa de 40% ou 20% quando o motivo selecionado prevê essa parcela.",
                [
                    "Salário mensal, quantidade de meses e saldo anterior informado.",
                    "Depósitos mensais estimados de 8%.",
                    "Multa de 40% ou 20% nos cenários previstos pelo modelo."
                ],
                [
                    "Correção monetária e rendimentos reais da conta vinculada.",
                    "Competências sem depósito ou salários variáveis.",
                    "Saques, antecipações e movimentações não informadas."
                ],
                new(
                    "Exemplo: salário de R$ 3.000,00 por 12 meses",
                    "Salário mensal de R$ 3.000,00, 12 meses e demissão sem justa causa, sem saldo anterior.",
                    new CalculatorInput(
                        3000m,
                        Months: 12,
                        TerminationReason: TerminationReason.DismissalWithoutCause),
                    "O extrato separa depósitos e multa. Compare o saldo com o aplicativo FGTS; a estimativa não substitui o extrato da conta vinculada."),
                "O total com multa é um cenário rescisório, não necessariamente o valor disponível para saque. Observe o motivo selecionado e se um saldo anterior foi informado.",
                [
                    "Confundir depósito do empregador com desconto do salário.",
                    "Aplicar multa de 40% em pedido de demissão.",
                    "Usar o saldo estimado como comprovante oficial."
                ],
                [FgtsSource],
                ReviewDate,
                ReviewedBy,
                ["rescisao-clt", "salario-liquido", "ferias"],
                "Estimativa educativa. Consulte o extrato oficial da conta FGTS para depósitos, rendimentos e saldo disponível."
            ),
            ["pj-vs-clt"] = new(
                "pj-vs-clt",
                "A calculadora compara uma estimativa mensal de remuneração CLT com um cenário PJ, incluindo benefícios provisionados, tributos e despesas informadas.",
                "O lado CLT estima líquido, FGTS, férias e décimo terceiro provisionados. O lado PJ aplica anexo, alíquota, pró-labore, INSS, IRRF e despesas informadas para mostrar cenários comparáveis.",
                [
                    "Salário CLT, dependentes e descontos preenchidos.",
                    "Faturamento PJ, anexo, alíquota, pró-labore e despesas.",
                    "Provisões mensais de férias, 13º e FGTS para comparação."
                ],
                [
                    "Enquadramento tributário definitivo, fator R e contabilidade real da empresa.",
                    "Riscos contratuais, estabilidade, benefícios não quantificados e períodos sem faturamento.",
                    "Impostos municipais ou particularidades da atividade não informadas."
                ],
                new(
                    "Exemplo: R$ 5.000,00 CLT versus R$ 9.000,00 PJ",
                    "Salário CLT de R$ 5.000,00, faturamento PJ de R$ 9.000,00, alíquota de 6% e parâmetros padrão de pró-labore.",
                    new CalculatorInput(5000m, SecondaryAmount: 9000m, Rate: 6m),
                    "Compare líquido mensal, provisões e despesas em linhas separadas. O maior valor imediato não significa automaticamente melhor contrato."),
                "Use o resultado para identificar quais premissas tornam os cenários equivalentes. Faça uma reserva para férias, décimo terceiro e períodos sem contrato antes de comparar somente o dinheiro do mês.",
                [
                    "Comparar salário CLT bruto diretamente com faturamento PJ.",
                    "Ignorar férias, 13º, FGTS, benefícios e custos contábeis.",
                    "Escolher alíquota ou anexo sem validação contábil."
                ],
                [CltSource, SimplesSource, FgtsSource],
                ReviewDate,
                ReviewedBy,
                ["salario-liquido", "simulador-mei", "custo-funcionario"],
                "Estimativa educativa. A escolha entre CLT e PJ exige análise contratual, tributária e profissional do caso concreto."
            ),
            ["simulador-mei"] = new(
                "simulador-mei",
                "A calculadora estima o DAS mensal do MEI e sinaliza como o faturamento informado se relaciona com o limite anual considerado pelo sistema.",
                "O motor identifica a atividade selecionada, aplica o valor fixo estimado do DAS de 2026 e projeta faturamento anual ou acumulado para exibir alertas de limite.",
                [
                    "Faturamento mensal e acumulado informado.",
                    "Tipo de atividade: comércio/indústria, serviços ou atividade mista.",
                    "DAS estimado e alertas de limite anual."
                ],
                [
                    "Elegibilidade da ocupação e situação cadastral do CNPJ.",
                    "Débitos, multas, juros e declarações em atraso.",
                    "Transição detalhada para microempresa após desenquadramento."
                ],
                new(
                    "Exemplo: prestação de serviços com R$ 5.000,00 por mês",
                    "Faturamento mensal de R$ 5.000,00, atividade de serviços e sem acumulado anterior informado.",
                    new CalculatorInput(5000m, MeiActivity: MeiActivityType.Services),
                    "Observe o DAS e a projeção anual. A projeção não confirma enquadramento: atividade permitida, data de abertura e faturamento acumulado também precisam ser verificados."),
                "O DAS é uma obrigação fixa estimada, enquanto o alerta de limite depende do faturamento. Informe o acumulado real do ano para evitar uma projeção incompleta.",
                [
                    "Confundir faturamento com lucro ou salário pessoal.",
                    "Não informar receitas acumuladas no ano.",
                    "Assumir que qualquer atividade pode ser registrada como MEI."
                ],
                [SimplesSource],
                ReviewDate,
                ReviewedBy,
                ["pj-vs-clt", "salario-liquido", "custo-funcionario"],
                "Estimativa educativa. Confirme atividade permitida, limite proporcional e obrigações no Portal do Empreendedor ou com contador."
            ),
            ["juros-compostos"] = new(
                "juros-compostos",
                "A calculadora projeta o crescimento de um capital com taxa mensal, prazo e aportes feitos ao fim de cada período.",
                "Em cada mês o motor aplica a taxa sobre o saldo acumulado e depois soma o aporte. Ao final, separa o total investido dos juros obtidos no cenário.",
                [
                    "Capital inicial, aporte mensal, taxa mensal e prazo.",
                    "Capitalização mensal composta.",
                    "Total investido e juros acumulados."
                ],
                [
                    "Imposto de renda, IOF, taxas e custos do produto.",
                    "Inflação e variação de taxas futuras.",
                    "Risco, liquidez e garantia do investimento."
                ],
                new(
                    "Exemplo: R$ 1.000,00 iniciais por 12 meses",
                    "Capital inicial de R$ 1.000,00, aporte mensal de R$ 200,00, taxa de 1% ao mês e prazo de 12 meses.",
                    new CalculatorInput(1000m, SecondaryAmount: 200m, Months: 12, Rate: 1m),
                    "Separe o que foi efetivamente aportado dos juros projetados. Uma taxa constante é apenas hipótese matemática, não promessa de rentabilidade."),
                "O montante final combina capital e juros. Compare cenários com a mesma unidade de taxa e prazo; 1% ao mês não deve ser digitado como 12% ao mês.",
                [
                    "Misturar taxa anual com campo de taxa mensal.",
                    "Tratar rentabilidade passada ou simulada como garantia.",
                    "Ignorar impostos, taxas e inflação na decisão."
                ],
                [BcbSource],
                ReviewDate,
                ReviewedBy,
                ["financiamento", "salario-liquido", "pj-vs-clt"],
                "Simulação matemática educativa. Não constitui recomendação nem garantia de rendimento."
            ),
            ["financiamento"] = new(
                "financiamento",
                "A calculadora estima parcelas e custo de juros nos sistemas Price e SAC usando valor financiado, taxa mensal e prazo.",
                "No Price, o motor calcula uma prestação fixa pela fórmula financeira. No SAC, usa amortização constante e parcelas decrescentes. O modo comparativo mostra os dois cenários com a mesma entrada.",
                [
                    "Principal financiado, taxa mensal, prazo e sistema de amortização.",
                    "Parcela Price ou primeira e última parcelas SAC.",
                    "Total pago e juros totais estimados."
                ],
                [
                    "Entrada, seguros, tarifas, tributos e custos cartorários não informados.",
                    "Custo Efetivo Total diferente da taxa digitada.",
                    "Correção por índices, renegociação ou atraso."
                ],
                new(
                    "Exemplo: financiamento de R$ 100.000,00",
                    "Valor de R$ 100.000,00, taxa de 0,9% ao mês, prazo de 360 meses e sistema Price.",
                    new CalculatorInput(100_000m, Months: 360, Rate: 0.9m),
                    "Compare a parcela com o total pago e os juros. Uma parcela que cabe no mês ainda pode representar custo total elevado ao longo de um prazo extenso."),
                "A taxa informada não é necessariamente o CET. Use o total pago para comparar propostas e solicite à instituição a planilha com seguros, tarifas e demais encargos.",
                [
                    "Informar taxa anual no campo mensal.",
                    "Comparar propostas apenas pelo valor da parcela.",
                    "Ignorar CET, seguros, tarifas e valor de entrada."
                ],
                [BcbSource],
                ReviewDate,
                ReviewedBy,
                ["juros-compostos", "salario-liquido", "pj-vs-clt"],
                "Simulação financeira educativa. Solicite o CET e as condições contratuais oficiais antes de contratar."
            )
        };

    private static readonly IReadOnlyDictionary<string, IReadOnlyList<FaqItem>> EditorialFaqs =
        new Dictionary<string, IReadOnlyList<FaqItem>>(StringComparer.OrdinalIgnoreCase)
        {
            ["salario-liquido"] =
            [
                new("Qual é a ordem dos descontos no salário líquido?", "A estimativa calcula primeiro o INSS, usa as deduções aplicáveis para chegar à base do IRRF e depois subtrai os descontos opcionais informados."),
                new("Por que o resultado pode ser diferente do meu holerite?", "Benefícios, empréstimos, adiantamentos, arredondamentos e eventos de outras competências podem alterar a folha real.")
            ],
            ["rescisao-clt"] =
            [
                new("O saldo do FGTS já está incluído na rescisão?", "A ferramenta pode usar o saldo informado para estimar a multa, mas o saldo da conta vinculada e a possibilidade de saque devem ser conferidos no extrato oficial."),
                new("Por que as datas mudam férias e décimo terceiro?", "A quantidade de dias e avos depende do período efetivamente trabalhado; frações e datas podem alterar as verbas proporcionais.")
            ],
            ["ferias"] =
            [
                new("O que muda quando vendo dez dias de férias?", "O abono pecuniário acrescenta o valor dos dias vendidos conforme a opção selecionada, sem transformar todo o período em dinheiro."),
                new("Quando o pagamento em dobro deve ser usado?", "A opção representa um cenário específico de férias devidas em dobro; confirme se a situação jurídica se aplica antes de selecioná-la.")
            ],
            ["decimo-terceiro"] =
            [
                new("Como a calculadora conta os avos do décimo terceiro?", "Cada mês computável representa 1/12. Em geral, mês com pelo menos 15 dias trabalhados conta como avo."),
                new("Por que a segunda parcela é menor?", "A parcela final concentra o desconto do adiantamento e os tributos estimados, por isso não costuma equivaler simplesmente à metade do bruto.")
            ],
            ["inss"] =
            [
                new("O INSS é calculado sobre o salário inteiro pela maior alíquota?", "Não. A contribuição do empregado é progressiva: cada faixa incide somente sobre a parcela do salário que está dentro dela."),
                new("O desconto continua aumentando acima do teto?", "No modelo de empregado usado pela ferramenta, a contribuição fica limitada ao teto previdenciário vigente.")
            ],
            ["irrf"] =
            [
                new("Devo informar salário bruto ou base do IRRF?", "Escolha o modo correspondente. No modo bruto, a ferramenta estima o INSS; no modo de base, informe o valor tributável já apurado."),
                new("O IRRF mensal é o imposto definitivo?", "Não necessariamente. A retenção mensal é uma antecipação e pode ser ajustada na declaração anual conforme rendimentos e deduções.")
            ],
            ["hora-extra"] =
            [
                new("Qual adicional usar para hora extra?", "A ferramenta aceita percentual informado e aplica mínimos conforme o tipo de turno. Convenções coletivas podem prever percentuais diferentes."),
                new("O que é o DSR estimado?", "É um reflexo simplificado das horas extras no descanso semanal remunerado, calculado pelas premissas exibidas no resultado.")
            ],
            ["fgts"] =
            [
                new("O depósito de FGTS é descontado do salário?", "Não. No contrato CLT comum, o depósito é obrigação do empregador e não deve ser tratado como desconto do salário líquido."),
                new("Quando existe multa de 40% ou 20%?", "A estimativa usa 40% na dispensa sem justa causa e 20% no acordo do artigo 484-A; outros motivos podem não gerar multa.")
            ],
            ["pj-vs-clt"] =
            [
                new("Posso comparar salário CLT diretamente com faturamento PJ?", "Não é uma comparação equivalente. O cenário PJ precisa descontar tributos, pró-labore, despesas e reservas que a ferramenta apresenta separadamente."),
                new("A ferramenta decide qual contrato é melhor?", "Não. Ela organiza premissas financeiras; estabilidade, riscos, benefícios e condições jurídicas também precisam ser avaliados.")
            ],
            ["simulador-mei"] =
            [
                new("Faturamento do MEI é a mesma coisa que lucro?", "Não. Faturamento é a receita bruta; despesas da atividade precisam ser descontadas para avaliar o resultado econômico."),
                new("Ultrapassar a projeção anual causa desenquadramento automático?", "A projeção é apenas alerta. O efeito depende do faturamento real, data de abertura e regras aplicáveis, que devem ser conferidos oficialmente.")
            ],
            ["juros-compostos"] =
            [
                new("Posso informar uma taxa anual no campo mensal?", "Não diretamente. A unidade da taxa deve ser a mesma do período; converter taxa anual para mensal exige equivalência composta."),
                new("Quando o aporte mensal entra no cálculo?", "O modelo adiciona o aporte ao fim de cada período, depois de aplicar os juros daquele mês sobre o saldo anterior.")
            ],
            ["financiamento"] =
            [
                new("Qual é a diferença entre Price e SAC?", "No Price a prestação calculada é fixa; no SAC a amortização é constante e as parcelas tendem a diminuir ao longo do prazo."),
                new("A taxa digitada representa o CET?", "Não necessariamente. O Custo Efetivo Total pode incluir seguros, tarifas e outros encargos ausentes da taxa nominal.")
            ]
        };

    public static IReadOnlyCollection<CalculatorEditorialContent> All => Items.Values.ToArray();

    public static CalculatorEditorialContent? GetBySlug(string slug) =>
        Items.GetValueOrDefault(slug);

    public static IReadOnlyList<FaqItem> GetFaqs(string slug) =>
        EditorialFaqs.GetValueOrDefault(slug) ?? [];
}
