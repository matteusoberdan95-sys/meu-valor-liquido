namespace MeuValorLiquido.WebApp.Data;
public sealed record BlogArticleSeed(
    string Slug,
    string Title,
    string Summary,
    string Content,
    DateOnly PublishedAt,
    string Category,
    string? RelatedCalculatorSlug = null);

public static class BlogArticleSeedData
{
    public static IReadOnlyList<BlogArticleSeed> GetAll() =>
    [
        Article(
            "o-que-e-salario-liquido",
            "O que é salário líquido?",
            "Entenda a diferença entre salário bruto e salário líquido e por que o valor do holerite raramente coincide com o que você espera receber.",
            "salario-liquido",
            "Trabalhista",
            new DateOnly(2026, 3, 3),
            """
            <p>O salário líquido é o valor que efetivamente entra na sua conta após todos os descontos obrigatórios e opcionais aplicados sobre o salário bruto. Enquanto o bruto aparece no contrato de trabalho, o líquido reflete a realidade do pagamento mensal.</p>
            <h2>Salário bruto x salário líquido</h2>
            <p>O salário bruto inclui o valor acordado antes de qualquer desconto. Já o líquido considera INSS, IRRF, vale-transporte, plano de saúde, pensão alimentícia e outros descontos autorizados. Por isso, comparar propostas de emprego apenas pelo bruto pode ser enganoso.</p>
            <h2>Principais descontos no holerite</h2>
            <ul>
            <li><strong>INSS:</strong> contribuição previdenciária calculada por faixas progressivas.</li>
            <li><strong>IRRF:</strong> imposto retido na fonte sobre a base após INSS e dependentes.</li>
            <li><strong>Vale-transporte:</strong> até 6% do salário bruto, quando optado.</li>
            <li><strong>Benefícios e convênios:</strong> descontos autorizados pelo trabalhador.</li>
            </ul>
            <h2>Como estimar seu líquido</h2>
            <p>Use a <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> para simular seu caso com as tabelas de 2026. Informe salário bruto, dependentes e descontos extras para ver um extrato estimado.</p>
            <h2>Limitações de uma estimativa</h2>
            <p>Convenções coletivas, adicionais noturnos, horas extras e benefícios específicos alteram o resultado final. Trate a simulação como referência educativa e confirme valores oficiais com o departamento pessoal.</p>
            """),
        Article(
            "como-calcular-ferias",
            "Como calcular férias",
            "Aprenda a estimar férias com adicional de um terço, descontos de INSS e IRRF e quando o pagamento costuma cair na conta.",
            "ferias",
            "Trabalhista",
            new DateOnly(2026, 3, 10),
            """
            <p>As férias são um direito do trabalhador CLT após 12 meses de contrato. O cálculo envolve a remuneração do período, o adicional constitucional de um terço e descontos legais sobre o total.</p>
            <h2>Componentes do cálculo</h2>
            <ul>
            <li>Salário base correspondente ao período de férias (30 dias ou proporcional).</li>
            <li>Adicional de 1/3 sobre o valor das férias.</li>
            <li>Descontos estimados de INSS e IRRF sobre o montante.</li>
            </ul>
            <h2>Férias proporcionais e abono pecuniário</h2>
            <p>Quem não completou o período aquisitivo pode ter direito a férias proporcionais na rescisão. O abono pecuniário permite vender até 10 dias de férias, reduzindo o descanso e alterando o valor recebido.</p>
            <h2>Simule seu caso</h2>
            <p>A <a href="/calculadoras/ferias">calculadora de férias</a> ajuda a visualizar bruto, adicional, descontos e líquido estimado. Ajuste salário e dias para comparar cenários.</p>
            <h2>Dicas práticas</h2>
            <p>Empresas costumam pagar férias até dois dias antes do início do período. Guarde parte do valor para despesas do mês seguinte, já que o orçamento mensal muda temporariamente.</p>
            """),
        Article(
            "como-calcular-rescisao-clt",
            "Como calcular rescisão CLT",
            "Veja os itens mais comuns em uma rescisão trabalhista e como estimar saldo de salário, verbas proporcionais e descontos.",
            "rescisao-clt",
            "Trabalhista",
            new DateOnly(2026, 3, 17),
            """
            <p>A rescisão CLT reúne verbas devidas no desligamento: saldo de salário, férias vencidas ou proporcionais, 13º proporcional, aviso prévio e multa do FGTS em alguns casos. Cada modalidade de desligamento altera o que é pago.</p>
            <h2>Tipos de desligamento</h2>
            <ul>
            <li><strong>Sem justa causa:</strong> o empregador dispensa; há multa de 40% sobre FGTS e seguro-desemprego, se elegível.</li>
            <li><strong>Pedido de demissão:</strong> verbas proporcionais, sem multa FGTS nem seguro-desemprego.</li>
            <li><strong>Justa causa:</strong> regras restritas; verbas reduzidas conforme a legislação.</li>
            </ul>
            <h2>Itens frequentes no TRCT</h2>
            <p>Além das verbas rescisórias, podem aparecer descontos de empréstimos consignados, vale-transporte e contribuições sindicais. O Termo de Rescisão detalha cada linha.</p>
            <h2>Estime sua rescisão</h2>
            <p>Use a <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a> para uma visão educativa dos componentes. Informe salário, meses trabalhados e tipo de desligamento.</p>
            """),
        Article(
            "como-calcular-inss",
            "Como calcular INSS em 2026",
            "Entenda as faixas progressivas do INSS, o teto de contribuição e como o desconto aparece no holerite.",
            "inss",
            "Fiscal",
            new DateOnly(2026, 3, 24),
            """
            <p>O INSS é calculado de forma progressiva: cada faixa salarial tem uma alíquota, e o desconto total é a soma parcial de cada faixa — não uma alíquota única sobre todo o salário.</p>
            <h2>Faixas progressivas</h2>
            <p>Quanto maior o salário de contribuição, maior a parcela sujeita a alíquotas mais altas, até o teto previdenciário. Acima do teto, não há contribuição adicional sobre o excedente.</p>
            <h2>Impacto no IRRF</h2>
            <p>O valor pago ao INSS reduz a base de cálculo do imposto de renda retido na fonte. Por isso, INSS e IRRF devem ser analisados em conjunto.</p>
            <h2>Calcule o desconto</h2>
            <p>A <a href="/calculadoras/inss">calculadora de INSS</a> aplica as faixas de 2026 automaticamente. Informe o salário bruto para ver o valor estimado da contribuição.</p>
            <h2>Autônomos e contribuintes individuais</h2>
            <p>Regras diferem para MEI, contribuinte individual e facultativo. Este artigo foca no desconto sobre salário CLT; consulte um contador para outros vínculos.</p>
            """),
        Article(
            "entenda-o-irrf",
            "Entenda o IRRF: imposto retido no salário",
            "Saiba como funciona o imposto de renda retido na fonte, deduções por dependente e por que nem todo salário paga IRRF.",
            "irrf",
            "Fiscal",
            new DateOnly(2026, 3, 31),
            """
            <p>O Imposto de Renda Retido na Fonte (IRRF) é descontado diretamente do salário quando a base de cálculo ultrapassa a faixa de isenção. A Receita Federal atualiza tabelas e deduções periodicamente.</p>
            <h2>Base de cálculo simplificada</h2>
            <p>Em linhas gerais, parte-se do salário bruto, deduzem-se INSS, dependentes (valor fixo por dependente) e pensão judicial, aplicando-se a tabela progressiva sobre o resultado.</p>
            <h2>Isenção e faixas</h2>
            <p>Salários menores podem ficar isentos de IRRF após os descontos legais. Conforme a base aumenta, alíquotas de 7,5% a 27,5% entram em vigor por faixa.</p>
            <h2>Simule o IRRF</h2>
            <p>A <a href="/calculadoras/irrf">calculadora de IRRF</a> estima o imposto com base nas regras vigentes. Combine com a calculadora de salário líquido para ver o holerite completo.</p>
            <h2>Restituição x saldo a pagar</h2>
            <p>O IRRF mensal é uma antecipação. Na declaração anual, você apura se pagou a mais (restituição) ou a menos (saldo devido).</p>
            """),
        Article(
            "pj-ou-clt-qual-melhor",
            "PJ ou CLT: qual é melhor?",
            "Compare remuneração PJ e CLT de forma educativa: impostos, benefícios, estabilidade e custos que impactam o bolso.",
            "pj-vs-clt",
            "Trabalhista",
            new DateOnly(2026, 4, 7),
            """
            <p>A escolha entre PJ e CLT vai além do valor bruto ofertado. Impostos, benefícios, férias, 13º, FGTS e custos operacionais mudam completamente o custo real para empresa e trabalhador.</p>
            <h2>Vantagens comuns da CLT</h2>
            <ul>
            <li>Férias, 13º salário e FGTS garantidos por lei.</li>
            <li>Seguro-desemprego e estabilidade em algumas situações.</li>
            <li>Desconto de INSS e IRRF já tratados pelo empregador.</li>
            </ul>
            <h2>Vantagens comuns da PJ</h2>
            <ul>
            <li>Maior flexibilidade de negociação e remuneração bruta.</li>
            <li>Possibilidade de otimização tributária (conforme regime).</li>
            <li>Menos encargos trabalhistas diretos para a contratante.</li>
            </ul>
            <h2>Compare números</h2>
            <p>A <a href="/calculadoras/pj-vs-clt">calculadora PJ vs CLT</a> ajuda a estimar líquido e custo total em cada modelo. Use propostas reais como entrada.</p>
            <h2>Cuidado com a relação de emprego disfarçada</h2>
            <p>Trabalhar exclusivamente para um cliente, com horário fixo e subordinação, pode caracterizar vínculo CLT mesmo com CNPJ. Avalie riscos jurídicos com um especialista.</p>
            """),
        Article(
            "guia-decimo-terceiro",
            "Guia do décimo terceiro salário",
            "Como funciona o 13º, pagamento em duas parcelas, descontos e cálculo proporcional para quem entrou ou saiu no meio do ano.",
            "decimo-terceiro",
            "Trabalhista",
            new DateOnly(2026, 4, 14),
            """
            <p>O décimo terceiro salário é um direito anual equivalente a 1/12 da remuneração por mês trabalhado no ano. Quem trabalhou o ano inteiro recebe uma remuneração extra; quem trabalhou parcialmente, valor proporcional.</p>
            <h2>Calendário de pagamento</h2>
            <p>A primeira parcela, geralmente sem descontos, vence até 30 de novembro. A segunda parcela, com INSS e IRRF, até 20 de dezembro.</p>
            <h2>Descontos na segunda parcela</h2>
            <p>INSS e IRRF incidem sobre o total do 13º na segunda parcela. Planeje despesas de fim de ano considerando o líquido, não o bruto.</p>
            <h2>Estime seu 13º</h2>
            <p>A <a href="/calculadoras/decimo-terceiro">calculadora de décimo terceiro</a> projeta bruto, descontos e líquido conforme meses trabalhados e salário informado.</p>
            """),
        Article(
            "juros-compostos-guia",
            "Juros compostos: guia prático para investimentos",
            "Entenda como juros compostos fazem seu dinheiro crescer ao longo do tempo e como projetar aportes mensais.",
            "juros-compostos",
            "Financeiro",
            new DateOnly(2026, 4, 21),
            """
            <p>Juros compostos são calculados sobre o montante acumulado — capital inicial mais rendimentos anteriores. Com o tempo, o efeito de “juros sobre juros” acelera o crescimento do patrimônio.</p>
            <h2>Fórmula intuitiva</h2>
            <p>A cada período, a taxa incide sobre o saldo total. Quanto maior a taxa, o prazo e a frequência de capitalização, maior o resultado final.</p>
            <h2>Aportes regulares</h2>
            <p>Investir todo mês potencializa o efeito composto. Mesmo valores modestos, mantidos por anos, podem surpreender quando reinvestidos consistentemente.</p>
            <h2>Projete seu investimento</h2>
            <p>Use a <a href="/calculadoras/juros-compostos">calculadora de juros compostos</a> para simular valor inicial, aportes, taxa e prazo. Compare cenários conservadores e otimistas.</p>
            <h2>Riscos e inflação</h2>
            <p>Rentabilidade passada não garante resultados futuros. Desconte inflação para entender o ganho real de poder de compra.</p>
            """),
        Article(
            "hora-extra-como-calcular",
            "Hora extra: como calcular e quanto receber",
            "Aprenda os adicionais de 50% e 100%, limite de horas extras e como estimar o impacto no salário mensal.",
            "hora-extra",
            "Trabalhista",
            new DateOnly(2026, 4, 28),
            """
            <p>Horas extras são pagas com adicional mínimo de 50% sobre a hora normal. Domingos e feriados, quando trabalhados, costumam ter adicional de 100%, salvo acordo ou convenção específica.</p>
            <h2>Como calcular a hora normal</h2>
            <p>Divide-se o salário mensal por 220 horas (jornada CLT padrão) para obter o valor da hora. Sobre esse valor aplica-se o adicional da hora extra.</p>
            <h2>Limite legal</h2>
            <p>A CLT limita horas extras a duas por dia, salvo exceções. Acordos coletivos podem flexibilizar regras em setores específicos.</p>
            <h2>Simule horas extras</h2>
            <p>A <a href="/calculadoras/hora-extra">calculadora de hora extra</a> estima o valor bruto adicional conforme salário, quantidade de horas e tipo de adicional.</p>
            """),
        Article(
            "financiamento-como-calcular-parcelas",
            "Financiamento: como calcular parcelas e custo total",
            "Entenda juros, prazo, CET e como comparar propostas de financiamento imobiliário ou veículos.",
            "financiamento",
            "Financeiro",
            new DateOnly(2026, 5, 5),
            """
            <p>Financiamentos usam sistemas de amortização (Price ou SAC) que distribuem juros e principal ao longo do prazo. A parcela inicial e o custo total variam conforme taxa, prazo e entrada.</p>
            <h2>Price x SAC</h2>
            <p>No Price, parcelas fixas; juros são maiores no início. No SAC, amortização constante e parcelas decrescentes, com juros totais geralmente menores.</p>
            <h2>CET: custo efetivo total</h2>
            <p>Além dos juros nominais, taxas de administração, seguros e IOF entram no CET. Compare sempre o CET entre bancos, não só a taxa anunciada.</p>
            <h2>Simule parcelas</h2>
            <p>A <a href="/calculadoras/financiamento">calculadora de financiamento</a> projeta parcelas e total pago. Teste prazos diferentes antes de assinar contrato.</p>
            """),
        Article(
            "tabela-inss-2026-guia",
            "Tabela INSS 2026: faixas, alíquotas e teto",
            "Consulte as faixas progressivas do INSS em 2026 e entenda como cada salário se encaixa no cálculo.",
            "inss",
            "Fiscal",
            new DateOnly(2026, 5, 12),
            """
            <p>A tabela do INSS é atualizada anualmente com novos tetos e faixas. Em 2026, o cálculo continua progressivo: cada faixa aplica sua alíquota apenas sobre a parcela correspondente do salário.</p>
            <h2>Por que não usar uma alíquota única?</h2>
            <p>Multiplicar o salário inteiro por 14%, por exemplo, superestima o desconto. O método correto soma parcelas parciais de cada faixa até o teto.</p>
            <h2>Teto previdenciário</h2>
            <p>Salários acima do teto contribuem somente até o limite máximo. Benefícios futuros também respeitam esse teto na maioria dos casos.</p>
            <h2>Calcule automaticamente</h2>
            <p>Evite erros manuais usando a <a href="/calculadoras/inss">calculadora de INSS</a>, já configurada com as faixas de 2026.</p>
            """),
        Article(
            "tabela-irrf-2026-guia",
            "Tabela IRRF 2026: faixas e deduções",
            "Veja como a tabela do imposto de renda retido na fonte funciona em 2026 e quando o desconto aparece no holerite.",
            "irrf",
            "Fiscal",
            new DateOnly(2026, 5, 19),
            """
            <p>A tabela IRRF define alíquotas progressivas sobre a base de cálculo após INSS, dependentes e outras deduções permitidas. Mudanças na legislação podem alterar faixas e valores de dedução.</p>
            <h2>Base de cálculo</h2>
            <p>Não se aplica IRRF diretamente sobre o salário bruto. Primeiro subtraem-se descontos obrigatórios e deduções legais; o resultado é tributado conforme a tabela.</p>
            <h2>Dependentes</h2>
            <p>Cada dependente válido reduz a base em valor fixo mensal. Mantenha cadastro atualizado no RH para evitar retenção indevida ou ajustes na declaração anual.</p>
            <h2>Simule o imposto</h2>
            <p>A <a href="/calculadoras/irrf">calculadora de IRRF</a> aplica a tabela vigente. Combine com salário líquido para visão completa do holerite.</p>
            """),
        Article(
            "desconto-vale-transporte",
            "Vale-transporte: como funciona o desconto no salário",
            "Entenda a regra dos 6%, opt-in do benefício e quando vale a pena usar vale-transporte.",
            "salario-liquido",
            "Trabalhista",
            new DateOnly(2026, 5, 26),
            """
            <p>O vale-transporte é benefício do empregador para deslocamento casa-trabalho. O trabalhador pode optar por recebê-lo; quando opta, o desconto no salário é limitado a 6% do bruto.</p>
            <h2>Opt-in e opt-out</h2>
            <p>Ninguém é obrigado a usar VT. Se o custo diário de transporte supera 6% do salário, o benefício costuma valer a pena. Caso contrário, pagar passagens avulsas pode ser mais barato.</p>
            <h2>Impacto no líquido</h2>
            <p>O desconto reduz o salário líquido, mas compensa passagens. Some VT + salário líquido para comparar com cenário sem benefício.</p>
            <h2>Simule seu holerite</h2>
            <p>Informe o desconto de transporte na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> junto com INSS e IRRF.</p>
            """),
        Article(
            "fgts-guia-completo",
            "FGTS: guia completo para trabalhadores CLT",
            "Saiba como funciona o depósito de 8%, multa rescisória, saque-aniversário e quando você pode retirar o saldo.",
            "fgts",
            "Trabalhista",
            new DateOnly(2026, 6, 2),
            """
            <p>O FGTS (Fundo de Garantia do Tempo de Serviço) recebe depósito mensal de 8% do salário bruto pelo empregador. O trabalhador não desconta do próprio salário; o valor fica em conta vinculada na Caixa.</p>
            <h2>Depósito mensal</h2>
            <p>Empregador deposita até o dia 7 do mês seguinte. Inclui salário, comissões e algumas gratificações. Atrasos geram multa para a empresa.</p>
            <h2>Multa de 40% na demissão sem justa causa</h2>
            <p>Quando o empregador dispensa sem justa causa, paga multa de 40% sobre saldo FGTS. Esse valor entra na estimativa de rescisão.</p>
            <h2>Modalidades de saque</h2>
            <p>Demissão sem justa causa, aposentadoria, compra da casa própria e saque-aniversário (com perda de multa em rescisão futura) são hipóteses comuns.</p>
            <h2>Calcule depósitos e multa</h2>
            <p>Use a <a href="/calculadoras/fgts">calculadora de FGTS</a> para estimar depósitos e multa rescisória. Para o pacote completo de desligamento, veja também a <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a>.</p>
            """),
        Article(
            "planejamento-financeiro-com-salario",
            "Planejamento financeiro: organize o salário líquido",
            "Aprenda a dividir o salário líquido em categorias, reservar emergências e evitar endividamento com base no que realmente entra na conta.",
            "salario-liquido",
            "Financeiro",
            new DateOnly(2026, 6, 9),
            """
            <p>Planejar finanças começa pelo salário líquido — não pelo bruto do contrato. Saber quanto sobra após INSS, IRRF e descontos é o primeiro passo para um orçamento realista.</p>
            <h2>Regra 50-30-20 adaptada</h2>
            <ul>
            <li><strong>50% necessidades:</strong> moradia, alimentação, transporte, contas fixas.</li>
            <li><strong>30% desejos:</strong> lazer, assinaturas, compras não essenciais.</li>
            <li><strong>20% futuro:</strong> reserva de emergência, investimentos, amortização de dívidas.</li>
            </ul>
            <h2>Reserva de emergência</h2>
            <p>Antes de investir em ativos voláteis, acumule 3 a 6 meses de despesas essenciais em aplicação líquida. Imprevistos no emprego ou saúde ficam menos devastadores.</p>
            <h2>Conheça seu líquido real</h2>
            <p>Use a <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> e a <a href="/calculadoras/juros-compostos">calculadora de juros compostos</a> para projetar quanto pode guardar todo mês.</p>
            <h2>Revise todo trimestre</h2>
            <p>Promoções, reajustes e novos descontos alteram o líquido. Atualize o orçamento quando o holerite mudar.</p>
            """)
    ];

    private static BlogArticleSeed Article(
        string slug,
        string title,
        string summary,
        string calculatorSlug,
        string category,
        DateOnly publishedAt,
        string content) =>
        new(slug, title, summary, content.Trim(), publishedAt, category, calculatorSlug);
}
