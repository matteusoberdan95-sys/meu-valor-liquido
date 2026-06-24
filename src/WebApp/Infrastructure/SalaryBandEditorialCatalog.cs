namespace MeuValorLiquido.WebApp.Infrastructure;

/// <summary>Texto editorial único por faixa salarial — evita thin content nas páginas programáticas.</summary>
public static class SalaryBandEditorialCatalog
{
    private static readonly Dictionary<int, EditorialAngle> Angles = new()
    {
        [1621] = new(
            "Salário mínimo nacional",
            "Faixa típica de contratos CLT enquadrados no piso de {year}, com desconto previdenciário concentrado na primeira faixa do INSS.",
            "Planeje o orçamento assumindo que o líquido fica próximo de {netPercent:0.#}% do bruto — pequenas variações de VT ou adiantamento mudam o bolso."),
        [1800] = new(
            "Logo acima do mínimo",
            "Comum em estágios remunerados, jovens aprendizes ou funções de entrada com adicional mínimo sobre o piso.",
            "Negocie benefícios não salariais (VR, plano) — eles não entram no bruto, mas alteram o custo real do mês."),
        [2000] = new(
            "Primeiro degrau acima do piso",
            "Perfil frequente em varejo, logística leve e serviços com escala 6×1 em capitais regionais.",
            "Se há horas extras recorrentes, o holerite real pode superar esta página — use a calculadora completa com médias."),
        [2200] = new(
            "Auxiliar administrativo e operacional",
            "Valor recorrente em vagas de auxiliar de escritório, recepcionista e operador de caixa com alguma experiência.",
            "Confira se o convênio médico é coparticipação ou mensalidade fixa; o desconto em folha reduz o líquido além do INSS/IRRF."),
        [2400] = new(
            "Operacional urbano em expansão",
            "Comum em atendimento ao cliente, estoquista e motorista de entregas leves com contrato CLT integral.",
            "Adicional noturno e HE podem elevar o bruto real — esta página considera apenas o salário base informado."),
        [2500] = new(
            "CLT de entrada consolidado",
            "Bate com analistas júnior, técnicos de nível médio e cargos operacionais com 1–3 anos de casa.",
            "Compare esta faixa com a proposta de {nearbyLower} ou {nearbyHigher} se estiver avaliando mudança de emprego."),
        [2600] = new(
            "Suporte e backoffice",
            "Típico de assistentes de compras, telemarketing especializado e técnicos de manutenção predial.",
            "Se tem filho dependente no IR, abra a variante com 1 ou 2 dependentes — a dedução pode zerar ou reduzir o IRRF."),
        [2800] = new(
            "Serviços e indústria leve",
            "Aparece em funções de suporte TI nível 1, assistente comercial e produção com adicional de insalubridade baixo.",
            "Pensão alimentícia ou empréstimo consignado podem retirar 10–30% do líquido — informe na calculadora interativa."),
        [3000] = new(
            "Marco psicológico dos R$ 3 mil",
            "Um dos valores mais buscados: equilibra mercado formal de capitais e interior para cargos plenos iniciais.",
            "Com {net} líquidos estimados, revise se o custo de deslocamento (VT) ainda cabe no orçamento."),
        [3200] = new(
            "Pleno inicial em cidades médias",
            "Típico de coordenadores de primeira linha, vendedores internos e técnicos com certificação profissional.",
            "Se recebe comissão variável, a rescisão e o 13º usam média — simule com o campo de média na rescisão CLT."),
        [3300] = new(
            "Analista júnior em crescimento",
            "Aparece em marketing, RH generalista e TI de suporte com alguns anos de experiência formal.",
            "Compare o líquido com e sem dependentes nesta mesma faixa — a diferença aparece principalmente no IRRF."),
        [3500] = new(
            "Faixa de analista pleno",
            "Compatível com analistas, desenvolvedores júnior e especialistas administrativos em empresas de médio porte.",
            "Avalie propostas pelo líquido, não só pelo percentual de aumento no bruto — impostos são progressivos."),
        [3600] = new(
            "Profissional pleno estável",
            "Comum em design, contabilidade de departamento e engenharia de aplicação em indústrias regionais.",
            "Vale-transporte acima de 6% do bruto exige autorização — confira se o desconto está dentro do limite legal."),
        [3800] = new(
            "Especialista técnico",
            "Comum em engenharia júnior, enfermagem plantão parcial e funções fiscal/contábil de apoio.",
            "Nesta faixa o IRRF ainda pode ser zero pela redução legal de {year}, dependendo da base após INSS."),
        [4000] = new(
            "Referência de mercado formal",
            "Valor âncora para comparações salariais; muitas vagas híbridas e tech júnior convergem aqui.",
            "Use a calculadora de proposta salarial para ver o ganho real no bolso entre {nearbyLower} e {nearbyHigher}."),
        [4200] = new(
            "Pleno em expansão",
            "Frequente em customer success, QA pleno e gestão de equipes pequenas no varejo ou serviços.",
            "FGTS e 13º proporcional crescem com o bruto — lembre disso ao comparar com PJ na calculadora PJ×CLT."),
        [4400] = new(
            "Especialista operacional",
            "Perfis de comprador pleno, analista fiscal e desenvolvedor com stack consolidada em empresas médias.",
            "Dedução por dependente ({dependentDeduction}) pode mudar a faixa efetiva de IRRF — teste as variantes desta página."),
        [4500] = new(
            "Meio da escala CLT urbana",
            "Cargos de supervisão, dev front-end pleno e profissionais de marketing digital experientes.",
            "Acima de R$ 4.500 o impacto percentual do INSS diminui, mas o IRRF pode voltar a aparecer conforme a base."),
        [4600] = new(
            "Coordenação técnica inicial",
            "Líderes de squad pequeno, enfermeiros CLT sem plantão fixo e consultores de implantação.",
            "Se está negociando aumento para {nearbyHigher}, simule a proposta salarial com os mesmos dependentes."),
        [4800] = new(
            "Coordenação e senioridade inicial",
            "Gestores de turno, devs com stack consolidada e profissionais de saúde em regime CLT parcial.",
            "Revise dependentes no IR: cada dependente reduz a base tributável em {dependentDeduction} em {year}."),
        [5000] = new(
            "Marco dos R$ 5 mil brutos",
            "Um dos maiores volumes de busca: líquido costuma ficar entre 82% e 88% do bruto sem outros descontos.",
            "Se o holerite mostra IRRF isento, confira a Lei 15.270/2025 — nossa estimativa já considera a redução."),
        [5200] = new(
            "Senioridade em ascensão",
            "Perfis de product owner júnior, engenheiros de campo e consultores internos de RH.",
            "Simule férias e 13º a partir deste bruto — verbas proporcionais usam o mesmo salário de referência."),
        [5500] = new(
            "Especialista consolidado",
            "Típico de tech pleno/sênior em cidades fora do eixo SP-RJ e cargos de confiança sem gratificação separada.",
            "Negociações de home office podem incluir auxílio internet — não confunda com aumento de salário base."),
        [5800] = new(
            "Tech e produto em maturação",
            "Engenheiros de software pleno+, product designers sênior e analistas de dados com stack definida.",
            "Nesta faixa, dependentes no IR costumam reduzir retenção — compare as três variantes indexadas desta faixa."),
        [6000] = new(
            "Faixa alta com IRRF relevante",
            "A partir daqui o IRRF tende a pesar mais; comissões e HE elevam a base real de imposto.",
            "Compare o líquido com a faixa de {nearbyLower} para ver se um aumento menor no bruto já atende sua meta."),
        [6200] = new(
            "Sênior técnico em escala",
            "Staff engineers iniciais, gerentes de conta enterprise e especialistas de compliance em bancos.",
            "PLR e bônus não entram aqui — apenas salário mensal fixo CLT para comparar holerite base."),
        [6500] = new(
            "Gestão e tech em crescimento",
            "Coordenadores de produto, engenheiros sênior iniciais e especialistas financeiros em indústria.",
            "Plano de saúde empresarial pode descontar 200–400 reais — informe na calculadora completa."),
        [6800] = new(
            "Liderança funcional",
            "Gerentes de área compacta, arquitetos de solução e médicos CLT em clínicas de médio porte.",
            "Acima de R$ 6.500 o IRRF pesa mais — valide dependentes cadastrados no eSocial antes de contestar retenção."),
        [7000] = new(
            "Sênior em mercado competitivo",
            "Referência para devs sênior, gerentes de projeto e profissionais de dados em empresas estruturadas.",
            "A multa de 40% do FGTS em demissão sem justa causa cresce com o tempo de casa — simule na rescisão CLT."),
        [7200] = new(
            "Especialista premium regional",
            "Tech lead, gerente comercial sênior e profissionais de saúde com agenda estável em capitais.",
            "Compare {nearbyLower} e {nearbyHigher} se estiver avaliando proposta interna de promoção."),
        [7500] = new(
            "Alta responsabilidade operacional",
            "Gerentes regionais, arquitetos de software e especialistas regulatórios costumam orbitar este valor.",
            "Stock options e PLR não entram nesta página — apenas salário fixo mensal CLT."),
        [8000] = new(
            "Executivo de base e tech lead",
            "Líderes técnicos, gerentes de área e profissionais de saúde especializados em hospitais privados.",
            "Nesta faixa, pequenas diferenças no bruto geram saltos maiores no IRRF — valide o holerite linha a linha."),
        [8500] = new(
            "Especialista sênior premium",
            "Perfis escassos em engenharia, compliance e produto digital em empresas de capital aberto.",
            "Se considera PJ, compare líquido e benefícios na calculadora PJ×CLT antes de pedir demissão."),
        [9000] = new(
            "Topo da média CLT urbana",
            "Gerentes médios, tech lead consolidados e consultores internos sênior em multinacionais.",
            "INSS atinge o teto progressivo — acima disso, aumentos de bruto vão mais para o líquido até o IRRF limitar."),
        [9500] = new(
            "Alta especialização formal",
            "Staff principal, diretores associados e profissionais de saúde de alta complexidade em regime CLT.",
            "Financiamento imobiliário usa renda comprovada — exporte o PDF da calculadora com seus dependentes reais."),
        [10000] = new(
            "Marco dos R$ 10 mil",
            "Faixa de referência para profissionais seniores; buscas frequentes para planejamento de crédito imobiliário.",
            "Bancos usam líquido comprovado — exporte o PDF da calculadora completa para simulações de financiamento."),
        [11000] = new(
            "Alta liderança técnica",
            "Heads de equipe, especialistas em cloud e médicos CLT sem plantão extraordinário recorrente.",
            "Revise tabela de IRRF {year}: deduções e faixas mudam o líquido de forma não linear."),
        [12000] = new(
            "Gestão e especialidade rara",
            "Diretores de primeira linha, staff engineers e profissionais jurídicos sênior em departamentos internos.",
            "Benefícios flexíveis (cartão multibenefícios) podem ser tributáveis — confirme com o RH."),
        [13000] = new(
            "Faixa executiva inicial",
            "Gerentes gerais de unidade e especialistas internacionais com contrato Brasil.",
            "Pensão por percentual sobre o bruto impacta fortemente o bolso — use o campo de pensão na calculadora."),
        [14000] = new(
            "Diretoria funcional",
            "Heads de departamento, principal engineers e consultores sênior em projetos longos.",
            "Pacotes com stock options exigem simulação separada — esta página cobre salário fixo mensal."),
        [15000] = new(
            "Executivo e expert sênior",
            "Diretores funcionais e especialistas com mais de uma década de mercado formal.",
            "Nesta altura, vale simular proposta com holerite completo (plano, VR, VT) antes de aceitar aumento."),
        [16000] = new(
            "Executivo de linha",
            "Diretores regionais e tech directors em empresas de capital nacional ou scale-ups maduras.",
            "IRRF e INSS no teto: foque em variável anual e benefícios flexíveis além do bruto fixo."),
        [17000] = new(
            "Alta liderança corporativa",
            "VPs de primeira linha e especialistas globais com contrato localizado no Brasil.",
            "Compare cenários PJ×CLT antes de migrar regime — benefícios CLT pesam nesta faixa salarial."),
        [18000] = new(
            "Alta gerência corporativa",
            "Gestores de VP em empresas nacionais e tech staff principal em scale-ups maduras.",
            "IRRF e INSS no teto: o percentual líquido/bruto estabiliza — foque em benefícios e bônus anuais."),
        [19000] = new(
            "Pacote executivo elevado",
            "C-level de empresas médias e staff principal em corporações com política salarial rígida.",
            "Use a calculadora interativa para holerite completo — VT, plano e pensão alteram o líquido final."),
        [20000] = new(
            "Teto das faixas indexadas",
            "Referência para comparação de pacotes executivos; poucos contratos CLT fixos chegam aqui sem variáveis.",
            "Para valores acima, use a calculadora interativa — o IRRF progressivo exige simulação caso a caso.")
    };

