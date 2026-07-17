namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record EditorialAuthorProfile(
    string Name,
    string Slug,
    string Role,
    string Bio,
    string Experience,
    string ImagePath,
    string LinkedInUrl,
    DateOnly LastReviewedAt)
{
    public string ProfilePath => $"/autores/{Slug}";
}

public static class EditorialAuthorCatalog
{
    public static EditorialAuthorProfile Primary { get; } = new(
        "Matteus Oberdan",
        "matteus-oberdan",
        "Criador e responsável editorial do Meu Valor Líquido",
        "Pesquisa, organiza e revisa conteúdos sobre salário, impostos trabalhistas, holerite e simulações CLT/PJ com foco em linguagem simples e fontes oficiais.",
        "Responsável pela estrutura do produto, seleção de fontes públicas, documentação das premissas e manutenção dos cenários automatizados do portal. O perfil não atribui formação ou certificação profissional não verificada.",
        "/images/authors/matteus-oberdan.svg",
        "https://www.linkedin.com/in/matteus-oberdan-203205289/",
        new DateOnly(2026, 7, 17));

    public static Dictionary<string, object> BuildPersonSchema(string baseUrl)
    {
        var author = Primary;

        return new Dictionary<string, object>
        {
            ["@type"] = "Person",
            ["name"] = author.Name,
            ["jobTitle"] = author.Role,
            ["description"] = author.Bio,
            ["url"] = SeoMetadataHelper.BuildCanonicalUrl(baseUrl, author.ProfilePath),
            ["sameAs"] = new[] { author.LinkedInUrl },
            ["image"] = SeoMetadataHelper.BuildCanonicalUrl(baseUrl, author.ImagePath)
        };
    }
}
