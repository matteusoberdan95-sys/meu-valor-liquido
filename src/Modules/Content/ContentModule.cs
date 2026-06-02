namespace MeuValorLiquido.Modules.Content;

public sealed record BlogPost(
    string Slug,
    string Title,
    string Summary,
    string Content,
    DateOnly PublishedAt);

public interface IContentService
{
    IReadOnlyList<BlogPost> GetPublishedPosts();

    BlogPost? GetBySlug(string slug);
}

public sealed class InMemoryContentService : IContentService
{
    private static readonly IReadOnlyList<BlogPost> Posts =
    [
        new("o-que-e-salario-liquido", "O que é salário líquido?", "Entenda a diferença entre salário bruto e salário líquido.", "Salário líquido é o valor estimado que sobra após descontos como INSS, IRRF e benefícios.", new DateOnly(2026, 6, 1)),
        new("como-calcular-ferias", "Como calcular férias", "Veja os principais componentes do cálculo de férias.", "As férias normalmente consideram a remuneração do período, o adicional de um terço e descontos estimados.", new DateOnly(2026, 6, 1)),
        new("como-calcular-rescisao-clt", "Como calcular rescisão CLT", "Conheça os itens comuns em uma estimativa de rescisão.", "Uma rescisão pode incluir saldo de salário, proporcionais, férias, décimo terceiro e verbas específicas conforme o caso.", new DateOnly(2026, 6, 1))
    ];

    public IReadOnlyList<BlogPost> GetPublishedPosts() => Posts;

    public BlogPost? GetBySlug(string slug)
    {
        return Posts.FirstOrDefault(post => post.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
    }
}
