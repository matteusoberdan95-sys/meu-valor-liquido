namespace MeuValorLiquido.WebApp.Infrastructure;

/// <summary>Artigos editoriais da Sprint 58 com seção de validação e links obrigatórios.</summary>
public static class BlogEditorialCatalog
{
    public static readonly IReadOnlyList<string> Sprint58EditorialSlugs =
    [
        "como-conferir-holerite",
        "como-avaliar-proposta-salarial",
        "rescisao-clt-vs-trct"
    ];

    public static bool RequiresEditorialValidation(string slug) =>
        Sprint58EditorialSlugs.Contains(slug, StringComparer.OrdinalIgnoreCase);
}
