namespace MeuValorLiquido.WebApp.Data;
public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db, CancellationToken cancellationToken = default)
    {
        if (await db.CalculatorCatalog.AnyAsync(cancellationToken))
        {
            await SeedMissingCalculatorsAsync(db, cancellationToken);
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

    private static async Task SeedMissingCalculatorsAsync(AppDbContext db, CancellationToken cancellationToken)
    {
        var existingSlugs = await db.CalculatorCatalog
            .Select(x => x.Slug)
            .ToListAsync(cancellationToken);

        var missing = CalculatorSeedData.GetDefinitions()
            .Where(d => !existingSlugs.Contains(d.Slug, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (missing.Count == 0)
        {
            return;
        }

        var categories = await db.CalculatorCategories.ToListAsync(cancellationToken);
        var maxSort = await db.CalculatorCatalog.MaxAsync(x => (int?)x.SortOrder, cancellationToken) ?? -1;

        foreach (var definition in missing)
        {
            var category = categories.FirstOrDefault(c =>
                string.Equals(c.Name, definition.Category, StringComparison.OrdinalIgnoreCase));

            if (category is null)
            {
                category = new CalculatorCategoryEntity
                {
                    Name = definition.Category,
                    SortOrder = categories.Count
                };
                db.CalculatorCategories.Add(category);
                await db.SaveChangesAsync(cancellationToken);
                categories.Add(category);
            }

            db.CalculatorCatalog.Add(new CalculatorCatalogEntity
            {
                Slug = definition.Slug,
                Name = definition.Name,
                CategoryId = category.Id,
                Summary = definition.Summary,
                SeoTitle = definition.SeoTitle,
                SeoDescription = definition.SeoDescription,
                EducationalContent = GetEducationalContent(definition.Slug),
                SortOrder = ++maxSort,
                IsActive = true
            });

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

        await db.SaveChangesAsync(cancellationToken);
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
            existing.Title = article.Title;
            existing.Summary = article.Summary;
            existing.Content = article.Content;
            existing.Category = article.Category;
            existing.RelatedCalculatorSlug = article.RelatedCalculatorSlug;
            existing.PublishedAt = article.PublishedAt;
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
            "Informe o salário bruto, dependentes e descontos opcionais. O resultado mostra INSS, IRRF e o líquido estimado com base nas tabelas de 2026. Para o caminho inverso, use a <a href=\"/calculadoras/salario-bruto-necessario\">calculadora de salário bruto necessário</a>. Consulte também as <a href=\"/salario-liquido\">páginas por valor bruto</a>.",
        "salario-bruto-necessario" =>
            "Informe o salário líquido que você quer receber, dependentes e descontos (vale-transporte, vale-refeição e outros). A ferramenta estima o bruto necessário com busca binária sobre INSS e IRRF 2026. Compare com a <a href=\"/calculadoras/salario-liquido\">calculadora de salário líquido</a> ou simule um aumento na <a href=\"/calculadoras/proposta-salarial\">proposta salarial</a>.",
        "proposta-salarial" =>
            "Informe o salário bruto atual e o valor proposto pela empresa, além de dependentes e descontos que se mantêm iguais. O resultado mostra ganho líquido mensal e anual, percentual real no bolso e opções para compartilhar com RH. Veja também <a href=\"/calculadoras/salario-bruto-necessario\">quanto bruto pedir para um líquido alvo</a>.",
        "ferias" =>
            "A calculadora considera o salário base mais o adicional de 1/3 constitucional e aplica descontos estimados.",
        "rescisao-clt" =>
            "Informe salário, tempo de empresa, dias trabalhados no mês e tipo de desligamento. No pedido de demissão sem aviso prévio, desconta-se até 30 dias de salário. A multa FGTS de 40% entra apenas na demissão sem justa causa.",
        "inss" =>
            "O INSS é calculado de forma progressiva por faixas, respeitando o teto de contribuição vigente.",
        "fgts" =>
            "Informe salário e meses trabalhados para ver depósitos de 8%. Opcionalmente informe saldo atual e tipo de desligamento para estimar multa rescisória.",
        "simulador-mei" =>
            "O DAS MEI é fixo conforme a atividade (comércio, serviços ou ambos). O limite anual é R$ 81.000.",
        "custo-funcionario" =>
            "Estimativa do custo mensal total para a empresa: salário + FGTS, INSS patronal, provisões de 13º e férias.",
        "multa-atraso" =>
            "Padrão comum: 2% de multa + 1% de juros ao mês proporcional aos dias. Ajuste conforme seu contrato.",
        "conversor-salario" =>
            "Converta entre mensal, diário (÷30) e hora (divisor 220h para jornada 44h). Altere a jornada se necessário.",
        "pj-vs-clt" =>
            "Informe salário bruto CLT, faturamento PJ (opcional), dependentes, alíquota do Simples e despesas fixas. " +
            "Veja o faturamento PJ equivalente ao líquido CLT e explore páginas por valor em <a href=\"/clt-pj\">CLT x PJ</a>.",
        _ => "Use os campos indicados para obter uma estimativa educativa. Consulte um profissional para decisões formais."
    };
}
