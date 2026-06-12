using MeuValorLiquido.Modules.Calculators;
using MeuValorLiquido.WebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeuValorLiquido.WebApp.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.CalculatorCatalog.AnyAsync(cancellationToken))
        {
            await SeedBlogPostsAsync(db, cancellationToken);
            return;
        }

        var categories = new Dictionary<string, CalculatorCategoryEntity>(StringComparer.OrdinalIgnoreCase);
        var sort = 0;
        foreach (var definition in CalculatorSeedData.GetDefinitions())
        {
            if (!categories.ContainsKey(definition.Category))
            {
                categories[definition.Category] = new CalculatorCategoryEntity
                {
                    Name = definition.Category,
                    SortOrder = sort++
                };
            }
        }

        db.CalculatorCategories.AddRange(categories.Values);
        await db.SaveChangesAsync(cancellationToken);

        var calculatorSort = 0;
        foreach (var definition in CalculatorSeedData.GetDefinitions())
        {
            var category = categories[definition.Category];
            var entity = new CalculatorCatalogEntity
            {
                Slug = definition.Slug,
                Name = definition.Name,
                CategoryId = category.Id,
                Summary = definition.Summary,
                SeoTitle = definition.SeoTitle,
                SeoDescription = definition.SeoDescription,
                EducationalContent = GetEducationalContent(definition.Slug),
                SortOrder = calculatorSort++,
                IsActive = true
            };

            db.CalculatorCatalog.Add(entity);
            await db.SaveChangesAsync(cancellationToken);

            var faqSort = 0;
            foreach (var faq in definition.FaqItems)
            {
                var exists = await db.FaqItems.AnyAsync(
                    x => x.CalculatorSlug == definition.Slug && x.Question == faq.Question,
                    cancellationToken);
                if (exists)
                {
                    continue;
                }

                db.FaqItems.Add(new FaqItemEntity
                {
                    CalculatorSlug = definition.Slug,
                    Question = faq.Question,
                    Answer = faq.Answer,
                    SortOrder = faqSort++
                });
            }
        }

        await db.SaveChangesAsync(cancellationToken);
        await SeedBlogPostsAsync(db, cancellationToken);
    }

    public static async Task SeedBlogPostsAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        foreach (var article in BlogArticleSeedData.GetAll())
        {
            var exists = await db.BlogPosts.AnyAsync(x => x.Slug == article.Slug, cancellationToken);
            if (!exists)
            {
                db.BlogPosts.Add(MapToEntity(article));
                continue;
            }

            var existing = await db.BlogPosts.FirstAsync(x => x.Slug == article.Slug, cancellationToken);
            if (existing.Content.Length < 500)
            {
                existing.Title = article.Title;
                existing.Summary = article.Summary;
                existing.Content = article.Content;
                existing.Category = article.Category;
                existing.RelatedCalculatorSlug = article.RelatedCalculatorSlug;
                existing.PublishedAt = article.PublishedAt;
            }
        }

        await db.SaveChangesAsync(cancellationToken);
    }

    private static BlogPostEntity MapToEntity(BlogArticleSeed article) => new()
    {
        Slug = article.Slug,
        Title = article.Title,
        Summary = article.Summary,
        Content = article.Content,
        PublishedAt = article.PublishedAt,
        Category = article.Category,
        RelatedCalculatorSlug = article.RelatedCalculatorSlug,
        IsPublished = true
    };

    private static string GetEducationalContent(string slug) => slug switch
    {
        "salario-liquido" =>
            "Informe o salário bruto, dependentes e descontos opcionais. O resultado mostra INSS, IRRF e o líquido estimado com base nas tabelas de 2026.",
        "ferias" =>
            "A calculadora considera o salário base mais o adicional de 1/3 constitucional e aplica descontos estimados.",
        "inss" =>
            "O INSS é calculado de forma progressiva por faixas, respeitando o teto de contribuição vigente.",
        _ => "Use os campos indicados para obter uma estimativa educativa. Consulte um profissional para decisões formais."
    };
}
