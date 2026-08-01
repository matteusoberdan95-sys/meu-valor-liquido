namespace MeuValorLiquido.WebApp.Infrastructure;

/// <summary>Artigos editoriais com seção de validação e links obrigatórios.</summary>
public static class BlogEditorialCatalog
{
    public static readonly IReadOnlyList<string> Sprint58EditorialSlugs =
    [
        "como-conferir-holerite",
        "como-avaliar-proposta-salarial",
        "rescisao-clt-vs-trct"
    ];

    public static readonly IReadOnlyList<string> Sprint66EditorialSlugs =
    [
        "irrf-2026-reducao-imposto",
        "seguro-desemprego-quem-tem-direito",
        "multa-fgts-40-ou-20",
        "aumento-salario-quanto-sobra-liquido"
    ];

    public static readonly IReadOnlyList<string> Sprint68EditorialSlugs =
    [
        "quanto-preciso-ganhar-para-receber-x",
        "mei-desenquadramento-o-que-fazer",
        "pro-labore-pj-quanto-retirar",
        "decimo-terceiro-primeira-segunda-parcela",
        "ferias-abono-pecuniario-vale-a-pena",
        "emprestimo-consignado-desconto-holerite",
        "reserva-emergencia-quanto-guardar"
    ];

    public static readonly IReadOnlyList<string> Sprint70EditorialSlugs =
    [
        "acordo-484a-verbas-e-multa-fgts",
        "custo-total-clt-para-empregador"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote2EditorialSlugs =
    [
        "ferias-coletivas-clt-guia-completo",
        "pedir-demissao-ou-aguardar-dispensa"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote3EditorialSlugs =
    [
        "dissidio-salarial-2026-como-avaliar",
        "vale-refeicao-desconto-holerite"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote4EditorialSlugs =
    [
        "experiencia-clt-direitos-e-rescisao",
        "home-office-clt-descontos"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote5EditorialSlugs =
    [
        "vale-transporte-home-office-hibrido",
        "plano-saude-holerite-coparticipacao"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote6EditorialSlugs =
    [
        "aviso-previo-trabalhado-vs-indenizado",
        "adicional-noturno-clt-como-calcular"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote7EditorialSlugs =
    [
        "banco-de-horas-clt-como-funciona",
        "ferias-vencidas-e-proporcionais-na-rescisao"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote8EditorialSlugs =
    [
        "dsr-sobre-horas-extras-como-calcular",
        "decimo-terceiro-proporcional-na-rescisao"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote9EditorialSlugs =
    [
        "comissao-variavel-no-holerite",
        "reserva-impostos-e-provisoes-ao-virar-pj"
    ];

    public static readonly IReadOnlyList<string> Sprint70Lote10EditorialSlugs =
    [
        "vale-transporte-vr-orcamento-mensal",
        "salario-minimo-impacto-holerite"
    ];

    public static bool IsSprint70Editorial(string slug) =>
        Sprint70EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote2EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote3EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote4EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote5EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote6EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote7EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote8EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote9EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint70Lote10EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase);

    public static bool RequiresEditorialValidation(string slug) =>
        Sprint58EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint66EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint68EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || IsSprint70Editorial(slug);
}