    public static bool TryGetAngle(int gross, out EditorialAngle angle) =>
        Angles.TryGetValue(gross, out angle!);

    public static string BuildEditorialHtml(
        int gross,
        NetSalaryBreakdown breakdown,
        IReadOnlyList<int> allBands)
    {
        if (!TryGetAngle(gross, out var angle))
        {
            return string.Empty;
        }

        var netPercent = gross > 0 ? breakdown.Net / gross * 100m : 0m;
        var (lower, higher) = FindNeighbors(gross, allBands);

        var scenario = angle.Scenario
            .Replace("{year}", BrTaxTables2026.Year.ToString(), StringComparison.Ordinal);
        var tip = angle.PlanningTip
            .Replace("{net}", Money.From(breakdown.Net).ToString(), StringComparison.Ordinal)
            .Replace("{netPercent:0.#}", netPercent.ToString("0.#"), StringComparison.Ordinal)
            .Replace("{nearbyLower}", lower > 0 ? SalaryBandCatalog.FormatCurrency(lower) : "faixas menores", StringComparison.Ordinal)
            .Replace("{nearbyHigher}", higher > 0 ? SalaryBandCatalog.FormatCurrency(higher) : "faixas maiores", StringComparison.Ordinal)
            .Replace("{dependentDeduction}", Money.From(BrTaxTables2026.DependentDeduction).ToString(), StringComparison.Ordinal)
            .Replace("{year}", BrTaxTables2026.Year.ToString(), StringComparison.Ordinal);

        return
            $"<h2 class=\"valora-h3\">{angle.Headline} — {SalaryBandCatalog.FormatCurrency(gross)}</h2>" +
            $"<p>{scenario}</p><p><strong>Planejamento:</strong> {tip}</p>";
    }

    public static SalaryBandFaqItem? BuildExtraFaq(int gross, NetSalaryBreakdown breakdown, int dependents = 0)
    {
        if (!TryGetAngle(gross, out var angle))
        {
            return null;
        }

        return new SalaryBandFaqItem(
            $"Quem costuma ganhar {SalaryBandCatalog.FormatCurrency(gross)} bruto?",
            $"{angle.Scenario.Replace("{year}", BrTaxTables2026.Year.ToString(), StringComparison.Ordinal)} " +
            $"Neste cenário {ProgrammaticDependentsCatalog.SeoPhrase(dependents)}, o líquido estimado é {Money.From(breakdown.Net)}.");
    }

    private static (int Lower, int Higher) FindNeighbors(int gross, IReadOnlyList<int> allBands)
    {
        var ordered = allBands.OrderBy(b => b).ToList();
        var index = ordered.IndexOf(gross);
        if (index < 0)
        {
            return (0, 0);
        }

        var lower = index > 0 ? ordered[index - 1] : 0;
        var higher = index < ordered.Count - 1 ? ordered[index + 1] : 0;
        return (lower, higher);
    }

    public sealed record EditorialAngle(string Headline, string Scenario, string PlanningTip);
}
