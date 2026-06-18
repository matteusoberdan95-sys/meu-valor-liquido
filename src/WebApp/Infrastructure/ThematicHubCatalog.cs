namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record ThematicHubDefinition(
    string Id,
    string RoutePath,
    string BreadcrumbLabel,
    string SeoTitle,
    string SeoDescription,
    string HeroKicker,
    string HeroTitle,
    string HeroLead,
    string IntroHtml,
    string JourneyTitle,
    string JourneyIntroduction,
    IReadOnlyList<CalculatorJourneyStepDefinition> JourneySteps,
    string PrimaryCalculatorSlug,
    IReadOnlyList<string> CalculatorSlugs,
    IReadOnlyList<string> BlogSlugs,
    IReadOnlyList<string> FaqSlugs);

public static class ThematicHubCatalog
{
    public const string Desligamento = "desligamento";
    public const string NegociarSalario = "negociar-salario";
    public const string VirarPj = "virar-pj";

    private static readonly IReadOnlyList<ThematicHubDefinition> All =
    [
        new(
            Desligamento,
            "/desligamento",
            "Desligamento CLT",
            "Desligamento CLT: rescisão, FGTS e próximos passos | Meu Valor Líquido",
            "Simule rescisão CLT, FGTS e entenda verbas, descontos e o que conferir no TRCT. Jornada educativa com calculadoras calibradas 2026.",
            "Ciclo de vida CLT",
            "Saiu da empresa? Entenda sua rescisão",
            "Estime verbas rescisórias, FGTS e multa — e saiba o que conferir no TRCT antes de assinar.",
            $"""
            <p>Um desligamento envolve mais que um número final: saldo de salário, verbas proporcionais, descontos, FGTS e multa conforme o motivo. Use as ferramentas abaixo para <strong>estimar cada parte</strong> e saber o que conferir no TRCT.</p>
            <p>As simulações são educativas e calibradas com tabelas de {BrTaxTables2026.Year} — não substituem homologação, contador ou sindicato.</p>
            """,
            "Saída da empresa",
            "A rescisão é só parte do pacote. Estime FGTS e revise direitos que costumam surgir após o desligamento.",
            [
                new(CalculatorJourneyStepKind.Calculator, "rescisao-clt", "Simule verbas rescisórias conforme o motivo do desligamento."),
                new(CalculatorJourneyStepKind.Calculator, "fgts", "Estime depósitos, saldo e multa rescisória para o seu tipo de saída."),
                new(CalculatorJourneyStepKind.Calculator, "seguro-desemprego", "Estime valor e quantidade de parcelas do seguro-desemprego."),
                new(CalculatorJourneyStepKind.Faq, "multa-fgts-40-porcento", "Entenda quando há multa de 40% e como isso se relaciona ao saque do FGTS.")
            ],
            "rescisao-clt",
            ["rescisao-clt", "fgts", "seguro-desemprego", "decimo-terceiro", "ferias", "salario-liquido"],
            ["como-calcular-rescisao-clt", "rescisao-clt-vs-trct", "fgts-guia-completo", "seguro-desemprego-quem-tem-direito", "multa-fgts-40-ou-20"],
            ["rescisao-pedido-demissao-o-que-recebo", "multa-fgts-40-porcento", "seguro-desemprego-quando-tem-direito"]),

        new(
            NegociarSalario,
            "/negociar-salario",
            "Negociar salário",
            "Como negociar salário: compare proposta pelo líquido | Meu Valor Líquido",
            "Avalie aumento salarial pelo que entra no bolso. Compare bruto atual e proposto com INSS e IRRF 2026. Compartilhe a simulação com RH.",
            "Holerite mensal",
            "Negociando salário? Compare pelo líquido",
            "O aumento no bruto nem sempre vira o mesmo ganho no bolso. Simule com INSS, IRRF e os mesmos descontos nos dois cenários.",
            """
            <p>Propostas costumam vir em <strong>salário bruto</strong>, mas o que importa na negociação é o <strong>líquido</strong> — após INSS, IRRF, vale-transporte e outros descontos do holerite.</p>
            <p>Compare cenários, veja ganho mensal e anual e <strong>compartilhe a simulação</strong> (link ou PDF) com transparência. Confirme valores finais com RH antes de assinar.</p>
            """,
            "Negociar salário",
            "Avalie a proposta pelo líquido, valide o holerite com os mesmos descontos e descubra o bruto necessário para sua meta.",
            [
                new(CalculatorJourneyStepKind.Calculator, "proposta-salarial", "Compare salário atual e proposto pelo ganho real no bolso."),
                new(CalculatorJourneyStepKind.Calculator, "salario-liquido", "Monte o extrato do bruto proposto com INSS, IRRF e descontos."),
                new(CalculatorJourneyStepKind.Calculator, "salario-bruto-necessario", "Descubra quanto de bruto precisa para receber o líquido desejado.")
            ],
            "proposta-salarial",
            ["proposta-salarial", "salario-liquido", "salario-bruto-necessario", "inss", "irrf"],
            ["como-avaliar-proposta-salarial", "o-que-e-salario-liquido", "como-conferir-holerite", "aumento-salario-quanto-sobra-liquido", "irrf-2026-reducao-imposto"],
            ["proposta-salarial-como-negociar", "diferenca-salario-bruto-e-liquido", "quanto-preciso-ganhar-para-receber-x-liquido"]),

        new(
            VirarPj,
            "/virar-pj",
            "Virar PJ",
            "CLT ou PJ: vale a pena? Compare líquido e custos | Meu Valor Líquido",
            "Compare salário CLT com faturamento PJ ou MEI. Simule Simples, pró-labore e custo oculto dos benefícios CLT.",
            "CLT vs autônomo",
            "Pensando em virar PJ? Compare antes de decidir",
            "PJ pode pagar mais no bolso — ou menos, depois de impostos, contador e benefícios que você deixa de ter como CLT.",
            """
            <p>Antes de pedir demissão ou abrir CNPJ, compare o <strong>líquido mensal</strong> com faturamento PJ ou MEI, incluindo Simples Nacional, pró-labore, DAS e despesas fixas.</p>
            <p>FGTS, férias, 13º e seguro-desemprego são <strong>custo oculto</strong> do emprego CLT — a calculadora destaca isso de forma educativa, sem substituir contador ou advogado.</p>
            """,
            "Virar PJ",
            "Antes de abrir CNPJ ou pedir demissão, compare líquido CLT com PJ ou MEI — impostos, pró-labore e custo oculto dos benefícios.",
            [
                new(CalculatorJourneyStepKind.Calculator, "pj-vs-clt", "Compare líquido CLT com faturamento PJ, Simples e pró-labore."),
                new(CalculatorJourneyStepKind.Calculator, "simulador-mei", "Estime DAS, limite anual e alertas de desenquadramento do MEI."),
                new(CalculatorJourneyStepKind.Calculator, "custo-funcionario", "Veja o custo total que a empresa teria com um funcionário CLT.")
            ],
            "pj-vs-clt",
            ["pj-vs-clt", "simulador-mei", "custo-funcionario", "salario-liquido"],
            ["pj-ou-clt-qual-melhor", "mei-faturamento-e-das", "simples-nacional-pj-guia-iniciantes"],
            ["pj-ou-clt-qual-compensa", "quanto-faturar-pj-para-equivaler-clt", "mei-pode-trabalhar-como-clt"])
    ];

    private static readonly Dictionary<string, ThematicHubDefinition> ById =
        All.ToDictionary(hub => hub.Id, StringComparer.OrdinalIgnoreCase);

    private static readonly Dictionary<string, ThematicHubDefinition> ByRoute =
        All.ToDictionary(hub => hub.RoutePath.TrimEnd('/'), StringComparer.OrdinalIgnoreCase);

    public static IReadOnlyList<ThematicHubDefinition> GetAll() => All;

    public static ThematicHubDefinition? TryGet(string? hubId) =>
        string.IsNullOrWhiteSpace(hubId) || !ById.TryGetValue(hubId, out var hub) ? null : hub;

    public static ThematicHubDefinition GetById(string hubId) =>
        TryGet(hubId) ?? throw new KeyNotFoundException($"Hub temático não encontrado: {hubId}");

    public static ThematicHubDefinition? TryGetByRoute(string? routePath)
    {
        if (string.IsNullOrWhiteSpace(routePath))
        {
            return null;
        }

        var normalized = routePath.StartsWith('/') ? routePath.TrimEnd('/') : $"/{routePath.TrimEnd('/')}";
        return ByRoute.TryGetValue(normalized, out var hub) ? hub : null;
    }
}
