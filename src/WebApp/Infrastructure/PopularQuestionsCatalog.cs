namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record PopularQuestionDefinition(
    string Slug,
    string Title,
    string Category,
    string SeoDescription,
    string AnswerHtml,
    string? RelatedCalculatorSlug,
    IReadOnlyList<PopularQuestionFaqItem> FaqItems,
    IReadOnlyList<string> RelatedQuestionSlugs);

public sealed record PopularQuestionFaqItem(string Question, string AnswerHtml);

public static class PopularQuestionsCatalog
{
    private static readonly IReadOnlyList<PopularQuestionDefinition> All =
    [
        Create(
            "como-calcular-salario-liquido",
            "Como calcular salário líquido?",
            "Trabalhista",
            "Entenda o passo a passo para estimar salário líquido com INSS, IRRF e descontos em 2026.",
            """
            <p>O salário líquido é o que cai na conta após descontos obrigatórios e autorizados. Em linhas gerais:</p>
            <ol>
            <li>Parta do <strong>salário bruto</strong> (valor do contrato).</li>
            <li>Desconte o <strong>INSS</strong> (tabela progressiva de 2026).</li>
            <li>Calcule o <strong>IRRF</strong> sobre a base após INSS, com redução legal quando aplicável.</li>
            <li>Subtraia descontos como <strong>vale-transporte</strong> e outros autorizados.</li>
            </ol>
            <p>Use a <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> para simular com seus números ou consulte <a href="/salario-liquido">valores brutos comuns</a>.</p>
            """,
            "salario-liquido",
            [
                new("O INSS sempre é descontado?", "Sim, para trabalhadores CLT o INSS é obrigatório sobre o salário, dentro do teto previdenciário."),
                new("Plano de saúde entra no cálculo?", "Descontos autorizados (plano, VT etc.) reduzem o líquido após impostos. Informe-os na calculadora completa.")
            ],
            ["diferenca-salario-bruto-e-liquido", "quanto-desconta-inss-2026", "irrf-quem-paga-e-como-calcular"]),

        Create(
            "diferenca-salario-bruto-e-liquido",
            "Qual a diferença entre salário bruto e líquido?",
            "Trabalhista",
            "Bruto é o valor do contrato; líquido é o que você recebe após INSS, IRRF e outros descontos.",
            """
            <p><strong>Salário bruto</strong> é o valor acordado com a empresa, antes de qualquer desconto. É a referência em propostas de emprego e dissídios.</p>
            <p><strong>Salário líquido</strong> é o valor efetivamente recebido após INSS, IRRF, vale-transporte e demais descontos legais ou autorizados.</p>
            <p>Comparar propostas só pelo bruto pode enganar: dois salários brutos iguais podem ter líquidos diferentes por dependentes, faixa de IRRF ou descontos. Simule na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a>.</p>
            """,
            "salario-liquido",
            [
                new("Holérine mostra bruto ou líquido?", "O holerite lista ambos: bruto nas verbas, líquido no total final após descontos."),
                new("Benefícios entram no bruto?", "VT e VR costumam ser descontados do salário; plano de saúde também pode aparecer como desconto.")
            ],
            ["como-calcular-salario-liquido", "proposta-salarial-como-negociar", "salario-minimo-liquido-2026"]),

        Create(
            "quanto-desconta-inss-2026",
            "Quanto desconta de INSS em 2026?",
            "Fiscal",
            "O INSS é progressivo por faixas em 2026. Veja como estimar o desconto sobre o salário.",
            """
            <p>Em 2026, o INSS segue alíquotas progressivas por faixa de salário de contribuição, até o teto previdenciário. Quanto maior o salário, maior a parcela efetiva descontada — mas nunca acima do teto.</p>
            <p>Para um valor exato no seu caso, use a <a href="/calculadoras/inss">calculadora de INSS</a> ou o extrato completo na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a>.</p>
            """,
            "inss",
            [
                new("Autônomo paga INSS diferente?", "Sim. Contribuintes individuais e pró-labore têm regras próprias; esta página foca no desconto CLT em folha."),
                new("Existe teto de contribuição?", "Sim. Acima do teto de contribuição, não há INSS adicional sobre o excedente na folha CLT.")
            ],
            ["irrf-quem-paga-e-como-calcular", "como-calcular-salario-liquido", "salario-minimo-liquido-2026"]),

        Create(
            "irrf-quem-paga-e-como-calcular",
            "IRRF: quem paga e como calcular?",
            "Fiscal",
            "Entenda quando o imposto de renda é retido na fonte e como estimar o IRRF em 2026.",
            """
            <p>O <strong>Imposto de Renda Retido na Fonte (IRRF)</strong> incide sobre rendimentos do trabalho quando a base tributável ultrapassa os limites de isenção. A base é calculada após o desconto do INSS, com dedução por dependente quando houver.</p>
            <p>Em 2026, há redução legal para bases menores (Lei 15.270/2025). Simule na <a href="/calculadoras/irrf">calculadora de IRRF</a> ou veja o impacto no <a href="/calculadoras/salario-liquido">salário líquido</a>.</p>
            """,
            "irrf",
            [
                new("Ter dependente reduz IRRF?", "Sim. Cada dependente legal reduz a base de cálculo conforme tabela vigente."),
                new("Recebo restituição se paguei IRRF?", "Depende da declaração anual e do seu caso. O retido na fonte é antecipação do imposto devido.")
            ],
            ["quanto-desconta-inss-2026", "como-calcular-salario-liquido", "diferenca-salario-bruto-e-liquido"]),

        Create(
            "vale-transporte-desconto-maximo",
            "Qual o desconto máximo de vale-transporte?",
            "Trabalhista",
            "O VT pode ser descontado em até 6% do salário bruto. Veja como isso afeta o líquido.",
            """
            <p>By law, o desconto de vale-transporte na folha é limitado a <strong>6% do salário bruto</strong>, mesmo que o benefício custe mais para a empresa. O excedente é custeado pelo empregador.</p>
            <p>Para conferir o proporcional por dias presenciais, use a <a href="/calculadoras/vale-transporte-hibrido">calculadora de vale-transporte híbrido</a>. Depois informe o desconto na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> para ver o impacto no bolso.</p>
            """,
            "vale-transporte-hibrido",
            [
                new("Posso recusar o vale-transporte?", "Em regra, o benefício é para deslocamento. Recusas têm regras específicas; consulte RH ou sindicato."),
                new("VT desconta antes ou depois do INSS?", "O INSS incide sobre o salário bruto; o VT é desconto posterior na folha, reduzindo o líquido.")
            ],
            ["como-calcular-salario-liquido", "diferenca-salario-bruto-e-liquido", "proposta-salarial-como-negociar"]),

        Create(
            "ferias-proporcionais-como-funciona",
            "Como funcionam as férias proporcionais?",
            "Trabalhista",
            "Férias proporcionais na rescisão e o adicional de 1/3: o que entra no cálculo.",
            """
            <p>Férias proporcionais são devidas quando o trabalhador não completou o período aquisitivo de 12 meses, mas tem direito a férias na rescisão (exceto em casos como justa causa, conforme regra).</p>
            <p>O valor inclui o salário correspondente aos meses proporcionais mais o <strong>adicional constitucional de 1/3</strong>, com descontos de INSS e IRRF sobre a verba.</p>
            <p>Simule férias completas na <a href="/calculadoras/ferias">calculadora de férias</a> ou a rescisão na <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a>.</p>
            """,
            "ferias",
            [
                new("Férias vencidas são diferentes?", "Sim. Férias vencidas (não gozadas) geram pagamento em dobro em parte dos casos na rescisão."),
                new("1/3 de férias tem imposto?", "Sim. O adicional de 1/3 integra a base de INSS e IRRF na estimativa da verba.")
            ],
            ["decimo-terceiro-quem-tem-direito", "rescisao-pedido-demissao-o-que-recebo", "multa-fgts-40-porcento"]),

        Create(
            "decimo-terceiro-quem-tem-direito",
            "Quem tem direito ao décimo terceiro?",
            "Trabalhista",
            "Décimo terceiro integral e proporcional: direitos e descontos estimados.",
            """
            <p>Todo trabalhador CLT com carteira assinada tem direito ao <strong>13º salário</strong>. O valor integral é pago em duas parcelas (novembro e dezembro). Na rescisão, recebe-se a parte proporcional aos meses trabalhados no ano.</p>
            <p>Sobre o 13º incidem INSS e IRRF. Estime na <a href="/calculadoras/decimo-terceiro">calculadora de décimo terceiro</a>.</p>
            """,
            "decimo-terceiro",
            [
                new("13º de quem entrou no meio do ano?", "Proporcional: cada mês trabalhado (15 dias ou mais conta como mês) dá 1/12 do salário."),
                new("Desconto no 13º é igual ao salário?", "As alíquotas são as mesmas, mas a base pode diferir se o valor do 13º for diferente do mensal.")
            ],
            ["ferias-proporcionais-como-funciona", "rescisao-pedido-demissao-o-que-recebo", "como-calcular-salario-liquido"]),

        Create(
            "rescisao-pedido-demissao-o-que-recebo",
            "Pedido de demissão: o que recebo na rescisão?",
            "Trabalhista",
            "Verbas na demissão voluntária, aviso prévio, FGTS e o que não tem direito.",
            """
            <p>No <strong>pedido de demissão</strong>, o trabalhador recebe em geral: saldo de salário, férias proporcionais (+ 1/3), 13º proporcional e férias vencidas se houver. Não há multa de 40% do FGTS nem seguro-desemprego na regra geral.</p>
            <p>Se não cumprir aviso prévio, pode haver <strong>desconto de 30 dias</strong> limitado às verbas rescisórias. Simule na <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a>.</p>
            """,
            "rescisao-clt",
            [
                new("Posso sacar FGTS ao pedir demissão?", "Em regra, não há saque do FGTS nem multa de 40% no pedido de demissão sem acordo."),
                new("Acordo 484-A é pedido de demissão?", "Não. É acordo entre partes, com regras e verbas diferentes (multa de 20% sobre FGTS, por exemplo).")
            ],
            ["multa-fgts-40-porcento", "ferias-proporcionais-como-funciona", "seguro-desemprego-quando-tem-direito"]),

        Create(
            "seguro-desemprego-quando-tem-direito",
            "Seguro-desemprego: quando tenho direito?",
            "Trabalhista",
            "Regras gerais do seguro-desemprego após demissão sem justa causa e o que a calculadora não simula.",
            """
            <p>O <strong>seguro-desemprego</strong> é um benefício pago pelo governo ao trabalhador demitido <strong>sem justa causa</strong>, desde que cumpra requisitos como tempo mínimo de vínculo, não ter recebido o benefício nos últimos meses e não ter renda própria suficiente.</p>
            <p>O valor e o número de parcelas dependem do salário médio e do tempo de contribuição. Não há valor fixo universal — consulte a Caixa, o portal gov.br ou o RH após a homologação.</p>
            <p>Na <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a>, o seguro-desemprego aparece como <strong>linha informativa</strong>. Use a <a href="/calculadoras/seguro-desemprego">calculadora de seguro-desemprego</a> para estimar parcelas; para FGTS e multa, veja também a <a href="/calculadoras/fgts">calculadora de FGTS</a>.</p>
            """,
            "rescisao-clt",
            [
                new("Pedido de demissão tem seguro-desemprego?", "Em regra, não. O benefício é típico da demissão sem justa causa pelo empregador."),
                new("Acordo 484-A dá direito ao seguro?", "Não. No acordo comum, em geral não há seguro-desemprego."),
                new("A calculadora mostra o valor das parcelas?", "Não. Mostramos apenas um aviso informativo; o cálculo oficial é feito pelo governo na solicitação.")
            ],
            ["rescisao-pedido-demissao-o-que-recebo", "multa-fgts-40-porcento", "decimo-terceiro-quem-tem-direito"]),

        Create(
            "multa-fgts-40-porcento",
            "Quando tenho direito à multa de 40% do FGTS?",
            "Trabalhista",
            "Multa rescisória do FGTS: demissão sem justa causa, acordo e como estimar o valor.",
            """
            <p>A multa de <strong>40% sobre o saldo do FGTS</strong> é devida na demissão <strong>sem justa causa</strong> pelo empregador. No acordo comum (art. 484-A), a multa é de 20% e há saque parcial do saldo.</p>
            <p>Estime depósitos, saldo e multa na <a href="/calculadoras/fgts">calculadora de FGTS</a> ou veja o pacote completo na <a href="/calculadoras/rescisao-clt">rescisão CLT</a>.</p>
            """,
            "fgts",
            [
                new("Justa causa tem multa FGTS?", "Não há multa de 40% para o empregado demitido por justa causa."),
                new("Como saber meu saldo de FGTS?", "Consulte o app FGTS da Caixa. Na calculadora, você pode estimar pelo tempo de empresa.")
            ],
            ["rescisao-pedido-demissao-o-que-recebo", "custo-empresa-contratar-funcionario", "seguro-desemprego-quando-tem-direito"]),

        Create(
            "hora-extra-valor-minimo",
            "Qual o valor mínimo da hora extra?",
            "Trabalhista",
            "Adicional mínimo de 50% em dia útil, 100% em domingo/feriado e reflexo em DSR.",
            """
            <p>A hora extra em dia útil deve pagar no mínimo <strong>50% a mais</strong> que a hora normal. Em domingos e feriados, o adicional mínimo é de <strong>100%</strong>. Convenções coletivas podem prever percentuais maiores.</p>
            <p>O valor da hora normal costuma ser salário ÷ divisor (220h para jornada 44h). Há ainda reflexo em <strong>DSR</strong>. Calcule na <a href="/calculadoras/hora-extra">calculadora de hora extra</a> ou converta salário em <a href="/calculadoras/conversor-salario">valor hora</a>.</p>
            """,
            "hora-extra",
            [
                new("Banco de horas dispensa pagamento?", "Depende do acordo e compensação. Horas não compensadas devem ser pagas como extra."),
                new("Adicional noturno soma com hora extra?", "Sim. Turno noturno pode ter adicional de 20% sobre a hora, além do adicional de hora extra.")
            ],
            ["como-calcular-salario-liquido", "conversor-salario-hora-mensal", "decimo-terceiro-quem-tem-direito"]),

        Create(
            "pj-ou-clt-qual-compensa",
            "PJ ou CLT: qual compensa mais?",
            "Financeiro",
            "Compare líquido mensal estimado, benefícios CLT e custos de PJ antes de decidir.",
            """
            <p>Não existe resposta única. <strong>CLT</strong> oferece férias, 13º, FGTS, seguro-desemprego e estabilidade em alguns casos. <strong>PJ</strong> pode ter faturamento maior, mas você arca com impostos, contador, benefícios e riscos.</p>
            <p>Compare números na <a href="/calculadoras/pj-vs-clt">calculadora PJ vs CLT</a> ou veja <a href="/clt-pj">quanto faturar para equivaler a um salário CLT</a>. Para MEI, use o <a href="/calculadoras/simulador-mei">simulador MEI</a>.</p>
            """,
            "pj-vs-clt",
            [
                new("PJ recebe menos imposto sempre?", "Nem sempre. Depende do faturamento, anexo do Simples, pró-labore e despesas."),
                new("Benefícios CLT valem quanto?", "FGTS (8%), férias+1/3 e 13º são custos indiretos importantes — não compare só o líquido mensal.")
            ],
            ["quanto-faturar-pj-para-equivaler-clt", "mei-pode-trabalhar-como-clt", "custo-empresa-contratar-funcionario"]),

        Create(
            "quanto-faturar-pj-para-equivaler-clt",
            "Quanto faturar como PJ para equivaler ao CLT?",
            "Financeiro",
            "Estimativa de faturamento PJ para chegar perto do líquido CLT com Simples e pró-labore.",
            """
            <p>Para equivaler ao <strong>líquido CLT</strong>, o PJ precisa faturar mais que o bruto CLT: parte vai para Simples Nacional, INSS e IRRF sobre pró-labore, além de despesas fixas.</p>
            <p>Consulte páginas por valor em <a href="/clt-pj">CLT x PJ</a> (ex.: <a href="/clt-pj/5000-clt-equivale-a-quanto-pj">R$ 5.000 CLT</a>) ou personalize na <a href="/calculadoras/pj-vs-clt">calculadora completa</a>.</p>
            """,
            "pj-vs-clt",
            [
                new("Pró-labore é sempre 28%?", "Não. 28% é referência educativa na calculadora. Muitas empresas usam pró-labore mínimo ou percentual contábil."),
                new("Simples de 6% vale para todo PJ?", "Não. O anexo e a faixa de faturamento mudam a alíquota efetiva.")
            ],
            ["pj-ou-clt-qual-compensa", "mei-pode-trabalhar-como-clt", "quanto-preciso-ganhar-para-receber-x-liquido"]),

        Create(
            "quanto-preciso-ganhar-para-receber-x-liquido",
            "Quanto preciso ganhar de bruto para receber X líquido?",
            "Trabalhista",
            "Calculadora inversa: descubra o salário bruto necessário para um líquido desejado.",
            """
            <p>Se você sabe quanto precisa receber no bolso (aluguel, contas, metas), a pergunta certa é: <strong>quanto de bruto preciso negociar?</strong></p>
            <p>A resposta depende de dependentes e descontos. A <a href="/calculadoras/salario-bruto-necessario">calculadora de salário bruto necessário</a> usa busca binária sobre INSS e IRRF 2026. Combine com a <a href="/calculadoras/proposta-salarial">proposta salarial</a> na negociação.</p>
            """,
            "salario-bruto-necessario",
            [
                new("O bruto necessário é exato?", "É estimativa com centavos de precisão. Holerite real pode variar por arredondamentos e benefícios."),
                new("Vale para PJ?", "Esta calculadora é para CLT. Para PJ, use <a href=\"/calculadoras/pj-vs-clt\">PJ vs CLT</a>.")
            ],
            ["proposta-salarial-como-negociar", "como-calcular-salario-liquido", "quanto-faturar-pj-para-equivaler-clt"]),

        Create(
            "proposta-salarial-como-negociar",
            "Como negociar proposta salarial com dados?",
            "Trabalhista",
            "Use líquido real, diferença anual e simulação para negociar aumento com RH.",
            """
            <p>Negocie com o <strong>líquido</strong>, não só com o percentual no bruto. Um aumento de 10% no bruto pode significar menos no bolso por causa de INSS e IRRF progressivos.</p>
            <p>Na <a href="/calculadoras/proposta-salarial">calculadora de proposta salarial</a>, compare atual vs proposta, veja ganho mensal e anual e compartilhe a simulação (link ou PDF) com transparência.</p>
            """,
            "proposta-salarial",
            [
                new("Devo mostrar simulação ao gestor?", "Como referência educativa, pode ajudar. Deixe claro que não substitui a proposta formal da empresa."),
                new("Benefícios entram na negociação?", "Sim. VT, VR, plano e bônus mudam o pacote total — compare o custo para você, não só o bruto.")
            ],
            ["quanto-preciso-ganhar-para-receber-x-liquido", "diferenca-salario-bruto-e-liquido", "vale-transporte-desconto-maximo"]),

        Create(
            "mei-pode-trabalhar-como-clt",
            "MEI pode trabalhar com carteira assinada?",
            "Fiscal",
            "Compatibilidade MEI e CLT, limites de faturamento e quando desenquadra.",
            """
            <p>Em geral, o MEI <strong>pode ser empregado CLT</strong> em outra empresa, desde que não haja vínculo com o próprio CNPJ MEI de forma irregular. O faturamento MEI não pode ultrapassar o limite anual (R$ 81.000 em 2026).</p>
            <p>Simule o DAS e limites no <a href="/calculadoras/simulador-mei">simulador MEI</a>. Para comparar renda MEI x emprego, use <a href="/calculadoras/pj-vs-clt">PJ vs CLT</a> como referência de modelo de renda.</p>
            """,
            "simulador-mei",
            [
                new("MEI pode ter funcionário?", "MEI pode ter um empregado com regras específicas. Verifique a legislação vigente."),
                new("Ultrapassar o limite anual o que acontece?", "Desenquadramento do MEI e migração para outro regime tributário.")
            ],
            ["pj-ou-clt-qual-compensa", "quanto-faturar-pj-para-equivaler-clt", "custo-empresa-contratar-funcionario"]),

        Create(
            "custo-empresa-contratar-funcionario",
            "Quanto custa para a empresa contratar um funcionário?",
            "Trabalhista",
            "Custo total CLT: salário, encargos, FGTS, provisões de 13º e férias.",
            """
            <p>O custo para o empregador é maior que o salário bruto: inclui <strong>INSS patronal</strong>, <strong>FGTS (8%)</strong>, provisões de 13º e férias (+1/3), e possíveis benefícios.</p>
            <p>Estime na <a href="/calculadoras/custo-funcionario">calculadora de custo de funcionário</a>. Para o que o funcionário recebe no bolso, use <a href="/calculadoras/salario-liquido">salário líquido</a>.</p>
            """,
            "custo-funcionario",
            [
                new("PJ evita encargos para a empresa?", "Em geral, PJ não gera FGTS nem 13º CLT, mas há riscos de vínculo se a relação for de fato empregatícia."),
                new("Benefícios aumentam o custo?", "Sim. VT, plano de saúde e outros itens entram no custo total da contratação.")
            ],
            ["pj-ou-clt-qual-compensa", "multa-fgts-40-porcento", "mei-pode-trabalhar-como-clt"]),

        Create(
            "salario-minimo-liquido-2026",
            "Quanto sobra do salário mínimo em 2026?",
            "Trabalhista",
            "Estimativa de líquido sobre o salário mínimo com INSS e IRRF 2026.",
            """
            <p>Com o salário mínimo de 2026, o trabalhador sem dependentes costuma ter <strong>isenção ou redução de IRRF</strong> e INSS na primeira faixa. O líquido fica abaixo do bruto, mas o impacto de impostos é menor que em salários altos.</p>
            <p>Veja a página <a href="/salario-liquido/1621">salário mínimo líquido</a> ou simule com descontos na <a href="/calculadoras/salario-liquido">calculadora completa</a>.</p>
            """,
            "salario-liquido",
            [
                new("Salário mínimo tem desconto de VT?", "Se houver vale-transporte, o desconto é limitado a 6% do mínimo."),
                new("Quem ganha mínimo paga IRRF?", "Depende da base após INSS e da redução legal de 2026. Muitos ficam isentos.")
            ],
            ["quanto-desconta-inss-2026", "como-calcular-salario-liquido", "diferenca-salario-bruto-e-liquido"]),

        Create(
            "conversor-salario-hora-mensal",
            "Como converter salário mensal em valor hora?",
            "Trabalhista",
            "Divisor 220h (44h/semana), diário e mensal na conversão CLT.",
            """
            <p>Na CLT com 44h semanais, o divisor usual é <strong>220 horas mensais</strong>. Valor hora ≈ salário mensal ÷ 220. Jornadas de 40h usam divisor 200, e assim por diante.</p>
            <p>Use o <a href="/calculadoras/conversor-salario">conversor de salário</a> e, para horas extras, a <a href="/calculadoras/hora-extra">calculadora de hora extra</a>.</p>
            """,
            "conversor-salario",
            [
                new("Dia útil usa 30 ou 22 dias?", "Para salário diário simplificado, divide-se por 30 dias. Convenções podem usar 22 dias úteis."),
                new("Horista tem 13º?", "Sim, se for CLT com remuneração por hora, com regras de média.")
            ],
            ["hora-extra-valor-minimo", "como-calcular-salario-liquido", "decimo-terceiro-quem-tem-direito"]),

        Create(
            "como-conferir-holerite",
            "Como conferir se o holerite está correto?",
            "Trabalhista",
            "Passo a passo para validar INSS, IRRF e líquido do holerite com as tabelas de 2026.",
            """
            <p>Antes de questionar o RH, compare o que aparece no holerite com uma simulação pelas tabelas oficiais de INSS e IRRF de 2026.</p>
            <ol>
            <li>Anote <strong>salário bruto</strong>, <strong>INSS</strong>, <strong>IRRF</strong> e <strong>líquido</strong> do holerite.</li>
            <li>Informe dependentes e descontos opcionais (VT, plano etc.) se constarem na folha.</li>
            <li>Use a ferramenta <a href="/conferir-holerite">Conferir holerite</a> ou a <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a>.</li>
            <li>Pequenas diferenças de centavos podem ser arredondamento; divergências maiores merecem conversa com o departamento pessoal.</li>
            </ol>
            """,
            "salario-liquido",
            [
                new("A ferramenta substitui o RH?", "Não. É apoio educativo para você chegar preparado à conversa."),
                new("E se o holerite tiver benefícios extras?", "Informe descontos opcionais na calculadora completa para aproximar o líquido.")
            ],
            ["como-calcular-salario-liquido", "quanto-desconta-inss-2026", "irrf-quem-paga-e-como-calcular"]),

        Create(
            "reducao-irrf-2026",
            "Como funciona a redução do IRRF em 2026?",
            "Fiscal",
            "Entenda a isenção e a redução decrescente do imposto de renda retido na fonte em 2026.",
            """
            <p>Em 2026, a <strong>Lei 15.270/2025</strong> mantém regras de redução do IRRF para rendimentos do trabalho. Em linhas gerais:</p>
            <ul>
            <li>Base tributável até <strong>R$ 5.000</strong>: isenção total do IRRF retido.</li>
            <li>Entre <strong>R$ 5.000,01</strong> e <strong>R$ 7.350</strong>: redução decrescente até zerar o benefício.</li>
            <li>Acima disso, aplica-se a tabela progressiva normal com dedução por dependente.</li>
            </ul>
            <p>Simule seu caso na <a href="/calculadoras/irrf">calculadora de IRRF</a> ou veja as tabelas em <a href="/como-calculamos">Como calculamos</a>.</p>
            """,
            "irrf",
            [
                new("A redução vale para 13º e férias?", "Sim, quando essas verbas entram na base de IRRF da folha ou rescisão."),
                new("MEI e PJ têm a mesma redução?", "Esta regra foca no IRRF retido de rendimentos do trabalho CLT. PJ e MEI seguem outros regimes.")
            ],
            ["irrf-quem-paga-e-como-calcular", "quanto-desconta-inss-2026", "salario-minimo-liquido-2026"]),

        Create(
            "teto-inss-2026",
            "Qual o teto do INSS em 2026?",
            "Fiscal",
            "Teto de contribuição previdenciária e impacto no desconto em folha em 2026.",
            """
            <p>Em 2026, o <strong>teto do INSS</strong> (salário de contribuição máximo) é de <strong>R$ 8.475,55</strong>. Sobre esse valor, o desconto máximo em folha CLT fica em torno de <strong>R$ 988,09</strong>, pela tabela progressiva.</p>
            <p>Salários acima do teto não pagam INSS adicional sobre o excedente na folha. Veja o detalhe por faixas na <a href="/calculadoras/inss">calculadora de INSS</a> e na página <a href="/como-calculamos">Como calculamos</a>.</p>
            """,
            "inss",
            [
                new("Quem ganha acima do teto paga mais INSS?", "Não na folha CLT. O desconto para no teto de contribuição."),
                new("O teto muda todo ano?", "Sim. É atualizado anualmente conforme legislação previdenciária.")
            ],
            ["quanto-desconta-inss-2026", "como-calcular-salario-liquido", "reducao-irrf-2026"]),

        Create(
            "vender-ferias-abono-pecuniario",
            "Posso vender 1/3 das férias (abono pecuniário)?",
            "Trabalhista",
            "Regras do abono pecuniário, impacto no líquido e quando faz sentido vender férias.",
            """
            <p>O trabalhador CLT pode converter até <strong>1/3 das férias</strong> em dinheiro — o chamado <strong>abono pecuniário</strong>. A conversão é opcional e deve ser solicitada ao empregador com antecedência.</p>
            <p>O valor pago inclui o adicional de 1/3 sobre a parcela vendida e entra na base de INSS e IRRF. Simule na <a href="/calculadoras/ferias">calculadora de férias</a> marcando a opção de venda de 1/3.</p>
            """,
            "ferias",
            [
                new("Vender férias reduz dias de descanso?", "Sim. Você recebe dinheiro, mas goza apenas 2/3 do período de férias."),
                new("A empresa pode recusar?", "A venda depende de acordo e prazos legais; consulte o RH da sua empresa.")
            ],
            ["ferias-proporcionais-como-funciona", "como-calcular-salario-liquido", "decimo-terceiro-quem-tem-direito"]),

        Create(
            "acordo-demissao-484-a",
            "O que é demissão por acordo (art. 484-A)?",
            "Trabalhista",
            "Verbas, multa de 20% do FGTS e diferenças em relação ao pedido de demissão e à demissão sem justa causa.",
            """
            <p>A <strong>demissão por acordo</strong> (art. 484-A da CLT) é um meio-termo entre pedido de demissão e dispensa sem justa causa. Em geral:</p>
            <ul>
            <li>Metade do aviso prévio indenizado e metade da multa do FGTS (<strong>20%</strong>).</li>
            <li>Saque de até <strong>80%</strong> do saldo FGTS (regra geral educativa).</li>
            <li><strong>Sem</strong> seguro-desemprego na regra usual.</li>
            </ul>
            <p>Compare verbas na <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a> escolhendo o motivo adequado.</p>
            """,
            "rescisao-clt",
            [
                new("Acordo é a mesma coisa que pedir demissão?", "Não. As verbas e o FGTS seguem regras próprias do acordo."),
                new("Posso negociar valores além da lei?", "Partes podem formalizar condições no termo, desde que respeitem direitos mínimos.")
            ],
            ["rescisao-pedido-demissao-o-que-recebo", "multa-fgts-40-porcento", "seguro-desemprego-quando-tem-direito"]),

        Create(
            "desconto-plano-saude-folha",
            "Como o plano de saúde desconta do salário?",
            "Trabalhista",
            "Coparticipação, limite de desconto e efeito no salário líquido.",
            """
            <p>O plano de saúde oferecido pelo empregador costuma aparecer no holerite como <strong>desconto autorizado</strong>. A participação do empregado depende do contrato coletivo ou da política da empresa.</p>
            <p>Esse desconto reduz o <strong>líquido</strong>, mas em geral <strong>não reduz a base do INSS</strong> (que incide sobre o bruto). Informe o valor na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> em &quot;Ajustar descontos&quot;.</p>
            """,
            "salario-liquido",
            [
                new("Plano desconta antes do IRRF?", "O IRRF usa base após INSS e dependentes; descontos como plano costumam vir depois, afetando o líquido."),
                new("Dependente no plano reduz IRRF?", "Dependente legal na folha reduz a base do IRRF; dependente só no plano não necessariamente.")
            ],
            ["como-calcular-salario-liquido", "vale-transporte-desconto-maximo", "como-conferir-holerite"]),

        Create(
            "fgts-saque-rescisao",
            "Quando posso sacar o FGTS na rescisão?",
            "Trabalhista",
            "Situações de saque do FGTS, multa rescisória e o que muda no pedido de demissão.",
            """
            <p>O saque do FGTS na rescisão depende do <strong>motivo do desligamento</strong>:</p>
            <ul>
            <li><strong>Sem justa causa:</strong> saque do saldo + multa de 40% (estimável na <a href="/calculadoras/fgts">calculadora de FGTS</a>).</li>
            <li><strong>Acordo 484-A:</strong> saque parcial (até 80% na regra geral) e multa de 20%.</li>
            <li><strong>Pedido de demissão:</strong> em regra, sem saque nem multa de 40%.</li>
            </ul>
            <p>Veja o pacote completo na <a href="/calculadoras/rescisao-clt">rescisão CLT</a> ou o guia <a href="/desligamento">desligamento</a>.</p>
            """,
            "fgts",
            [
                new("Quanto tempo demora o saque?", "Após homologação e liberação pela Caixa; prazos variam por canal."),
                new("Multa de 40% cai na conta junto com o FGTS?", "A multa é paga pelo empregador ao FGTS; o trabalhador saca conforme regras do fundo.")
            ],
            ["multa-fgts-40-porcento", "acordo-demissao-484-a", "rescisao-pedido-demissao-o-que-recebo"])
    ];

    private static readonly Dictionary<string, PopularQuestionDefinition> BySlug =
        All.ToDictionary(q => q.Slug, StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyList<PopularQuestionDefinition> GetAll() => All;

    public static PopularQuestionDefinition? GetBySlug(string slug) =>
        BySlug.TryGetValue(slug, out var question) ? question : null;

    public static string SlugPath(string slug) => $"/duvidas/{slug}";

    public static IReadOnlyList<PopularQuestionDefinition> GetRelated(PopularQuestionDefinition question) =>
        question.RelatedQuestionSlugs
            .Select(GetBySlug)
            .Where(q => q is not null)
            .Cast<PopularQuestionDefinition>()
            .ToList();

    private static PopularQuestionDefinition Create(
        string slug,
        string title,
        string category,
        string seoDescription,
        string answerHtml,
        string? relatedCalculatorSlug,
        IReadOnlyList<PopularQuestionFaqItem> faqItems,
        IReadOnlyList<string> relatedQuestionSlugs) =>
        new(slug, title, category, seoDescription, answerHtml.Trim(), relatedCalculatorSlug, faqItems, relatedQuestionSlugs);
}
