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
        return db.BlogPosts
            .AsNoTracking()
            .Where(x => x.IsPublished)
            .OrderByDescending(x => x.PublishedAt)
            .Select(x => new BlogPost(x.Slug, x.Title, x.Summary, x.Content, x.PublishedAt))
            .ToList();
    }

    public BlogPost? GetBySlug(string slug)
    {
        var entity = db.BlogPosts
            .AsNoTracking()
            .FirstOrDefault(x => x.IsPublished && x.Slug == slug);

        return entity is null
            ? null
            : new BlogPost(entity.Slug, entity.Title, entity.Summary, entity.Content, entity.PublishedAt);
    }
}
