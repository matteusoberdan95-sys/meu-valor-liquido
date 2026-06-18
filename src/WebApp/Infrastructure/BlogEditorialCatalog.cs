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

    public static bool RequiresEditorialValidation(string slug) =>
        Sprint58EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint66EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint68EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase);
}
