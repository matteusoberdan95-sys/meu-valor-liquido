namespace MeuValorLiquido.WebApp.Data;
public sealed record BlogArticleSeed(
    string Slug,
    string Title,
    string Summary,
    string Content,
    DateOnly PublishedAt,
    string Category,
    string? RelatedCalculatorSlug = null,
    string Author = "Matteus Oberdan");

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
            <li><strong>IRRF:</strong> imposto retido na fonte sobre a base após INSS e dependentes. Em 2026, bases até R$ 5.000 podem ficar isentas pela Lei 15.270/2025.</li>
            <li><strong>Vale-transporte:</strong> até 6% do salário bruto, quando optado.</li>
            <li><strong>Vale-refeição/alimentação, plano de saúde e pensão:</strong> descontos opcionais informados separadamente na calculadora.</li>
            <li><strong>Outros:</strong> empréstimo consignado, sindicato e demais descontos autorizados.</li>
            </ul>
            <h2>Como estimar seu líquido</h2>
            <p>Use a <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> para simular seu caso com as tabelas de 2026. Informe salário bruto, dependentes e abra <strong>Ajustar descontos</strong> para vale-transporte, VR/VA, plano, pensão e outros itens do holerite.</p>
            <h2>Limitações de uma estimativa</h2>
            <p>Convenções coletivas, adicionais noturnos, horas extras e benefícios específicos alteram o resultado final. Trate a simulação como referência educativa e confirme valores oficiais com o departamento pessoal.</p>
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>A <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> é calibrada com cenários de referência documentados em <a href="/como-calculamos">Como calculamos</a>, incluindo tabelas INSS/IRRF de 2026 e tolerância de paridade.</p>
            <p>Leia <a href="/blog/como-conferir-holerite">como conferir holerite</a> e a <a href="/duvidas/como-calcular-salario-liquido">FAQ de salário líquido</a>.</p>
            <p><strong>Estimativa educativa:</strong> não substitui holerite oficial, TRCT ou consultoria contábil/trabalhista.</p>
            """),
        Article(
            "como-avaliar-proposta-salarial",
            "Como avaliar uma proposta salarial pelo líquido",
            "Compare bruto atual e proposto pelo que entra no bolso — com os mesmos descontos de holerite nos dois cenários.",
            "proposta-salarial",
            "Trabalhista",
            new DateOnly(2026, 6, 17),
            """
            <p>Uma proposta de emprego ou aumento costuma ser comunicada em salário bruto. O que muda seu orçamento, porém, é o <strong>líquido</strong> — e impostos progressivos fazem o percentual no bolso ser menor que o percentual no bruto.</p>
            <h2>Compare cenários com os mesmos descontos</h2>
            <p>Para uma comparação justa, mantenha vale-transporte, VR/VA, plano de saúde, pensão e outros descontos fixos iguais no cenário atual e no proposto. Só assim você vê o ganho real no bolso.</p>
            <h2>Bruto maior nem sempre é bolso maior na mesma proporção</h2>
            <p>INSS e IRRF são progressivos: parte do aumento pode ir para impostos. A <a href="/calculadoras/proposta-salarial">calculadora de proposta salarial</a> destaca o ganho líquido mensal e os percentuais no bruto e no líquido.</p>
            <h2>Próximo passo</h2>
            <p>Depois de comparar a proposta, simule o holerite completo na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> ou descubra o bruto necessário na <a href="/calculadoras/salario-bruto-necessario">calculadora de salário bruto necessário</a>.</p>
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>Cenários de holerite e proposta estão documentados em <a href="/como-calculamos">Como calculamos</a>, com paridade entre as três calculadoras de salário.</p>
            <p>Consulte a <a href="/duvidas/proposta-salarial-como-negociar">FAQ de negociação de proposta</a> para argumentos objetivos na conversa com o RH.</p>
            <p><strong>Estimativa educativa:</strong> benefícios não monetários (PLR, bônus, stock options) não entram nesta simulação.</p>
            <p>Veja o artigo <a href="/blog/como-conferir-holerite">como conferir holerite</a> para validar o holerite após aceitar a proposta.</p>
            """),
        Article(
            "como-conferir-holerite",
            "Como conferir holerite: checklist linha a linha",
            "Aprenda a comparar seu holerite com uma simulação de salário líquido e identificar divergências comuns em INSS, IRRF e descontos.",
            "salario-liquido",
            "Trabalhista",
            new DateOnly(2026, 6, 18),
            """
            <p>O holerite oficial é o documento que a empresa emite todo mês. Uma <strong>simulação educativa</strong> ajuda a entender cada linha antes de questionar o RH — mas não substitui o contracheque assinado.</p>
            <h2>Checklist rápido</h2>
            <ol>
            <li><strong>Salário base:</strong> confira se o bruto bate com o contrato ou com adicionais fixos do mês.</li>
            <li><strong>INSS:</strong> verifique se o desconto segue faixas progressivas de 2026 (não é alíquota única sobre tudo).</li>
            <li><strong>IRRF:</strong> compare a base após INSS e dependentes; isenção até R$ 5.000 de base pode zerar o imposto (Lei 15.270/2025).</li>
            <li><strong>Vale-transporte:</strong> desconto limitado a 6% do bruto, se você optou pelo benefício.</li>
            <li><strong>VR/VA, plano, pensão:</strong> cada item deve aparecer em linha separada — não somados em "outros" sem detalhe.</li>
            <li><strong>Outros descontos:</strong> empréstimo consignado, sindicato e faltas devem ter rubrica identificada.</li>
            </ol>
            <h2>Quando a simulação diverge do holerite</h2>
            <ul>
            <li>Horas extras, comissões ou adicional noturno no mês (médias alteram INSS/IRRF).</li>
            <li>Dependentes não cadastrados ou pensão judicial não informada na simulação.</li>
            <li>Arredondamentos de centavos ou competência diferente (adiantamento, férias no mesmo mês).</li>
            <li>Convenção coletiva com regras específicas de desconto ou benefício.</li>
            </ul>
            <h2>Simule antes de reclamar</h2>
            <p>Monte o cenário na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> com os mesmos descontos do holerite. Para validar só INSS e IRRF, use a <a href="/conferir-holerite">ferramenta de conferir holerite</a>.</p>
            <p>Use <strong>Ajustar descontos</strong> para VT, VR/VA, plano, pensão (% ou valor) e outros itens. Para negociar aumento, combine com a <a href="/calculadoras/proposta-salarial">calculadora de proposta salarial</a> e leia <a href="/blog/como-avaliar-proposta-salarial">como avaliar proposta pelo líquido</a>.</p>
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>Cenários de holerite estão documentados em <a href="/como-calculamos">Como calculamos</a>, com benchmarks de INSS/IRRF 2026 e paridade entre calculadoras de salário.</p>
            <p>Consulte também a <a href="/duvidas/como-calcular-salario-liquido">FAQ de salário líquido</a> e a <a href="/duvidas/diferenca-salario-bruto-e-liquido">diferença entre bruto e líquido</a>.</p>
            <p><strong>Estimativa educativa:</strong> não substitui holerite oficial, TRCT ou consultoria contábil/trabalhista.</p>
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
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>Compare cenários na <a href="/calculadoras/ferias">calculadora de férias</a> e leia a <a href="/como-calculamos">metodologia por categoria</a> para entender premissas, fontes e limitações.</p>
            <p><strong>Estimativa educativa:</strong> médias salariais e adiantamentos não informados podem alterar o holerite real.</p>
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
            <p>Leia também <a href="/blog/rescisao-clt-vs-trct">rescisão CLT vs TRCT</a> para entender o que o documento oficial pode trazer a mais.</p>
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>Os cenários de rescisão passam por testes de paridade documentados em <a href="/como-calculamos">Como calculamos</a>. Divergências com TRCT costumam vir de médias salariais, HE ou adiantamentos não informados.</p>
            <p>Veja a <a href="/duvidas/rescisao-pedido-demissao-o-que-recebo">FAQ de pedido de demissão</a> e <a href="/duvidas/seguro-desemprego-quando-tem-direito">seguro-desemprego</a> quando aplicável.</p>
            <p><strong>Estimativa educativa:</strong> não substitui documento oficial assinado pelo empregador.</p>
            """),
        Article(
            "rescisao-clt-vs-trct",
            "Rescisão CLT vs TRCT: o que a calculadora estima e o que só o documento traz",
            "Entenda diferenças entre simulação educativa de rescisão e o Termo de Rescisão oficial — médias, adiantamentos e homologação.",
            "rescisao-clt",
            "Trabalhista",
            new DateOnly(2026, 6, 18),
            """
            <p>A <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a> projeta verbas comuns: saldo de salário, 13º proporcional, férias + 1/3, aviso prévio, multa FGTS e descontos típicos. O <strong>TRCT</strong> (Termo de Rescisão do Contrato de Trabalho) é o documento que a empresa homologa — pode incluir valores que você não informou na simulação.</p>
            <h2>O que a calculadora cobre bem</h2>
            <ul>
            <li>Verbas rescisórias por tipo de desligamento (demissão, pedido, acordo 484-A, justa causa).</li>
            <li>INSS e IRRF sobre saldo e 13º conforme regras usuais na rescisão.</li>
            <li>Multa FGTS (40%, 20% ou zero) e aviso prévio indenizado ou descontado.</li>
            <li>Campos opcionais: adiantamento de 13º, média de HE/comissão, férias vencidas.</li>
            </ul>
            <h2>O que pode aparecer só no TRCT</h2>
            <ul>
            <li><strong>Médias salariais</strong> de horas extras, comissões ou adicionais nos últimos meses.</li>
            <li><strong>Adiantamentos</strong> já pagos (13º, férias, salário) descontados na rescisão.</li>
            <li><strong>Descontos</strong> de empréstimo, pensão, VT do último mês ou acordos sindicais.</li>
            <li><strong>Seguro-desemprego</strong> — direito informativo; valor e parcelas são calculados pelo governo, não pela empresa na rescisão.</li>
            </ul>
            <h2>Como usar a simulação com segurança</h2>
            <p>Informe salário, datas de admissão e saída, motivo do desligamento e campos avançados (adiantamento, média HE). Compare o extrato com o TRCT linha a linha antes de assinar.</p>
            <p>Para FGTS e multa, use também a <a href="/calculadoras/fgts">calculadora de FGTS</a>. Para visão geral do desligamento, leia <a href="/blog/como-calcular-rescisao-clt">como calcular rescisão CLT</a>.</p>
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>Mais de 15 cenários de rescisão estão em <a href="/como-calculamos">Como calculamos</a> (seção Rescisão CLT), com regra dos 15 dias e motivos raros já suportados no motor.</p>
            <p>Consulte <a href="/duvidas/multa-fgts-40-porcento">multa de 40% do FGTS</a> e <a href="/duvidas/seguro-desemprego-quando-tem-direito">seguro-desemprego</a> para direitos pós-demissão.</p>
            <p><strong>Estimativa educativa:</strong> não substitui homologação, sindicato, advogado trabalhista ou Caixa Econômica.</p>
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
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>A <a href="/calculadoras/inss">calculadora de INSS</a> usa as faixas oficiais de 2026. Veja fontes, data de calibração e limitações em <a href="/como-calculamos">Como calculamos</a>.</p>
            <p><strong>Estimativa educativa:</strong> não substitui guia da Previdência ou holerite oficial.</p>
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
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>Simule na <a href="/calculadoras/irrf">calculadora de IRRF</a> e confira a tabela vigente em <a href="/como-calculamos">Como calculamos</a>, com dedução por dependente e regras de isenção de 2026.</p>
            <p><strong>Estimativa educativa:</strong> não substitui orientação da Receita Federal ou declaração anual.</p>
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
            <p>Evite erros manuais usando a <a href="/calculadoras/inss">calculadora de INSS</a>, já configurada com as faixas de 2026. Para ver o INSS no contexto do holerite completo, veja o guia <a href="/negociar-salario">negociar salário pelo líquido</a>.</p>
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
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>Cenários de FGTS e multa rescisória estão documentados em <a href="/como-calculamos">Como calculamos</a>. O saldo real depende do extrato da Caixa.</p>
            <p><strong>Estimativa educativa:</strong> não substitui consulta ao extrato oficial do FGTS.</p>
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
            """),
        Article(
            "mei-faturamento-e-das",
            "MEI: limite de faturamento, DAS e desenquadramento",
            "Entenda o teto anual do MEI, como estimar o DAS mensal e quando o excesso de faturamento leva ao desenquadramento.",
            "simulador-mei",
            "Fiscal",
            new DateOnly(2026, 6, 17),
            """
            <p>O Microempreendedor Individual (MEI) combina formalização simplificada com DAS mensal fixo. O ponto crítico para quem está começando é o <strong>limite anual de faturamento</strong> — ultrapassá-lo pode gerar desenquadramento.</p>
            <h2>Limite anual e tolerância</h2>
            <p>Em 2026, o teto de faturamento MEI é de R$ 81.000 por ano. Há tolerância de até 20% (R$ 97.200) com desenquadramento no ano seguinte. Acima disso, o risco de desenquadramento retroativo aumenta.</p>
            <h2>DAS por atividade</h2>
            <p>O valor do DAS varia conforme comércio, serviços ou indústria. Mesmo com faturamento baixo, o pagamento mensal é obrigatório enquanto o MEI estiver ativo.</p>
            <h2>Simule seu cenário</h2>
            <p>Use o <a href="/calculadoras/simulador-mei">simulador MEI</a> para estimar DAS, informar o <strong>faturamento já acumulado no ano</strong> e projetar o teto com o faturamento mensal estimado.</p>
            <p>Para comparar MEI com emprego CLT ou PJ, veja o <a href="/calculadoras/pj-vs-clt">comparador PJ vs CLT</a> e o <a href="/duvidas/pj-ou-clt-qual-compensa">FAQ PJ ou CLT</a>.</p>
            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>As regras de limite e DAS seguem parâmetros vigentes documentados em <a href="/como-calculamos">Como calculamos</a>. Mudanças na legislação exigem recalibração.</p>
            <p><strong>Estimativa educativa:</strong> não substitui contador para migração ao Simples Nacional ou ME.</p>
            """),
        Article(
            "cdb-ou-tesouro-direto-investimentos",
            "CDB ou Tesouro Direto: qual escolher para começar?",
            "Compare liquidez, segurança, tributação e rentabilidade entre CDB e Tesouro Direto para montar sua primeira reserva.",
            "juros-compostos",
            "Financeiro",
            new DateOnly(2026, 6, 19),
            """
            <p>Quem está começando a investir no Brasil costuma travar na primeira dúvida: <strong>CDB</strong> ou <strong>Tesouro Direto</strong>? Os dois são opções conservadoras, mas funcionam de formas diferentes — e isso muda o que você recebe no bolso e quando pode resgatar.</p>
            <h2>O que é cada um</h2>
            <ul>
            <li><strong>CDB:</strong> você empresta dinheiro ao banco e recebe juros prefixados ou atrelados ao CDI.</li>
            <li><strong>Tesouro Direto:</strong> você compra títulos públicos federais (Selic, IPCA+ ou prefixado) com liquidação em dias úteis.</li>
            </ul>
            <h2>Segurança e garantias</h2>
            <p>Tesouro tem risco de crédito soberano (governo federal). CDBs de bancos médios costumam pagar mais, mas dependem da solidez da instituição — o FGC cobre até R$ 250 mil por CPF por conglomerado financeiro.</p>
            <h2>Liquidez e prazo</h2>
            <p>Tesouro Selic permite resgate rápido com taxa baixa. CDBs podem ter carência ou penalidade para sair antes do vencimento. Para reserva de emergência, liquidez diária pesa mais que meio ponto a mais de rentabilidade.</p>
            <h2>Impostos</h2>
            <p>Ambos seguem a tabela regressiva de IR do investimento (22,5% até 180 dias; 15% após 720 dias). IOF pode incidir em resgates muito curtos. Compare sempre o <strong>líquido</strong>, não só a taxa bruta anunciada.</p>
            <h2>Projete o crescimento</h2>
            <p>Use a <a href="/calculadoras/juros-compostos">calculadora de juros compostos</a> para simular aportes mensais em cada taxa. Leia também <a href="/blog/juros-compostos-guia">juros compostos: guia prático</a> e <a href="/blog/planejamento-financeiro-com-salario">planejamento financeiro com salário</a>.</p>
            """),
        Article(
            "reserva-emergencia-onde-investir",
            "Reserva de emergência: quanto guardar e onde investir",
            "Defina o tamanho ideal da reserva com base no seu custo de vida e saiba onde aplicar com liquidez e baixo risco.",
            "juros-compostos",
            "Financeiro",
            new DateOnly(2026, 6, 20),
            """
            <p>A reserva de emergência é o colchão financeiro para imprevistos: demissão, saúde, conserto urgente. Antes de buscar rentabilidade alta, o objetivo é <strong>não perder dinheiro</strong> e conseguir sacar em poucos dias.</p>
            <h2>Quanto guardar</h2>
            <p>Regra comum: de <strong>3 a 6 meses</strong> das despesas essenciais (moradia, alimentação, transporte, contas fixas). Quem é CLT com estabilidade pode mirar 3 meses; autônomo ou PJ costuma precisar de 6 ou mais.</p>
            <h2>Calcule com o líquido real</h2>
            <p>Baseie a reserva no que entra na conta, não no salário bruto. Simule seu holerite na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> e multiplique o líquido pelas despesas mensais reais.</p>
            <h2>Onde investir a reserva</h2>
            <ul>
            <li><strong>Tesouro Selic</strong> ou <strong>CDB com liquidez diária</strong> para acesso rápido.</li>
            <li>Evite ações, cripto ou fundos ilíquidos para essa parcela.</li>
            <li>Mantenha a reserva separada mentalmente do dinheiro de objetivos de médio prazo.</li>
            </ul>
            <h2>Revisão anual</h2>
            <p>Reajuste de aluguel, filhos ou novo financiamento alteram o custo de vida. Atualize a meta da reserva quando o orçamento mudar.</p>
            <p>Veja <a href="/blog/cdb-ou-tesouro-direto-investimentos">CDB ou Tesouro Direto</a> para comparar onde aplicar o primeiro aporte.</p>
            """),
        Article(
            "como-investir-com-pouco-dinheiro",
            "Como investir com pouco dinheiro: guia para iniciantes",
            "Comece com aportes pequenos, entenda taxas e monte hábito de investir sem depender de um salário alto.",
            "juros-compostos",
            "Financeiro",
            new DateOnly(2026, 6, 21),
            """
            <p>Investir não exige milhares de reais no primeiro mês. Com <strong>aportes regulares</strong> — mesmo de R$ 50 ou R$ 100 — o efeito dos juros compostos aparece quando o hábito se mantém por anos.</p>
            <h2>Passo 1: organize o básico</h2>
            <p>Quite dívidas com juros altos (cartão, cheque especial) antes de buscar rentabilidade em renda fixa. O “investimento” mais rentável pode ser pagar o rotativo.</p>
            <h2>Passo 2: defina uma meta realista</h2>
            <p>Escolha um percentual do líquido — por exemplo, 10% após montar a reserva de emergência. Use a <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> para saber quanto sobra de fato.</p>
            <h2>Passo 3: automatize</h2>
            <p>Programe transferência no dia do pagamento. Automatizar reduz a tentação de gastar o que “sobrou” no fim do mês.</p>
            <h2>Passo 4: simule o futuro</h2>
            <p>A <a href="/calculadoras/juros-compostos">calculadora de juros compostos</a> mostra quanto R$ 100/mês podem virar em 5 ou 10 anos com taxas conservadoras. Compare cenários — o tempo é o maior aliado do iniciante.</p>
            <h2>Cuidados</h2>
            <p>Desconfie de promessas de ganho rápido. Rentabilidade passada não garante retorno futuro. Priorize educação e diversificação gradual.</p>
            """),
        Article(
            "quanto-cobrar-servicos-pj",
            "Quanto cobrar como PJ: preço/hora, margem e proposta",
            "Aprenda a precificar serviços como PJ sem vender barato demais: custos, impostos, férias não pagas e lucro desejado.",
            "pj-vs-clt",
            "Financeiro",
            new DateOnly(2026, 6, 22),
            """
            <p>Freelancer ou consultor PJ que cobra “o que o mercado paga” sem planilha costuma descobrir, no fim do ano, que trabalhou mais e guardou menos do que um CLT com salário parecido. Precificar bem exige somar <strong>custos, impostos e margem</strong>.</p>
            <h2>Liste todos os custos</h2>
            <ul>
            <li>Contador, DAS ou impostos do Simples Nacional.</li>
            <li>Plano de saúde, previdência (INSS como contribuinte individual, se aplicável).</li>
            <li>Software, equipamento, coworking, deslocamento.</li>
            <li>Férias e 13º que você paga a si mesmo — não existem automaticamente na PJ.</li>
            </ul>
            <h2>Da hora ao valor mensal</h2>
            <p>Divida a meta de faturamento líquido pelas horas faturáveis reais (desconte prospecção, administração e imprevistos). Muitos profissionais faturam só 60–70% das horas do mês.</p>
            <h2>Compare com CLT</h2>
            <p>Antes de aceitar um contrato PJ, simule o equivalente CLT na <a href="/calculadoras/pj-vs-clt">calculadora PJ vs CLT</a>. Leia <a href="/blog/pj-ou-clt-qual-melhor">PJ ou CLT: qual é melhor?</a> para entender benefícios que você deixa de ter.</p>
            <h2>Proposta comercial</h2>
            <p>Apresente escopo, prazo e entregáveis — não só “valor/hora”. Isso protege contra scope creep e facilita reajuste anual.</p>
            """),
        Article(
            "mei-nota-fiscal-quando-emitir",
            "Nota fiscal MEI: quando emitir e como não errar",
            "Entenda obrigatoriedade de NFS-e, prazos, cliente PJ e o que acontece se você faturar sem nota.",
            "simulador-mei",
            "Financeiro",
            new DateOnly(2026, 6, 23),
            """
            <p>MEI que presta serviço ou vende para outra empresa quase sempre precisa de <strong>nota fiscal</strong>. Emitir corretamente mantém o CNPJ regular, evita autuação e permite o cliente deduzir a despesa quando aplicável.</p>
            <h2>Quando emitir</h2>
            <ul>
            <li><strong>Serviços para PJ:</strong> em geral, NFS-e é obrigatória a cada prestação.</li>
            <li><strong>Vendas para consumidor final:</strong> regras variam por município e tipo de atividade.</li>
            <li><strong>Exportação de serviço:</strong> consulte regras específicas e conversão cambial.</li>
            </ul>
            <h2>Prazos e DAS</h2>
            <p>Nota emitida entra no faturamento do mês. O <strong>DAS</strong> continua devido mesmo com faturamento zero — enquanto o MEI estiver ativo. Acompanhe o teto anual no <a href="/calculadoras/simulador-mei">simulador MEI</a>.</p>
            <h2>Erros comuns</h2>
            <ul>
            <li>Faturar acima do limite sem planejar desenquadramento.</li>
            <li>Confundir recebimento na conta com competência da nota.</li>
            <li>Não guardar XML/PDF das notas para comprovação.</li>
            </ul>
            <h2>Relação com PJ e CLT</h2>
            <p>MEI não substitui análise de vínculo empregatício. Veja <a href="/blog/mei-faturamento-e-das">MEI: limite de faturamento e DAS</a> e compare modelos na <a href="/calculadoras/pj-vs-clt">calculadora PJ vs CLT</a>.</p>
            """),
        Article(
            "simples-nacional-pj-guia-iniciantes",
            "Simples Nacional para PJ: guia para quem está começando",
            "Entenda anexos, alíquotas efetivas, pró-labore e quando vale migrar do MEI para ME no Simples.",
            "pj-vs-clt",
            "Financeiro",
            new DateOnly(2026, 6, 24),
            """
            <p>Quem ultrapassa o limite do MEI ou abre empresa como ME/Sociedade Limitada encontra o <strong>Simples Nacional</strong> — regime que unifica vários impostos em guias mensais (DAS). A alíquota depende do anexo da atividade e do faturamento acumulado.</p>
            <h2>Anexos principais</h2>
            <ul>
            <li><strong>Anexo III:</strong> serviços com folha de pagamento relevante.</li>
            <li><strong>Anexo V:</strong> serviços intelectuais — pode haver redução para Anexo III com folha mínima.</li>
            <li><strong>Anexo I:</strong> comércio.</li>
            </ul>
            <h2>Alíquota efetiva</h2>
            <p>A tabela é progressiva: quanto maior o faturamento 12 meses, maior a faixa. Dividir imposto pago pelo faturamento revela a <strong>alíquota efetiva real</strong> — útil para comparar com proposta PJ na calculadora.</p>
            <h2>Pró-labore e distribuição</h2>
            <p>Sócio que retira pró-labore paga INSS sobre o valor. Lucro distribuído segue regras próprias — converse com contador antes de misturar PF e PJ.</p>
            <h2>MEI → Simples</h2>
            <p>Desenquadramento do MEI exige migração planejada. Simule faturamento e impostos no <a href="/calculadoras/simulador-mei">simulador MEI</a> e compare cenários na <a href="/calculadoras/pj-vs-clt">calculadora PJ vs CLT</a>. Leia também <a href="/blog/mei-faturamento-e-das">limite de faturamento MEI</a>.</p>
            """),
        Article(
            "irrf-2026-reducao-imposto",
            "IRRF 2026: redução de imposto e quem está isento",
            "Entenda a Lei 15.270/2025, a isenção para bases menores e como conferir o desconto no holerite.",
            "irrf",
            "Fiscal",
            new DateOnly(2026, 6, 19),
            """
            <p>Em 2026, a <strong>Lei 15.270/2025</strong> alterou a forma como o IRRF é calculado para rendimentos do trabalho assalariado. Muitos salários que antes tinham retenção passaram a ficar isentos ou com imposto reduzido — o que explica holerites com IRRF zerado mesmo acima do salário mínimo.</p>
            <h2>Quem pode ficar isento em 2026</h2>
            <p>A regra vigente reduz ou zera o imposto quando a <strong>base de cálculo</strong> (após INSS e deduções) está em faixas menores. Não confunda salário bruto com base tributável: dependentes, pensão e o próprio INSS mudam o número.</p>
            <h2>Como conferir no holerite</h2>
            <ol>
            <li>Identifique a rubrica de IRRF e a base informada.</li>
            <li>Compare com a <a href="/calculadoras/irrf">calculadora de IRRF</a> usando o mesmo bruto e dependentes.</li>
            <li>Monte o extrato completo na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a>.</li>
            </ol>
            <h2>Negociação salarial</h2>
            <p>Aumentos pequenos no bruto podem não mudar o líquido na mesma proporção por causa do imposto progressivo. Use o guia <a href="/negociar-salario">negociar salário pelo líquido</a> antes de aceitar proposta.</p>
            <p>Complemente com <a href="/blog/tabela-irrf-2026-guia">tabela IRRF 2026</a> e a <a href="/duvidas/irrf-quem-paga-e-como-calcular">FAQ de IRRF</a>.</p>
            """),
        Article(
            "seguro-desemprego-quem-tem-direito",
            "Seguro-desemprego: quem tem direito e como estimar",
            "Requisitos após demissão sem justa causa, parcelas e o que a rescisão não substitui.",
            "rescisao-clt",
            "Trabalhista",
            new DateOnly(2026, 6, 20),
            """
            <p>O <strong>seguro-desemprego</strong> é benefício do trabalhador CLT dispensado sem justa causa (e em alguns casos de extinção do contrato). Ele <strong>não entra no TRCT</strong> como verba rescisória — é solicitado depois, com requisitos próprios de tempo de serviço e número de solicitações anteriores.</p>
            <h2>Quem costuma ter direito</h2>
            <ul>
            <li>Demissão sem justa causa pelo empregador.</li>
            <li>Extinção normal do contrato por prazo determinado (em regras específicas).</li>
            <li>Alguns casos de rescisão indireta homologada — confirme com sindicato ou advogado.</li>
            </ul>
            <p><strong>Não têm direito</strong> em regra: pedido de demissão, justa causa e aposentadoria.</p>
            <h2>Quantas parcelas</h2>
            <p>O número de parcelas depende do tempo de trabalho na última empresa e de quantas vezes você já solicitou o benefício na vida. O valor de cada parcela segue tabela do Ministério do Trabalho — não é igual ao último salário líquido.</p>
            <h2>Relação com a rescisão</h2>
            <p>Simule verbas rescisórias na <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a>, FGTS na <a href="/calculadoras/fgts">calculadora de FGTS</a> e parcelas na <a href="/calculadoras/seguro-desemprego">calculadora de seguro-desemprego</a>. O valor oficial só o governo calcula na solicitação.</p>
            <p>Siga o guia completo em <a href="/desligamento">desligamento CLT</a> e leia <a href="/duvidas/seguro-desemprego-quando-tem-direito">quando solicitar seguro-desemprego</a>.</p>
            """),
        Article(
            "multa-fgts-40-ou-20",
            "Multa FGTS 40% ou 20%: quando cada uma se aplica",
            "Entenda demissão sem justa causa, acordo 484-A e pedido de demissão no contexto da multa rescisória.",
            "fgts",
            "Trabalhista",
            new DateOnly(2026, 6, 21),
            """
            <p>A <strong>multa rescisória do FGTS</strong> é paga pelo empregador sobre o saldo da conta vinculada em situações específicas de desligamento. O percentual mais conhecido é <strong>40%</strong>, mas o <strong>acordo entre empregado e empresa (art. 484-A da CLT)</strong> usa <strong>20%</strong> sobre o saldo, com regras de saque diferentes.</p>
            <h2>Multa de 40%</h2>
            <p>Aplica-se na <strong>demissão sem justa causa</strong> pelo empregador. O valor é creditado na conta FGTS junto com a possibilidade de saque (conforme modalidade). A multa não é descontada do seu salário — é obrigação da empresa.</p>
            <h2>Multa de 20% (acordo)</h2>
            <p>No acordo para encerrar o contrato, as partes combinam verbas e a multa sobre FGTS cai para 20%. O trabalhador pode sacar parte do saldo, mas abre mão de parte da multa cheia.</p>
            <h2>Sem multa para a empresa</h2>
            <p>Pedido de demissão e demissão por justa causa, em regra, <strong>não geram multa de 40%</strong> para o empregador. O saldo FGTS pode ficar bloqueado para saque até outra hipótese legal.</p>
            <h2>Estime saldo e multa</h2>
            <p>Use a <a href="/calculadoras/fgts">calculadora de FGTS</a> com tipo de desligamento e a <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a> para o pacote completo. Veja também <a href="/blog/fgts-guia-completo">FGTS: guia completo</a> e o hub <a href="/desligamento">desligamento CLT</a>.</p>
            <p>Consulte a <a href="/duvidas/multa-fgts-40-porcento">FAQ multa FGTS 40%</a>.</p>
            """),
        Article(
            "aumento-salario-quanto-sobra-liquido",
            "Aumento de salário: quanto sobra no líquido de verdade",
            "Por que 10% no bruto pode virar menos no bolso — e como simular antes de aceitar a proposta.",
            "proposta-salarial",
            "Trabalhista",
            new DateOnly(2026, 6, 22),
            """
            <p>Comunicar um aumento de <strong>10% no bruto</strong> parece simples, mas INSS e IRRF são progressivos: parte do ganho vira contribuição e imposto. O percentual real no bolso costuma ser <strong>menor</strong> que o anunciado no contracheque futuro.</p>
            <h2>Exemplo ilustrativo</h2>
            <p>De R$ 4.000 para R$ 4.400 de bruto (+10%), o líquido pode subir algo como 7% a 9% — dependendo de dependentes, faixa de IRRF e descontos fixos (VT, plano, pensão). Só a simulação com seus números responde com precisão.</p>
            <h2>Compare com os mesmos descontos</h2>
            <p>Para negociação justa, mantenha vale-transporte, VR/VA, plano e pensão iguais nos dois cenários. A <a href="/calculadoras/proposta-salarial">calculadora de proposta salarial</a> mostra ganho mensal, anual e percentuais no bruto e no líquido.</p>
            <h2>Meta de bolso</h2>
            <p>Se você precisa de um líquido mínimo (aluguel, financiamento), use também a <a href="/calculadoras/salario-bruto-necessario">calculadora de salário bruto necessário</a> — ela responde quanto de bruto pedir para chegar no valor desejado.</p>
            <p>Explore o guia <a href="/negociar-salario">negociar salário pelo líquido</a>, <a href="/blog/como-avaliar-proposta-salarial">como avaliar proposta</a> e a <a href="/duvidas/proposta-salarial-como-negociar">FAQ de negociação</a>.</p>
            """),
        Article(
            "quanto-preciso-ganhar-para-receber-x",
            "Quanto preciso ganhar para receber X líquido?",
            "Use a calculadora inversa: informe o líquido desejado e descubra o salário bruto com INSS e IRRF 2026.",
            "salario-bruto-necessario",
            "Trabalhista",
            new DateOnly(2026, 6, 23),
            """
            <p>Na negociação ou no planejamento pessoal, a pergunta certa costuma ser: <strong>quanto preciso ganhar de bruto</strong> para sobrar X no bolso? Como INSS e IRRF são progressivos, a resposta não é uma conta de três simples — é uma busca sobre as tabelas oficiais de 2026.</p>
            <h2>Como funciona a calculadora inversa</h2>
            <p>A <a href="/calculadoras/salario-bruto-necessario">calculadora de salário bruto necessário</a> estima o bruto a partir do líquido desejado, usando as mesmas regras da calculadora de salário líquido (INSS progressivo, IRRF com redução legal e dependentes).</p>
            <ol>
            <li>Informe o <strong>líquido alvo</strong> (ex.: R$ 4.000).</li>
            <li>Replique descontos do holerite: VT, VR/VA, plano, pensão.</li>
            <li>Confira o bruto encontrado na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a>.</li>
            </ol>
            <h2>Quando usar na prática</h2>
            <ul>
            <li>Definir piso salarial em entrevista ou promoção interna.</li>
            <li>Comparar proposta de aumento com meta de orçamento mensal.</li>
            <li>Entender quanto um bônus ou PLR precisa ser para cobrir uma despesa fixa.</li>
            </ul>
            <p>Combine com <a href="/blog/aumento-salario-quanto-sobra-liquido">aumento salarial e líquido real</a> e o hub <a href="/negociar-salario">negociar salário</a>. Veja também <a href="/duvidas/quanto-preciso-ganhar-para-receber-x-liquido">FAQ: quanto ganhar para receber X líquido</a>.</p>
            """),
        Article(
            "mei-desenquadramento-o-que-fazer",
            "MEI: o que acontece ao ultrapassar o limite",
            "Tolerância de 20%, desenquadramento, DAS e quando migrar para ME no Simples.",
            "simulador-mei",
            "Fiscal",
            new DateOnly(2026, 6, 24),
            """
            <p>O MEI tem teto de faturamento anual (R$ 81.000 em 2026, com tolerância até R$ 97.200). Ultrapassar o limite sem planejamento gera <strong>desenquadramento</strong> — e custos que não aparecem no DAS fixo mensal.</p>
            <h2>O que muda ao desenquadrar</h2>
            <ul>
            <li>Fim do DAS fixo e início de tributação como ME no Simples Nacional (em regra).</li>
            <li>Obrigações contábeis e fiscais mais complexas (contador, DAS por faturamento).</li>
            <li>Retroatividade em alguns casos — confirme com contador o mês do desenquadramento.</li>
            </ul>
            <h2>Simule antes de estourar o teto</h2>
            <p>Use o <a href="/calculadoras/simulador-mei">simulador MEI</a> com faturamento mensal e acumulado no ano. O alerta de uso do teto mostra se você ainda está dentro do limite ou na faixa de tolerância.</p>
            <h2>MEI e CLT ao mesmo tempo?</h2>
            <p>Ter CNPJ MEI não autoriza, por si só, burlar vínculo CLT. Antes de pedir demissão para virar PJ, compare modelos no hub <a href="/virar-pj">virar PJ</a> e na <a href="/calculadoras/pj-vs-clt">calculadora PJ vs CLT</a>.</p>
            <p>Leia também <a href="/blog/mei-faturamento-e-das">MEI: faturamento e DAS</a>, <a href="/blog/mei-nota-fiscal-quando-emitir">quando emitir nota fiscal</a> e <a href="/duvidas/mei-pode-trabalhar-como-clt">FAQ MEI e CLT</a>.</p>
            """),
        Article(
            "pro-labore-pj-quanto-retirar",
            "Pró-labore na PJ: quanto retirar sem prejudicar o líquido",
            "INSS, IRRF e distribuição de lucros: como o pró-labore afeta o bolso do sócio.",
            "pj-vs-clt",
            "Financeiro",
            new DateOnly(2026, 6, 25),
            """
            <p>Na PJ no Simples Nacional, o sócio costuma retirar <strong>pró-labore</strong> — remuneração formal sujeita a INSS e IRRF. O valor escolhido muda o líquido pessoal e a base de contribuição previdenciária, sem ser o mesmo que o faturamento da empresa.</p>
            <h2>Pró-labore não é o faturamento</h2>
            <p>Faturamento é o que a empresa recebe dos clientes. Pró-labore é a parcela destinada ao sócio como salário. O restante pode ficar na empresa ou ser distribuído conforme regras contábeis — sempre com orientação profissional.</p>
            <h2>Como simular</h2>
            <p>Na <a href="/calculadoras/pj-vs-clt">calculadora PJ vs CLT</a>, ajuste o percentual de pró-labore sobre o faturamento e compare o líquido pessoal com o cenário CLT. Inclua alíquota do Simples, dependentes e despesas fixas (contador, software).</p>
            <h2>Erros comuns</h2>
            <ul>
            <li>Pró-labore zerado para “pagar menos imposto” — pode gerar problemas previdenciários e fiscais.</li>
            <li>Confundir lucro distribuído com pró-labore sem critério contábil.</li>
            <li>Ignorar que benefícios CLT (férias, 13º, FGTS) não entram na comparação automática.</li>
            </ul>
            <p>Explore <a href="/virar-pj">virar PJ</a>, <a href="/blog/simples-nacional-pj-guia-iniciantes">Simples Nacional para iniciantes</a> e <a href="/duvidas/pj-ou-clt-qual-compensa">FAQ PJ ou CLT</a>.</p>
            """),
        Article(
            "decimo-terceiro-primeira-segunda-parcela",
            "13º salário: 1ª e 2ª parcela e descontos",
            "Entenda o pagamento em novembro e dezembro, adiantamento e INSS/IRRF na segunda parcela.",
            "decimo-terceiro",
            "Trabalhista",
            new DateOnly(2026, 6, 26),
            """
            <p>O <strong>décimo terceiro</strong> é pago em duas parcelas no fim do ano: a primeira até 30/11 (em geral metade do valor bruto, sem descontos) e a segunda até 20/12, quando entram INSS e IRRF sobre o <strong>valor integral</strong> do 13º.</p>
            <h2>Por que a 2ª parcela “some” mais</h2>
            <p>Na 2ª parcela, o imposto incide sobre o 13º cheio, não só sobre a metade que falta. Por isso o líquido de dezembro costuma ser menor do que a conta mental de “metade do bruto”.</p>
            <h2>Adiantamento e proporcional</h2>
            <p>Se a empresa adiantou parte do 13º antes de novembro, informe o valor na <a href="/calculadoras/decimo-terceiro">calculadora de décimo terceiro</a>. Quem entrou ou saiu no meio do ano recebe <strong>proporcional</strong> (avos) — na rescisão, use a <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a>.</p>
            <h2>Planejamento</h2>
            <p>Reserve parte da 2ª parcela para despesas de janeiro e possível IR complementar. Compare com o <a href="/calculadoras/salario-liquido">salário líquido</a> habitual para não superestimar o “extra” de fim de ano.</p>
            <p>Veja também <a href="/blog/guia-decimo-terceiro">guia do 13º</a> e <a href="/duvidas/decimo-terceiro-quem-tem-direito">FAQ do décimo terceiro</a>.</p>
            """),
        Article(
            "ferias-abono-pecuniario-vale-a-pena",
            "Abono pecuniário: vale a pena vender 1/3 das férias?",
            "Simule férias com venda de até 10 dias, 1/3 constitucional e descontos de INSS e IRRF.",
            "ferias",
            "Trabalhista",
            new DateOnly(2026, 6, 27),
            """
            <p>O <strong>abono pecuniário</strong> permite converter até <strong>1/3 dos dias de férias</strong> em dinheiro (venda de 10 dias em férias de 30). Você goza o restante e recebe o abono junto com férias + 1/3 constitucional — mas o extrato muda e os descontos incidem sobre o total.</p>
            <h2>Quando pode compensar</h2>
            <ul>
            <li>Prioridade imediata de caixa (dívida cara, emergência planejada).</li>
            <li>Preferência por descanso menor e receber mais naquele mês.</li>
            </ul>
            <h2>Quando pensar duas vezes</h2>
            <ul>
            <li>Descanso necessário por saúde ou burnout — dias vendidos não voltam.</li>
            <li>IRRF e INSS sobre o valor maior de férias + abono reduzem o ganho líquido.</li>
            </ul>
            <h2>Simule antes de pedir ao RH</h2>
            <p>Marque a opção de abono na <a href="/calculadoras/ferias">calculadora de férias</a> e compare com férias integrais no mesmo salário. Inclua dependentes e descontos habituais.</p>
            <p>Complemente com <a href="/blog/como-calcular-ferias">como calcular férias</a> e <a href="/duvidas/ferias-proporcionais-como-funciona">FAQ de férias proporcionais</a>.</p>
            """),
        Article(
            "emprestimo-consignado-desconto-holerite",
            "Empréstimo consignado: quanto desconta do salário",
            "Margem consignável, teto de desconto e como estimar o impacto no líquido do holerite.",
            "salario-liquido",
            "Financeiro",
            new DateOnly(2026, 6, 28),
            """
            <p>O <strong>empréstimo consignado</strong> desconta parcelas direto no salário ou benefício, dentro de um <strong>teto legal</strong> (em geral 35% do salário, sendo 5% para cartão — regras podem variar por contrato). Isso reduz o líquido disponível todo mês até quitar a dívida.</p>
            <h2>Margem consignável</h2>
            <p>Bancos calculam a margem sobre o salário bruto ou base definida no convênio. Antes de contratar, simule o holerite <strong>com</strong> o desconto na <a href="/calculadoras/salario-liquido">calculadoras de salário líquido</a> (campo “outros descontos”).</p>
            <h2>Consignado vs outros empréstimos</h2>
            <p>Taxas costumam ser menores que crédito pessoal porque o risco de inadimplência é menor — mas comprometer a margem limita novas operações e reduz folga no orçamento.</p>
            <h2>Checklist antes de assinar</h2>
            <ol>
            <li>Some todas as parcelas consignadas já ativas.</li>
            <li>Verifique se o total cabe na margem e no seu orçamento líquido.</li>
            <li>Compare CET com outras linhas de crédito — não só a taxa mensal anunciada.</li>
            </ol>
            <p>Para planejar reserva e metas, veja <a href="/blog/planejamento-financeiro-com-salario">planejamento com salário</a>, <a href="/blog/reserva-emergencia-quanto-guardar">reserva de emergência</a> e <a href="/duvidas/como-calcular-salario-liquido">FAQ de salário líquido</a>.</p>
            """),
        Article(
            "reserva-emergencia-quanto-guardar",
            "Reserva de emergência: quanto guardar com seu salário",
            "Regra dos 3 a 6 meses de despesas, usando salário líquido e projeção com juros compostos.",
            "salario-liquido",
            "Financeiro",
            new DateOnly(2026, 6, 29),
            """
            <p>A <strong>reserva de emergência</strong> cobre despesas essenciais quando falta salário, vêm demissão ou gasto imprevisto. A referência clássica é guardar de <strong>3 a 6 meses</strong> do seu custo de vida — não necessariamente do bruto contratual.</p>
            <h2>Comece pelo líquido real</h2>
            <p>Use o valor que cai na conta na <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a>. Some aluguel, contas, alimentação, transporte e parcelas obrigatórias. Multiplique por 3 (mínimo) ou 6 (mais conservador).</p>
            <h2>Projete o crescimento</h2>
            <p>Depois de definir a meta, use a <a href="/calculadoras/juros-compostos">calculadora de juros compostos</a> com aporte mensal fixo e taxa realista (poupança, CDB líquido, Tesouro Selic). O tempo para chegar na meta depende mais do hábito de aportar do que da rentabilidade no início.</p>
            <h2>Onde guardar</h2>
            <p>Liquidez diária e baixo risco vêm antes de rentabilidade máxima. Leia <a href="/blog/reserva-emergencia-onde-investir">onde investir a reserva</a> e <a href="/blog/como-investir-com-pouco-dinheiro">como investir com pouco dinheiro</a>.</p>
            <p>Integre a reserva ao <a href="/blog/planejamento-financeiro-com-salario">planejamento financeiro com salário</a> — especialmente após aumento ou mudança de emprego. Consulte <a href="/duvidas/como-calcular-salario-liquido">como calcular salário líquido</a> para definir a meta em valor real.</p>
            """),
        Article(
            "acordo-484a-verbas-e-multa-fgts",
            "Acordo trabalhista (484-A): verbas e multa de 20% do FGTS",
            "Entenda o que muda na rescisão por acordo comum: verbas, multa de FGTS e saque do fundo.",
            "rescisao-clt",
            "Trabalhista",
            new DateOnly(2026, 6, 30),
            """
            <p>O <strong>acordo trabalhista</strong> previsto no art. 484-A da CLT permite encerrar o contrato por mútuo consentimento com regras próprias de verbas e FGTS. A multa sobre o saldo do fundo, em geral, é de <strong>20%</strong> — metade da demissão sem justa causa.</p>
            <h2>O que costuma ser negociado</h2>
            <p>As partes definem aviso prévio, verbas rescisórias e condições de homologação. O acordo não é “pedido de demissão” nem “demissão sem justa causa” — tem efeitos próprios sobre seguro-desemprego e saque do FGTS.</p>
            <h2>Multa do FGTS no acordo</h2>
            <p>Na demissão sem justa causa, a multa é de 40% sobre o saldo. No acordo 484-A, a referência legal é <strong>20%</strong>. Compare o líquido total da rescisão antes de assinar — em alguns casos o trabalhador prefere outro tipo de desligamento.</p>
            <h2>Simule antes de assinar</h2>
            <p>Use a <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a> selecionando o motivo <strong>Acordo comum (Art. 484-A)</strong>. Cruze com a <a href="/calculadoras/fgts">calculadora de FGTS</a> e o hub <a href="/desligamento">desligamento CLT</a>.</p>
            <p>Leia também <a href="/blog/multa-fgts-40-ou-20">multa FGTS 40% ou 20%</a>, <a href="/blog/como-calcular-rescisao-clt">como calcular rescisão</a> e a <a href="/duvidas/multa-fgts-40-porcento">FAQ da multa de FGTS</a>.</p>
            """),
        Article(
            "custo-total-clt-para-empregador",
            "Custo total CLT para o empregador: além do salário bruto",
            "FGTS, 13º, férias, encargos e provisões que entram no custo real de um funcionário CLT.",
            "custo-funcionario",
            "Trabalhista",
            new DateOnly(2026, 7, 1),
            """
            <p>Para o trabalhador, o que importa é o <strong>líquido</strong>. Para a empresa, o custo real de um CLT vai muito além do salário contratual — há encargos, provisões e benefícios que muitas propostas PJ ignoram na comparação.</p>
            <h2>Componentes do custo empregador</h2>
            <ul>
            <li><strong>Salário bruto</strong> e adicionais habituais.</li>
            <li><strong>FGTS</strong> de 8% e provisão de multa em cenários de desligamento.</li>
            <li><strong>13º salário</strong> e <strong>férias + 1/3</strong> provisionados ao longo do ano.</li>
            <li><strong>INSS patronal</strong> e outros encargos conforme o regime da empresa.</li>
            <li><strong>Benefícios</strong> (VR/VA, plano, VT) que não descontam 100% do colaborador.</li>
            </ul>
            <h2>Por que isso importa na negociação PJ</h2>
            <p>Quem compara CLT com PJ só pelo bruto subestima o pacote CLT. A <a href="/calculadoras/custo-funcionario">calculadora de custo de funcionário</a> estima esse total; combine com <a href="/calculadoras/pj-vs-clt">PJ vs CLT</a> e o hub <a href="/virar-pj">virar PJ</a>.</p>
            <p>Veja <a href="/blog/pj-ou-clt-qual-melhor">PJ ou CLT: qual compensa?</a>, <a href="/blog/pro-labore-pj-quanto-retirar">pró-labore na PJ</a> e a <a href="/duvidas/pj-ou-clt-qual-compensa">FAQ PJ ou CLT</a>.</p>
            """),
        Article(
            "ferias-coletivas-clt-guia-completo",
            "Férias coletivas na CLT: direitos, calendário e como calcular",
            "Guia completo sobre férias coletivas no verão e no inverno: aviso, pagamento, abono pecuniário e impacto no holerite.",
            "ferias",
            "Trabalhista",
            new DateOnly(2026, 7, 15),
            """
            <p>Entre <strong>dezembro e março</strong>, muitas empresas concedem <strong>férias coletivas</strong> — período em que parte ou todo o quadro para ao mesmo tempo. No <strong>meio do ano</strong>, alguns setores repetem a prática no recesso de julho. Para o trabalhador CLT, isso muda calendário de pagamento, descanso e, em alguns casos, o valor que cai na conta.</p>
            <p>Este guia explica o que a lei prevê, o que costuma aparecer no holerite e como <strong>estimar o valor</strong> antes de assinar o aviso — sem substituir o departamento pessoal ou o sindicato da categoria.</p>

            <h2>O que são férias coletivas</h2>
            <p>Férias coletivas são concedidas <strong>simultaneamente</strong> a todos os empregados de um estabelecimento ou a grupos definidos (setor, unidade, função). A empresa define o período, respeitando regras de aviso prévio e limites legais. O direito ao descanso continua sendo o mesmo do trabalhador CLT: após cada período aquisitivo de 12 meses, você tem direito a férias — a diferença é que a <strong>data de gozo</strong> é escolhida pelo empregador, não individualmente.</p>
            <p>Em épocas de baixa demanda (fim de ano, carnaval, julho em comércio e indústria), férias coletivas ajudam a empresa a reduzir custos fixos sem dispensar funcionários. Para você, o impacto financeiro depende de <strong>quando o pagamento cai</strong>, se há <strong>abono pecuniário</strong> (venda de 1/3) e se o mês terá só férias ou salário + férias proporcionais.</p>

            <h2>Aviso e comunicação ao empregado</h2>
            <p>A empresa deve comunicar as férias coletivas com <strong>antecedência mínima de 15 dias</strong>. O aviso costuma vir por e-mail corporativo, mural, sistema de RH ou termo assinado. Guarde o comprovante: a data de início e fim do período define prazos de pagamento e eventuais questionamentos depois.</p>
            <ul>
            <li><strong>Quem pode ser incluído:</strong> em regra, todos do estabelecimento, salvo exceções negociadas em convenção coletiva.</li>
            <li><strong>Quem aderiu há pouco:</strong> quem não completou o período aquisitivo recebe férias <strong>proporcionais</strong> na mesma lógica das férias individuais.</li>
            <li><strong>Menores de 18 e maiores de 50:</strong> a legislação prevê cuidados específicos; confirme com RH se a categoria permite inclusão no mesmo bloco.</li>
            </ul>

            <h2>Pagamento: quando o dinheiro cai</h2>
            <p>As férias devem ser pagas até <strong>dois dias antes</strong> do início do período de descanso. Na prática, muitas empresas antecipam na folha do mês anterior ou pagam em lote no mesmo dia para todo o time. O holerite de férias costuma mostrar:</p>
            <ol>
            <li><strong>Valor das férias</strong> (30 dias ou proporcional).</li>
            <li><strong>Adicional constitucional de 1/3</strong> sobre o valor das férias.</li>
            <li><strong>Descontos de INSS e IRRF</strong> sobre o total da verba de férias.</li>
            <li><strong>Abono pecuniário</strong>, se você vendeu até 10 dias (1/3 do período).</li>
            </ol>
            <p>Se o mês tiver <strong>salário normal + férias</strong>, o holerite fica mais complexo: parte do salário pode ser proporcional aos dias trabalhados, e a verba de férias entra em rubricas separadas. Isso altera a base de INSS e IRRF do mês — por isso o líquido de janeiro ou julho pode parecer “diferente” mesmo sem mudança de salário contratual.</p>

            <h2>Abono pecuniário nas férias coletivas</h2>
            <p>Você pode converter até <strong>1/3 das férias</strong> em dinheiro (abono pecuniário), reduzindo os dias de descanso. A solicitação deve ser feita com antecedência ao empregador. Nas férias coletivas, o RH costuma perguntar no mesmo formulário do aviso — se não perguntar, procure o setor antes do prazo interno.</p>
            <p>O abono paga o valor dos dias vendidos <strong>mais o adicional de 1/3</strong> sobre essa parcela, com INSS e IRRF. Nem sempre compensa financeiramente vender descanso: simule com e sem abono na <a href="/calculadoras/ferias">calculadora de férias</a> e leia <a href="/blog/ferias-abono-pecuniario-vale-a-pena">abono pecuniário: vale a pena?</a> e a <a href="/duvidas/vender-ferias-abono-pecuniario">FAQ de venda de férias</a>.</p>

            <h2>Férias coletivas e rescisão</h2>
            <p>Se você pedir demissão ou for desligado perto do período coletivo, as verbas mudam. Férias <strong>vencidas</strong> (não gozadas de períodos anteriores) podem ser pagas em dobro na rescisão em alguns casos. Férias <strong>proporcionais</strong> entram no pacote rescisório com o adicional de 1/3.</p>
            <p>Se a empresa concedeu férias coletivas e você não trabalhou o aviso prévio na rescisão, pode haver descontos ou ajustes. Simule o pacote na <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a> e consulte <a href="/duvidas/ferias-proporcionais-como-funciona">férias proporcionais</a> e o hub <a href="/desligamento">desligamento CLT</a>.</p>

            <h2>Exemplo ilustrativo (não é recomendação individual)</h2>
            <p>Salário bruto de <strong>R$ 4.500</strong>, férias coletivas de 30 dias com gozo integral (sem abono): o bruto de férias + 1/3 fica em torno de R$ 6.000 antes dos descontos. INSS e IRRF incidem sobre essa verba; o líquido depende de dependentes e da redução legal de IRRF em 2026. O mesmo salário com venda de 10 dias (abono) reduz o período de descanso e adiciona uma parcela extra tributada — o líquido total do mês pode subir um pouco, mas você troca dias de folga por dinheiro.</p>
            <p>Use a <a href="/calculadoras/ferias">calculadora de férias</a> marcando <strong>vender 1/3 das férias</strong> para comparar os dois cenários com seus números reais.</p>

            <h2>Checklist antes das férias coletivas</h2>
            <ol>
            <li>Confirme datas, aviso de 15 dias e forma de pagamento com o RH.</li>
            <li>Decida sobre abono pecuniário dentro do prazo interno.</li>
            <li>Simule líquido de férias e impacto no mês seguinte (contas fixas não param).</li>
            <li>Verifique se haverá adiantamento de 13º ou descontos de consignado no mesmo holerite.</li>
            <li>Guarde holerite e comprovante de depósito para conferir INSS e IRRF.</li>
            </ol>
            <p>Para o dia a dia após o retorno, combine com <a href="/blog/como-calcular-ferias">como calcular férias</a>, <a href="/blog/planejamento-financeiro-com-salario">planejamento financeiro com salário</a> e a <a href="/calculadoras/salario-liquido">calculadora de salário líquido</a> do mês de retorno.</p>
            """),
        Article(
            "pedir-demissao-ou-aguardar-dispensa",
            "Pedir demissão ou aguardar dispensa: comparativo financeiro",
            "Compare verbas, FGTS, seguro-desemprego e multa antes de decidir entre pedir demissão e esperar demissão sem justa causa.",
            "rescisao-clt",
            "Trabalhista",
            new DateOnly(2026, 7, 22),
            """
            <p>Sair do emprego é uma decisão emocional e <strong>financeira</strong>. Muita gente debate: <strong>peço demissão agora</strong> ou <strong>espero a empresa me dispensar</strong> sem justa causa? A resposta depende de verbas rescisórias, FGTS, seguro-desemprego, multa e do seu prazo para encontrar outro trabalho — não há regra única para todos os casos.</p>
            <p>Este artigo organiza os cenários para você simular com números antes de conversar com RH, sindicato ou profissional habilitado. É conteúdo <strong>educativo</strong>; contrato, convenção coletiva e acordos internos podem mudar o resultado.</p>

            <h2>Panorama rápido dos dois caminhos</h2>
            <table>
            <thead>
            <tr><th>Item</th><th>Pedido de demissão</th><th>Demissão sem justa causa</th></tr>
            </thead>
            <tbody>
            <tr><td>Multa FGTS 40%</td><td>Em regra, não</td><td>Sim (paga pela empresa)</td></tr>
            <tr><td>Saque do saldo FGTS</td><td>Em regra, não</td><td>Sim (conforme regras Caixa)</td></tr>
            <tr><td>Seguro-desemprego</td><td>Em regra, não</td><td>Sim, se cumprir requisitos</td></tr>
            <tr><td>Aviso prévio</td><td>Cumprido ou descontado</td><td>Indenizado ou trabalhado</td></tr>
            <tr><td>Verbas proporcionais</td><td>Saldo, férias, 13º (regras gerais)</td><td>Saldo, férias, 13º (regras gerais)</td></tr>
            </tbody>
            </table>
            <p>A tabela é simplificada. O <strong>acordo 484-A</strong> (art. 484-A da CLT) é um terceiro caminho: multa de 20% sobre FGTS, saque parcial e sem seguro-desemprego na regra usual. Leia <a href="/blog/acordo-484a-verbas-e-multa-fgts">acordo 484-A: verbas e multa</a> e <a href="/blog/multa-fgts-40-ou-20">multa FGTS 40% ou 20%</a>.</p>

            <h2>O que você recebe ao pedir demissão</h2>
            <p>No <strong>pedido de demissão</strong> voluntário, o trabalhador em geral recebe:</p>
            <ul>
            <li><strong>Saldo de salário</strong> dos dias trabalhados no mês da saída.</li>
            <li><strong>Férias vencidas</strong> (se houver) e <strong>férias proporcionais</strong> + adicional de 1/3.</li>
            <li><strong>13º salário proporcional</strong> ao ano da rescisão.</li>
            </ul>
            <p>Não há multa de 40% do FGTS nem seguro-desemprego na regra geral. O saldo do FGTS permanece na conta, em regra <strong>sem saque</strong>, até outra hipótese legal (compra da casa própria, aposentadoria etc.). Se você <strong>não cumprir o aviso prévio</strong> trabalhado, a empresa pode descontar até 30 dias das verbas rescisórias — isso pode reduzir muito o valor final e até gerar saldo negativo em casos extremos.</p>
            <p>Simule na <a href="/calculadoras/rescisao-clt">calculadora de rescisão CLT</a> com motivo <strong>Pedido de demissão</strong> e consulte a <a href="/duvidas/rescisao-pedido-demissao-o-que-recebo">FAQ de pedido de demissão</a>.</p>

            <h2>O que muda na demissão sem justa causa</h2>
            <p>Quando o <strong>empregador dispensa</strong> sem justa causa, entram benefícios que muitas vezes superam meses de salário:</p>
            <ul>
            <li><strong>Multa de 40%</strong> sobre o saldo do FGTS, paga pela empresa ao fundo (você saca conforme regras).</li>
            <li><strong>Seguro-desemprego</strong>, se cumprir carência, tempo de vínculo e demais requisitos — valor e parcelas calculados pelo governo.</li>
            <li><strong>Aviso prévio indenizado</strong> (quando não trabalhado), que integra a base de algumas verbas.</li>
            </ul>
            <p>O pacote pode parecer maior, mas você não controla o <strong>timing</strong>: a empresa pode demorar, aplicar justa causa se houver motivo, ou propor acordo 484-A. Além disso, o desemprego entre um emprego e outro tem custo emocional e de busca — o seguro-desemprego cobre parte do buraco, não necessariamente 100% do líquido anterior.</p>
            <p>Estime FGTS e multa na <a href="/calculadoras/fgts">calculadora de FGTS</a> e parcelas na <a href="/calculadoras/seguro-desemprego">calculadora de seguro-desemprego</a>. Veja também <a href="/blog/seguro-desemprego-quem-tem-direito">seguro-desemprego: quem tem direito</a>.</p>

            <h2>Como comparar com números (passo a passo)</h2>
            <ol>
            <li>Levante <strong>salário bruto</strong>, tempo de empresa, saldo FGTS (app Caixa) e se há férias vencidas.</li>
            <li>Simule <strong>pedido de demissão</strong> na calculadora de rescisão com data de saída e aviso prévio.</li>
            <li>Simule <strong>demissão sem justa causa</strong> com os mesmos dados.</li>
            <li>Some à simulação 2: multa 40% + parcelas estimadas de seguro-desemprego (cenário 3).</li>
            <li>Desconte do cenário 1 o custo de ficar no emprego até a dispensa (meses de desgaste, oportunidade de outro emprego).</li>
            </ol>
            <p>A diferença entre os pacotes líquidos pode ser de <strong>milhares de reais</strong> em contratos longos com FGTS alto — mas só faz sentido “esperar dispensa” se isso for plausível no seu contexto (empresa em reestruturação, histórico de layoffs, negociação com gestor). Forçar demissão indireta ou simular falta grave para ser demitido envolve riscos jurídicos que este site não orienta.</p>

            <h2>Quando pedir demissão pode fazer sentido</h2>
            <ul>
            <li>Você já tem <strong>outra proposta assinada</strong> e precisa começar em data fixa.</li>
            <li>O saldo FGTS é baixo e a multa de 40% não mudaria sua vida financeira.</li>
            <li>A saúde mental ou ambiente tóxico exige saída imediata — coloque isso na balança além do Excel.</li>
            <li>Você vai abrir PJ/MEI com faturamento garantido e comparou no hub <a href="/virar-pj">virar PJ</a>.</li>
            </ul>

            <h2>Quando aguardar dispensa pode compensar</h2>
            <ul>
            <li>Empresa anunciou <strong>programa de demissão</strong> ou há negociação de acordo 484-A com multa parcial.</li>
            <li>FGTS acumulado alto e você precisa do <strong>colchão</strong> da multa + seguro-desemprego entre empregos.</li>
            <li>Tempo curto até aposentadoria ou meta que depende de saque FGTS (avalie regras Caixa).</li>
            <li>RH sinalizou dispensa em poucos meses — vale simular se esperar bate com pedir demissão + novo emprego.</li>
            </ul>

            <h2>Documentos e prazos após a decisão</h2>
            <p>Independentemente do caminho, exija <strong>TRCT</strong>, chave FGTS, guias de seguro-desemprego (se aplicável) e holerite final. Compare linha a linha com a simulação — leia <a href="/blog/rescisao-clt-vs-trct">rescisão CLT vs TRCT</a> e use o <strong>checklist interativo</strong> no hub <a href="/desligamento">desligamento CLT</a>.</p>
            <p>Para o próximo emprego, simule o líquido da proposta na <a href="/calculadoras/proposta-salarial">calculadora de proposta salarial</a> antes de assinar contrato.</p>
            """),
        Article(
            "dissidio-salarial-2026-como-avaliar",
            "Dissídio e reajuste: quanto sobra no líquido",
            "Entenda como avaliar dissídio salarial em 2026 pelo valor líquido, considerando INSS, IRRF e descontos do holerite.",
            "proposta-salarial",
            "Trabalhista",
            new DateOnly(2026, 8, 6),
            """
            <p>Quando chega a conversa de <strong>dissidio salarial</strong>, muita gente olha apenas para o percentual anunciado: 4%, 5%, 8% de reajuste. O problema e que o salario que melhora o orcamento nao e o bruto do comunicado, e sim o <strong>liquido que cai na conta</strong> depois de INSS, IRRF e descontos do holerite.</p>
            <p>Este guia mostra como transformar o reajuste coletivo em uma comparacao pratica: quanto voce ganha antes, quanto passaria a receber, quais impostos mudam e que perguntas levar ao RH. E uma analise educativa para planejar o bolso, sem substituir sindicato, convencao coletiva ou orientacao trabalhista.</p>
            <h2>O que e dissidio salarial na pratica</h2>
            <p>No uso comum, dissidio e o reajuste anual negociado entre sindicato de trabalhadores e sindicato patronal, ou definido em acordo ou convencao coletiva. Ele pode corrigir inflacao, repor perdas e, em alguns casos, trazer ganho real. O valor pode aparecer como percentual sobre o salario base, piso da categoria, abono, retroativo ou combinacao desses itens.</p>
            <p>Por isso, duas pessoas da mesma empresa podem sentir impactos diferentes. Quem estava proximo do piso pode ter reajuste maior. Quem recebe acima do piso pode ter apenas percentual geral. Quem teve promocao recente pode ter compensacao interna. Antes de comemorar ou reclamar, confirme se o reajuste incide sobre o salario base, sobre adicionais habituais ou apenas sobre determinada faixa.</p>
            <h2>Percentual no bruto nao e percentual no bolso</h2>
            <p>INSS e IRRF sao progressivos. Quando o bruto sobe, uma parte do aumento pode entrar em faixa maior de desconto, e o ganho liquido fica menor que o percentual anunciado. Tambem ha descontos fixos ou variaveis: vale-transporte, VR/VA, plano de saude, pensao, sindicato, consignado e outros itens que aparecem no holerite.</p>
            <p>Exemplo ilustrativo: um salario bruto de R$ 4.500 com reajuste de 6% vai para R$ 4.770. O aumento bruto e R$ 270. Mas o liquido nao cresce exatamente R$ 270, porque INSS e possivel IRRF usam a nova base. Se o trabalhador tem dependentes, plano de saude ou vale-transporte, o resultado final muda de novo. A decisao correta e comparar cenarios completos, nao apenas multiplicar o bruto.</p>
            <h2>Como simular o reajuste em 5 passos</h2>
            <ol>
            <li>Pegue o salario bruto atual no holerite, nao no extrato bancario.</li>
            <li>Aplique o percentual do dissidio ou informe o novo bruto comunicado pelo RH.</li>
            <li>Repita dependentes, vale-transporte, VR/VA, plano, pensao e outros descontos nos dois cenarios.</li>
            <li>Compare ganho liquido mensal, ganho anual e percentual real no bolso.</li>
            <li>Se houver retroativo, simule o mes normal separadamente do pagamento acumulado.</li>
            </ol>
            <p>A forma mais simples e usar a <a href="/calculadoras/proposta-salarial">calculadora de proposta salarial</a>. Coloque o bruto atual de um lado, o bruto reajustado do outro e mantenha os mesmos descontos. Para conferir um holerite depois que o reajuste entrar na folha, use tambem a <a href="/conferir-holerite">ferramenta de conferir holerite</a>.</p>
            <h2>Retroativo pode distorcer o holerite do mes</h2>
            <p>E comum a convencao ser assinada depois da data-base. Nesse caso, a empresa pode pagar diferencas retroativas em uma folha posterior. O holerite do mes com retroativo pode parecer alto e ter descontos maiores, porque junta salario normal, diferenca de meses anteriores e, as vezes, reflexos em ferias, 13o ou horas extras.</p>
            <p>Para nao confundir renda recorrente com valor pontual, separe mentalmente: <strong>novo salario mensal</strong> e <strong>ajuste retroativo</strong>. O primeiro melhora seu orcamento daqui para frente. O segundo e uma parcela eventual; pode ajudar a quitar divida ou formar reserva, mas nao deve virar despesa fixa.</p>
            <h2>O que perguntar ao RH ou ao sindicato</h2>
            <ul>
            <li>Qual e a data-base da categoria e a partir de quando o reajuste vale?</li>
            <li>O percentual incide sobre salario base, piso, adicionais ou total da remuneracao?</li>
            <li>Havera retroativo? Em qual folha e com quais rubricas?</li>
            <li>O vale-refeicao, vale-alimentacao ou plano de saude tambem mudam?</li>
            <li>Existe abono ou parcela unica fora do salario mensal?</li>
            </ul>
            <p>Guarde o comunicado, a convencao coletiva e o primeiro holerite com reajuste. Se o valor liquido ficar muito diferente do esperado, confira INSS e IRRF em <a href="/como-calculamos">Como calculamos</a> e veja a <a href="/duvidas/proposta-salarial-como-negociar">FAQ de proposta salarial</a>.</p>
            <h2>Como usar o reajuste para negociar melhor</h2>
            <p>Dissidio repoe uma regra coletiva; promocao e merito sao conversas diferentes. Se seu ganho real ficou pequeno, leve numeros objetivos: liquido atual, liquido apos dissidio, impacto anual e comparacao com responsabilidades novas. O hub <a href="/negociar-salario">Negociar salario</a> organiza calculadoras e artigos para montar essa conversa sem depender de achismo.</p>
            <p>Depois de simular, salve o resultado ou gere PDF quando a calculadora oferecer essa opcao. Isso ajuda a conversar com RH, gestor ou sindicato com uma base clara: qual e o ganho real no bolso e qual e apenas aumento no papel.</p>
            <h2>Reflexos em 13o, ferias e horas extras</h2>
            <p>O reajuste tambem pode afetar verbas calculadas sobre salario: 13o, ferias, adicional de 1/3, horas extras e medias habituais. Quando o aumento entra no meio do ano, alguns valores futuros usam o novo salario e outros podem considerar medias ou regras da convencao. Se estiver planejando ferias, rescisao ou troca de emprego, simule novamente depois que o primeiro holerite reajustado sair.</p>
            <p>Essa revisao evita um erro comum: contar apenas com o ganho mensal e esquecer que parte do aumento aparece ao longo do ano em verbas futuras. Para o trabalhador, isso melhora previsibilidade; para a conversa com RH, mostra que voce esta avaliando o pacote completo, nao so o valor de agosto.</p>
            """),
        Article(
            "vale-refeicao-desconto-holerite",
            "VR e VA: o que desconta e o que não desconta no holerite",
            "Entenda como vale-refeição e vale-alimentação aparecem no holerite, quando há coparticipação e como simular o salário líquido.",
            "salario-liquido",
            "Trabalhista",
            new DateOnly(2026, 8, 20),
            """
            <p>Vale-refeicao (VR) e vale-alimentacao (VA) parecem simples: a empresa deposita um valor no cartao e o trabalhador usa para comer. Mas, no holerite, esses beneficios podem aparecer com desconto, coparticipacao, integracao parcial em acordos especificos ou mudanca por convencao coletiva.</p>
            <p>Para entender o <strong>salario liquido real</strong>, voce precisa separar o que e dinheiro que cai na conta, o que e beneficio em cartao e o que sai como desconto em folha. Este artigo explica como conferir VR/VA sem confundir com vale-transporte, plano de saude ou outros descontos autorizados.</p>
            <h2>VR, VA e salario liquido: cada coisa em seu lugar</h2>
            <p>O salario liquido e o valor depositado em conta apos descontos obrigatorios e autorizados. VR e VA normalmente nao entram como dinheiro livre na conta bancaria; entram como beneficio para refeicao ou alimentacao. Mesmo assim, podem afetar o liquido quando a empresa cobra uma parte do custo do empregado.</p>
            <p>Exemplo: se a empresa concede R$ 700 de VA e desconta R$ 70 de coparticipacao, o cartao recebe R$ 700, mas o salario liquido bancario cai R$ 70. Para o orcamento mensal, voce deve considerar os dois lados: mais poder de compra em alimentacao e menos dinheiro livre na conta.</p>
            <h2>Quando pode haver desconto no holerite</h2>
            <p>O desconto de VR/VA depende de politica interna, acordo coletivo, adesao a programas de alimentacao e regras da categoria. Em muitos casos, a empresa subsidia quase tudo e desconta um valor simbolico. Em outros, ha percentual fixo, valor por dia util ou coparticipacao diferente por faixa salarial.</p>
            <ul>
            <li><strong>Valor fixo mensal:</strong> exemplo de R$ 50 ou R$ 100 descontados todo mes.</li>
            <li><strong>Valor por dia util:</strong> desconto muda conforme dias trabalhados, ferias, afastamentos e feriados.</li>
            <li><strong>Percentual do beneficio:</strong> a empresa paga uma parte e desconta outra.</li>
            <li><strong>Sem desconto:</strong> algumas empresas oferecem o beneficio integralmente subsidiado.</li>
            </ul>
            <p>Se o holerite mostra apenas "outros descontos" sem detalhar, peca a rubrica ao RH. O ideal e que VR, VA, VT, plano e consignado aparecam separados para voce conferir cada linha.</p>
            <h2>VR/VA nao e igual a vale-transporte</h2>
            <p>Vale-transporte tem regra conhecida de desconto limitado a ate 6% do salario bruto quando o trabalhador opta pelo beneficio. VR e VA nao seguem automaticamente esse mesmo limite. O tratamento depende da natureza do beneficio, do acordo e da politica da empresa.</p>
            <p>Por isso, nao use a regra do VT para estimar VR/VA. Na <a href="/calculadoras/salario-liquido">calculadora de salario liquido</a>, informe vale-transporte em campo separado e coloque VR/VA no campo proprio de desconto de refeicao/alimentacao. Isso evita somar tudo como se fosse uma regra unica.</p>
            <h2>Como simular corretamente no Meu Valor Liquido</h2>
            <ol>
            <li>Abra seu holerite mais recente e localize a rubrica de VR, VA ou alimentacao.</li>
            <li>Informe o salario bruto e dependentes na calculadora de salario liquido.</li>
            <li>Preencha vale-transporte apenas se houver desconto de VT.</li>
            <li>Coloque a coparticipacao de VR/VA no campo "Vale-refeicao/alimentacao".</li>
            <li>Deixe o valor total carregado no cartao fora do liquido bancario, a menos que a empresa pague em dinheiro.</li>
            </ol>
            <p>Depois, compare o resultado com o holerite oficial. Se INSS, IRRF ou liquido divergirem muito, use a pagina <a href="/conferir-holerite">conferir holerite</a> para ver qual linha merece revisao.</p>
            <h2>Impacto em ferias, afastamentos e home office</h2>
            <p>VR e VA podem mudar em meses com ferias, afastamento, licenca ou home office. Algumas empresas mantem o beneficio integral; outras reduzem conforme dias uteis trabalhados. O desconto em folha tambem pode acompanhar essa mudanca. Por isso, nao compare um holerite de ferias com um mes normal sem ajustar os dias.</p>
            <p>Se voce recebeu aumento, mudou de jornada ou entrou em escala hibrida, confira se o beneficio continuou coerente. Um desconto pequeno recorrente pode parecer irrelevante, mas em 12 meses pesa no orcamento tanto quanto uma parcela fixa.</p>
            <h2>Checklist para conversar com o RH</h2>
            <ul>
            <li>Qual e o valor mensal carregado no cartao de VR/VA?</li>
            <li>Qual parte e paga pela empresa e qual parte e descontada do empregado?</li>
            <li>O desconto muda por falta, ferias, home office ou afastamento?</li>
            <li>A convencao coletiva define piso de beneficio ou coparticipacao?</li>
            <li>O holerite separa VR, VA, VT e plano de saude em rubricas diferentes?</li>
            </ul>
            <p>Para entender outros descontos, leia <a href="/blog/como-conferir-holerite">como conferir holerite</a>, consulte a <a href="/duvidas/diferenca-salario-bruto-e-liquido">FAQ sobre bruto e liquido</a> e veja a metodologia em <a href="/como-calculamos">Como calculamos</a>.</p>
            <h2>Como diferenciar beneficio bom de desconto escondido</h2>
            <p>Um beneficio bom e aquele que aumenta seu poder de compra sem criar surpresa no holerite. Se a empresa oferece R$ 900 de VA e desconta R$ 20, o impacto costuma ser positivo. Se oferece R$ 500 e desconta R$ 250, a vantagem real depende do seu gasto com alimentacao, da rede aceita e da possibilidade de usar o valor todo no mes.</p>
            <p>Compare tres numeros: valor carregado no cartao, desconto em folha e gasto real que voce teria fora do beneficio. Se voce ja gastaria R$ 800 em mercado e recebe R$ 700 de VA com desconto de R$ 70, o ganho pratico e diferente de alguem que mora com a familia e quase nao usa o cartao. Beneficio tambem tem qualidade: cobertura de estabelecimentos, prazo de credito, acumulacao de saldo e regras durante ferias importam.</p>
            <p>Na negociacao salarial, nao some VR/VA ao salario bruto como se fosse dinheiro livre. Use como parte do pacote total, mas mantenha separado do liquido bancario. Essa separacao deixa sua comparacao mais honesta quando voce avalia proposta nova, dissidio ou troca entre CLT e PJ.</p>
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
        new(
            slug,
            title,
            summary,
            EnrichContent(slug, content.Trim(), calculatorSlug, category),
            publishedAt,
            category,
            calculatorSlug);

    private static string EnrichContent(string slug, string content, string calculatorSlug, string category)
    {
        content = AppendPracticalSection(slug, content);
        if (content.Contains("id=\"como-validamos\"", StringComparison.Ordinal))
        {
            return content;
        }

        var categoryNote = category switch
        {
            "Fiscal" => "As tabelas e regras fiscais seguem parâmetros de 2026 documentados em <a href=\"/como-calculamos\">Como calculamos</a>.",
            "Financeiro" => "As projeções financeiras usam fórmulas transparentes descritas em <a href=\"/como-calculamos\">Como calculamos</a>.",
            _ => "Os cenários trabalhistas são calibrados com benchmarks de 2026 em <a href=\"/como-calculamos\">Como calculamos</a>."
        };

        return content + $"""

            <h2 id="como-validamos">Como validamos esta estimativa</h2>
            <p>{categoryNote}</p>
            <p>Simule seu caso na <a href="/calculadoras/{calculatorSlug}">calculadora relacionada</a> e consulte a <a href="/duvidas">central de ajuda</a> para perguntas frequentes sobre o tema.</p>
            <p><strong>Estimativa educativa:</strong> não substitui holerite oficial, contrato assinado, extrato bancário ou orientação de contador, advogado ou RH.</p>
            """;
    }

    private static string AppendPracticalSection(string slug, string content)
    {
        if (content.Contains("id=\"dica-pratica\"", StringComparison.Ordinal))
        {
            return content;
        }

        var tip = slug switch
        {
            "o-que-e-salario-liquido" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Antes de negociar salário, simule o líquido com os mesmos descontos do holerite atual — VT, VR/VA, plano e pensão — para comparar propostas de forma justa.</p>",
            "como-avaliar-proposta-salarial" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Peça a proposta por escrito com salário bruto, benefícios e descontos previstos. Depois replique os mesmos parâmetros na calculadora de proposta para ver o ganho real no bolso.</p>",
            "como-conferir-holerite" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Guarde holerites dos últimos três meses: médias de horas extras e comissões explicam boa parte das divergências com a simulação.</p>",
            "como-calcular-ferias" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Combine a simulação de férias com o salário líquido do mês seguinte — o orçamento muda quando o pagamento cai fora do ciclo habitual.</p>",
            "como-calcular-rescisao-clt" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Informe na calculadora se houve adiantamento de 13º ou férias e a média de horas extras dos últimos meses; isso aproxima a simulação do TRCT.</p>",
            "rescisao-clt-vs-trct" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Assine o TRCT somente depois de comparar linha a linha com a simulação e pedir esclarecimento sobre rubricas que não reconhece.</p>",
            "como-calcular-inss" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Se o desconto de INSS no holerite parece alto, verifique se o mês inclui férias, 13º ou PLR — a base de contribuição muda conforme a rubrica.</p>",
            "entenda-o-irrf" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Cadastre dependentes e pensão no RH antes de questionar o IRRF; deduções não informadas aumentam a retenção indevida.</p>",
            "pj-ou-clt-qual-melhor" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Na comparação PJ vs CLT, inclua custos que a PJ paga por conta própria: contador, INSS, plano de saúde e férias não remuneradas.</p>",
            "guia-decimo-terceiro" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Reserve parte da segunda parcela do 13º para IR e despesas de janeiro — o imposto costuma surpreender quem planeja só pelo valor bruto.</p>",
            "juros-compostos-guia" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Simule a mesma meta com taxas conservadora e realista; a diferença mostra o quanto a rentabilidade impacta seu plano de longo prazo.</p>",
            "hora-extra-como-calcular" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Confira no holerite se HE e DSR sobre horas extras aparecem em rubricas separadas — isso altera a média usada em férias e 13º.</p>",
            "financiamento-como-calcular-parcelas" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Compare pelo CET e pelo total pago, não só pela parcela inicial — prazos longos podem parecer baratos mês a mês e caros no acumulado.</p>",
            "tabela-inss-2026-guia" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Salários perto do teto previdenciário mudam pouco o desconto de INSS ao receber aumento — entenda onde sua faixa estabiliza.</p>",
            "tabela-irrf-2026-guia" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Com a isenção para bases menores em 2026, vale revisar o holerite após reajuste — um aumento pequeno pode não aumentar o IRRF na mesma proporção.</p>",
            "desconto-vale-transporte" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Multiplique o custo diário de ida e volta por 22 dias úteis e compare com 6% do bruto; só então decida se o VT compensa.</p>",
            "fgts-guia-completo" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Antes de aderir ao saque-aniversário, entenda que você abre mão da multa de 40% em uma eventual demissão sem justa causa.</p>",
            "planejamento-financeiro-com-salario" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Atualize o orçamento sempre que o holerite mudar — promoção no bruto nem sempre aumenta o líquido na mesma proporção por causa do imposto progressivo.</p>",
            "mei-faturamento-e-das" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Acompanhe o faturamento acumulado mês a mês no simulador MEI; ultrapassar o teto sem planejamento gera desenquadramento e custos de migração.</p>",
            "cdb-ou-tesouro-direto-investimentos" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Compare CDB e Tesouro pelo valor líquido após IR no prazo em que você pretende resgatar — não só pela taxa bruta do anúncio.</p>",
            "reserva-emergencia-onde-investir" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Guarde a reserva em instituição diferente do banco do dia a dia; isso reduz a tentação de gastar em compras por impulso.</p>",
            "como-investir-com-pouco-dinheiro" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Automatize um aporte fixo no dia seguinte ao pagamento — mesmo valor pequeno cria hábito antes de buscar rentabilidade máxima.</p>",
            "quanto-cobrar-servicos-pj" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Inclua na planilha de preço pelo menos 25% de horas não faturáveis (prospecção, reuniões, nota fiscal) para não trabalhar de graça.</p>",
            "mei-nota-fiscal-quando-emitir" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Emita a NFS-e no mesmo mês do serviço prestado; atrasar pode complicar o DAS e a comprovação perante o cliente PJ.</p>",
            "simples-nacional-pj-guia-iniciantes" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Peça ao contador a simulação da alíquota efetiva nos anexos III e V antes de fechar o primeiro contrato como ME no Simples.</p>",
            "irrf-2026-reducao-imposto" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Depois de um reajuste salarial, rode de novo a calculadora de IRRF — a isenção de 2026 pode fazer o imposto cair mais do que em anos anteriores.</p>",
            "seguro-desemprego-quem-tem-direito" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Separe a solicitação do seguro-desemprego da homologação da rescisão; prazos e documentos são diferentes e perder a data pode custar parcelas.</p>",
            "multa-fgts-40-ou-20" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Antes de aceitar acordo 484-A, compare o líquido da rescisão com multa de 20% contra o cenário de demissão sem justa causa com 40% — nem sempre o acordo compensa.</p>",
            "aumento-salario-quanto-sobra-liquido" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Peça ao RH o valor líquido estimado ou use nossa calculadora e compartilhe o PDF na conversa — negociar só pelo bruto esconde metade da história.</p>",
            "quanto-preciso-ganhar-para-receber-x" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Arredonde o bruto encontrado para cima na negociação — pequenas diferenças de desconto no holerite real podem comer a margem.</p>",
            "mei-desenquadramento-o-que-fazer" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Se está perto do teto em outubro ou novembro, modele o faturamento dos meses restantes no simulador antes de fechar contratos grandes.</p>",
            "pro-labore-pj-quanto-retirar" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Teste dois ou três percentuais de pró-labore na calculadora PJ vs CLT — o líquido pessoal nem sempre cresce quando o pró-labore sobe.</p>",
            "decimo-terceiro-primeira-segunda-parcela" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Programe transferência automática da 1ª parcela para uma conta separada; assim a 2ª parcela não vira único “13º” no orçamento de dezembro.</p>",
            "ferias-abono-pecuniario-vale-a-pena" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Simule férias com e sem abono no mesmo salário — a diferença líquida costuma ser menor que 10 dias de descanso vendidos parecem valer.</p>",
            "emprestimo-consignado-desconto-holerite" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Antes de um novo consignado, some as parcelas ativas e informe o total em “outros descontos” na calculadora de líquido.</p>",
            "reserva-emergencia-quanto-guardar" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Defina a meta em meses de despesas essenciais, não de salário bruto — o líquido é o que paga as contas.</p>",
            "acordo-484a-verbas-e-multa-fgts" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Peça simulação por escrito dos dois cenários — acordo 484-A com multa de 20% e demissão sem justa causa com 40% — antes de assinar qualquer termo.</p>",
            "custo-total-clt-para-empregador" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Na conversa PJ, some ao seu custo pessoal o que a CLT te dava de benefício líquido — VR, plano e estabilidade de caixa entre empregos.</p>",
            "ferias-coletivas-clt-guia-completo" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Programe transferência automática na volta das férias coletivas — o primeiro holerite “cheio” após o recesso costuma coincidir com contas atrasadas do mês de folga.</p>",
            "pedir-demissao-ou-aguardar-dispensa" =>
                "<h2 id=\"dica-pratica\">Dica prática</h2><p>Exporte duas simulações da calculadora de rescisão (pedido vs sem justa causa) em PDF e arquive com o saldo FGTS do app — facilita comparar com o TRCT depois.</p>",
            "dissidio-salarial-2026-como-avaliar" =>
                "<h2 id=\"dica-pratica\">Dica pratica</h2><p>Antes de ajustar seu orcamento, simule o salario atual e o salario reajustado lado a lado; trate retroativo como parcela pontual, nao como renda fixa mensal.</p>",
            "vale-refeicao-desconto-holerite" =>
                "<h2 id=\"dica-pratica\">Dica pratica</h2><p>No holerite, separe desconto de VR/VA do valor carregado no cartao: um reduz o liquido bancario, o outro aumenta seu orcamento de alimentacao.</p>",
            _ => string.Empty
        };

        if (string.IsNullOrEmpty(tip))
        {
            return content;
        }

        var insertAt = content.LastIndexOf("<h2 id=\"como-validamos\">", StringComparison.Ordinal);
        if (insertAt < 0)
        {
            return content + tip;
        }

        return content.Insert(insertAt, tip);
    }
}
