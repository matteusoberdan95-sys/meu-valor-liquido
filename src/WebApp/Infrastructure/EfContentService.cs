using MeuValorLiquido.Modules.Content;
using MeuValorLiquido.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class EfContentService : IContentService
{
    private readonly AppDbContext db;

    public EfContentService(AppDbContext db)
    {
        this.db = db;
    }

    public IReadOnlyList<BlogPost> GetPublishedPosts()
    {
        var entities = db.BlogPosts
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.PublishedAt)
            .ToList();

        return entities.Select(Map).ToList();
    }

    public BlogPost? GetBySlug(string slug)
    {
        var entity = db.BlogPosts
            .AsNoTracking()
            .FirstOrDefault(x => x.IsPublished && x.Slug == slug);

        return entity is null ? null : Map(entity);
    }

    private static BlogPost Map(Data.Entities.BlogPostEntity x) =>
        new(x.Slug, x.Title, x.Summary, x.Content, x.PublishedAt, x.Category, x.RelatedCalculatorSlug);
}
