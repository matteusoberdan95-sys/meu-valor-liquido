namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed record EditorialAuthorProfile(
    string Name,
    string Role,
    string Bio,
    string ImagePath,
    string LinkedInUrl);

public static class EditorialAuthorCatalog
{
    public static EditorialAuthorProfile Primary { get; } = new(
        "Matteus Oberdan",
        "Criador e responsável editorial do Meu Valor Líquido",
        "Pesquisa, organiza e revisa conteúdos sobre salário, impostos trabalhistas, holerite e simulações CLT/PJ com foco em linguagem simples e fontes oficiais.",
        "/images/authors/matteus-oberdan.png",
        "https://www.linkedin.com/in/matteus-oberdan-203205289/");

    public static Dictionary<string, object> BuildPersonSchema(string baseUrl)
    {
        var author = Primary;

        return new Dictionary<string, object>
        {
            ["@type"] = "Person",
            ["name"] = author.Name,
            ["jobTitle"] = author.Role,
            ["description"] = author.Bio,
            ["url"] = author.LinkedInUrl,
            ["sameAs"] = new[] { author.LinkedInUrl },
            ["image"] = SeoMetadataHelper.BuildCanonicalUrl(baseUrl, author.ImagePath)
        };
    }
}
