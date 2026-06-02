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
                db.FaqItems.Add(new FaqItemEntity
                {
                    CalculatorSlug = definition.Slug,
                    Question = faq.Question,
                    Answer = faq.Answer,
                    SortOrder = faqSort++
                });
            }
        }

        if (!await db.BlogPosts.AnyAsync(cancellationToken))
        {
            db.BlogPosts.AddRange(
                new BlogPostEntity
                {
                    Slug = "o-que-e-salario-liquido",
                    Title = "O que é salário líquido?",
                    Summary = "Entenda a diferença entre salário bruto e salário líquido.",
                    Content = "Salário líquido é o valor estimado que sobra após descontos como INSS, IRRF e benefícios. Use a calculadora para simular seu caso.",
                    PublishedAt = new DateOnly(2026, 6, 1)
                },
                new BlogPostEntity
                {
                    Slug = "como-calcular-ferias",
                    Title = "Como calcular férias",
                    Summary = "Veja os principais componentes do cálculo de férias.",
                    Content = "As férias normalmente consideram a remuneração do período, o adicional de um terço e descontos estimados de INSS e IRRF.",
                    PublishedAt = new DateOnly(2026, 6, 1)
                },
                new BlogPostEntity
                {
                    Slug = "como-calcular-rescisao-clt",
                    Title = "Como calcular rescisão CLT",
                    Summary = "Conheça os itens comuns em uma estimativa de rescisão.",
                    Content = "Uma rescisão pode incluir saldo de salário, proporcionais, férias, décimo terceiro e verbas específicas conforme o tipo de desligamento.",
                    PublishedAt = new DateOnly(2026, 6, 1)
                });
        }

        await db.SaveChangesAsync(cancellationToken);
    }

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
