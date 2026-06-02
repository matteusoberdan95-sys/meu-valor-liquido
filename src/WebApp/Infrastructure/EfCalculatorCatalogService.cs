using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.WebApp.Data;
using MeuValorLiquido.WebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuValorLiquido.WebApp.Infrastructure;

public sealed class EfCalculatorCatalogService : ICalculatorCatalogService
{
    private readonly AppDbContext db;

    public EfCalculatorCatalogService(AppDbContext db)
    {
        this.db = db;
    }

    public IReadOnlyList<CalculatorDefinition> GetAll()
    {
        var entities = db.CalculatorCatalog
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(x => x.IsActive)
            .OrderBy(x => x.SortOrder)
            .ToList();

        return entities.Select(Map).ToList();
    }

    public CalculatorDefinition? GetBySlug(string slug)
    {
        var entity = db.CalculatorCatalog
            .AsNoTracking()
            .Include(x => x.Category)
            .FirstOrDefault(x => x.Slug == slug && x.IsActive);

        return entity is null ? null : Map(entity);
    }

    private CalculatorDefinition Map(CalculatorCatalogEntity entity)
    {
        var faqs = db.FaqItems
            .AsNoTracking()
            .Where(x => x.CalculatorSlug == entity.Slug)
            .OrderBy(x => x.SortOrder)
            .Select(x => new FaqItem(x.Question, x.Answer))
            .ToList();

        return new CalculatorDefinition(
            entity.Slug,
            entity.Name,
            entity.Category.Name,
            entity.Summary,
            entity.SeoTitle,
            entity.SeoDescription,
            faqs,
            entity.EducationalContent);
    }
}
