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

    public static bool RequiresEditorialValidation(string slug) =>
        Sprint58EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase)
        || Sprint66EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase);
}
